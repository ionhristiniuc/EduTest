using System.Windows.Controls;
using EduTestClient.Services;
using EduTestContract.Models;

namespace TeacherEduTest.ContentMenu
{
    /// <summary>
    /// Interaction logic for CourseMenuPanel.xaml
    /// </summary>
    public partial class CourseMenuPanel : UserControl
    {
        private readonly IAccountService _accountService;
        private readonly CourseModel _courseModel;

        private CourseMenuPanel()
        {
            InitializeComponent();
        }

        public CourseMenuPanel(IAccountService accountService, CourseModel courseModel)
            :this()
        {
            _accountService = accountService;
            _courseModel = courseModel;
            _courseModel = courseModel;
        }
    }
}
