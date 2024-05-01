using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FluentApi_ITcompany.M
{
    public class Employee
    {


        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public int? PosId {  get; set; }
        public virtual Position? Position { get; set; }




    }
}
