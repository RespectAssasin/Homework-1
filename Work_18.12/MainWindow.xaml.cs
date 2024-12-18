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

namespace Work_18._12
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Binding binding1 = new Binding();
            binding1.ElementName = "SylfaenTextBox";
            binding1.Path = new PropertyPath("Text");
            SylfaenTextBlock.SetBinding(TextBlock.TextProperty, binding1);

            Binding binding2 = new Binding();
            binding2.ElementName = "CalibriTextBox";
            binding2.Path = new PropertyPath("Text");
            CalibriTextBlock.SetBinding(TextBlock.TextProperty, binding2);
        }
        
    }
}
