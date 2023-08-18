using System.Collections.Generic;
using System.Globalization;
using Windows.Globalization;
using Windows.UI.Xaml;

namespace Virtual_Piano.Elements
{
    public sealed class CultureInfoCollection : List<CultureInfo>
    {
        readonly List<string> Languages;
        //readonly List<string> Languages = new List<string>
        //{
        //    "ar",
        //     "de",
        //     "en-US",
        //     "es",
        //     "fr",
        //     "it",
        //     "ja",
        //     "ko",
        //     "nl",
        //     "pt",
        //     "ru",
        //     "zh-Hans-CN",
        //};

        public int Index
        {
            get
            {
                string language = ApplicationLanguages.PrimaryLanguageOverride;
                if (this.Languages.Contains(language))
                    return this.Languages.IndexOf(language) + 1;
                else
                    return 0;
            }
        }

        public CultureInfoCollection()
        {
            this.Languages = new List<string>(ApplicationLanguages.ManifestLanguages);
            this.Languages.Sort();

            base.Add(new CultureInfo(string.Empty));
            foreach (string item in this.Languages)
            {
                base.Add(new CultureInfo(item));
            }
        }

        public static void SetLanguageEmpty()
        {
            if (ApplicationLanguages.PrimaryLanguageOverride == string.Empty) return;
            ApplicationLanguages.PrimaryLanguageOverride = string.Empty;
        }

        public static void SetLanguage(string language)
        {
            if (ApplicationLanguages.PrimaryLanguageOverride == language) return;
            ApplicationLanguages.PrimaryLanguageOverride = language;

            if (string.IsNullOrEmpty(language)) return;

            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                if (frameworkElement.Language != language)
                {
                    frameworkElement.Language = language;
                }
            }
        }
    }
}