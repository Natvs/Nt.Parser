// See https://aka.ms/new-console-template for more information
using Nt.Parser.Application.Programs;

internal class Program
{
    private ProgramMethod? currentMethod;

    public Program()
    {
        currentMethod = new Home(this);
    }

    public void SetNewMethod(ProgramMethod newmethod)
    {
        currentMethod = newmethod; 
        currentMethod.Execute();
    }

    private static void Main(string[] args)
    {
        var program = new Program();
        program.currentMethod?.Execute();
    }
}