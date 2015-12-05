using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TeacherEduTest.ContentMenu;
using EduTestClient.Services;
using EduTestClient.Services.Utils;

namespace TeacherEduTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContentMenuPanel contentMenuPanel;

        public MainWindow()
        {
            InitializeComponent();
            Foo();
        }

        public async void Foo()
        {
            IAccountService accountService = new AccountService();
            if (await accountService.Authenticate("ionhristiniuc@yahoo.com", "ion123"))
            {
                ICoursesService coursesService = new CoursesService(accountService.AuthResponse.access_token, new JsonSerializer());
                var courses = await coursesService.GetCourses();
                MessageBox.Show(courses.First().Name);
            }
        }
    }
}
