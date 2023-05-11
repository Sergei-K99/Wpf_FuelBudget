using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelBudget.Model
{
    public class DepartmentButget
    {
        public int Id { get; set; }
        public Department? Department { get; set; }
        public List<FuelDetail> FuelDetails { get; set; } = new();
        public double GetAllPlanCost { get; set; } = 0;
        public double GetAllFactCost { get; set; } = 0;

        public int MeasuringPointId { get; set; }
        public MeasuringPoint? MeasuringPoint { get; set; }


        public void CalcAllPlanCost()
        {
            if (FuelDetails.Count > 0)
                GetAllPlanCost = 0;
                foreach (var x in FuelDetails)
                {
                    GetAllPlanCost += x.FuelPlanCost * x.VolumePlan;
                }
            
        }
        public void CalcAllFactCost()
        {
            if (FuelDetails.Count > 0)
                GetAllFactCost = 0;
            foreach (var x in FuelDetails)
            {
                GetAllFactCost += x.FuelFactCost * x.VolumeFact;
            }

        }
    }
}
