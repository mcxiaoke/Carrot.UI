using System.Windows.Media;

namespace Carrot.UI.Controls.Font {

    public class FontColor {
        public string Name { get; set; }
        public SolidColorBrush Brush { get; set; }

        public FontColor(string name, SolidColorBrush brush) {
            Name = name;
            Brush = brush;
        }

        public override bool Equals(System.Object obj) {
            if (obj == null) {
                return false;
            }

            if (!(obj is FontColor p)) {
                return false;
            }

            return (this.Name == p.Name) && (this.Brush.Equals(p.Brush));
        }

        public bool Equals(FontColor p) {
            if (p == null) {
                return false;
            }

            return (this.Name == p.Name) && (this.Brush.Equals(p.Brush));
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return "FontColor [Color=" + this.Name + ", " + this.Brush.ToString() + "]";
        }
    }
}