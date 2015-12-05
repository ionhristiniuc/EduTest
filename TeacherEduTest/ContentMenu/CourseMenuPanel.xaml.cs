using System.Windows.Controls;
using EduTestContract.Models;

namespace TeacherEduTest.ContentMenu
{
    /// <summary>
    /// Interaction logic for CourseMenuPanel.xaml
    /// </summary>
    public partial class CourseMenuPanel : UserControl
    {
        private readonly CourseModel _courseModel;

        public CourseMenuPanel()
        {
            InitializeComponent();
        }
        public CourseMenuPanel(CourseModel courseModel)
            :this()
        {
            _courseModel = courseModel;
            _courseModel = courseModel;
        }
    }
}
