using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerClass
{
    class Office
    {
        public Employee[] Employees { get; private set; }
        public Office(Employee[] employees)
        {
            Employees = employees;
        }
    }
}
