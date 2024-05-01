using FluentApi_ITcompany.M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApi_ITcompany.VM
{
    public class VM_Employee:VM_Base
    {
        private Employee _Employee;

        public VM_Employee(Employee emp)
        {
            _Employee = emp;
        }

        public string Name
        {
            get { return _Employee.Name; }
            set
            {
                _Employee.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Surname
        {
            get { return _Employee.Surname; }
            set
            {
                _Employee.Surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
    }
}
