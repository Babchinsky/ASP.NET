using FluentApi_ITcompany.M;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FluentApi_ITcompany.VM
{
    public class VM_Main:VM_Base
    {

        public ObservableCollection<VM_Employee> EmployeeList { get; set; }
        public ObservableCollection<VM_Position> PositionList { get; set; }


        public VM_Main(IQueryable<Employee> games, IQueryable<Position> studios)
        {
            EmployeeList = new ObservableCollection<VM_Employee>(games.Select(g => new VM_Employee(g)));
            PositionList = new ObservableCollection<VM_Position>(studios.Select(s => new VM_Position(s)));
          

        }

        int SelectedIndexEmployee = -1;
        public int selectedIndexEmployee
        {
            get { return SelectedIndexEmployee; }
            set
            {
                SelectedIndexEmployee = value;
                OnPropertyChanged(nameof(selectedIndexEmployee));
                if (SelectedIndexEmployee != -1)
                {
                   
                }
            }
        }

        int SelectedIndexPos = -1;
        public int selectedIndexPos
        {
            get { return SelectedIndexPos; }
            set
            {
                SelectedIndexPos = value;
                OnPropertyChanged(nameof(selectedIndexPos));
                if (SelectedIndexPos != -1)
                {

                }
            }
        }

        bool filterName = false;
        public bool FilterName
        {
            get { return filterName; }
            set
            {
                filterName = value;
                ShowFiltered();
                OnPropertyChanged(nameof(FilterName));
              
            }
        }
        public void ShowFiltered()
        {
       
                using (var db = new Context())
                {
                    IQueryable<Employee> query = db.Employees;
                    if (FilterName)
                    {
                        query = query.Where(g => g.Name == Text);
                    }
                    if (FilterSurname)
                    {
                        query = query.Where(g => g.Surname == Text2);
                    }
                    if (FilterPosition)
                    {
                        var pos= PositionList[SelectedIndexPos];                   
                        query = query.Where(g => g.Position.Name == pos.Name);
                    }

                    if (!filterName && !filterSurname && !filterPosition)
                    {
                        EmployeeList = new ObservableCollection<VM_Employee>(db.Employees.Select(g => new VM_Employee(g)));
                        OnPropertyChanged(nameof(EmployeeList));
                    }
                    var filteredEmployees = query.ToList();

                   
                    EmployeeList = new ObservableCollection<VM_Employee>(filteredEmployees.Select(g => new VM_Employee(g)));
                    OnPropertyChanged(nameof(EmployeeList));
                };
        }
        bool filterSurname = false;
        public bool FilterSurname
        {
            get { return filterSurname; }
            set
            {
                filterSurname = value;
                OnPropertyChanged(nameof(FilterSurname));
                ShowFiltered();
            }
        }

        bool filterPosition = false;
        public bool FilterPosition
        {
            get { return filterPosition; }
            set
            {
                filterPosition = value;
                OnPropertyChanged(nameof(FilterPosition));
                ShowFiltered();
            }
        }


        private string text;

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged(nameof(text));
            }
        }
        private string text2;

        public string Text2
        {
            get
            {
                return text2;
            }
            set
            {
                text2 = value;
                OnPropertyChanged(nameof(Text2));
            }
        }
        private string posText;
        public string PosText
        {
            get
            {
                return posText;
            }
            set
            {
                posText = value;
                OnPropertyChanged(nameof(PosText));
            }
        }






        private bool CanEdit()
        {
            if (SelectedIndexEmployee >-1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private DelegateCommand EditCommand;

        public ICommand editCommand
        {
            get
            {
                if (EditCommand == null)
                {
                    EditCommand = new DelegateCommand(param => Edit(), can => CanEdit());
                }
                return EditCommand;
            }
        }

        public void Edit()
        {
            using (var db=new Context())
            {
                var Empl = EmployeeList[SelectedIndexEmployee];
               
                var Emp = (from g in db.Employees
                           where g.Name == Empl.Name && g.Surname == Empl.Surname
                           select g);

                var Pos = (from E in db.Positions
                               where E.Name == Emp.Select(g=>g.Position.Name).FirstOrDefault()
                               select E.Id).Single();

                Text = Empl.Name;
                Text2 = Empl.Surname;   
              
                selectedIndexPos = Pos - 1;



            };
        }





        private bool CanAdd()
        {
            if (Text !=null && Text2!=null&& SelectedIndexPos>-1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private DelegateCommand AddCommand;

        public ICommand AddEmployeeCommand
        {
            get
            {
                if (AddCommand == null)
                {
                    AddCommand = new DelegateCommand(param => AddEmp(), can => CanAdd());
                }
                return AddCommand;
            }
        }

        public void AddEmp()
        {
            using (var db = new Context())
            {
                var Pos = PositionList[SelectedIndexPos];
                var P = (from g in db.Positions
                               where g.Name == Pos.Name
                               select g).Single();
                db.Employees?.Add(new Employee() { Name = text ,Surname=text2,Position=P});
                db.SaveChanges();
                EmployeeList = new ObservableCollection<VM_Employee>(db.Employees.Select(g => new VM_Employee(g)));
                OnPropertyChanged(nameof(EmployeeList));
            }
        }



        private bool CanAdd2()
        {
            if (PosText !=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private DelegateCommand AddPosCommand;

        public ICommand AddPositionCommand
        {
            get
            {
                if (AddPosCommand == null)
                {
                    AddPosCommand = new DelegateCommand(param => AddPos(), can => CanAdd2());
                }
                return AddPosCommand;
            }
        }

        public void AddPos()
        {
            using (var db = new Context())
            {
                db.Positions?.Add(new Position() { Name = PosText});
                db.SaveChanges();
                PositionList = new ObservableCollection<VM_Position>(db.Positions.Select(g => new VM_Position(g)));
                OnPropertyChanged(nameof(PositionList));
            };
        }






        private bool CanSubmitEdit()
        {
            if (Text != null && Text2 != null && SelectedIndexPos > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private DelegateCommand submitEditCommand;

        public ICommand SubmitEditCommand
        {
            get
            {
                if (submitEditCommand == null)
                {
                    submitEditCommand = new DelegateCommand(param => SubmitEdit(), can => CanSubmitEdit());
                }
                return submitEditCommand;
            }
        }

        public void SubmitEdit()
        {
            using (var db = new Context())
            {
                var Empl = EmployeeList[SelectedIndexEmployee];
                var Pos= PositionList[SelectedIndexPos];


                var Emp = (from g in db.Employees
                           where g.Name == Empl.Name && g.Surname == Empl.Surname
                           select g).Single();

                var Pos2 = (from E in db.Positions
                           where E.Name == Pos.Name
                           select E).Single();

                Emp.Name = Text;
                Emp.Surname = Text2;
                Emp.Position = Pos2;

                db.SaveChanges();

                EmployeeList = new ObservableCollection<VM_Employee>(db.Employees.Select(g => new VM_Employee(g)));
                OnPropertyChanged(nameof(EmployeeList));




            };
        }


        private bool CanEditPos()
        {
            if (SelectedIndexPos>-1&&posText!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private DelegateCommand submitEditPosCommand;

        public ICommand SubmitEditPosCommand
        {
            get
            {
                if (submitEditPosCommand == null)
                {
                    submitEditPosCommand = new DelegateCommand(param => SubmitEditPos(), can => CanEditPos());
                }
                return submitEditPosCommand;
            }
        }

        public void SubmitEditPos()
        {
            using (var db = new Context())
            {
              
                var Pos = PositionList[SelectedIndexPos];



                var Pos2 = (from E in db.Positions
                            where E.Name == Pos.Name
                            select E).Single();

                Pos2.Name = PosText;

                db.SaveChanges();

                PositionList = new ObservableCollection<VM_Position>(db.Positions.Select(g => new VM_Position(g)));
                OnPropertyChanged(nameof(PositionList));




            };
        }






        private bool CanDel()
        {
            if (SelectedIndexEmployee > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private DelegateCommand delCommand;

        public ICommand DelCommand
        {
            get
            {
                if (delCommand == null)
                {
                    delCommand = new DelegateCommand(param => Del(), can => CanDel());
                }
                return delCommand;
            }
        }

        public void Del()
        {
            using (var db = new Context())
            {
                var Empl = EmployeeList[SelectedIndexEmployee];

                var Emp = (from g in db.Employees
                           where g.Name == Empl.Name && g.Surname == Empl.Surname
                           select g).Single();

                db.Employees?.Remove(Emp);

                db.SaveChanges();
                EmployeeList = new ObservableCollection<VM_Employee>(db.Employees.Select(g => new VM_Employee(g)));
                OnPropertyChanged(nameof(EmployeeList));






            };
        }


        private bool CanDelPos()
        {
            if (SelectedIndexPos > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private DelegateCommand delPosCommand;

        public ICommand DelPosCommand
        {
            get
            {
                if (delPosCommand == null)
                {
                    delPosCommand = new DelegateCommand(param => delPos(), can => CanDelPos());
                }
                return delPosCommand;
            }
        }

        public void delPos()
        {
            using (var db = new Context())
            {

                var Pos = PositionList[SelectedIndexPos];



                var Pos2 = (from E in db.Positions
                            where E.Name == Pos.Name
                            select E).Single();

                db.Positions?.Remove(Pos2);

                db.SaveChanges();

                PositionList = new ObservableCollection<VM_Position>(db.Positions.Select(g => new VM_Position(g)));
                OnPropertyChanged(nameof(PositionList));




            };
        }




    }












}


