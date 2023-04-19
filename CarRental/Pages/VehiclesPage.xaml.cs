using CarRental.Data;
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

namespace CarRental.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehiclesPage.xaml
    /// </summary>
    public partial class VehiclesPage : Page
    {
        public VehiclesPage()
        {
            InitializeComponent();
            Page_IsVisibleChanged();
        }


        private void Page_IsVisibleChanged()
        {
            if (Visibility == Visibility.Visible)
            {
                try
                {
                    CarRentalEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    DGridVehicles.ItemsSource = CarRentalEntities.getContext().Vehicles.ToList();
                }
                catch
                {
                    MessageBox.Show("Ошибка обновления данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void BtnAddVehicle_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnDeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (DGridVehicles.SelectedItems.Count > 0)
            {
                var VehicleForRemove = DGridVehicles.SelectedItems.Cast<Vehicles>().ToList();
                if (MessageBox.Show($"Вы точно хотите удалить следующие {VehicleForRemove.Count()} элементов?", "",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        CarRentalEntities.getContext().Vehicles.RemoveRange(VehicleForRemove);
                        CarRentalEntities.getContext().SaveChanges();
                        MessageBox.Show("Данные удалены!");
                        DGridVehicles.ItemsSource = CarRentalEntities.getContext().Vehicles.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали ни одну запись для удаления!", "Информация", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
