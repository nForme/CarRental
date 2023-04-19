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
    /// Логика взаимодействия для AddEditClient.xaml
    /// </summary>
    public partial class AddEditClientPage : Page
    {
        private Clients currentClient;

        public AddEditClientPage(Clients choosenClient)
        {
            InitializeComponent();

            currentClient = new Clients();
            if (choosenClient != null)
                currentClient = choosenClient;
            else DataContext = currentClient;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(currentClient.FirstName))
            {
                stringBuilder.AppendLine("Не введено имя");
            }
            if (string.IsNullOrEmpty(currentClient.LastName))
            {
                stringBuilder.AppendLine("Не введена фамилия");
            }

            if (string.IsNullOrEmpty(currentClient.Patronymic))
            {
                stringBuilder.AppendLine("Не введено отчество");
            }

             
            if (stringBuilder.Length != 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                currentClient.PasportNumber = txtBoxNumberPassport.Text;    
                if (currentClient.Id == 0)
                {
                    CarRentalEntities.getContext().Clients.Add(currentClient);
                }
                CarRentalEntities.getContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.baseFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString());
            }
        }
    }
}
