namespace Nt.Parser.States
{
    internal class SymbolState(SymbolsParser parser) : IState
    {

        public void Handle(char c)
        {
            List<char> next = parser.NextSymbols(parser.CurrentToken);
            if (c == '\\')
            {
                parser.CurrentState = new EscapeCharState(parser);
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
                parser.CurrentState = new DefaultState(parser);
            }
            else
            {
                parser.ParseCurrent();
                parser.CurrentToken = c.ToString();
                parser.CurrentState = new DefaultState(parser);
            }
        }
    }
}
