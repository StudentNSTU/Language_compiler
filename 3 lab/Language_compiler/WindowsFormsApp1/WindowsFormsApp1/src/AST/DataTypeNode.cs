using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src.AST
{
    internal class DataTypeNode:ExpressionNode
    {
        Token type;
        public DataTypeNode(Token type)
        {
            this.type = type;
        }
    }
}
