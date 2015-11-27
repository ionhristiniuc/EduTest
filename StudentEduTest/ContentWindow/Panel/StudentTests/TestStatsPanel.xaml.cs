using System;
using System.Windows.Controls;

namespace StudentEduTest.ContentWindow.Panel
{
    /// <summary>
    /// Interaction logic for TestStatsPanel.xaml
    /// </summary>
    public partial class TestStatsPanel : UserControl
    {
        public TestStatsPanel()
        {
            InitializeComponent();
        }

        public TestStatsPanel(string name, string course, string status, DateTime passingDateTime)
            :this()
        {            
            this.Name.Text = name;
            this.Course.Text = course;
            this.Status.Text = status;
            this.Date.Text = passingDateTime.ToShortDateString();
        }
    }
}
