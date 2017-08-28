using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for VisualNode.xaml
    /// </summary>
    public partial class VisualNode: UserControl
    {
        public Object node = null; 
        public VisualNode()
        {
            InitializeComponent();
            
        }
        public void changeValue(object s)
        {
            value.Content = s.ToString(); 
            
        }
        public void changeKey(object s)
        {
            key.Content = s.ToString();
        }
        public void highLight()
        {
          
            rect.Stroke = new SolidColorBrush(Color.FromRgb(69, 218, 13));
            rect.StrokeThickness = 4; 
        }
    }
}
