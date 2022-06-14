using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Carrot.UI.Controls.Font {
    public class LocalizedFontFamily {
        public string Name => LocalizedName ?? EnglishName;
        public string EnglishName { get; set; }
        public string LocalizedName { get; set; }
        public FontFamily Font { get; set; }

        public LocalizedFontFamily(string englishName, string localizedName, FontFamily font) {
            EnglishName = englishName;
            LocalizedName = localizedName;
            this.Font = font;
        }

        public override string ToString() {
            return $"{{{nameof(Name)}={Name}, {nameof(EnglishName)}={EnglishName}, {nameof(LocalizedName)}={LocalizedName}, {nameof(Font)}={Font}}}";
        }
    }
}
