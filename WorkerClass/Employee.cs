using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerClass
{
    //Описать в базовом классе абстрактный метод для расчёта среднемесячной заработной платы.
    //Для «повременщиков» формула для расчета такова: 
    //«среднемесячная заработная плата = 20.8 * 8 * почасовая ставка», 
    //для работников с фиксированной оплатой «среднемесячная заработная плата = фиксированная месячная оплата».
    abstract class Employee
    {
        protected string Name { get; init; }
        protected string Surname { get; init; }

        protected double MonthlySalary { get; private set; }
        public Employee(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        protected abstract void CalculateSalary();
    }
}
