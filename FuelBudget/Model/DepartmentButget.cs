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
        public double GetAllPlanCost { get; set; }
        public double GetAllFactCost { get; set; }

    }
}
