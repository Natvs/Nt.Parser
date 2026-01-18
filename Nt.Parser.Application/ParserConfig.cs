namespace Nt.Parser.Application
{
    internal class ParserConfig
    {
        private static ParserConfig? _instance = null;

        public static ParserConfig GetConfig()
        {
            _instance ??= new ParserConfig();
            return _instance;
        }

        public List<string> SymbolsList { get; } = [];
    }
}
