using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EcoRoute.Infrastructure.Models;
using OfficeOpenXml;

namespace EcoRoute.Data
{
    public static class SensorsDataParser
    {
        static SensorsDataParser()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public static List<SensorData> ParseExcelFile(string path)
        {
            var sensorDataList = new List<SensorData>();

            using var fileStream = new FileStream(path, FileMode.Open);
            var address = Path.GetFileNameWithoutExtension(path);

            var excelPackage = new ExcelPackage();
            excelPackage.Load(fileStream);

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
                    Address: address,
                    ChangeDate: DateTime.Parse(changeDateString.Text),
                    Temperature: cells[row, 2].GetValue<float>(),
                    Humidity: cells[row, 3].GetValue<float>(),
                    Co2: cells[row, 4].GetValue<float>(),
                    Los: cells[row, 5].GetValue<float>(),
                    DustPm1: cells[row, 6].GetValue<float>(),
                    DustPm25: cells[row, 7].GetValue<float>(),
                    DustPm10: cells[row, 8].GetValue<float>(),
                    Pressure: cells[row, 9].GetValue<float>(),
                    Aqi: cells[row, 9].GetValue<float>(),
                    Formaldehyde: cells[row, 10].GetValue<float>());
                
                sensorDataList.Add(sensorData);
            }

            return sensorDataList;
        }
    }
}