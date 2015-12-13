using System.Windows;
using System.Windows.Controls;
using EduTestClient.Services;
using EduTestContract.Models;
using TeacherEduTest.ContentMenu;
using TeacherEduTest.TopMenu;

namespace TeacherEduTest
{
    static class WindowCreator
    {
        public static Grid ContentGrid;
        public static Grid TopMenuGrid;

        private static UserControl _currentOpenedContentWindow;
        private static UserControl _currentOpenedTopMenuWindow;

        public static void GetMainMenuPanel(IAccountService accountService)
        {                     
            InitTopMenuPanel(new HomeTopMenuPanel(accountService));
            InitContentPanel(new MainMenuPanel(accountService));  
        }

        public static void GetCurseMenuPanel(IAccountService accountService, CourseModel courseModel)
        {            
            InitTopMenuPanel(new CourseTopMenuPanel(accountService, courseModel));
            InitContentPanel(new CourseMenuPanel(accountService, courseModel));                       
        }

        #region Private Methods

        private static void InitTopMenuPanel(UserControl topMenuPanel)
        {
            if (_currentOpenedTopMenuWindow != null)
                _currentOpenedTopMenuWindow.Visibility = Visibility.Hidden;

            _currentOpenedTopMenuWindow = topMenuPanel;
            TopMenuGrid.Children.Add(topMenuPanel);
        }

        private static void InitContentPanel(UserControl contentPanel)
        {
            if (_currentOpenedContentWindow != null)
                _currentOpenedContentWindow.Visibility = Visibility.Hidden;

            _currentOpenedContentWindow = contentPanel;
            ContentGrid.Children.Add(contentPanel);
        }        

        #endregion
    }
}
