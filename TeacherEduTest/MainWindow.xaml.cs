using System.Windows;
using TeacherEduTest.ContentMenu;
using EduTestClient.Services;
using TeacherEduTest.SideMenu;
using TeacherEduTest.TopMenu;

namespace TeacherEduTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SideMenuPanel _sideMenuPanel;
        private HomeTopMenuPanel _homeTopMenuPanel;
        private readonly IAccountService _accountService;

        private MainWindow()
        {
            InitializeComponent();             
        }

        public MainWindow(IAccountService accountService)
            :this()
        {
            this._accountService = accountService;
            WindowCreator.ContentGrid = ContentMenuGrid;
            WindowCreator.TopMenuGrid = TopMenuGrid;

            InitSideMenuPanel();
         //   InitTopMenuPanel();
            InitMainMenuPanel();
        }

        private void InitTopMenuPanel()
        {
           _homeTopMenuPanel = new HomeTopMenuPanel(_accountService);
            this.TopMenuGrid.Children.Add(_homeTopMenuPanel);
        }

        private void InitSideMenuPanel()
        {
            _sideMenuPanel = new SideMenuPanel(_accountService, this.ContentMenuGrid);
            this.SideMenuGrid.Children.Add(_sideMenuPanel);
        }

        private void InitMainMenuPanel()
        {
            WindowCreator.GetMainMenuPanel(_accountService);          
        }
    }
}
