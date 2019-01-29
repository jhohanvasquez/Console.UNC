using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace ConsoleUNC
{
    class Program
    {
        static string _networkName;

        static void Main(string[] args)
        {
            var filePath = @"\\DESKTOP-T6QH60F\Prueba2\file.txt";
            var savePath = @"C:\\temp\file.txt";
            SaveACopyfileToServer(filePath, savePath);
        }


        public static void SaveACopyfileToServer(string filePath, string savePath)
        {
            var directory = Path.GetDirectoryName(savePath).Trim();
            var username = @"Jhohan";
            var password = "santiago0520";
            var filenameToSave = Path.GetFileName(savePath);

            if (!directory.EndsWith("\\"))
                filenameToSave = "\\" + filenameToSave;

            var command = "NET USE " + directory;
            ExecuteCommand(command, 5000);

            command = "NET USE " + directory + " /user:" + username + " " + password;
            ExecuteCommand(command, 5000);

            command = " copy \"" + filePath + "\"  \"" + directory + filenameToSave + "\"";

            ExecuteCommand(command, 5000);


            command = "NET USE " + directory;
            ExecuteCommand(command, 5000);
        }

        public static int ExecuteCommand(string command, int timeout)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/C " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = "C:\\",
            };

            var process = Process.Start(processInfo);
            process.WaitForExit(timeout);
            var exitCode = process.ExitCode;
            process.Close();
            return exitCode;
        }


    }
}
