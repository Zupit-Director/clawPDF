using System.Collections.Generic;
using zupit.zupitPDF.Core.Settings.Enums;
using zupit.zupitPDF.Shared.Helper;
using pdfforge.DynamicTranslator;

namespace zupit.zupitPDF.Shared.ViewModels.UserControls
{
    public class ImageFormatsTabViewModel : CurrentProfileViewModel
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;

        public static IEnumerable<EnumValue<JpegColor>> JpegColorValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<JpegColor>();

        public static IEnumerable<EnumValue<PngColor>> PngColorValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<PngColor>();

        public static IEnumerable<EnumValue<TiffColor>> TiffColorValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<TiffColor>();
    }
}