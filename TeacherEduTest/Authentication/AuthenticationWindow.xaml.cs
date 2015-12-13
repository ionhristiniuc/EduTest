using System;
using System.Linq;
using System.Threading;
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
        private static readonly object AuthenticationMonitor = new object();
        private static bool _isAuthenticationBegun;
        public AuthenticationWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();
        }

        private async void SignInButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isAuthenticationBegun)
                return;
            
            _isAuthenticationBegun = true;

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

                    if (!user.Roles.Any(
                            r => r.Equals(RoleType.Teacher.ToString())
                                || r.Equals(RoleType.Admin.ToString())))
                        return;

                    lock (AuthenticationMonitor)
                    {
                        var mainWindow = new MainWindow(accountService);
                        Close();
                        mainWindow.Show();
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Email address or Password are incorrect.");
                //MessageBox.Show(ex.ToString());
            }

            _isAuthenticationBegun = false;
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2) - 100;
        }
    }
}