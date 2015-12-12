using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        private ObservableCollection<ModuleModel> moduleList;

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

        private void CoursesTreeView_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }        

        static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }

        private async void CoursesComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ICoursesService coursesService =
               new CoursesService(_accountService.AuthResponse.access_token,
               new JsonSerializer());

            CourseModel selectedCourse = await coursesService.GetCourse((int) CoursesComboBox.SelectedValue);

            WindowCreator.GetCurseMenuPanel(_accountService, selectedCourse);
            InitCourseTreeView(selectedCourse);
        }

        private void InitCourseTreeView(CourseModel currentCourseModel)
        {
            moduleList = new ObservableCollection<ModuleModel>(currentCourseModel.Modules);
            CoursesTreeView.DataContext = moduleList;
        }

        private void CoursesTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //MessageBox.Show(CoursesTreeView.SelectedItem.GetType().ToString());
        }

        private void ModuleMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            ModuleModel selectedModuleModel = (ModuleModel)CoursesTreeView.SelectedItem;
            MenuItem menuItem = (MenuItem) sender;

            if (menuItem.Name.Equals("ModuleRemoveMenuItem"))
            {
                //
            }
            else if (menuItem.Name.Equals("AddChapterMenuItem"))
            {
                //
            }
        }        
    }
}