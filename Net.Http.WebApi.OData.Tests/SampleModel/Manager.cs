using System.Collections.Generic;

namespace Sample.Model
{
    public class Manager : Employee
    {
        public decimal AnnualBudget { get; set; }

        public IList<Employee> Employees { get; set; }
    }
}
