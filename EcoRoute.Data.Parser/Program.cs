using DocumentsConverter;

namespace EcoRoute.Data.Parser
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            XlsConverter.ConvertXlsToXlsx(
                @"C:\Users\SU\Desktop\Данные по коробкам\Кетчерская улица_3.xls", 
                @"xlsx\", true);
            
            var data = SensorsDataParser.ParseExcelFile(
                @"C:\Users\SU\Desktop\Данные по коробкам\xlsx\Кетчерская улица_3.xlsx");
        }
    }
}