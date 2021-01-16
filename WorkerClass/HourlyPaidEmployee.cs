using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerClass
{
    class HourlyPaidEmployee : Employee
    {
        private const double _HourRate = 5.0; //Почасовая ставка
        public HourlyPaidEmployee(string name, string surname) : base(name, surname)
        {
            MonthlySalary = CalculateSalary();
        }

        protected override double CalculateSalary()
        {
            return MonthlySalary = 20.8 * 8 * _HourRate;
        }
    }
}
