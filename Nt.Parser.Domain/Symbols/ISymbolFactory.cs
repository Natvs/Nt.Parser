namespace Nt.Parser.Symbols
{
    public interface ISymbolFactory<T>
    {
        T Create(string name);
    }

    ///// <summary>
    ///// Represents a token. A token is just a word.
    ///// </summary>
    //public class Symbol(string name)
    //{
    //    public string Name { get; } = name;

    //    public override string ToString() => Name;

    //}
}
