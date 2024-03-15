using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src.AST
{
    internal class BinOperationNode: ExpressionNode
    {
        Token operatorr;
        ExpressionNode leftNode, rigthNode;

        public BinOperationNode(Token operatorr, ExpressionNode leftNode, ExpressionNode rigthNode)
        {
            this.operatorr = operatorr;
            this.leftNode = leftNode;
            this.rigthNode = rigthNode;
        }
    }
}
