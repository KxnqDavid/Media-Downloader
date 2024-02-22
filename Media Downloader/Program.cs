using System;
using System.Diagnostics;

namespace Downloader
{
    internal class Program
    {
        static void Main(string[] args)
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
            Console.WriteLine("4. MP3");

            choice = int.Parse(Console.ReadLine());

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
                    case 4:
                        downloadAudio(link, directory, "mp3");
                        break;
                }
            }
        }
        static void downloadVideo(string link, string directory, string format)
        {
        }

        static void downloadAudio(string link, string directory, string format)
        {

        }
        static void RunYTDLProcess(string arguments)
        {
            try
            {
                string ytDlpPath = "yt-dlp";

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = ytDlpPath,
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
