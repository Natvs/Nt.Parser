using Nt.Parser;
using Nt.Parser.Exceptions;
using Nt.Parser.States;

namespace Nt.Parser
{

    /// <summary>
    /// Represents a parser that can be customized
    /// </summary>
    public class SymbolsParser
    {

        #region Parameters

        internal string CurrentToken { get; set; } = "";
        internal int CurrentLine { get; private set; } = -1;
        private ParserResult Result { get; set; } = new();

        internal List<char> Separators { get; } = [' '];
        internal List<char> Breaks { get; } = [];
        internal List<string> Symbols { get; set; } = [];

        internal IState? CurrentState { get; set; }

        #endregion



        #region Constructors

        /// <summary>
        /// Represents a parser with default separators and symbols list
        /// </summary>
        public SymbolsParser()
        {
            SetSymbols();
        }

        /// <summary>
        /// Represents a parser with custom separators and symbols list
        /// </summary>
        /// <param name="separators">List of words separators</param>
        /// <param name="symbols">List of symbols</param>
        public SymbolsParser(List<char> separators, List<string> symbols)
        {
            Separators = separators;
            Symbols = symbols;
            SetSymbols();
        }

        /// <summary>
        /// Add all symbols in tokens list and set breaker symbols
        /// </summary>
        /// <exception cref="EmptySymbolException">The symbols list might contain an empty string</exception>
        private void SetSymbols()
        {
            foreach (string symbol in Symbols)
            {
                if (symbol.Length == 0) throw new EmptySymbolException();
                if (!Breaks.Contains(symbol[0])) Breaks.Add(symbol[0]);
                Result.Symbols.Add(symbol);
            }
        }

        #endregion



        #region Public Methods

        /// <summary>
        /// Adds a new symbol
        /// </summary>
        /// <param name="symbol">Symbol to add</param>
        /// <exception cref="EmptySymbolException">Symbol should not be empty</exception>
        /// <exception cref="RegisteredSymbolException">Symbol should not be already registered</exception>
        public void AddSymbol(string symbol)
        {
            if (symbol.Length == 0) throw new EmptySymbolException();
            if (Symbols.Contains(symbol)) throw new RegisteredSymbolException(symbol);
            Symbols.Add(symbol);
            if (!Breaks.Contains(symbol[0])) Breaks.Add(symbol[0]);
        }

        /// <summary>
        /// Removes a symbol
        /// </summary>
        /// <param name="symbol">Symbol to remove</param>
        /// <exception cref="EmptySymbolException">Symbol should not be empty</exception>
        /// <exception cref="UnregisteredSymbolException">Symbol should not be already registered</exception>
        public void RemoveSymbol(string symbol)
        {
            if (symbol.Length == 0) throw new EmptySymbolException();
            if (!Symbols.Contains(symbol)) throw new UnregisteredSymbolException(symbol);
            Symbols.Remove(symbol);

            bool breaks = false;
            foreach (string sym in Symbols)
            {
                if (sym.StartsWith(symbol[0].ToString())) { breaks = true; break; }
            }
            if (!breaks) Breaks.Remove(symbol[0]);
        }

        /// <summary>
        /// Parses a string
        /// </summary>
        /// <param name="content">String to parse</param>
        /// <returns>Informations about the parsing stored in a parser result class</returns>
        public ParserResult Parse(string content)
        {
            // Resets all parameters
            CurrentToken = "";
            CurrentLine = 1;
            Result = new();
            CurrentState = new DefaultState(this);

            // Parses all characters
            foreach (char c in content)
            {
                if (c == '\r') continue;  // Ignores carriage return
                CurrentState.Handle(c);
                if (c == '\n') CurrentLine += 1;
            }
            ParseCurrent(); // Ensures the last token is also parsed

            return Result;
        }

        #endregion

        #region Project Methods

        /// <summary>
        /// Gets a list of all characters that could happen after a sequence of letters among symbols list
        /// </summary>
        /// <param name="start">Sequence of letters (being the first letters of the word)</param>
        /// <returns>List of all characters that can be found after the given sequence of letters (among symbols list)</returns>
        internal List<char> NextSymbols(string start)
        {
            var next = new List<char>();
            foreach (string symbol in Symbols)
            {
                if (symbol.Length > start.Length && symbol.StartsWith(start)) next.Add(symbol[start.Length]);
            }
            return next;
        }

        /// <summary>
        /// Parses the current token (if not empty) and resets it to an empty string
        /// </summary>
        internal void ParseCurrent()
        {
            if (CurrentToken.Length > 0)
            {
                var symbol = Result.Symbols.Add(CurrentToken);
                Result.Parsed.Add(symbol, CurrentLine);
                CurrentToken = "";
            }
        }
        #endregion


    }
}
