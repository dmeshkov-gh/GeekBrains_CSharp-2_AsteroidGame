using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WorkerClass
{
    abstract class Employee
    {
        public string Name { get; init; }
        public string Surname { get; init; }

        public double MonthlySalary { get; protected set; } //Среднемесячная заработная плата
        public Employee(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        protected abstract double CalculateSalary();
    }
}
