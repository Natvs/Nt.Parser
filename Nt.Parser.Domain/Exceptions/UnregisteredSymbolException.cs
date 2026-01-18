namespace Nt.Parser.Exceptions
{
    public class UnregisteredSymbolException(string symbol) : Exception($"Symbol {symbol} is not registered")
    {
    }
}
