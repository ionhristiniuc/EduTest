using System;
using System.Collections;
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
using TeacherEduTest.SideMenu.AddNewModelsWindows;

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
        private IModulesService _modulesService;
        private IChaptersService _chaptersService;
        private ITopicsService _topicsService;

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
            InitServices();
        }

        private void InitServices()
        {
            _modulesService = new ModulesService(_accountService.AuthResponse.access_token,
                new JsonSerializer());
            _chaptersService = new ChaptersService(_accountService.AuthResponse.access_token,
                new JsonSerializer());
            _topicsService = new TopicsService(_accountService.AuthResponse.access_token,
                new JsonSerializer());
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

        private async void ModuleMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            ObservableModuleModel selectedModuleModel = (ObservableModuleModel)CoursesTreeView.SelectedItem;
            MenuItem menuItem = (MenuItem)sender;

            if (menuItem.Name.Equals("ModuleRemoveMenuItem"))
            {
                if (isRemovable(selectedModuleModel.Chapters.Count))
                {
                    if (await _modulesService.DeleteModule(selectedModuleModel.Id))
                        moduleList.Remove(selectedModuleModel);
                    else
                        MessageBox.Show("Remove Module error occured",
                            "Error",MessageBoxButton.OK,MessageBoxImage.Warning);
                }
            }
            else if (menuItem.Name.Equals("AddChapterMenuItem"))
            {
                var addChapterWindow = new AddNewChapterWindows();
                addChapterWindow.ShowDialog();

               if (await _chaptersService.AddChapter(selectedModuleModel.Id,new ChapterModel()
               {
                   Name = addChapterWindow.AddedChapterName
               }))
                {
                    selectedModuleModel.Chapters.Add(new ObservableChapterModel()
                    {
                        Name = addChapterWindow.AddedChapterName,
                        Topics = new ObservableCollection<ObservableTopicModel>(),
                    });
                }
                else
                {
                    MessageBox.Show("Add Chapter error occured", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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
                        if (isRemovable(selectedChapterModel.Topics.Count))
                        {
                            if (await _chaptersService.DeleteChapter(selectedChapterModel.Id))
                                module.Chapters.Remove(selectedChapterModel);
                            else
                                MessageBox.Show("Remove Chapter error occured", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                }
            }
            else if (menuItem.Name.Equals("AddTopicMenuItem"))
            {
                // no_implementation _topicsService.AddTopic()
                var addTopicWindow = new AddNewTopicWindows();
                addTopicWindow.ShowDialog();

                selectedChapterModel.Topics.Add(new ObservableTopicModel()
                {
                    Name = addTopicWindow.AddedTopicName,
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
  
        private bool isRemovable( int size )
        {
            if (size != 0)
            {
                MessageBox.Show("Remove error occured, the element must be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

    }
}