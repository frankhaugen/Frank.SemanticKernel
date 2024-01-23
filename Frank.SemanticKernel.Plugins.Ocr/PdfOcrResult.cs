namespace Frank.SemanticKernel.Plugins.Ocr;

public record PdfOcrResult(string FileName, IEnumerable<PdfOcrPageResult> Pages);