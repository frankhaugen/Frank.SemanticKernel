namespace Frank.SemanticKernel.Plugins.Ocr;

public record PdfOcrRequest(string FileName, byte[] Data, OcrOutputType OutputType);