using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Parser.Application.Programs
{
    internal abstract class ProgramMethod(Program program)
    {
        protected Program Program { get; set; } = program;

        abstract internal void Execute();

        protected static void Transition()
        {
            Console.WriteLine("");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine("");
        }
    }
}
