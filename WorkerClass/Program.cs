﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerClass
{
    class Program
    {
        static void Main()
        {
            Employee[] employees = new Employee[7];

            for(int i = 0; i < employees.Length; i++)
            {
                if (i % 2 == 0)
                    employees[i] = new FixPaidEmployee($"Fix paid employee - Name-{i}", $"Surname-{i}");
                else
                    employees[i] = new HourlyPaidEmployee($"Hourly pain employeee - Name-{i}", $"Surname-{i}");
            }

            EmployeeComparer comparer = new EmployeeComparer();
            comparer.SortBy = SortCriteria.Name;
            Array.Sort(employees, comparer);

            PrintEmployess(employees);
        }
        private static void PrintEmployess(Employee[] employees)
        {
            foreach(Employee employee in employees)
                Console.WriteLine(employee.Name + " " + employee.Surname + " " + employee.MonthlySalary);
        }
    }
}
