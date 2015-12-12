using System.Windows;
using System.Windows.Controls;
using EduTestClient.Services;
using EduTestClient.Services.Utils;
using EduTestContract.Models;
using TeacherEduTest.ContentMenu;

namespace TeacherEduTest.SideMenu
{
    /// <summary>
    /// Interaction logic for SideMenuPanel.xaml
    /// </summary>
    public partial class SideMenuPanel : UserControl
    {
        private readonly IAccountService _accountService;
        private readonly Grid _contentMenuGrid;

        private SideMenuPanel()
        {
            InitializeComponent();            
        }

        public SideMenuPanel(IAccountService accountService, Grid contentMenuGrid)
            : this()
        {
            _accountService = accountService;
            _contentMenuGrid = contentMenuGrid;
            InitCourses();
        }

        private async void InitCourses()
        {
            ICoursesService coursesService = 
                new CoursesService(_accountService.AuthResponse.access_token,
                new JsonSerializer());

            var courses = await coursesService.GetCourses(); 
           
            CoursesComboBox.ItemsSource = courses.Courses;
            CoursesComboBox.DisplayMemberPath = "Name";
            CoursesComboBox.SelectedValuePath = "Id";
        }

        private void CoursesComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ICoursesService coursesService =
               new CoursesService(_accountService.AuthResponse.access_token,
               new JsonSerializer());

            //CourseModel selectedCourse = await coursesService.GetCourseById() //TODO 

            WindowCreator.GetCurseMenuPanel(_accountService, new CourseModel());
        }
    }
}