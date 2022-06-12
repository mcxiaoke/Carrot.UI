using System;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Carrot.UI.Controls;
using Carrot.UI.Controls.Utils;
using Color = System.Windows.Media.Color;

namespace Carrot.UI.Controls.Picker {
    /// <summary>
    /// ColorComboBox.xaml 的交互逻辑
    /// </summary>
    public partial class ColorComboBox : UserControl {

        public static KeyValuePair<string, Color> InvalidColorItem = new KeyValuePair<string, Color>("Invalid", UIHelper.ParseColor("#000001"));

        private static IEnumerable<KeyValuePair<string, Color>> GetAllColors() =>
            typeof(Colors).GetProperties()
            .Where(prop => typeof(Color).IsAssignableFrom(prop.PropertyType))
            .Select(prop => new KeyValuePair<string, Color>(prop.Name, (Color)prop.GetValue(null)));

        #region DependencyProperty

        public static readonly DependencyProperty ExtraColorsProperty =
    DependencyProperty.Register(nameof(ExtraColors), typeof(List<KeyValuePair<string, Color>>),
        typeof(ColorComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register(nameof(ColumnCount), typeof(int),
                typeof(ColorComboBox), new PropertyMetadata(1));

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(nameof(SelectedIndex), typeof(int),
                typeof(ComboBox), new PropertyMetadata(-1));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object),
                typeof(ComboBox), new PropertyMetadata(null));

        #endregion

        public List<KeyValuePair<string, Color>> ExtraColors {
            get => (List<KeyValuePair<string, Color>>)GetValue(ExtraColorsProperty) ?? new List<KeyValuePair<string, Color>>();
            set {
                SetValue(ExtraColorsProperty, value);
                //cmbColors.Items.Insert(0, value);
            }
        }

        public int ColumnCount {
            get => Utilities.Clamp((int)GetValue(ColumnCountProperty), 1, 6);
            set => SetValue(ColumnCountProperty, Utilities.Clamp(value, 1, 6));
        }

        public int SelectedIndex {
            get => cmbColors.SelectedIndex;
            set => cmbColors.SelectedIndex = value;
        }

        public object SelectedItem {
            get => cmbColors.SelectedItem;
            set => cmbColors.SelectedItem = value;
        }

        public ColorComboBox() {
            InitializeComponent();
        }

        private void ColorComboBox_Loaded(object sender, RoutedEventArgs e) {
            Debug.WriteLine("ColorComboBox_Loaded");
            foreach (KeyValuePair<string, Color> item in ExtraColors) {
                Debug.WriteLine($"loaded extra {item.Key} {item.Value}");
            }
            var allColors = new List<KeyValuePair<string, Color>>();
            allColors.AddRange(ExtraColors);
            allColors.AddRange(GetAllColors());
            cmbColors.ItemsSource = allColors;
        }

        private void ComboBox_Table_Loaded(object sender, RoutedEventArgs e) {
            Debug.WriteLine("ComboBox_Table_Loaded");
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

        #region Event Handlers
        private void CmbColors_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            KeyValuePair<string, Color> oldColor = InvalidColorItem;
            KeyValuePair<string, Color> newColor = InvalidColorItem;
            if (e.RemovedItems.Count > 0) {
                oldColor = (KeyValuePair<string, Color>)e.RemovedItems[0];
            }
            if (e.AddedItems.Count > 0) {
                newColor = (KeyValuePair<string, Color>)e.AddedItems[0];
            }
            Debug.WriteLine($"SelectionChanged old={oldColor} new={newColor}");
            var newEventArgs = new RoutedPropertyChangedEventArgs<KeyValuePair<string, Color>>
                (oldColor, newColor) { RoutedEvent = SelectedColorChangedEvent };

            RaiseEvent(newEventArgs);
        }



        public static readonly RoutedEvent SelectedColorChangedEvent =
    EventManager.RegisterRoutedEvent(nameof(SelectedColorChanged),
        RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<KeyValuePair<string, Color>>),
        typeof(ColorComboBox));


        public event RoutedPropertyChangedEventHandler<KeyValuePair<string, Color>> SelectedColorChanged {
            add {
                AddHandler(SelectedColorChangedEvent, value);
            }

            remove {
                RemoveHandler(SelectedColorChangedEvent, value);
            }
        }


        private void CmbColors_DropDownClosed(object sender, EventArgs e) {
            Debug.WriteLine($"CmbColors_DropDownClosed selected={cmbColors.SelectedItem}");
        }

        #endregion
    }


}
