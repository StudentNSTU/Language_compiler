using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src
{
    internal class tokensList
    {
        public Dictionary<string, TokenType> tokenTypeList = new Dictionary<string, TokenType>
        {
            { "NUMBER", new TokenType("NUMBER", "\\d") },
            { "NUMERIC", new TokenType("NUMERIC", "numeric") },
            { "DECLARE", new TokenType("DECLARE", "declare") },
            { "CONSTANT", new TokenType("CONSTANT", "constant") },
            { "ASSIGMENT", new TokenType("ASSIGMENT", "\\:=") },
            { "MINUS", new TokenType("MINUS", "\\-") },
            { "PLUS", new TokenType("PLUS", "\\+") },
            { "LPAR", new TokenType("LPAR", "\\(") },
            { "RPAR", new TokenType("RPAR", "\\)") },
            { "SEMICOLON", new TokenType("SEMICOLON", ";") },
            { "VARIABLE", new TokenType("VARIABLE", "[a-z]*") }
        };
    }
}

//declare const
