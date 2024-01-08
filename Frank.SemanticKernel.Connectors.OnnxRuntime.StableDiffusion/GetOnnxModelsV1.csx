using System;
using System.Diagnostics;
using System.IO;

// Define the paths
string repoUrl = "https://huggingface.co/CompVis/stable-diffusion-v1-4";
string branch = "onnx";
string workingDir = AppContext.BaseDirectory;
string tempRepoDir = Path.Combine(workingDir, ".temp_repo");
string targetDir = Path.Combine(workingDir, "models");
string[] filesToCheckout = {
    "safety_checker/model.onnx",
    "text_encoder/model.onnx",
    "unet/model.onnx",
    "unet/weights.pb",
    "vae_decoder/model.onnx",
    "vae_encoder/model.onnx"
};

// Clone the repository with sparse-checkout
RunGitCommand($"lfs install", workingDir);
RunGitCommand($"clone {repoUrl} -b {branch} {tempRepoDir}", workingDir);
foreach (var file in filesToCheckout)
{
    string sourceFile = Path.Combine(tempRepoDir, file);
    string destFile = Path.Combine(targetDir, file);
    Directory.CreateDirectory(Path.GetDirectoryName(destFile) ?? throw new InvalidOperationException("Path.GetDirectoryName returned null."));
    File.Move(sourceFile, destFile, true);
}

// Reset directory and delete the temporary repository
Directory.Delete(tempRepoDir, true);

Console.WriteLine("Files downloaded and copied successfully.");

void RunGitCommand(string command, string dir)
{
    ProcessStartInfo startInfo = new ProcessStartInfo()
    {
        FileName = "git",
        Arguments = command,
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true,
        WorkingDirectory = dir
    };

    using (Process process = Process.Start(startInfo))
    {
        using (StreamReader reader = process.StandardOutput)
        {
            string result = reader.ReadToEnd();
            Console.WriteLine(result);
        }
        process.WaitForExit();
    }
}
