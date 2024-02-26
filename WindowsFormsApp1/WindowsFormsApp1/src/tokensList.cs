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
            { "INT", new TokenType("INT", "int") }
        };
    }
}
