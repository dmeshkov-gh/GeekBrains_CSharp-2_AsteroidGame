using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerClass
{
    class HourlyPaidEmployee : Employee
    {
        public HourlyPaidEmployee(string name, string surname, double monthlySalary) : base(name, surname, monthlySalary)
        {
        }

        protected override void CalculateSalary()
        {
            throw new NotImplementedException();
        }
    }
}
