using Frank.SemanticKernel.Plugins.Ocr;
using Xunit.Abstractions;

namespace Frank.SemanticKernel.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _outputHelper;

    public UnitTest1(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    // [Fact]
    public async Task Test1()
    {
        var path = @"D:\repos\Frank.SemanticKernel\Frank.SemanticKernel.Plugins.Ocr\25406207-1.pdf";
        var content = await File.ReadAllBytesAsync(path);
        var filename = Path.GetFileName(path);
        var request = new PdfOcrRequest(filename, content, OcrOutputType.Text);
        var result = await request.GetTextFromPdf();
        _outputHelper.WriteLine(result.FileName);

        foreach (var resultPage in result.Pages)
        {
            _outputHelper.WriteLine($"Page {resultPage.PageNumber}");
            _outputHelper.WriteLine(resultPage.Text);
        }
    }
}