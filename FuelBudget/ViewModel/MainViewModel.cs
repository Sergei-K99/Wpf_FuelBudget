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
        public ObservableCollection<DepartmentButget> DepartmentButgetList { get; set; }
        public List<Fuel> FuelList { get; set; }
        public List<Department> DepatrmentList { get; set; }

        public MainViewModel()
        {
            using (DataContext context = new DataContext())
            {
                DepartmentButgetList = new ObservableCollection<DepartmentButget>(context.DepartmentButgets.Include(x => x.Department).Include(x => x.FuelDetails).ThenInclude(x => x.Fuel).ToList());
                FuelList = new List<Fuel>(context.Fuels.ToList());
                DepatrmentList = new List<Department>(context.Departments.ToList());
            }
        }
        private bool isMouseOverRow;
        public bool IsMouseOverRow
        {
            get { return isMouseOverRow; }
            set
            {
                isMouseOverRow = value;
                NotifyPropertyChanged(nameof(IsMouseOverRow));
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
                          foreach (var x in DepartmentButgetList)
                          {
                              context.DepartmentButgets.Update(x);
                          }
                          context.SaveChanges();
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
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(obj =>
                  {
                      using (DataContext context = new DataContext())
                      {
                          DepartmentButget? selected = obj as DepartmentButget;

                          if (selected != null)
                          {
                              DepartmentButget? x = context.DepartmentButgets.FirstOrDefault(x => x.Id == selected.Id);
                              if (x != null)
                              {
                                  context.DepartmentButgets.Remove(x);
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



        public List<Fuel> GetFuels()
        {
            using (DataContext context = new DataContext())
            {
                return FuelList = new List<Fuel>(context.Fuels.ToList());
            }
        }

    }
}

