namespace Nt.Parser.Application.Programs
{
    internal class DefineSymbols(Program program) : ProgramMethod(program)
    {
        internal override void Execute()
        {
            var config = ParserConfig.GetConfig();

            var action = "";
            while (action != "3")
            {
                Transition();

                for (int i = 0; i < config.SymbolsList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {config.SymbolsList[i]}");
                }

                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1. Add a symbol");
                Console.WriteLine("2. Delete a symbol");
                Console.WriteLine("3. Back to main menu");
                action = Console.ReadLine();
                if (action == "1")
                {
                    Console.WriteLine("Enter the symbol to add:");
                    var symbolToAdd = Console.ReadLine();
                    if (!string.IsNullOrEmpty(symbolToAdd))
                    {
                        config.SymbolsList.Add(symbolToAdd);
                        Console.WriteLine($"Symbol '{symbolToAdd}' added.");
                    }
                }
                else if (action == "2")
                {
                    Console.WriteLine("Enter the number of the symbol to delete:");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= config.SymbolsList.Count)
                    {
                        var symbolToDelete = config.SymbolsList[index - 1];
                        config.SymbolsList.RemoveAt(index - 1);
                        Console.WriteLine($"Symbol '{symbolToDelete}' deleted.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid index.");
                    }
                }
            }
        }
    }
}
