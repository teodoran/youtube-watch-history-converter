using System.IO;
using CommandLine;

namespace HistoryConverter
{
    public class CommandLineOptions
    {
        [Value(0, MetaName = "input file", Required = true, HelpText = "Name and location of the file with the YouTube history in JSON format. I.e. Exports/watch-history.json")]
        public string InputFile { get; set; }

        [Option('o', "output", Default = "converted-watch-history.jsonl", HelpText = "Name of the output file, where the converted history will be written.")]
        public string Output { get; set; }

        public string OutputFile => Path.Combine(Path.GetDirectoryName(InputFile), Output);
    }
}
