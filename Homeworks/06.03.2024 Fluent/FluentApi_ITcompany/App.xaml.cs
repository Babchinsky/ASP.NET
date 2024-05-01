using FluentApi_ITcompany.M;
using FluentApi_ITcompany.VM;
using System.Configuration;
using System.Data;
using System.Windows;

namespace FluentApi_ITcompany
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                using (var db = new Context())
                {
                    /*
                         var studio1 = new Studio { Name = "Rockstar Games" };
                         var studio2 = new Studio { Name = "Naughty Dog" };
                         var studio3 = new Studio { Name = "Ubisoft" };

                         var style1 = new Style { Name = "Action" };
                         var style2 = new Style { Name = "Adventure" };
                         var style3 = new Style { Name = "RPG" };
                         var style4 = new Style { Name = "Simulation" };

                         var game1 = new Game { Name = "Grand Theft Auto V", Realise = new DateTime(2013, 9, 17), Studio = studio1, Styles = new List<Style> { style1 } };
                         var game2 = new Game { Name = "The Last of Us Part II", Realise = new DateTime(2020, 6, 19), Studio = studio2, Styles = new List<Style> { style1, style2 } };
                         var game3 = new Game { Name = "Assassin's Creed Valhalla", Realise = new DateTime(2020, 11, 10), Studio = studio3, Styles = new List<Style> { style1, style3 } };
                         var game4 = new Game { Name = "The Sims 4", Realise = new DateTime(2014, 9, 2), Studio = studio1, Styles = new List<Style> { style4 } };

                         db.Studios.Add(studio1);
                         db.Studios.Add(studio2);
                         db.Studios.Add(studio3);

                         db.Styles.Add(style1);
                         db.Styles.Add(style2);
                         db.Styles.Add(style3);
                         db.Styles.Add(style4);

                         db.Games.Add(game1);
                         db.Games.Add(game2);
                         db.Games.Add(game3);
                         db.Games.Add(game4);

                         db.SaveChanges();*/


                    var S = from st in db.Employees
                            select st;

                    var St = from st in db.Positions
                             select st;



                    MainWindow view = new MainWindow();
                    VM_Main viewModel = new VM_Main(S, St);
                    view.DataContext = viewModel;
                    view.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
