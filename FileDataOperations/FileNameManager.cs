using System.IO;

namespace FileDataOperations
{
    public class FileNameManager
    {
        public static string GetNextFileName(string fileNameBase,string fileExtension, string folderPath)
        {
            int i = 0;
            string fileName = fileNameBase;

            do
            {
                fileName = $"{fileNameBase}{i}.{fileExtension}";
                i++;
            }
            while (File.Exists($"{folderPath}\\{fileName}"));

            return fileName;
        }
    }
}
