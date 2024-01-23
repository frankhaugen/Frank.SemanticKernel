using PdfLibCore;
using PdfLibCore.Enums;
using SixLabors.ImageSharp;
using Image = TesseractOCR.Pix.Image;

namespace Frank.SemanticKernel.Plugins.Ocr;

public static class PdfPageExtensions
{
    public static async Task<Image> GetImageAsync(this PdfPage page, int dpi = 300)
    {
        using var bitmap = new PdfiumBitmap(
            (int) (dpi * page.Size.Width / 72),
            (int) (dpi * page.Size.Height / 72),
            true);
        page.Render(bitmap, PageOrientations.Normal, RenderingFlags.LcdText);
        await using var imageStream = bitmap.AsBmpStream(dpi, dpi);
        using var png = await SixLabors.ImageSharp.Image.LoadAsync(imageStream);
        using var pngStream = new MemoryStream();
        await png.SaveAsPngAsync(pngStream);
        return Image.LoadFromMemory(pngStream);
    }
}