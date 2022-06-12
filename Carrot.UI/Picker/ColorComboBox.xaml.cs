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
using static System.Net.Mime.MediaTypeNames;

namespace Carrot.UI.Controls.Picker {
    /// <summary>
    /// ColorComboBox.xaml 的交互逻辑
    /// </summary>
    public partial class ColorComboBox : UserControl {

        public static T Clamp<T>(T value, T min, T max)
where T : System.IComparable<T> {
            T result = value;
            if (value.CompareTo(max) > 0)
                result = max;
            if (value.CompareTo(min) < 0)
                result = min;
            return result;
        }

        private static IEnumerable<KeyValuePair<string, Color>> GetAllColors() =>
            typeof(Colors).GetProperties()
            .Where(prop => typeof(Color).IsAssignableFrom(prop.PropertyType))
            .Select(prop => new KeyValuePair<string, Color>(prop.Name, (Color)prop.GetValue(null)));


        public static readonly DependencyProperty ColumnCountProperty =
  DependencyProperty.Register(nameof(ColumnCount), typeof(int),
    typeof(ColorComboBox), new PropertyMetadata(1));

        public static readonly DependencyProperty SelectedIndexProperty =
DependencyProperty.Register(nameof(SelectedIndex), typeof(int),
typeof(ComboBox), new PropertyMetadata(-1));

        public int ColumnCount {
            get => Clamp((int)GetValue(ColumnCountProperty), 1, 6);
            set => SetValue(ColumnCountProperty, Clamp(value, 1, 6));
        }

        public int SelectedIndex {
            get => cmbColors.SelectedIndex;
            set => cmbColors.SelectedIndex = value;
        }


        public ColorComboBox() {
            InitializeComponent();
            cmbColors.ItemsSource = GetAllColors();
        }

        private void ComboBox_Table_Loaded(object sender, RoutedEventArgs e) {
            if (sender is Grid grid) {
                if (grid.RowDefinitions.Count == 0) {
                    for (int r = 0; r <= cmbColors.Items.Count / ColumnCount; r++) {
                        grid.RowDefinitions.Add(new RowDefinition());
                    }
                }
                if (grid.ColumnDefinitions.Count == 0) {
                    for (int c = 0; c < Math.Min(cmbColors.Items.Count, ColumnCount); c++) {
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                    }
                }
                for (int i = 0; i < grid.Children.Count; i++) {
                    Grid.SetColumn(grid.Children[i], i % ColumnCount);
                    Grid.SetRow(grid.Children[i], i / ColumnCount);
                }
            }
        }

    }


}
