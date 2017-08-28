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
using Treaps;
namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Treap<string> tr;
        MainWindow parentWindow = null;
        public MainWindow()
        {
            InitializeComponent();
            tr = new Treap<string>();            
            doneBtn.Visibility = System.Windows.Visibility.Hidden;
        }
        public MainWindow(MainWindow parentWindow)
        {
            this.parentWindow = parentWindow;
            InitializeComponent();
            tr = new Treap<string>();
        }

        private void printNode(node<string> t, Tuple<double , double > parent, double x1, double x2, double y , Canvas cnv)
        {
            if (t == null) return;

            double x = (x2 + x1) / 2.0;
            
            VisualNode v = new VisualNode();
            v.node = t;
            t.vnode = v; 
            v.changeKey(t.key);
            v.changeValue(t.value());

            double curx = x - v.Width / 2;
            double cury = y - v.Height / 2;
            
            Tuple<double, double> p = new Tuple<double, double>(x, y); 

            if (parent != null)
            {
                Line l = new Line();
                l.X1 = p.Item1;
                l.Y1 = p.Item2;
                l.X2 = parent.Item1;
                l.Y2 = parent.Item2;
                l.Stroke = Brushes.Olive; 
                l.StrokeThickness = 2;
                l.SnapsToDevicePixels = true;
                l.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                cnv.Children.Add(l);


            }
            
            v.SetValue(Canvas.LeftProperty, curx);
            v.SetValue(Canvas.TopProperty, cury);
            cnv.Children.Add(v);
            printNode(t.left, p, x1 ,x, y + 80 , cnv);
            printNode(t.right, p, x ,x2, y + 80 , cnv);
            v.MouseDoubleClick += v_MouseDoubleClick;
            v.MouseRightButtonUp += v_MouseRightButtonUp;
                
        }

        void v_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (parentWindow == null)
            {
                VisualNode cur = (VisualNode)sender;
                node<string> curNode = (node<string>)cur.node;
                Treap<string> first = new Treap<string>(), second = new Treap<string>();
                tr.split(ref first, ref second, curNode.key);
                MainWindow firstMainWindow = new MainWindow();
                firstMainWindow.tr = first;
                firstMainWindow.printTreap();
                MainWindow secondMainWindow = new MainWindow();
                secondMainWindow.tr = second;
                
                secondMainWindow.printTreap();
                firstMainWindow.Show();
                secondMainWindow.Show();
                this.Close();
            }
        }

        void v_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            VisualNode cur = (VisualNode)sender;
            node<string> curNode = (node<string>)cur.node;
            tr.delete(curNode.key);

            printTreap();
        }
        private void printTreap()
        {
            canvas.Children.Clear();
            printNode(tr.getRoot(), null, 0, canvas.Width, 40 , canvas); 

        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            string x = keyInput.Text.Trim(); 
            tr.insert(x);            
            printTreap();
            
        }


        private void keyInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string x = keyInput.Text.Trim();
                tr.insert(x);
                printTreap();
                e.Handled = true; 
            }
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            tr = new Treap<string>();
            printTreap();
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            string x = searchInput.Text.Trim(); 
            var node = tr.Search(x);
            printTreap();
            if (node != null)
            {
                node.vnode.highLight();
            }
            else
            {
                MessageBox.Show("Not Found");
            }
        }

        private void joinBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow child = new MainWindow(this);
            child.ShowDialog();

        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.tr.join(tr);
            parentWindow.printTreap();
            this.Close();
        }

        private void helpBtn_Click(object sender, RoutedEventArgs e)
        {
            Help win = new Help();
            win.ShowDialog();
        }
    }
}
