using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerClass
{
    class FixPaidEmployee : Employee
    {
        public FixPaidEmployee(string name, string surname, double monthlySalary) : base(name, surname, monthlySalary)
        {
        }
        protected override void CalculateSalary()
        {
            throw new NotImplementedException();
        }
    }
}
