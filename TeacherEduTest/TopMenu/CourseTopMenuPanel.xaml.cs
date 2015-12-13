using System.Windows;
using System.Windows.Controls;
using EduTestClient.Services;
using EduTestContract.Models;
using TeacherEduTest.ContentMenu;

namespace TeacherEduTest.TopMenu
{
    /// <summary>
    /// Interaction logic for TopMenuPanel.xaml
    /// </summary>
    public partial class CourseTopMenuPanel : UserControl
    {
        private readonly IAccountService _accountService;
        private readonly CourseModel _courseModel;

        private CourseTopMenuPanel()
        {
            InitializeComponent();
        }

        public CourseTopMenuPanel(IAccountService accountService, CourseModel courseModel)
            :this()
        {
            _accountService = accountService;
            _courseModel = courseModel;
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowCreator.GetMainMenuPanel(_accountService);
        }
    }
}
