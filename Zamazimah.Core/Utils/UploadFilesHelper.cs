using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
namespace Zamazimah.Helpers
{

    public static class UploadFilesHelper
    {

        public static string UploadPicture(IFormFile? file, string folder)
        {

            string pictureFilePath = "";
            if (file != null)
            {
                var folderName = Path.Combine("Resources", folder);
                var pathToSave = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
                if (file.Length > 0)
                {
                    string extension = Path.GetExtension(file.FileName);
                    string fileName = GenerateFileName(extension);
                    string fullPath = Path.Combine(pathToSave, fileName);
                    pictureFilePath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
            return pictureFilePath;
        }

        public static string GenerateFileName(string extension)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff") + extension;
        }

        public static string UploadPictureFromBase64String(string base64String, string folder, string fileName, string _path)
        {
            var bytes = Convert.FromBase64String(base64String);
            string file = System.Environment.GetEnvironmentVariable("TEMP") + fileName;
            File.WriteAllBytes(file, bytes);

            string pictureFilePath = "";
            if (file != null)
            {
                var folderName = Path.Combine("Resources", "Images", folder);
                var pathToSave = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
                if (file.Length > 0 && File.Exists(file))
                {
                    //string fileName2 = Path.GetFileName(file);
                    string fullPath = Path.Combine(pathToSave, fileName);
                    pictureFilePath = Path.Combine(folderName, fileName);
                    File.Copy(file, pictureFilePath, true);
                }
            }
            return pictureFilePath;
        }
        public static string UploadPictureFromFilePath(string file, string folder, string _env)
        {
            string pictureFilePath = "";
            if (file != null)
            {
                var folderName = Path.Combine("Resources", "Images", folder);
                var pathToSave = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
                if (file.Length > 0 && File.Exists(file))
                {
                    string fileName = Path.GetFileName(file);
                    string fullPath = Path.Combine(pathToSave, fileName);
                    pictureFilePath = Path.Combine(folderName, fileName);
                    File.Copy(file, fullPath, true);
                }
            }
            return pictureFilePath;
        }


        public static string UploadMultipe(List<string> files)
        {
            string filename = $"ExpotedTY_{ DateTime.Now.ToString("yyyyMMddHHmmssffff")}";
            var destFile = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP"), filename);
            if (!Directory.Exists(destFile))
            {
                Directory.CreateDirectory(destFile);
            }
            foreach (string s in files)
            {
                FileInfo f = new FileInfo(s);
                string filePath = Path.Combine(destFile, f.Name);
                if (!File.Exists(filePath))
                {
                    f.CopyTo(destFile + "\\" + $"{f.Name}");
                }
            }
            return destFile;
        }

        public static string UploadExcelFiles(IFormFile file)
        {
            var fileextension = Path.GetExtension(file.FileName);
            var filename = Guid.NewGuid().ToString() + fileextension;
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "files", filename);
            using (FileStream fs = System.IO.File.Create(filepath))
            {
                file.CopyTo(fs);
            }
            return filepath;
        }

    }
}
