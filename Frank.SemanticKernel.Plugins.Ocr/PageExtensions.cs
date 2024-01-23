using TesseractOCR;

namespace Frank.SemanticKernel.Plugins.Ocr;

public static class PageExtensions
{
    public static string GetText(this Page page, OcrOutputType outputType = OcrOutputType.Text) =>
        outputType switch
        {
            OcrOutputType.Text => page.Text,
            OcrOutputType.Hocr => page.HOcrText(),
            OcrOutputType.ALtoXml => page.AltoText,
            OcrOutputType.Box => page.BoxText,
            OcrOutputType.Unlv => page.UnlvText,
            OcrOutputType.Tsv => page.TsvText,
            
            _ => throw new ArgumentOutOfRangeException(nameof(outputType), outputType, null)
        };
}