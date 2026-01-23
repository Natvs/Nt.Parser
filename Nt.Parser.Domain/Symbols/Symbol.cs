namespace Nt.Parser.Symbols
{

    /// <summary>
    /// Represents a symbol with a name.
    /// </summary>
    public class Symbol(string name) : ISymbol
    {
        public string Name { get; } = name;

        public override string ToString() => Name;

    }
}
