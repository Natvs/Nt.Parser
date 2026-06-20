using Nt.Parser.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Tests.Parser.Instances
{
    internal class CustomSymbol(string name) : ISymbol
    {
        public string Name { get; } = name;
    }

    internal class CustomSymbolFactory : ISymbolFactory<CustomSymbol>
    {
        public CustomSymbol Create(string name)
        {
            return new CustomSymbol(name);
        }
    }
}
