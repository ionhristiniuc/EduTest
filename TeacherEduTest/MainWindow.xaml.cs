using System.Windows;
using TeacherEduTest.ContentMenu;
using EduTestClient.Services;
using TeacherEduTest.SideMenu;

namespace TeacherEduTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SideMenuPanel _sideMenuPanel;        

        private readonly IAccountService _accountService;

        private MainWindow()
        {
            InitializeComponent();             
        }

        public MainWindow(IAccountService accountService)
            :this()
        {
            this._accountService = accountService;
            InitSideMenuPanel();
        }

        private void InitSideMenuPanel()
        {
            _sideMenuPanel = new SideMenuPanel(_accountService, this.ContentMenuGrid);
            this.SideMenuGrid.Children.Add(_sideMenuPanel);
        }
    }
}
