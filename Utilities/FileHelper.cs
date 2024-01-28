using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TenderProject.Utilities
{
    public class FileHelper
    {
        public static string[] GetFilesInDirectoryWithExtension(string directoryPath, string extension)
        {
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Директория {directoryPath} не существует.");
                return new string[0];
            }

            try
            {
                string[] files = Directory.GetFiles(directoryPath, $"*.{extension}");
                return files;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске файлов: {ex.Message}");
                return new string[0];
            }
        }
    }
}
