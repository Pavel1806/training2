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
using _2MultitargetingStandart;

namespace _2WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = InputField.Text;

            if (name == "")
            {
                MessageBox.Show("Вы не ввели свое имя в поле");
            }
            else
            {
                StringWithTime str = new StringWithTime();
                MessageBox.Show($"{str.OutputNameDatetime(name)}");
            }
        }
    }
}

