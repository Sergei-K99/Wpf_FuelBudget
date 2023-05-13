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
      
        public double GetAllPlanCost { get; set; } = 0;
        public double GetAllFactCost { get; set; } = 0;

        public int MeasuringPointId { get; set; }
        public MeasuringPoint? MeasuringPoint { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public List<FuelDetail> FuelDetails { get; set; } = new();

  
    }
}
