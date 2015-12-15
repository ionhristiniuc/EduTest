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
    /// Interaction logic for AddNewTopicWindows.xaml
    /// </summary>
    public partial class AddNewTopicWindows : Window
    {
        public string AddedTopicName;
        public AddNewTopicWindows()
        {
            InitializeComponent();
        }
        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            AddedTopicName = ChapterNameTextBox.Text;
            this.Close();
        }
    }
}
