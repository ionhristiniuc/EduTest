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
using EduTestClient;

namespace TeacherEduTest.ContentMenu
{
    /// <summary>
    /// Interaction logic for ContentMenuPanel.xaml
    /// </summary>
    public partial class ContentMenuPanel : UserControl
    {
        private string user;

        public ContentMenuPanel()
        {
            InitializeComponent();
        }

        public ContentMenuPanel(string user)
            :this()
        {
            this.user = user;
        }
    }
}
