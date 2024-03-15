using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src.AST
{
    internal class VariableNode: ExpressionNode
    {
        Token variable;

        public VariableNode(Token variable)
        {
            this.variable = variable;
        }
    }
}
