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
            { "DECLARE", new TokenType("DECLARE", "declare") },
            { "CONST", new TokenType("CONST", "const") },
            { "VAT", new TokenType("VAT", "vat") },
            { "ASSIGMENT", new TokenType("ASSIGMENT", "\\:=") },
            { "MINUS", new TokenType("MINUS", "\\-") },
            { "PLUS", new TokenType("PLUS", "\\+") },
            { "LPAR", new TokenType("LPAR", "\\(") },
            { "RPAR", new TokenType("RPAR", "\\)") }
        };
    }
}

//declare const
