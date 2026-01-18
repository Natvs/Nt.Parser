namespace Nt.Parser.Application.Programs
{

    internal class Home(Program program) : ProgramMethod(program)
    {
        internal override void Execute()
        {
            var answer = "";
            while (answer != "3")
            {
                Transition();

                Console.WriteLine("Welcome to Nt Syntax Analyser!");
                Console.WriteLine("Select the program you want to execute:");
                Console.WriteLine("1. View or edit the symbols list");
                Console.WriteLine("2. Enter text to parse");
                Console.WriteLine("3. Exit");

                answer = Console.ReadLine();
                if (answer == "1")
                {
                    var grammarParsing = new DefineSymbols(program);
                    grammarParsing.Execute();
                }
                else if (answer == "2")
                {
                    var codeAnalysis = new TextParsing(program);
                    codeAnalysis.Execute();
                }
            }
        }
    }
}
