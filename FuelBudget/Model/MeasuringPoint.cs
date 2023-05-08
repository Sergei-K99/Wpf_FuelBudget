using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelBudget.Model
{
    public  class MeasuringPoint
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public List<DepartmentButget> DepartmentButgets { get; set; } = new();
    }
}
