using System;
using System.Collections.Generic;
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

            string email = "ionh";
            string password = "123";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return;
            try
            {
                IAccountService accountService = new AccountService();
                var response = await accountService.Authenticate(email, password);

                if (!string.IsNullOrEmpty(response?.access_token))
                {
                    IQuestionsService questionsService = new QuestionsService(response.access_token, new JsonSerializer());
                    var question = new VariantQuestionModel()
                    {
                        Content = "Choose an answer!",
                        Enabled = true,
                        TopicId = 1,
                        Type = QuestionType.Radio,
                        UserId = 1,
                        Variants = new List<VariantModel>
                        {
                            new VariantModel() { Body = "Answer A", Correct = false},
                            new VariantModel() { Body = "Answer B", Correct = false},
                            new VariantModel() { Body = "Answer C", Correct = true},
                         }
                    };
                    var result = await questionsService.AddQuestion(question.TopicId, question);
                    MessageBox.Show(result.ToString());

                    IUsersService usersService = new UsersService(response.access_token,
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
                MessageBox.Show(ex.ToString());
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