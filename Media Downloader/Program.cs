using System;
using System.Diagnostics;

namespace Downloader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string directory = "";
                Console.WriteLine("Enter file directory for download: ");
                directory = Console.ReadLine();

                string link = "";
                Console.WriteLine("Enter media link: ");
                link = Console.ReadLine();

                int choice = 0;
                Console.WriteLine("Choices:");
                Console.WriteLine("1. Highest Quality!");
                Console.WriteLine("2. MP4");
                Console.WriteLine("3. WebM");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            downloadVideo(link, directory, "best");
                            break;
                        case 2:
                            downloadVideo(link, directory, "mp4");
                            break;
                        case 3:
                            downloadVideo(link, directory, "webm");
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static void downloadVideo(string link, string directory, string format)
        {
            string arguments = $"-f {format} -o \"{directory}\\%(title)s.%(ext)s\" {link}";
            RunYTDLProcess(arguments);
        }

        static void downloadAudio(string link, string directory, string format)
        {
            string arguments = $"--extract-audio --audio-format {format} -o \"{directory}\\%(title)s.%(ext)s\" {link}";
            RunYTDLProcess(arguments);
        }

        static void RunYTDLProcess(string arguments)
        {
            try
            {
                string ytDlpExecutable = "yt-dlp.exe";

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = ytDlpExecutable,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;

                    process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                    process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);

                    process.Start();

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();

                    Console.WriteLine("Download complete!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}