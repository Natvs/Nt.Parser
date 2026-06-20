using Nt.Parser;
using Nt.Parser.Exceptions;
using Nt.Parser.Symbols;
using Nt.Tests.Parser.Instances;

namespace Nt.Tests.Parser
{
    public class SymbolsParserTest()
    {
        [Fact]
        public void ParseSeparator_Test1()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [' '], []);
            var stringToParse = "a b c d e";
            var expectedTokens = new List<string>(["a", "b", "c", "d", "e"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSeparator_Test2()
        {
            var parser = new SymbolsParser(new SymbolFactory(), ['-'], []);
            var stringToParse = "a-b--c---d----e";
            var expectedTokens = new List<string>(["a", "b", "c", "d", "e"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSymbols_Test1()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [' '], ["+", "-", "*", "/"]);
            var stringToParse = "a + b - c * d / e";
            var expectedTokens = new List<string>(["a", "+", "b", "-", "c", "*", "d", "/", "e"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSymbols_Test2()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [], ["+", "-", "*", "/"]);
            var stringToParse = "a+b-c*d/e";
            var expectedTokens = new List<string>(["a", "+", "b", "-", "c", "*", "d", "/", "e"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSymbols_Test3()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [' '], ["+", "++", "+++"]);
            var stringToParse = "a+ b++ c+++";
            var expectedTokens = new List<string>(["a", "+", "b", "++", "c", "+++"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSymbols_Test4()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [], ["+", "++", "+++"]);
            var stringToParse = "a+b++c+++";
            var expectedTokens = new List<string>(["a", "+", "b", "++", "c", "+++"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_Test1()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [], []);
            var stringToParse = "\\a";
            var expectedTokens = new List<string>(["a"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_Test2()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [], []);
            var stringToParse = "a\\b";
            var expectedTokens = new List<string>(["ab"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_Test3()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [], []);
            var stringToParse = "\\ab";
            var expectedTokens = new List<string>(["ab"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_Test4()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [], []);
            var stringToParse = "a\\\\b";
            var expectedTokens = new List<string>(["a\\b"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_TestSeparator()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [' '], []);
            var stringToParse = "a\\ b";
            var expectedTokens = new List<string>(["a b"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_TestSymbol()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [], ["+"]);
            var stringToParse = "a\\+b";
            var expectedTokens = new List<string>(["a+b"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void AddEmptySymbol_Test()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [], []);
            Assert.Throws<EmptySymbolException>(() => parser.AddSymbol(""));
        }
        [Fact]
        public void AddExistingSymbol_Exception()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [], ["*"]);
            Assert.Throws<RegisteredSymbolException>(() => parser.AddSymbol("*"));
        }

        [Fact]
        public void RemoveEmptySymbol_Test()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [], []);
            Assert.Throws<EmptySymbolException>(() => parser.RemoveSymbol(""));
        }
        [Fact]
        public void RemoveNonExistingSymbol_Test()
        {
            var parser = new SymbolsParser(new SymbolFactory(), [], []);
            Assert.Throws<UnregisteredSymbolException>(() => parser.RemoveSymbol("/"));
        }

        [Fact]
        public void ValidSymbolType_Test1()
        {
            var parser = new SymbolsParser<CustomSymbol>(new CustomSymbolFactory(), [' '], ["+", "-", "*", "/", ";"]);
            var result = parser.Parse("var d = $;");

            foreach (var token in result.GetParsed())
            {
                Assert.IsType<CustomSymbol>(token.Symbol);
            }
        }

        private static void ParseString(SymbolsParser<Symbol> parser, string stringToParse, List<string> expectedTokens)
        {
            var result = parser.Parse(stringToParse);
            var parsed = result.GetParsed();

            Assert.Equal(expectedTokens.Count, parsed.Count);
            for (var i = 0; i < expectedTokens.Count; i++)
            {
                Assert.Equal(expectedTokens[i], parsed[i].Symbol.Name);
            }
        }

    }
}

