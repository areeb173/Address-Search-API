using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CsvToJsonApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var addresses = JsonHelper.ReadAll(101);
            string jsonOutput = JsonSerializer.Serialize(addresses, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("output.json", jsonOutput);
            Console.WriteLine("JSON file generated");
        }
    }

    public class JsonHelper
    {
        private static string filePath = "SampleDataset.csv";

        public static List<AddressSearchItem> ReadAll(int limit = 101)
        {
            var lines = File.ReadAllLines(filePath).Skip(1).Take(limit);
            var addresses = lines.Select(line =>
            {
                var parts = line.Split(',');

                return new AddressSearchItem
                {
                    AssessorsParcelNumber = ToNullableString(parts[0]),
                    StandardizedAssessorsParcelNumber = ToNullableString(parts[1]),
                    AddressLine1 = ToNullableString(parts[2]),
                    AddressLine2 = ToNullableString(parts[3]),
                    City = ToNullableString(parts[4]),
                    State = ToNullableString(parts[5]),
                    PostalCode = ToNullableInt(parts[6]),
                    PostalCodePlus4 = ToNullableInt(parts[7]),
                    AddressHouseNumber = ToNullableInt(parts[8]),
                    AddressPreDirection = ToNullableString(parts[9]),
                    AddressStreetName = ToNullableString(parts[10]),
                    AddressStreetSuffix = ToNullableString(parts[11]),
                    AddressPostDirection = ToNullableString(parts[12]),
                    AddressUnitType = ToNullableString(parts[13]),
                    AddressUnitNumber = ToNullableString(parts[14]),
                    CountyFips = ToNullableInt(parts[15]),
                    County = ToNullableString(parts[16]),
                    Latitude = ToNullableDouble(parts[17]),
                    Longitude = ToNullableDouble(parts[18]),
                    CurrentOwner = ToNullableString(parts[19]),
                    OwnerOccupied = ToNullableBool(parts[20]),
                    ZIP1st3 = ToNullableInt(parts[21]),
                    ZIP1st4 = ToNullableInt(parts[22]),
                    FullAddress = ToNullableString(parts[23])
                };
            }).ToList();

            return addresses;
        }

        // Helper methods
        private static string? ToNullableString(string input) =>
            string.IsNullOrWhiteSpace(input) ? null : input;

        private static int? ToNullableInt(string input) =>
            int.TryParse(input, out var result) ? result : null;

        private static double? ToNullableDouble(string input) =>
            double.TryParse(input, out var result) ? result : null;

        private static bool? ToNullableBool(string input) =>
            bool.TryParse(input, out var result) ? result : null;
    }
}
