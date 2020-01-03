namespace NorthwindModel
{
    using System.Collections.Generic;

    public class Manager : Employee
    {
        public decimal AnnualBudget { get; set; }

        public IList<Employee> Employees { get; set; }
    }
}