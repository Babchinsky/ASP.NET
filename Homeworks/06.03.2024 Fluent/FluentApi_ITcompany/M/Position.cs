using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApi_ITcompany.M
{
    public class Position
    {

        public int Id { get; set; }
        public string? Name { get; set; }


        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
