namespace Nt.Parser.States
{
    internal class DefaultState(SymbolsParser parser) : IState
    {
        public void Handle(char c)
        {
            if (parser.Breaks.Contains(c))
            {
                parser.ParseCurrent();
                parser.CurrentToken = c.ToString();
                parser.CurrentState = new SymbolState(parser);
            }
            else if (parser.Separators.Contains(c))
            {
                parser.ParseCurrent();
            }
            else if (c == '\\')
            {
                parser.CurrentState = new EscapeCharState(parser);
            }
            else parser.CurrentToken += c;
        }

    }
}
