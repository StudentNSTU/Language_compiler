using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src
{
    internal class Token
    {
        public TokenType type;
        public string value;
        public int pos;

        
        public Token(TokenType type, string value, int pos)
        {
            this.type = type;
            this.value = value;
            this.pos = pos;
        }
    }
}
