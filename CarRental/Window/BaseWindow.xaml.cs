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
using CarRental.Data;
using CarRental.Pages;

namespace CarRental.Window
{
    /// <summary>
    /// Логика взаимодействия для BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : System.Windows.Window
    {
        private User currentUser;

        public BaseWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            Manager.baseFrame = baseFrame;
            txtLogin.Text = Manager.loginedUser.Login;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (baseFrame.CanGoBack)
                Manager.baseFrame.GoBack();
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if (baseFrame.CanGoForward)
                Manager.baseFrame.GoForward();
        }

        private void BtnVehicle_OnClick(object sender, RoutedEventArgs e)
        {
            Manager.baseFrame.Navigate(new VehiclesPage());
        }

        private void BtnOrders_OnClick(object sender, RoutedEventArgs e)
        {
            Manager.baseFrame.Navigate(new OrdersPage());
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            Manager.baseFrame.Navigate(new ClientsPage());
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            Manager.baseFrame.Navigate(new ReportsPage());
        }
    }
}
