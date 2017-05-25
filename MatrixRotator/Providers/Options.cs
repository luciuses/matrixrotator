using System.ComponentModel;
using CommandLine;
using CommandLine.Text;

namespace MatrixRotator.Providers
{
    public enum RotateValue
    {
        Rotate90 = 90,

        Rotate180 = 180,

        Rotate270 = 270
    }

    public interface IOptions
    {
        string InputFile { get; set; }

        RotateValue Rotate { get; }

        string OutputFile { get; }

        IParserState LastParserState { get; }

        bool Show { get; }

        string GetUsage();

        bool Parse(string[] args);
    }

    public class Options : IOptions{
        [Option('f', "file", Required = true,
            HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option('r', "rotate", DefaultValue = RotateValue.Rotate90,
            HelpText = "Rotate value, available 90, 180, 270")]
        public RotateValue Rotate { get; set; }

        [Option('o', "out", DefaultValue = "result.csv",
            HelpText = "Output file with result.")]
        public string OutputFile { get; set; }

        [Option('s', "show", DefaultValue = true,
            HelpText = "Show result to console.")]
        public bool Show { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage() {
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        public bool Parse(string[] args)
        {
            return Parser.Default.ParseArguments(args, this);
        }
    }
}