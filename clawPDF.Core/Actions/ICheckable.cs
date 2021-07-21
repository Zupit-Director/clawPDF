using zupit.zupitPDF.Core.Settings;

namespace zupit.zupitPDF.Core.Actions
{
    internal interface ICheckable
    {
        ActionResult Check(ConversionProfile profile);
    }
}