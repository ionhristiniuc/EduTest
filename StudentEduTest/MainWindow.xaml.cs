using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using StudentEduTest.ContentWindow;
using StudentEduTest.ContentWindow.Panel;

namespace StudentEduTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private MainContentPanel mainContentPanel = new MainContentPanel();

        public MainWindow()
        {                     
            InitializeComponent();            
            InitTests();
            TestPanel.Children.Add(mainContentPanel);            
        }

        //gavna cod
        private void InitTests()
        {
            mainContentPanel.TestStackPanel.Children.Add(new TestStatsPanel("Test 1", "Prolog", "Passed", DateTime.Now));
            mainContentPanel.TestStackPanel.Children.Add(new TestStatsPanel("Test 2", "Prolog", "Passed", DateTime.Now));
            mainContentPanel.TestStackPanel.Children.Add(new TestStatsPanel("Test 3", "Grafica", "Failed", DateTime.Now));
            mainContentPanel.TestStackPanel.Children.Add(new TestStatsPanel("Test 4", "Etica", "Failed", DateTime.Now));
            mainContentPanel.TestStackPanel.Children.Add(new TestStatsPanel("Test 5", "Sisteme de operare", "Passed", DateTime.Now));
        }
    }
}
