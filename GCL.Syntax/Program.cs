﻿using System;
using System.IO;
using GCL.Lex;
using GCL.Syntax.Dynamic;
using Semantic;

namespace GCL.Syntax
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceCode = File.ReadAllText(@"SourceCode.txt");
            var sourceTokens = File.ReadAllText(@"Tokens.txt");
            var grammarCode = File.ReadAllText(@"GrammarGCL.txt");
            var grammarTokens = File.ReadAllText(@"GrammarTokens.txt");
            var codeParser = new CodeParser(new Lexer(sourceTokens),
                grammarCode,
                new Lexer(grammarTokens),
                new GclCodeGenerator(),
                new DynamicCodeProvider(),
                new SemanticAnalysis());
            codeParser.Parse(sourceCode);
            Console.ReadLine();
        }

    }
}
