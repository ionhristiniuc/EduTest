using System.Windows;
using System.Windows.Controls;
using EduTestClient.Services;
using EduTestContract.Models;

namespace TeacherEduTest.ContentMenu
{
    static class WindowCreator
    {
        public static Grid ContentGrid;
        private static UserControl _currentOpenedWindow;

        public static UserControl GetMainMenuPanel(IAccountService accountService)
        {
            HideCurrentOpenedWindow(); 

            _currentOpenedWindow = new MainMenuPanel(accountService);
            ContentGrid.Children.Add(_currentOpenedWindow);
            return _currentOpenedWindow;     
        }

        public static UserControl GetCurseMenuPanel(IAccountService accountService, CourseModel courseModel)
        {
            HideCurrentOpenedWindow();                

            _currentOpenedWindow = new CourseMenuPanel(accountService, courseModel);
            ContentGrid.Children.Add(_currentOpenedWindow);
            return _currentOpenedWindow;
        }

        private static void HideCurrentOpenedWindow()
        {
            if (_currentOpenedWindow != null)
                _currentOpenedWindow.Visibility = Visibility.Hidden;
        }
    }
}
