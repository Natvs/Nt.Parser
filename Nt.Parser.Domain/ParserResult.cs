using Nt.Parser.Structures;
using System.Text;

namespace Nt.Parser
{
    /// <summary>
    /// Contains results of a successive parsing. 
    /// </summary>
    public class ParserResult
    {
        /// <summary>
        /// List of unique symbols that the parser read. These words are referenced by parsed tokens.
        /// </summary>
        internal SymbolsList Symbols { get; } = new();

        /// <summary>
        /// List of tokens that have been parsed. Value of a parsed token refers to the index in tokens list.
        /// </summary>
        internal ParsedList Parsed { get; } = new();

        /// <summary>
        /// Retrieves a list of symbol names available in the current context.
        /// </summary>
        /// <returns>A list of strings containing the names of all available symbols.</returns>
        public List<string> GetSymbols()
        {
            var result = new List<string>();
            foreach (var symbol in Symbols.GetSymbols())
            {
                result.Add(symbol.Name);
            }
            return result;

        }

        /// <summary>
        /// Returns a list of symbol names corresponding to the parsed tokens.
        /// </summary>
        /// <returns>A list of strings containing the names of symbols for each token in the parsed sequence.</returns>
        public List<string> GetParsed()
        {
            var result = new List<string>();
            foreach (var token in Parsed.GetTokens())
            {
                result.Add(token.Symbol.Name);
            }
            return result;
        }

        /// <summary>
        /// Returns a concise string that represents the list of parsed tokens
        /// </summary>
        /// <returns>A list of string containing all the parsed tokens</returns>
        public string GetParsedString()
        {
            var sb = new StringBuilder().Append('{');

            for (int i = 0; i < Parsed.GetCount() - 1; i++)
            {
                var token = Parsed.Get(i).Symbol.Name;
                sb.Append($"{token}, ");
            }
            if (Parsed.GetTokens().Count > 0)
            {
                var token = Parsed.Get(Parsed.GetCount() - 1).Symbol.Name;
                sb.Append(token);
            }
            sb.Append('}');
            return sb.ToString();
        }

        /// <summary>
        /// Returns a string that represents the current object, including the list of symbols and their associated
        /// tokens.
        /// </summary>
        /// <remarks>The returned string includes the values of the Symbols collection and the Parsed
        /// tokens, displaying each token's line number and corresponding symbol value. This method is intended
        /// primarily for debugging and logging purposes.</remarks>
        /// <returns>A string containing the symbols and tokens in the object, formatted for readability.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder().Append("Symbols = {");

            for (int i = 0; i < Symbols.GetCount() - 1; i++)
            {
                sb.Append($"'{Symbols.Get(i)}', ");
            }
            if (Symbols.GetCount() > 0)
            {
                sb.Append($"'{Symbols.Get(Symbols.GetCount() - 1)}'");
            }
            sb.Append("}, Tokens = {");
            for (int i = 0; i < Parsed.GetCount() - 1; i++)
            {
                var token = Parsed.Get(i);
                sb.Append($"(Line: {token.Line}, Value: '{token.Symbol.Name}'), ");
            }
            if (Parsed.GetCount() > 0)
            {
                var token = Parsed.Get(Parsed.GetCount() - 1);
                sb.Append($"(Line: {token.Line}, Value: '{token.Symbol.Name}')");
            }

            return sb.ToString();
        }

    }
}
