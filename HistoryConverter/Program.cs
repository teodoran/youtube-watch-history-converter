using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using CommandLine;

namespace HistoryConverter
{
    class Program
    {
        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private static int ConvertHistoryFile(CommandLineOptions options)
        {
            if (!File.Exists(options.InputFile))
            {
                Console.WriteLine($"{ options.InputFile } is not a file");
                return 1;
            }

            var json = File.ReadAllText(options.InputFile);
            var views = JsonSerializer
                .Deserialize<HistoryRecord[]>(json, _serializerOptions)
                .Select(record => new View(record))
                .Select(view => JsonSerializer.Serialize(view));

            File.WriteAllLines(options.OutputFile, views);

            return 0;
        }

        public static int Main(string[] args) =>
            Parser.Default
                .ParseArguments<CommandLineOptions>(args)
                .MapResult(ConvertHistoryFile, errs => 1);
    }
}
