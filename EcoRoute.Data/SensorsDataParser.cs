using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using EcoRoute.Data.Mapping;
using EcoRoute.Infrastructure.Models;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace EcoRoute.Data
{
    public static class SensorsDataParser
    {
        static SensorsDataParser()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        private static List<SensorData> ParseExcelPackage(ExcelPackage excelPackage, string filePath)
        {
            var address = Path.GetFileNameWithoutExtension(filePath).Trim();//.Replace("_", " ");

            var sensorDataList = new List<SensorData>();
            
            var workbook = excelPackage.Workbook;
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet == null) return sensorDataList;

            var cells = worksheet.Cells;
            var rows = cells.Rows;

            
            for (var row = 2; row <= rows; row++)
            {
                var changeDateString = cells[row, 1];
                if (string.IsNullOrEmpty(changeDateString.GetValue<string>())) break;

                var sensorData = new SensorData(
                    address: address,
                    changeDate: DateTime.Parse(changeDateString.Text),
                    temperature: cells[row, 2].GetValue<float>(),
                    humidity: cells[row, 3].GetValue<float>(),
                    co2: cells[row, 4].GetValue<float>(),
                    los: cells[row, 5].GetValue<float>(),
                    dustPm1: cells[row, 6].GetValue<float?>(),
                    dustPm25: cells[row, 7].GetValue<float?>(),
                    dustPm10: cells[row, 8].GetValue<float?>(),
                    pressure: cells[row, 9].GetValue<float>(),
                    aqi: cells[row, 10].GetValue<float>(),
                    formaldehyde: cells[row, 11].GetValue<float>());
                
                sensorDataList.Add(sensorData);
            }
            
            worksheet.Dispose();
            workbook.Dispose();
            excelPackage.Dispose();

            return sensorDataList;
        }

        public static List<SensorData> ParseExcelFile(string path)
        {
            using var fileStream = new FileStream(path, FileMode.Open);

            var excelPackage = new ExcelPackage();
            excelPackage.Load(fileStream);

            return ParseExcelPackage(excelPackage, path);
        }
        
        public static async Task<List<SensorData>> ParseExcelFileAsync(string path)
        {
            await using var fileStream = new FileStream(path, FileMode.Open);

            var excelPackage = new ExcelPackage();
            await excelPackage.LoadAsync(fileStream);

            return ParseExcelPackage(excelPackage, path);
        }

        public static List<SensorData> ParseExcelFilesFolder(string folderPath, int? filesLimit = null, Action<int, int> progressCallback = null)
        {
            var files = Directory.GetFiles(folderPath, "*.xlsx");
            if (filesLimit != null)
            {
                files = files.Take(filesLimit.Value).ToArray();
            }
            var filesCount = files.Length;
            var progress = 0;

            var sensorsDataList = new List<SensorData>();
            foreach (var file in files)
            {
                var data = ParseExcelFile(file);
                sensorsDataList.AddRange(data);
                progressCallback?.Invoke(++progress, filesCount);
            }

            return sensorsDataList;
        }
        
        public static async IAsyncEnumerable<List<SensorData>> 
            ParseExcelFilesFolderAsync(string folderPath)
        {
            var files = Directory.GetFiles(folderPath, "*.xlsx");

            foreach (var file in files)
            {
                yield return await ParseExcelFileAsync(file);
            }
        }

        public static List<SensorData> ParseCsv(string filePath)
        {
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(filePath, Encoding.UTF8);
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<SensorDataMap>();
            return csvReader.GetRecords<SensorData>().ToList();
        }

        public static List<AnalyzedSensorsData> ParseAnalyzedJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<AnalyzedSensorsData>>(json);
        }
        
        public static async Task<List<AnalyzedSensorsData>> ParseAnalyzedJsonAsync(string filePath)
        {
            var json = await File.ReadAllTextAsync(filePath);
            return JsonConvert.DeserializeObject<List<AnalyzedSensorsData>>(json);
        }
    }
}