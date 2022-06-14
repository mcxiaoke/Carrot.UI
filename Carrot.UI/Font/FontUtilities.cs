using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace Carrot.UI.Controls.Font {
    internal static class FontUtilities {

        public static string[] ourLocales = { "zh-CN", "zh-HK", "zh-TW" };
        public static LocalizedFontFamily GetLocalizedFontFamily(FontFamily fontfamily) {

            var lsd = fontfamily.FamilyNames;
            lsd.TryGetValue(XmlLanguage.GetLanguage("en-US"), out string englishName);
            string localizedName = null;
            //var locale = CultureInfo.CurrentCulture.Name;
            foreach (var loc in ourLocales) {
                if (!string.IsNullOrEmpty(localizedName)) { break; }
                lsd.TryGetValue(XmlLanguage.GetLanguage(loc), out localizedName);
            }
            //if (lsd.ContainsKey(XmlLanguage.GetLanguage(locale))) {
            //    lsd.TryGetValue(XmlLanguage.GetLanguage(locale), out localizedName);
            //}
            var a = new LocalizedFontFamily(englishName ?? lsd.FirstOrDefault().Value, localizedName, fontfamily);
            return a;
        }

        public static IEnumerable<LocalizedFontFamily> AllFonts => LocalizedFonts();

        public static IEnumerable<LocalizedFontFamily> LocalizedFonts() {
            var cnlist = new List<LocalizedFontFamily>();
            var enlist = new List<LocalizedFontFamily>();
            foreach (var font in Fonts.SystemFontFamilies) {
                var localizedFont = GetLocalizedFontFamily(font);
                if (string.IsNullOrEmpty(localizedFont.LocalizedName)) {
                    enlist.Add(localizedFont);
                } else {
                    cnlist.Add(localizedFont);
                }
            }
            //Debug.WriteLine($"{cl.Count} {el.Count}");
            var result = cnlist.OrderBy(it => it.Name).ToList();
            result.AddRange(enlist.OrderBy(it => it.Name));
            return result;
        }
    }
}
