using Nt.Parser.Symbols;

namespace Nt.Parser.States
{
    internal class DefaultState<T>(SymbolsParser<T> parser) : IState where T : ISymbol
    {
        public void Handle(char c)
        {
            if (parser.Breaks.Contains(c))
            {
                parser.ParseCurrent();
                parser.CurrentToken = c.ToString();
                parser.CurrentState = new SymbolState<T>(parser);
            }
            else if (parser.Separators.Contains(c))
            {
                parser.ParseCurrent();
            }
            else if (c == '\\')
            {
                parser.CurrentState = new EscapeCharState<T>(parser);
            }
            else parser.CurrentToken += c;
        }

    }
}
