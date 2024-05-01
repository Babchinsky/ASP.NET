using FluentApi_ITcompany.M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApi_ITcompany.VM
{
    public class VM_Position : VM_Base
    {

        private Position _Position;

        public VM_Position(Position pos)
        {
            _Position = pos;
        }

        public string Name
        {
            get { return _Position.Name; }
            set
            {
                _Position.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

}
