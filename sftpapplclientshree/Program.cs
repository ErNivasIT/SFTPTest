using Renci.SshNet;
using System;
using System.Threading;

namespace sftpapplclientshree
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var version = typeof(SftpClient).Assembly.GetName().Version;
            Console.WriteLine($"Renci.SshNet version: {version}");

            var fullVersion = typeof(SftpClient).Assembly.FullName;
            Console.WriteLine($"Renci.SshNet full version: {fullVersion}");

            SFTPUsingAsync();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void SFTPUsingAsync()
        {

            string host = "44.200.128.220"; // Replace with your SFTP host
            string username = "sftpuser"; // Replace with your SFTP username
            string password = "Ranga*12345"; // Replace with your SFTP password
            int port = 22; // SFTP port typically is 22

            using (var client = new SftpClient(host, port, username, password))
            {
                try
                {

                    client.Connect();

                    for (int i = 0; i < 10000; i++)
                    {
                        string localFilePath = @"D:\Shree\MVCCoreApp\sftpapplclientshree\sftpapplclientshree\FilePayment.xml";
                        string remoteFilePath = "/sftpuser/uploads/FilePayment_" + i + ".xml";
                        using (var fileStream = System.IO.File.OpenRead(localFilePath))
                        {
                            client.UploadFile(fileStream, remoteFilePath, true);
                        }
                        Console.WriteLine("File " + i + " uploaded successfully. by Task " + Thread.CurrentThread.ManagedThreadId);
                    }
                    // disconnect like you normally would
                    client.Disconnect();
                    // List directory contents
                    Console.WriteLine("List of files in the SFTP server:");
                    var listing = client.ListDirectory(".");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
         
    }
}
