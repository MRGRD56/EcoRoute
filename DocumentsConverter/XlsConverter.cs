using System;
using System.IO;

namespace DocumentsConverter
{
    public static class XlsConverter
    {
        public static void ConvertXlsToXlsxFolder(string xlsFilesDirectory, string outputDirectory,
            bool isRelativePath = false)
        {
            var xlsFiles = Directory.GetFiles(xlsFilesDirectory);
            foreach (var xlsFile in xlsFiles)
            {
                ConvertXlsToXlsx(xlsFile, outputDirectory, isRelativePath);
            }
        }
        
        public static void ConvertXlsToXlsx(string xlsFile, string outputDirectory, bool isRelativePath = false)
        {
            var fileInfo = new FileInfo(xlsFile);
            var fileDirectory = fileInfo.Directory.FullName;
            var fileName = fileInfo.Name;

            var outputDirectoryPath = isRelativePath
                ? Path.Combine(fileDirectory, outputDirectory)
                : outputDirectory;

            if (!Directory.Exists(outputDirectoryPath))
            {
                Directory.CreateDirectory(outputDirectoryPath);
            }

            var app = new Microsoft.Office.Interop.Excel.Application();
            var workbook = app.Workbooks.Open(xlsFile);
            
            try
            {
                workbook.SaveAs(Path.Combine(outputDirectoryPath, fileName + "x"),
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
            }
            finally
            {
                workbook.Close();
                app.Quit();
            }
        }
    }
}