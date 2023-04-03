using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFWebAPITest.Data;

namespace WPFWebAPITest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task Refresh()
        {
            dgUsers.ItemsSource = await NetManager.Get<List<User>>("api/Users/GetAllUsers");
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            await Refresh();

            await GetRoles();
        }

        private async Task GetRoles()
        {
            var rolesList = (await NetManager.Get<List<Role>>("api/Roles/GetAllRoles")).Select(r => r.Name);
            cbRole.ItemsSource = rolesList;
        }

        private async void AddButtonClick(object sender, RoutedEventArgs e)
        {
            if(tbName.Text == "" || tbAge.Text == "" || cbRole.SelectedItem== null)
            {
                return;
            }

            var role = (await NetManager.Get<Role>($"api/Roles/{cbRole.SelectedItem.ToString()}"));

            var user = new User()
            {
                Name = tbName.Text,
                Age = int.Parse(tbAge.Text),
                Role_Id = role.Id
            };

            await NetManager.Post("api/Users/Add", user);

            await Refresh();
        }
    }
}
