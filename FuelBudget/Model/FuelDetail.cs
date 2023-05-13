using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelBudget.Model
{
    public class FuelDetail
    {
        public int Id { get; set; }
        public double FuelPlanCost { get; set; }
        public double FuelFactCost { get; set; }
        public double VolumePlan { get; set; }
        public double VolumeFact { get; set; }

        public int FuelId { get; set; }
        public Fuel? Fuel { get; set; }

        public int DepartmentButgetId { get; set; }
        public DepartmentButget? DepartmentButget { get; set; }
    }
}
