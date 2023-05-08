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
        public Fuel? Fuel { get; set; }

        public double FuelPlanCost { get; set; }
        public double FuelFactCost { get; set; }
        public double VolumePlan { get; set; }
        public double VolumeFact { get; set; }
    }
}
