using System.Text;

namespace Nt.Parser.Structures
{

    /// <summary>
    /// Represents a list of parsed words, identified by their value.
    /// </summary>
    /// <param name="symbols">List of tokens that contains all parsed tokens</param>
    public class ParsedList
    {
        private List<ParsedToken> Tokens { get; } = [];

        /// <summary>
        /// Returns a list of all parsed tokens.
        /// </summary>
        /// <returns>A list of objects representing the parsed tokens.</returns>
        public List<ParsedToken> GetTokens() => [.. Tokens];

        /// <summary>
        /// Gets the total number of parsed tokens in the list.
        /// </summary>
        /// <returns>Total number of parsed tokens</returns>
        public int GetCount() => Tokens.Count;

        /// <summary>
        /// Retrieves the parsed token at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the token to retrieve.</param>
        /// <returns>The parsed token located at the specified index.</returns>
        public ParsedToken Get(int index) => Tokens[index];

        /// <summary>
        /// Adds a parsed token for the specified symbol at the given line number.
        /// </summary>
        /// <param name="symbol">The symbol to associate with the new parsed token.</param>
        /// <param name="line">The line number where the symbol appears. Must be greater or equal to 1, or -1 for no line.</param>
        public void Add(Symbol symbol, int line)
        {
            Tokens.Add(new ParsedToken(symbol, line));
        }

        /// <summary>
        /// Gets a string representing the parsed list
        /// </summary>
        /// <returns>String that represents the parsed list</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder().Append('{');

            for (int i = 0; i < Tokens.Count - 1; i++)
            {
                sb.Append(Tokens[i].Symbol.Name);
            }
            if (Tokens.Count > 0) sb.Append(Tokens[^1].Symbol.Name);
            sb.Append('}');
            return sb.ToString();
        }
    }

}
