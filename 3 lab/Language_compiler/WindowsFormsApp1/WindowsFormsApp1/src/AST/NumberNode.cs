using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src.AST
{
    internal class NumberNode: ExpressionNode
    {
        Token number;

        public NumberNode(Token number)
        {
            this.number = number;
        }
    }
}
