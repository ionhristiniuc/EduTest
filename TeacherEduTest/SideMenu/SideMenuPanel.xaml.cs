using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EduTestClient.Services;
using EduTestClient.Services.Utils;
using EduTestContract.Models;
using EduTestContract.UIModels;
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
        private ObservableCollection<ObservableModuleModel> moduleList;

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

            CourseModel selectedCourse = await coursesService.GetCourse((int)CoursesComboBox.SelectedValue);

            WindowCreator.GetCurseMenuPanel(_accountService, selectedCourse);
            InitCourseTreeView(selectedCourse);
        }

        private void InitCourseTreeView(CourseModel currentCourseModel)
        {
            moduleList = new ObservableCollection<ObservableModuleModel>(ConvertToObservable(currentCourseModel.Modules));
            CoursesTreeView.DataContext = moduleList;
        }

        private void CoursesTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //MessageBox.Show(CoursesTreeView.SelectedItem.GetType().ToString());
        }

        private void ModuleMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            ObservableModuleModel selectedModuleModel = (ObservableModuleModel)CoursesTreeView.SelectedItem;
            MenuItem menuItem = (MenuItem)sender;

            if (menuItem.Name.Equals("ModuleRemoveMenuItem"))
            {
                moduleList.Remove(selectedModuleModel);
            }
            else if (menuItem.Name.Equals("AddChapterMenuItem"))
            {
                selectedModuleModel.Chapters.Add(new ObservableChapterModel()
                {
                    Name = "Ion",
                    Topics = new ObservableCollection<ObservableTopicModel>(),
                });
            }
        }

        private async void ChepterItem_OnClick(object sender, RoutedEventArgs e)
        {
            ObservableChapterModel selectedChapterModel = (ObservableChapterModel)CoursesTreeView.SelectedItem;
            MenuItem menuItem = (MenuItem)sender;

            if (menuItem.Name.Equals("ChapterRemoveMenuItem"))
            {

                foreach (var module in moduleList)
                {
                    if (module.Id == selectedChapterModel.ModuleId)
                    {
                        IChaptersService service = new ChaptersService(_accountService.AuthResponse.access_token, new JsonSerializer());
                        if( await service.DeleteChapter(selectedChapterModel.Id))
                            module.Chapters.Remove(selectedChapterModel);
                        else
                        {
                            MessageBox.Show("Error");
                        }
                    }
                }

            }
            else if (menuItem.Name.Equals("AddTopicMenuItem"))
            {
                selectedChapterModel.Topics.Add(new ObservableTopicModel()
                {
                    Name = "Adr",
                    ChapterId = selectedChapterModel.Id
                });
            }
        }

        private void TopicMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            ObservableTopicModel selectedTopicModel = (ObservableTopicModel)CoursesTreeView.SelectedItem;

            foreach (var model in moduleList)
            {
                foreach (var chapter in model.Chapters)
                {
                    chapter.Topics.Remove(selectedTopicModel);
                }
            }
        }

        private ObservableCollection<ObservableModuleModel> ConvertToObservable(IEnumerable<ModuleModel> model)
        {
            return new ObservableCollection<ObservableModuleModel>(model.Select(m => new ObservableModuleModel()
            {
                Name = m.Name,
                Id = m.Id,
                Chapters = new ObservableCollection<ObservableChapterModel>(m.Chapters.Select(c => new ObservableChapterModel()
                {
                    Name = c.Name,
                    Id = c.Id,
                    ModuleId = m.Id,
                    Topics = new ObservableCollection<ObservableTopicModel>(c.Topics.Select(t => new ObservableTopicModel()
                    {
                        Name = t.Name,
                        Id = t.Id,
                        ChapterId = c.Id
                    }))
                }))
            }));
        }

    }
}