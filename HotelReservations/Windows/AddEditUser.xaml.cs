using HotelReservations.Model;
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
using System.Windows.Shapes;

namespace HotelReservations.Windows
{
    /// <summary>
    /// Interaction logic for AddEditUser.xaml
    /// </summary>
    public partial class AddEditUser : Window
    {
        public AddEditUser(User user = null)
        {
            InitializeComponent();
            AdjustWindow(user);
        }

        private void AdjustWindow(User user = null)
        {
            // TODO: Inicijalizovati combobox za selekciju tipa korisnika
            UserTypeCB.Items.Add(typeof(Receptionist).Name);
            UserTypeCB.Items.Add(typeof(Administrator).Name);

            if(user != null)
            {
                Title = "Edit user";
                
                UserTypeCB.SelectedItem = user.GetType().Name;
                UserTypeCB.IsEnabled = false;
            }
            else
            {
                Title = "Add user";
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedUserType = UserTypeCB.SelectedItem as string;
        }
    }
}
