using System.Text;

namespace Nt.Parser.Application.Programs
{
    internal class TextParsing(Program program) : ProgramMethod(program)
    {
        internal override void Execute()
        {
            var answer = "";
            while (answer != "2")
            {
                Transition();

                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1. Enter text to parse");
                Console.WriteLine("2. Back to main menu");
                answer = Console.ReadLine();
                if (answer == "1")
                {
                    var text = new StringBuilder();
                    Console.WriteLine("Enter the text to parse (write end to exit):");

                    var newline = "";
                    while (newline != null && !newline.Equals("end"))
                    {
                        if (newline != "")
                        {
                            text.AppendLine(newline);
                        }
                        newline = Console.ReadLine();
                    }

                    var textToParse = text.ToString();

                    var config = ParserConfig.GetConfig();
                    var parser = new SymbolsParser([' ', '\n'], config.SymbolsList);
                    var result = parser.Parse(textToParse);

                    Console.WriteLine("Parsing result:");
                    Console.WriteLine(result);
                }
            }
        }
    }
}
