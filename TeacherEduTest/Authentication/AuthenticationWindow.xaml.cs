using System;
using System.Linq;
using System.Windows;
using EduTestClient.Services;
using EduTestClient.Services.Utils;
using EduTestContract.Models;
using EduTestContract.Models.Enums;

namespace TeacherEduTest.Authentication
{
    /// <summary>
    ///     Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        private async void SignInButton_OnClick(object sender, RoutedEventArgs e)
        {
            string email = "ionhristiniuc@yahoo.com";
            string password = "ion123";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return;
            try
            {
                IAccountService accountService = new AccountService();

                if (await accountService.Authenticate(email, password))
                {
                    IUsersService usersService = new UsersService(accountService.AuthResponse.access_token,
                        new JsonSerializer());
                    UserModel user = await usersService.GetUser();

                    if (user.Roles.Any(r => r.Equals(RoleType.Teacher.ToString()) || r.Equals(RoleType.Admin.ToString())))                        
                    {
                        var mainWindow = new MainWindow(accountService);
                        Close();
                        mainWindow.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}