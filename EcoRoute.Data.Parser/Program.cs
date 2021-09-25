using System;
using System.Collections.Generic;
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
using EcoRoute.Infrastructure.Models;
using Newtonsoft.Json;
using OpenStreetMap.Interop;

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
                @"C:\Users\SU\Desktop\Данные по коробкам\xlsx\", null, (progress, count) =>
                {
                    Console.WriteLine($"Progress: {progress} / {count}");
                });

            using var fileStream = new FileStream("sensors_data.csv", FileMode.Create, FileAccess.Write);
            using var fsWriter = new StreamWriter(fileStream, Encoding.UTF8);
            using var csvWriter = new CsvWriter(fsWriter, CultureInfo.InvariantCulture);
            csvWriter.Context.RegisterClassMap<SensorDataMap>();
            csvWriter.WriteRecords(data);
        }

        private static void AnalyzeData()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var data = SensorsDataParser.ParseCsv(
                @"C:\Users\SU\Desktop\Данные по коробкам\sensors_data.csv");
            stopwatch.Stop();
            Console.WriteLine("Строк: " + data.Count);
            Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " s");
            
            var analyzedData = AnalyzedSensorsData.FromSensorsData(data);
            var analyzedDataJson = JsonConvert.SerializeObject(analyzedData);
            File.WriteAllText("analyzed_data_new.json", analyzedDataJson);
        }

        public static void FixData()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var data = SensorsDataParser.ParseCsv(
                @"C:\Users\SU\Desktop\Данные по коробкам\sensors_data.csv");
            stopwatch.Stop();
            Console.WriteLine("Строк: " + data.Count);
            Console.WriteLine(stopwatch.Elapsed.TotalSeconds + " s");
            
            data.ForEach(d =>
            {
                d.Address = d.Address
                    .Replace("Кремлевская набережная", "Кремлёвская набережная")
                    .Replace("Мосфильмосвкая улица", "Мосфильмовская улица")
                    .Replace("Нагатскинская набережная", "Нагатинская набережная")
                    .Replace("Садовая-самотечная", "Садовая-самотёчная")
                    .Replace("Щелковское шоссе", "Щёлковское шоссе");
            });
            
            SensorsDataParser.WriteCsv(@"C:\Users\SU\Desktop\Данные по коробкам\sensors_data_2.csv", data);
        }

        private static async Task FixAnalyzedDataAsync()
        {
            var osm = new OpenStreetMapClient();
            
            var analyzedDataJson = await File.ReadAllTextAsync("analyzed_data_new.json");
            var analyzedData = JsonConvert.DeserializeObject<List<AnalyzedSensorsData>>(analyzedDataJson);
            foreach (var d in analyzedData)
            {
                foreach (var g in d.GeoData)
                {
                    if (g.Sensor.Coordinates == null)
                    {
                        var coordinates = await g.Sensor.FindCoordinatesAsync(osm);
                        if (coordinates == null)
                        {
                            Console.WriteLine("NULL " + g.Sensor.Address);
                        }
            
                        g.Sensor.Coordinates = coordinates;
                    }
                }
            }
            
            var newAnalyzedDataJson = JsonConvert.SerializeObject(analyzedData);
            await File.WriteAllTextAsync("analyzed_data_new2.json", newAnalyzedDataJson);
        }
        
        private static async Task Main(string[] args)
        {
            await FixAnalyzedDataAsync();
        }
    }
}