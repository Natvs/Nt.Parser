using Nt.Parser.Symbols;

namespace Nt.Parser.States
{
    internal class EscapeCharState<T>(SymbolsParser<T> parser) : IState where T : ISymbol
    {
        public void Handle(char c)
        {
            parser.CurrentState = new DefaultState<T>(parser);
            parser.CurrentToken += c;
        }
    }
}
