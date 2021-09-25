using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using DocumentsConverter;
using EcoRoute.Data.Mapping;
using EcoRoute.Database;
using Newtonsoft.Json;

namespace EcoRoute.Data.Parser
{
    internal static class Program
    {
        private static void WriteCsvData()
        {
            // XlsConverter.ConvertXlsToXlsxFolder(
            //     @"C:\Users\SU\Desktop\Данные по коробкам\", 
            //     @"xlsx\", true);

            var data = SensorsDataParser.ParseExcelFilesFolder(
                @"C:\Users\SU\Desktop\Данные по коробкам\xlsx\", (progress, count) =>
                {
                    Console.WriteLine($"Progress: {progress} / {count}");
                });

            using var fileStream = new FileStream("sensors_data.csv", FileMode.Create, FileAccess.Write);
            using var fsWriter = new StreamWriter(fileStream, Encoding.UTF8);
            using var csvWriter = new CsvWriter(fsWriter, CultureInfo.InvariantCulture);
            csvWriter.Context.RegisterClassMap<SensorDataMap>();
            csvWriter.WriteRecords(data);
        }
        
        private static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var data = SensorsDataParser.ParseCsv(
                @"C:\Users\SU\Desktop\Данные по коробкам\sensors_data.csv");
            stopwatch.Stop();
            Console.WriteLine("Строк: " + data.Count);
            Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " s");
        }
    }
}