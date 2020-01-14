using System.Collections.Generic;

namespace NorthwindModel
{
    public class Manager : Employee
    {
        public decimal AnnualBudget { get; set; }

        public IList<Employee> Employees { get; set; }
    }
}
