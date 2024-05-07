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
        static string sourceImagePath = null;
        static string targetImagePath = null;
        static readonly string dirName = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
        static readonly DirectoryInfo directory = new DirectoryInfo(dirName.ToString());
        static bool isAllowCopy = false;

        public static string GetImagePath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Файлы изображений (*.jpg, *.png, *.jpeg)|*.jpg;*.png;*.jpeg";
            if (fileDialog.ShowDialog() == true)
            {
                sourceImagePath = fileDialog.FileName;
                targetImagePath = $"{dirName}{Path.GetFileName(sourceImagePath)}";
                isAllowCopy = true;
                return sourceImagePath;
            }
            return null;
        }
        public static string CopyImageToProject()
        {
            if (isAllowCopy)
            {
                if (!directory.Exists) directory.Create();
                if (!File.Exists(targetImagePath)) File.Copy(sourceImagePath, targetImagePath);

                return targetImagePath;
            }
            return null;

        }
    }
}
