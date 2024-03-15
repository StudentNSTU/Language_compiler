using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src.AST
{
    internal class UnarOperationNode
    {
        Token operatorr;
        ExpressionNode operand;

        public UnarOperationNode(Token operatorr, ExpressionNode operand)
        {
            this.operatorr = operatorr;
            this.operand = operand;
        }
    }
}
