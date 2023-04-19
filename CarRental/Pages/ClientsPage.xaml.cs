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
using CarRental.Data;

namespace CarRental.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            Page_IsVisibleChanged();
        }

        private void Page_IsVisibleChanged()
        {
            try
            {
                if (Visibility == Visibility.Visible)
                {
                    CarRentalEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    DGridClients.ItemsSource = CarRentalEntities.getContext().Clients.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка обновления данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            Manager.baseFrame.Navigate(new AddEditClientPage((sender as Button).DataContext as Clients));
        }

        private void btnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (DGridClients.SelectedItems.Count > 0)
            {
                var clientsForRemove = DGridClients.SelectedItems.Cast<Clients>().ToList();
                if (MessageBox.Show($"Вы точно хотите удалить следующие {clientsForRemove.Count()} элементов?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        CarRentalEntities.getContext().Clients.RemoveRange(clientsForRemove);
                        CarRentalEntities.getContext().SaveChanges();
                        MessageBox.Show("Данные удалены!");
                        DGridClients.ItemsSource = CarRentalEntities.getContext().Clients.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали ни одну запись для удаления!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.baseFrame.Navigate(new AddEditClientPage((sender as Button).DataContext as Clients));
        }
    }
}
