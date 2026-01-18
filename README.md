# Nt.Parser

## Introduction
Many algorithm involving string requires parsing them into tokens before analysing the list of tokens created.

With this project, you can:
- Split a string into a list of tokens
- Customize the parser depending on your needs

## Customize the parser
The customisation parameters are:
- a list of separators: these separators are used to split the input string into tokens (default is `[' ']`)
- a list of symbols: these symbols are used to identify tokens in the input string (default is `[]`)

### Separators
Separators are characters that separate two different tokens and will be ignored when parsing. 

The intuitive separator is the white space (`a b` is parsed as two tokens: `a` and `b`), but you can overwrite it and set any separator. Common separators are:
|Separator|Use|Example|
|---------|---|-------|
|White space|White spaces separate tokens|`a b` -> `a`, `b`|
|`\n`|A new line always separate tokens|```a<newline>b``` -> `a`, `b`|
|`-`|The symbol `-` separate tokens|`a-b` -> `a`, `b`|

Please pay attention that each of these separators must be only one character (into simple quotes).

### Symbols
Symbols are sequence of characters that forms tokens when read. Symbols can be whatever you like, usually operators like `+` or `%`.

Please note that any first character of a symbol is considered as a breaker and will create a new token once read (except in the continuity of symbols). Here are some examples:

|Symbols|Example|Comments|
|-------|-------|--------|
|`+`|`a+b` -> `a`, `+`, `b`|`+` is a symbol|
|`+`|`a*b` -> `a*b` |`*` is not a symbol|
|`+`|`a++b`-> `a`, `+`, `+` `b`|`+` is a symbol but not `++`|
|`+`, `++`|`a++b` -> `a`, `++`, `b`|`++` is a symbol and thus primes on the symbol `+`|
|`++`|`a+b` -> `a`, `+`, `+`, `b`|`+` is not a symbol, but `++` marks `+` as a breaker symbol|
|`+=`|`a=b` -> `a=b`|`=` is not the first character in `+=`, and thus not marked as a breaker|
|`>`|`-->` -> `--`, `>`|here `--` is not a symbol but just a simple token|
|`>`,`-->`|`-->` -> `-->`|although `>` is a breaker symbol, `-->` is also a symbol|
|`>`, `->`, `-->`|`-->` -> `-->`|although `->` is a symbol, the presence of `--` before `>` makes `-->` prime on `->`|

## Using the parser
You can create an instance of the `SymbolsParser`
```csharp
using Nt.Parser;
var parser = new SymbolsParser(separators, symbols);
```

Use the method `SetSymbols`, `AddSymbol` or `RemoveSymbol` to set the symbols after creating the parser.

To parse a string, use the method `ParseString`
```csharp
ParserResult parsed = parser.ParseString(inputString);
```

A `ParserResult` is a structure that contains
- a list of tokens: list of the unique symbols that are present in the input string
- a list of token indices: list of parsed tokens indices, each index corresponds to a token in the tokens list

## Escape character
The escape character `\` is a special character that allows any symbol following it to be parsed in the continuity of the token being currently parsed instead of creating a new one. The escape character is used when you need to include symbols or separators in a token.

For example, consider the two following cases:
1. Consider a parser with `+` defined as a symbol. Then `a+b` will be parsed as 3 tokens : `a`, `+` and `b`. With the escape character, `a\+b` is parsed as a unique symbol `a+b`.
2. Consider a parser with `_` defined as a separator. Then `a_b` will be parsed as 3 tokens: `a`, `\_` and `b`. But `a\_b` is parsed as the unique symbol `a_b`.

## List of exceptions
This project contains several custom exceptions to handle errors that may occur during the parsing. Below is a list of these exceptions along with their descriptions.

|**Exception**|**Description**|
|---|---|
|`EmptySymbolException`| You are trying to add or remove an empty symbol to the parser symbols list. |
|`RegisteredSymbolException`| You are trying to add a symbol that is already registered in the parser symbols list. |
|`UnregisteredSymbolException`| You are trying to remove a symbol that is not registered in the parser symbols list. |