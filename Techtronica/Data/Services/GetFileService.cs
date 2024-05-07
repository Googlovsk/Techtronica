using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techtronica.Data.Services
{
    class GetFileService
    {
        static string sourceImagePath = string.Empty;
        static string targetImagePath = string.Empty;
        static readonly string dirName = ConfigurationManager.AppSettings["ImageDirectory"];
        static readonly DirectoryInfo directory = new DirectoryInfo(dirName);
        static bool isAllowCopy = false;

        public static string GetImagePath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Файлы изображений (*.jpg, *.png, *.jpeg)|*.jpg;*.png;*.jpeg";
            sourceImagePath = (fileDialog.ShowDialog() == true) ? fileDialog.FileName : null;

            if (sourceImagePath != string.Empty) 
            {
                targetImagePath = $"{dirName}{Path.GetFileName(sourceImagePath)}";
                isAllowCopy = true;
                return dirName + Path.GetFileName(targetImagePath);
            }
            else
            {
                isAllowCopy = false;
                return string.Empty;
            }
        }
        public static void CopyImageToProject()
        {
            
            if (isAllowCopy)
            {
                if (!directory.Exists) directory.Create();
                if (!File.Exists(targetImagePath)) File.Copy(sourceImagePath, targetImagePath);
            }
        }
    }
}
