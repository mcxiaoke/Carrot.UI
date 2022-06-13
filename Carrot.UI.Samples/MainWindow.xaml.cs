using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Carrot.UI.Controls;
using Carrot.UI.Controls.Utils;
using Carrot.UI.Controls.Picker;

namespace Carrot.UI.Samples {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {
        public List<ColorComboBoxItem> _extraColors1 = new List<ColorComboBoxItem>() {
             ColorComboBoxItem.Create("TestColor111",
                UIHelper.ParseColor("#334455")),
             ColorComboBoxItem.Create("TestColor211",
                UIHelper.ParseColor("#6688aa")),
        };

        public List<ColorComboBoxItem> _extraColors2 = new List<ColorComboBoxItem>() {
             ColorComboBoxItem.Create("TestColor133",
                UIHelper.ParseColor("#123456")),
             ColorComboBoxItem.Create("TestColor244",
                UIHelper.ParseColor("#80cccccc")),
        };


        public MainWindow() {
            InitializeComponent();
            colorBox1.DataContext = _extraColors1;
        }

        private void ColorBox_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<ColorComboBoxItem> e) {
            Debug.WriteLine($"ColorBox_SelectedColorChanged changed {e.OldValue} => {e.NewValue}");
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
        }
    }
}
