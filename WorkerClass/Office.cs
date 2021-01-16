using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerClass
{
    class Office : IEnumerable<Employee>
    {
        private Employee[] _Employees;
        public Office(Employee[] employees)
        {
            _Employees = employees;
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return new OfficeEnumerator(_Employees);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
    }
    class OfficeEnumerator : IEnumerator<Employee>
    {
        private Employee[] _Employees;
        int position = -1;
        public OfficeEnumerator(Employee[] employees)
        {
            _Employees = employees;
        }
        public bool MoveNext()
        {
            position++;
            return (position < _Employees.Length);
        }
        public Employee Current 
        {
            get
            {
                if (position == -1 || position >= _Employees.Length)
                    throw new InvalidOperationException();
                return _Employees[position];
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public void Reset()
        {
            position = -1;
        }
    }
}
