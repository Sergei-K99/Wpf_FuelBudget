using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FuelBudget.Model.Data
{
    internal class CommandToDB
    {
        public List<Fuel> GetFuel()
        {
            using (DataContext context = new DataContext())
            {
                return context.Fuels.ToList();
            }
        }
      
        public List<Department> GetDepartment()
        {
            using (DataContext context = new DataContext())
            {
                return context.Departments.ToList();
            }
        }
      
        public List<DepartmentButget> GetDepartmentButget()
        {
            using (DataContext context = new DataContext())
            {
                return context.DepartmentButgets.Include(x => x.Department).Include(x => x.FuelDetails).ToList();
            }
        }
      
        public List<FuelDetail> GetFuelDetails()
        {
            using (DataContext context = new DataContext())
            {
                return context.FuelDetails.Include(x => x.Fuel).ToList();
            }
        }
      
        public List<MeasuringPoint> GetMeasuringPoints()
        {
            using (DataContext context = new DataContext())
            {
                return context.MeasuringPoints.Include(x => x.DepartmentButgets).ToList();
            }
        }


        public MeasuringPoint? GetMeasuringPointsByDate(DateTime date)
        {
            using (DataContext context = new DataContext())
            {
                return context.MeasuringPoints.Where(q => q.DateTime.Month == date.Month && q.DateTime.Year == date.Year).FirstOrDefault();
            }
        }

        public FuelDetail? GetFuelDetails(int Id)
        {
            using (DataContext context = new DataContext())
            {
                return context.FuelDetails.Find(Id);
            }
        }

        public List<DepartmentButget> GetDepartmentButgetByIdMeasuringPoints(int MeasuringPointsId)
        {
            using (DataContext context = new DataContext())
            {
                return context.DepartmentButgets.Include(x => x.FuelDetails).ThenInclude(x=>x.Fuel).Include(x => x.Department).Where(x => x.MeasuringPointId == MeasuringPointsId).ToList();
            }
        }

      
        public bool CreateMeasuringPoints(MeasuringPoint point)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    foreach (var x in point.DepartmentButgets)
                    {
                        context.Attach(x.Department);
                        foreach (var y in x.FuelDetails)
                        {
                            context.Attach(y.Fuel);
                        }

                    }
                    context.MeasuringPoints.Add(point);
                    context.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public bool CreateDepartmentButget(DepartmentButget departmentButget)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    context.Attach(departmentButget.Department);
                    foreach (var y in departmentButget.FuelDetails)
                    {
                        context.Attach(y.Fuel);
                    }
                    context.DepartmentButgets.Add(departmentButget);
                    context.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public bool CreateFuelDetail(FuelDetail fuelDetail)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    context.FuelDetails.Add(fuelDetail);
                    context.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }


        public bool UpdateMeasuringPoints(MeasuringPoint point)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    context.MeasuringPoints.Update(point);
                    context.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
      
        public bool UpdateDepartmentButget(DepartmentButget point)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    context.DepartmentButgets.Update(point);
                    context.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
      
        public bool UpdateFuelDetail(FuelDetail point)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    context.FuelDetails.Update(point);
                    context.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }


        public bool DeleteMeasuringPoints(int id)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    var obj = context.MeasuringPoints.Find(id);
                    if (obj != null)
                    {
                        context.MeasuringPoints.Remove(obj);
                        context.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public bool DeleteDepartmentButget(int id)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    var obj = context.DepartmentButgets.Find(id);
                    if (obj != null)
                    {
                        var point = context.MeasuringPoints.Find(obj.MeasuringPointId);
                        if (point.DepartmentButgets.Count == 1)
                        {
                            DeleteMeasuringPoints(point.Id);
                        }
                        else
                        {
                            context.DepartmentButgets.Remove(obj);
                            context.SaveChanges();

                        }

                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public bool DeleteFuelDetails(int id)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    var obj = context.FuelDetails.Find(id);
                    if (obj != null)
                    {
                        context.FuelDetails.Remove(obj);
                        context.SaveChanges();

                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

     
        public void CalcBudget(int id)
        {
            using (DataContext context = new DataContext())
            {
                var obj = context.DepartmentButgets.Find(id);
                if(obj!= null)
                {
                    context.Entry(obj).Reference(x => x.FuelDetails).Load();

                    if (obj.FuelDetails.Count > 0)
                    {
                        obj.GetAllFactCost = 0;
                        foreach (var x in obj.FuelDetails)
                        {
                            obj.GetAllFactCost += x.FuelFactCost * x.VolumeFact;
                        }
                    }
                    if (obj.FuelDetails.Count > 0)
                    {
                        obj.GetAllPlanCost = 0;
                        foreach (var x in obj.FuelDetails)
                        {
                            obj.GetAllPlanCost += x.FuelPlanCost * x.VolumePlan;
                        }
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
