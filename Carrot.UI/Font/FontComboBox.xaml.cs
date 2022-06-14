using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Diagnostics;

namespace Carrot.UI.Controls.Font {

    public partial class FontComboBox : UserControl {


        public static IEnumerable<LocalizedFontFamily> AllFonts => FontUtilities.LocalizedFonts();

        public static readonly RoutedEvent FontChangedEvent = EventManager.RegisterRoutedEvent(
    nameof(FontChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FontComboBox));

        public static readonly DependencyProperty SelectedFontProperty = DependencyProperty.Register(
    nameof(SelectedFont), typeof(LocalizedFontFamily), typeof(FontComboBox), new UIPropertyMetadata(null));

        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
nameof(SelectedIndex), typeof(int), typeof(FontComboBox), new PropertyMetadata(-1));


        public FontComboBox() {
            InitializeComponent();
            Debug.WriteLine(CultureInfo.CurrentCulture);
            //this.DataContext = this;
            // = DataContext="{Binding RelativeSource={RelativeSource Self}}"
        }


        public event EventHandler<RoutedEventArgs> FontChanged {
            add { AddHandler(FontChangedEvent, value); }
            remove { RemoveHandler(FontChangedEvent, value); }
        }

        public LocalizedFontFamily SelectedFont {
            get => (LocalizedFontFamily)GetValue(SelectedFontProperty);
            set {
                SetValue(SelectedFontProperty, value);
            }
        }

        public int SelectedIndex {
            get => (int)GetValue(SelectedIndexProperty);
            set {
                SetValue(SelectedIndexProperty, value);
            }
        }

        private void FontComboBox_Loaded(object sender, RoutedEventArgs e) {

        }

        private void CBFonts_Loaded(object sender, RoutedEventArgs e) {
        }

        private void CBFonts_DropDownClosed(object sender, EventArgs e) {
            this.SetValue(SelectedFontProperty, CBFonts.SelectedItem);
            RaiseEvent(new RoutedEventArgs(FontComboBox.FontChangedEvent));
            Debug.WriteLine($"CBFonts_DropDownClosed selected={this.SelectedFont}");
        }
    }
}
