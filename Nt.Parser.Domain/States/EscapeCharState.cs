namespace Nt.Parser.States
{
    internal class EscapeCharState(SymbolsParser parser) : IState
    {
        public void Handle(char c)
        {
            var next = parser.NextSymbols(parser.CurrentToken);

            parser.CurrentState = new DefaultState(parser);
            parser.CurrentToken += c;
        }
    }
}
