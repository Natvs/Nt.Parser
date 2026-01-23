using Nt.Parser.Symbols;

namespace Nt.Parser
{
    public interface IParser
    {
        ParserResult Parse(string content);
    }
}
