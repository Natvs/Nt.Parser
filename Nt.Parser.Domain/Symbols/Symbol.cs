namespace Nt.Parser.Symbols
{

    /// <summary>
    /// Represents a token. A token is just a word.
    /// </summary>
    public class Symbol(string name)
    {
        public string Name { get; } = name;

        public override string ToString() => Name;

    }
}
