using System.Windows.Controls;
using EduTestClient.Services;

namespace TeacherEduTest.ContentMenu
{
    /// <summary>
    /// Interaction logic for ContentMenuPanel.xaml
    /// </summary>
    public partial class MainMenuPanel : UserControl
    {
        private readonly IAccountService _accountService;

        private MainMenuPanel()
        {
            InitializeComponent();
        }

        public MainMenuPanel(IAccountService accountService)
            :this()
        {
            _accountService = accountService;
        }
    }
}
