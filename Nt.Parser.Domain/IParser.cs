using Nt.Parser.Symbols;

namespace Nt.Parser
{
    public interface IParser<T> where T : ISymbol
    {
        ParserResult<T> Parse(string content);
    }
}
