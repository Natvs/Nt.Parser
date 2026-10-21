namespace Nt.Parser.Exceptions
{
    public class RegisteredSymbolException(string symbol) : Exception($"Symbol {symbol} already registered")
    {
    }
}
