using Nt.Parser.Symbols;
using System.Numerics;

namespace Nt.Parser.Structures
{
    /// <summary>
    /// Represents a parsed token identified by its index in list of tokens
    /// </summary>
    /// <param name="index">Index in tokens list</param>
    /// <param name="line">Line the token have been parsed</param>
    public class ParsedToken(ISymbol symbol, int line)
    {
        /// <summary>
        /// Represents the symbol associated with this parsed token.
        /// </summary>
        public ISymbol Symbol { get; } = symbol;
        /// <summary>
        /// Line the token have been parsed
        /// </summary>
        public int Line { get; } = line;
    }
}
