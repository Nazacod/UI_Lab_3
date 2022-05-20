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
using ViewModel;

using System.Globalization;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(new MessageBoxErrorReporter());
        }
    }

    public class MessageBoxErrorReporter : IErrorReporter
    {
        public void ReportError(string message) => MessageBox.Show(message);
    }

    [ValueConversion(typeof(Double[]), typeof(String))]
    public class StringToDoubleSConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double[] val = (double[])value;
            return $"{val[0]};{val[1]}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string[] words = ((string)value).Split(';');
                if (words.Length != 2)
                    return new double[2] { 0.0, 0.0 };
                double[] values = new double[2];
                values[0] = double.Parse(words[0]);
                values[1] = double.Parse(words[1]);
                return values;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return DependencyProperty.UnsetValue;
            }
        }
    }
}