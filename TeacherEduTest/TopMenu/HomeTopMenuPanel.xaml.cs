using System.Windows;
using System.Windows.Controls;
using EduTestClient.Services;
using TeacherEduTest.ContentMenu;

namespace TeacherEduTest.TopMenu
{
    /// <summary>
    /// Interaction logic for TopMenuPanel.xaml
    /// </summary>
    public partial class HomeTopMenuPanel : UserControl
    {
        private readonly IAccountService _accountService;     

        private HomeTopMenuPanel()
        {
            InitializeComponent();
        }

        public HomeTopMenuPanel(IAccountService accountService)
            :this()
        {
            _accountService = accountService;          
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowCreator.GetMainMenuPanel(_accountService);
        }
    }
}
