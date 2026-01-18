namespace Nt.Parser.States
{
    internal class EscapeCharState(SymbolsParser parser) : IState
    {
        public void Handle(char c)
        {
            parser.CurrentState = new DefaultState(parser);
            parser.CurrentToken += c;
        }
    }
}
