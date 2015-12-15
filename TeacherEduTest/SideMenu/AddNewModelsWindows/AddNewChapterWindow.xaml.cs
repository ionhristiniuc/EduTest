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
using System.Windows.Shapes;

namespace TeacherEduTest.SideMenu.AddNewModelsWindows
{
    /// <summary>
    /// Interaction logic for AddNewChapterWindow.xaml
    /// </summary>
    public partial class AddNewChapterWindow : Window
    {
        public string AddedChapterName;
        public AddNewChapterWindow()
        {
            InitializeComponent();
        }

        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            AddedChapterName = ChapterNameTextBox.Text;
            this.Close();
        }
    }
}
