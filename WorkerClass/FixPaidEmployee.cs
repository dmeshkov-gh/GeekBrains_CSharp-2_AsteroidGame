using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerClass
{
    class FixPaidEmployee : Employee
    {
        private const double FixedSalary = 1200.0; //Фиксированная месячная оплата
        public FixPaidEmployee(string name, string surname) : base(name, surname)
        {
            MonthlySalary = CalculateSalary();
        }
        protected override double CalculateSalary() // Для работников с фиксированной оплатой "среднемесячная заработная плата = фиксированная месячная оплата"
        {
            return FixedSalary;
        }
    }
}
