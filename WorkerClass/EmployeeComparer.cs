using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerClass
{
    enum SortCriteria
    {
        Name, Surname, Salary
    }
    class EmployeeComparer : IComparer<Employee>
    {
        public SortCriteria SortBy = SortCriteria.Name;
        public int Compare(Employee x, Employee y)
        {
            if (SortBy == SortCriteria.Salary)
            {
                if (x.MonthlySalary > y.MonthlySalary)
                    return 1;
                if (x.MonthlySalary < y.MonthlySalary)
                    return -1;
                else
                    return 0;
            }
            else if (SortBy == SortCriteria.Name)
                return x.Name.CompareTo(y.Name);

            else
                return x.Surname.CompareTo(y.Surname);
        }
    }
}
