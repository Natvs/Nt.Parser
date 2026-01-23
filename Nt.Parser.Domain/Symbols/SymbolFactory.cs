namespace Nt.Parser.Symbols
{
    /// <summary>
    /// Provides a factory for creating instances of the <see cref="Symbol"> class.
    /// </summary>
    public class SymbolFactory : ISymbolFactory<Symbol>
    {
        public Symbol Create(string name) => new Symbol(name);
    }
}
