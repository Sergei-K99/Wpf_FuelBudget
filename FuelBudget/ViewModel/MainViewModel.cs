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
        DateTime? _selectedDate;
        public DateTime? SelectedDate { get { return _selectedDate; } set {
                using (DataContext context = new DataContext())
                {
                    var point = context.MeasuringPoints.Where(q=>q.DateTime.Month==value.Value.Month && q.DateTime.Year == value.Value.Year).FirstOrDefault();
                    if (point != null)
                    {
                        if (DepartmentButgetList.Count > 0)
                        {
                            DepartmentButgetList.Clear();
                        }
                        var list = context.DepartmentButgets.Include(x => x.Department).Include(x => x.FuelDetails).ThenInclude(x => x.Fuel).Where(x => x.MeasuringPointId == point.Id);
                        foreach (var q in list)
                        {
                            DepartmentButgetList.Add(q);
                        }
                    }
                    else
                    {
                        DepartmentButgetList.Clear();
                    }
                    _selectedDate = value;
                }
            } }

        public MainViewModel()
        {
            using (DataContext context = new DataContext())
            {
                FuelList = new List<Fuel>(context.Fuels.ToList());
                DepatrmentList = new List<Department>(context.Departments.ToList());
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
                      using (DataContext context = new DataContext())
                      {
                          MeasuringPoint? point = context.MeasuringPoints.Where(q => q.DateTime.Month == SelectedDate.Value.Month && q.DateTime.Year == SelectedDate.Value.Year).FirstOrDefault();
                          if (point == null) {
                              point = new MeasuringPoint { DateTime = (DateTime)SelectedDate };
                              foreach (var x in DepartmentButgetList)
                              {
                                  x.CalcAllFactCost();
                                  x.CalcAllPlanCost();
                                  point.DepartmentButgets.Add(x);
                              }
                              context.MeasuringPoints.Add(point);
                          }
                          else
                          {
                              foreach (var x in DepartmentButgetList)
                              {
                                  x.CalcAllFactCost();
                                  x.CalcAllPlanCost();
                                  context.DepartmentButgets.Update(x);
                              }
                          }
                          context.SaveChanges(); // Error -------------------------------------

                          DepartmentButgetList.Clear();
                          foreach (var q in (context.DepartmentButgets.Include(x => x.Department).Include(x => x.FuelDetails).ThenInclude(x => x.Fuel)))
                          {
                              DepartmentButgetList.Add(q);
                          }
                      }
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
                      using (DataContext context = new DataContext())
                      {
                          FuelDetail? selected = obj as FuelDetail;

                          if (selected != null)
                          {
                              FuelDetail? x = context.FuelDetails.FirstOrDefault(x => x.Id == selected.Id);
                              if (x != null)
                              {
                                  context.FuelDetails.Remove(x);
                                  context.SaveChanges();
                                  DepartmentButgetList.Clear();
                                  foreach (var q in (context.DepartmentButgets.Include(x => x.Department).Include(x => x.FuelDetails).ThenInclude(x => x.Fuel)))
                                  {
                                      DepartmentButgetList.Add(q);
                                  }
                              }
                          }
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
                      using (DataContext context = new DataContext())
                      {
                          DepartmentButget? selected = obj as DepartmentButget;

                          if (selected != null)
                          {
                               context.DepartmentButgets.Remove(selected);
                               context.SaveChanges();
                               DepartmentButgetList.Clear();
                              foreach (var q in (context.DepartmentButgets.Include(x => x.Department).Include(x => x.FuelDetails).ThenInclude(x => x.Fuel)))
                              {
                                  DepartmentButgetList.Add(q);
                              }
                          }
                          
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

