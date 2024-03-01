using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src
{
    public class TokenType
    {
        public string regexp, name;

        public TokenType(string name, string regexp)
        {
            this.regexp = regexp;
            this.name = name;
        }
    }

    
}
