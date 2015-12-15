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
    /// Interaction logic for AddNewChapterWindows.xaml
    /// </summary>
    public partial class AddNewChapterWindows : Window
    {
        public string AddedChapterName;
        public AddNewChapterWindows()
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
