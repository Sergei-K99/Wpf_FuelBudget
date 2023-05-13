using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using FuelApp.Model;
using FuelBudget.Model;
using FuelBudget.Model.Data;
using Microsoft.EntityFrameworkCore;

namespace FuelBudget.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<DepartmentButget> DepartmentButgetList { get; set; } = new();
        public List<Fuel> FuelList { get; set; }
        public List<Department> DepatrmentList { get; set; }
        DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value; ShowInfoByDate();
            }
        }

        private CommandToDB command;

        public MainViewModel()
        {
            command = new CommandToDB();
            FuelList = new List<Fuel>(command.GetFuel());
            DepatrmentList = new List<Department>(command.GetDepartment());
            SelectedDate = DateTime.Today.Date;
        }

        void ShowInfoByDate()
        {
            DepartmentButgetList.Clear();
            var point = command.GetMeasuringPointsByDate(SelectedDate);

            if (point != null)
            {
                var list = command.GetDepartmentButgetByIdMeasuringPoints(point.Id);
                foreach (var q in list)
                {
                    DepartmentButgetList.Add(q);
                }
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {

                      MeasuringPoint? point = command.GetMeasuringPointsByDate(SelectedDate);
                      if (point == null)
                      {
                          point = new MeasuringPoint { DateTime = SelectedDate, DepartmentButgets = new List<DepartmentButget>() };
                          foreach (var x in DepartmentButgetList)
                          {
                              command.CalcBudget(x);
                              point.DepartmentButgets.Add(x);
                          }
                          command.CreateMeasuringPoints(point);
                      }
                      else
                      {
                          foreach (var x in DepartmentButgetList)
                          {
                              x.MeasuringPointId = point.Id;
                              if (x.Id != 0)
                              {
                                  command.CalcBudget(x);
                                  command.UpdateDepartmentButget(x);
                              }
                              else
                              {
                                  command.CalcBudget(x);
                                  command.CreateDepartmentButget(x);
                              }
                          }
                      }
                      ShowInfoByDate();

                  }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(obj =>
                  {
                      FuelDetail? selected = obj as FuelDetail;
                      if (selected != null)
                      {
                          command.DeleteFuelDetails(selected.Id);
                          ShowInfoByDate();
                      }
                  }));
            }
        }

        private RelayCommand deleteDepartmantCommand;
        public RelayCommand DeleteDepartmantCommand
        {
            get
            {
                return deleteDepartmantCommand ??
                  (deleteDepartmantCommand = new RelayCommand(obj =>
                  {
                      DepartmentButget? selected = obj as DepartmentButget;

                      if (selected != null)
                      {
                          command.DeleteDepartmentButget(selected.Id);

                          ShowInfoByDate();
                      }
                  }));
            }
        }

        public List<Fuel> GetFuels()
        {
            using (DataContext context = new DataContext())
            {
                return FuelList = new List<Fuel>(context.Fuels.ToList());
            }
        }

    }
}

