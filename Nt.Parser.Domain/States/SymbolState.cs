using Nt.Parser.Symbols;

namespace Nt.Parser.States
{
    internal class SymbolState<T>(SymbolsParser<T> parser) : IState where T : ISymbol
    {

        public void Handle(char c)
        {
            List<char> next = parser.NextSymbols(parser.CurrentToken);
            if (c == '\\')
            {
                parser.CurrentState = new EscapeCharState<T>(parser);
            }
            else if (next.Contains(c))
            {
                parser.CurrentToken += c.ToString();
            }
            else if (parser.Breaks.Contains(c))
            {
                parser.ParseCurrent();
                parser.CurrentToken = c.ToString();
            }
            else if (parser.Separators.Contains(c))
            {
                parser.ParseCurrent();
                parser.CurrentState = new DefaultState<T>(parser);
            }
            else
            {
                parser.ParseCurrent();
                parser.CurrentToken = c.ToString();
                parser.CurrentState = new DefaultState<T>(parser);
            }
        }
    }
}
