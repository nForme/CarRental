using CarRental.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace CarRental
{

    public partial class AuthWindow : System.Windows.Window
    {
        private int countErrorAuths;

        public AuthWindow()
        {
            InitializeComponent();
            DataObject.AddCopyingHandler(txtBoxPassword, this.OnCancelCommand);
        }

        private void checkBoxShowPass_Checked(object sender, RoutedEventArgs e)
        {
            txtBoxPassword.Visibility = Visibility.Collapsed;
            txtBoxPasswordShow.Visibility = Visibility.Visible;

            txtBoxPasswordShow.Text = new NetworkCredential(string.Empty, txtBoxPassword.SecurePassword).Password;
            txtBoxPasswordShow.Focus();
        }
        private void checkBoxShowPass_Unchecked(object sender, RoutedEventArgs e)
        {
            txtBoxPassword.Visibility = Visibility.Visible;
            txtBoxPasswordShow.Visibility = Visibility.Collapsed;

            txtBoxPasswordShow.Text = "";
            txtBoxPassword.Focus();
        }
        private void OnCancelCommand(object sender, DataObjectEventArgs e)
        {
            e.CancelCommand();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var loginStatus = false;
                User currentUser = null;
                if (CarRentalEntities.getContext().User.Count(p => p.Login == txtBoxLogin.Text) != 0)
                {
                    currentUser = CarRentalEntities.getContext().User.First(p => p.Login == txtBoxLogin.Text);
                    var result = CarRentalEntities.getContext().User.ToList().Select(p => p).Where(p =>
                        p.Login == txtBoxLogin.Text && (p.Password == txtBoxPassword.Password ||
                                                        p.Password == txtBoxPassword.Password));
                    if (result.Count() != 0)
                    {
                        CarRentalEntities.getContext().SaveChanges();
                        Manager.loginedUser = currentUser;
                        loginStatus = true;
                        MessageBox.Show("Вы успешно авторизовались", "Авторизация", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        var baseWindow = new BaseWindow(currentUser);
                        baseWindow.Show();
                        Close();
                    }
                    else
                    {
                        countErrorAuths++;
                        MessageBox.Show("Неверный пароль!", "Авторизация", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        if (countErrorAuths == 3)
                        {
                            countErrorAuths = 0;
                            MessageBox.Show("Превышено количество попыток входа!\nПовторите попытку через 10 секунд.",
                                "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            Thread.Sleep(10000);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка связи с базой данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
