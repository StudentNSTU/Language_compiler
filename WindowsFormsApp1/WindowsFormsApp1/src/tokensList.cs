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
            //{ "SPACE", new TokenType("SPACE", "[\\s\\t]*") },
            { "INT", new TokenType("INT", "int") },
            { "MINUS", new TokenType("MINUS", "\\-") },
            { "PLUS", new TokenType("PLUS", "\\+") },
            { "LPAR", new TokenType("LPAR", "\\(") },
            { "RPAR", new TokenType("RPAR", "\\)") }
        };
    }
}
