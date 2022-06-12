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

namespace Carrot.UI.Samples {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {
        private List<KeyValuePair<string, Color>> _extraColors = new List<KeyValuePair<string, Color>>() {
            new KeyValuePair<string, Color>("TestColor1",
                UIHelper.ParseColor("#123456")),
            new KeyValuePair<string, Color>("TestColor2",
                UIHelper.ParseColor("#80cccccc")),
        };

        public List<KeyValuePair<string, Color>> CustomColors {
            get => _extraColors;
            set => _extraColors = value;
        }


        public MainWindow() {
            InitializeComponent();
            colorBox.DataContext = CustomColors;
        }

        private void ColorBox_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<KeyValuePair<string, Color>> e) {
            Debug.WriteLine($"ColorBox_SelectedColorChanged changed {e.OldValue} => {e.NewValue}");
        }
    }
}
