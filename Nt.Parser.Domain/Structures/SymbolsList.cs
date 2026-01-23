using Nt.Parser.Symbols;
using System.Text;

namespace Nt.Parser.Structures
{

    public class SymbolsList
    {
        private List<ISymbol> Symbols { get; } = [];
        private ISymbolFactory Factory { get; }

        #region Constructors

        /// <summary>
        /// Instantiates a list of tokens
        /// </summary>
        public SymbolsList(ISymbolFactory factory) : base() 
        {
            Factory = factory;
        }
        /// <summary>
        /// Instantiates a list of tokens from a list of strings.
        /// </summary>
        /// <param name="list">List of string used to instantiate the tokens</param>
        public SymbolsList(ISymbolFactory factory, List<string> list)
        {
            Factory = factory;
            foreach (var word in list) Symbols.Add(factory.Create(word));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a new token to the list if not already existent.
        /// </summary>
        /// <param name="name">Name of the token to add</param>
        /// <returns>Last index of the list once the token has been added, or index of the existing one</returns>
        public ISymbol Add(string name)
        {
            if (!Contains(name))
            {
                var symbol = Factory.Create(name);
                Symbols.Add(symbol);
                return symbol;
            }
            return Symbols[IndexOf(name)];
        }

        /// <summary>
        /// Add a range of tokens to the list.
        /// </summary>
        /// <param name="names">Names of the tokens to add</param>
        /// <returns>Last index of the list once all the tokens have been added</returns>
        public int AddRange(IEnumerable<string> names)
        {
            foreach (var name in names) Symbols.Add(Factory.Create(name));
            return Symbols.Count - 1;
        }

        /// <summary>
        /// Returns a list of all symbols currently contained in the collection.
        /// </summary>
        /// <returns>A list of objects representing the symbols in the collection.</returns>
        public List<ISymbol> GetSymbols()
        {
            return [.. Symbols];
        }

        /// <summary>
        /// Gets a token in this list by its string content.
        /// </summary>
        /// <param name="name">Name of the token to get</param>
        /// <returns>First occurence of such token with the given name in the list</returns>
        /// <exception cref="KeyNotFoundException">It might be that no token with the given name was found.</exception>
        public ISymbol Get(string name)
        {
            foreach (var token in Symbols)
            {
                if (token.Name == name) { return token; }
            }
            throw new KeyNotFoundException("No token " + name + " found in list of symbols");
        }

        /// <summary>
        /// Gets a token in this list by its index.
        /// </summary>
        /// <param name="index">Index of the token</param>
        /// <returns>Symbol at the specified index</returns>
        public ISymbol Get(int index)
        {
            return Symbols[index];
        }

        /// <summary>
        /// Gets the number of symbols in the collection.
        /// </summary>
        /// <returns>The total number of symbols contained in the collection.</returns>
        public int GetCount()
        {
            return Symbols.Count;
        }

        /// <summary>
        /// Gets the index of a token by its string content
        /// </summary>
        /// <param name="name">Name of the token to get</param>
        /// <returns>First occurence of such token indice with the given name</returns>
        /// <exception cref="KeyNotFoundException">It might be that no token with the given name was found.</exception>
        public int IndexOf(string name)
        {
            for (int i = 0; i < Symbols.Count; i++)
            {
                if (Symbols[i].Name == name)
                {
                    return i;
                }
            }
            throw new KeyNotFoundException("No token " + name + " found in list of symbols");
        }

        /// <summary>
        /// Check if a token is in the list
        /// </summary>
        /// <param name="name">Name of the token to look for</param>
        /// <returns>True if the token is in the list, False if not</returns>
        public bool Contains(string name)
        {
            foreach (var token in Symbols)
            {
                if (token.Name.Equals(name)) { return true; }
            }
            return false;
        }

        /// <summary>
        /// Gets a string of the tokens in this list.
        /// </summary>
        /// <returns>A string representation of tokens in this list</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder().Append('{');
            for (int i = 0; i < Symbols.Count - 1; i++)
            {
                sb.Append(Symbols[i].Name).Append(", ");
            }
            if (Symbols.Count > 0) sb.Append(Symbols[Symbols.Count - 1].Name);
            sb.Append('}');
            return sb.ToString();
        }

        #endregion
    }

}
