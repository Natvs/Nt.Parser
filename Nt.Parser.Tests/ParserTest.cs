using Nt.Parser;
using Nt.Parser.Exceptions;

namespace Nt.Tests.Parser
{
    public class ParserTest()
    {
        [Fact]
        public void ParseSeparator_Test1()
        {
            var parser = new SymbolsParser([' '], []);
            var stringToParse = "a b c d e";
            var expectedTokens = new List<string>(["a", "b", "c", "d", "e"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSeparator_Test2()
        {
            var parser = new SymbolsParser(['-'], []);
            var stringToParse = "a-b--c---d----e";
            var expectedTokens = new List<string>(["a", "b", "c", "d", "e"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSymbols_Test1()
        {
            var parser = new SymbolsParser([' '], ["+", "-", "*", "/"]);
            var stringToParse = "a + b - c * d / e";
            var expectedTokens = new List<string>(["a", "+", "b", "-", "c", "*", "d", "/", "e"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSymbols_Test2()
        {
            var parser = new SymbolsParser([], ["+", "-", "*", "/"]);
            var stringToParse = "a+b-c*d/e";
            var expectedTokens = new List<string>(["a", "+", "b", "-", "c", "*", "d", "/", "e"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSymbols_Test3()
        {
            var parser = new SymbolsParser([' '], ["+", "++", "+++"]);
            var stringToParse = "a+ b++ c+++";
            var expectedTokens = new List<string>(["a", "+", "b", "++", "c", "+++"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSymbols_Test4()
        {
            var parser = new SymbolsParser([], ["+", "++", "+++"]);
            var stringToParse = "a+b++c+++";
            var expectedTokens = new List<string>(["a", "+", "b", "++", "c", "+++"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_Test1()
        {
            var parser = new SymbolsParser([], []);
            var stringToParse = "\\a";
            var expectedTokens = new List<string>(["a"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_Test2()
        {
            var parser = new SymbolsParser([], []);
            var stringToParse = "a\\b";
            var expectedTokens = new List<string>(["ab"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_Test3()
        {
            var parser = new SymbolsParser([], []);
            var stringToParse = "\\ab";
            var expectedTokens = new List<string>(["ab"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_Test4()
        {
            var parser = new SymbolsParser([], []);
            var stringToParse = "a\\\\b";
            var expectedTokens = new List<string>(["a\\b"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_TestSeparator()
        {
            var parser = new SymbolsParser([' '], []);
            var stringToParse = "a\\ b";
            var expectedTokens = new List<string>(["a b"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_TestSymbol()
        {
            var parser = new SymbolsParser([], ["+"]);
            var stringToParse = "a\\+b";
            var expectedTokens = new List<string>(["a+b"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void AddEmptySymbol_Test()
        {
            var parser = new SymbolsParser([], []);
            Assert.Throws<EmptySymbolException>(() => parser.AddSymbol(""));
        }
        [Fact]
        public void AddExistingSymbol_Exception()
        {
            var parser = new SymbolsParser([], ["*"]);
            Assert.Throws<RegisteredSymbolException>(() => parser.AddSymbol("*"));
        }

        [Fact]
        public void RemoveEmptySymbol_Test()
        {
            var parser = new SymbolsParser([], []);
            Assert.Throws<EmptySymbolException>(() => parser.RemoveSymbol(""));
        }
        [Fact]
        public void RemoveNonExistingSymbol_Test()
        {
            var parser = new SymbolsParser([], []);
            Assert.Throws<UnregisteredSymbolException>(() => parser.RemoveSymbol("/"));
        }

        private static void ParseString(SymbolsParser parser, string stringToParse, List<string> expectedTokens)
        {
            var result = parser.Parse(stringToParse);
            var parsed = result.GetParsed();

            Assert.Equal(expectedTokens.Count, parsed.Count);
            for (var i = 0; i < expectedTokens.Count; i++)
            {
                Assert.Equal(expectedTokens[i], parsed[i]);
            }
        }

    }
}

