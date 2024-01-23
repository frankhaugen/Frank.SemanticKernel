using PdfLibCore;
using TesseractOCR.Enums;

namespace Frank.SemanticKernel.Plugins.Ocr;

public static class PdfOcrRequestExtensions
{
    public static async Task<PdfOcrResult> GetTextFromPdf(this PdfOcrRequest ocrRequest)
    {
        using var engine = new TesseractOCR.Engine(@"tessdata", Language.English, EngineMode.Default);
        var pages = new List<string>();
        
        using var pdfDocument = new PdfDocument(ocrRequest.Data);
        foreach (var page in pdfDocument.Pages)
        {
            var image = await page.GetImageAsync();
            using var page2 = engine.Process(image);
            var text = page2.GetText(ocrRequest.OutputType);
            pages.Add(text);
        }
        
        return new PdfOcrResult(ocrRequest.FileName, pages.Select((x, i) => new PdfOcrPageResult(x, i)));
    }

}