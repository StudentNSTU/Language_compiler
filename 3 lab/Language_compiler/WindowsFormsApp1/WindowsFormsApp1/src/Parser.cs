using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.src.AST;

namespace WindowsFormsApp1.src
{
    internal class Parser
    {
        private List<Token> tokens;
        private int pos = 0;
        public Dictionary<string, int> scoupe = new Dictionary<string, int>();
        private Dictionary<string, TokenType> tokenTypeList = new tokensList().tokenTypeList;
        Label errorLine;

        public Parser(List<Token>listTokens, Label error)
        {
            tokens = listTokens;
            errorLine = error;
        }

        public Token match(params TokenType[] expected)
        {
            if(this.pos < this.tokens.Count)
            {
                Token currentToken = this.tokens[this.pos];
                var aa = expected.FirstOrDefault(type => type.name == currentToken.type.name);
                if (aa != null)
                {
                    this.pos += 1;
                    return currentToken;
                }
            }
            return null;
        }

        public Token require(params TokenType[] expected)
        {
            Token token = this.match(expected);
            if(token == null)
            {
                this.errorLine.Text = "На позиции " + this.pos + " ожидается " + expected[0].name;
                return null;
            }
            this.errorLine.Text = "";
            return token;
        }

        public ExpressionNode parseVariableOrNumber()
        {
            var number = match(tokenTypeList["NUMBER"]);
            if (number != null)
            {
                return new NumberNode(number);
            }
            var variable = match(tokenTypeList["VARIABLE"]);
            if (variable != null)
            {
                return new VariableNode(variable);
            }
            errorLine.Text = "На позиции "+ this.pos + " ожидалась переменная или число";
            return null;
        }

        public ExpressionNode parseDataType()
        {
            var numericType = match(this.tokenTypeList["NUMERIC"]);
            if (numericType != null)
                return new DataTypeNode(numericType);
            errorLine.Text = "На позиции " + this.pos + " ожидается тип переменной";
            return null;
        }

        public ExpressionNode parseConstant()
        {

            var constant = match(this.tokenTypeList["CONSTANT"]);
            if (constant == null)
            {
                errorLine.Text = "На позиции " + this.pos + " ожидалась constant";
                return null;
            }
            return new ConstantNode();
        }

        public ExpressionNode parsePerentheses()
        {
            if(match(tokenTypeList["LPAR"]) != null)
            {
                var node = this.parseFormula();
                if(node == null)
                {
                    return null;
                }
                if (require(tokenTypeList["RPAR"])==null)
                {
                    return null;
                }
                return node;
            }
            else
            {
                return parseVariableOrNumber();
            }
        }

        public ExpressionNode parseFormula()
        {
            var leftNode = parsePerentheses();
            if(leftNode == null)
            {
                return null;
            }
            var operatorr = match(tokenTypeList["MINUS"], tokenTypeList["PLUS"]);
            while(operatorr != null)
            {
                var rightNode = parsePerentheses();
                if(rightNode == null)
                {
                    return null;
                }
                leftNode = new BinOperationNode(operatorr, leftNode, rightNode);
                operatorr = match(tokenTypeList["MINUS"], tokenTypeList["PLUS"]);
            }
            return leftNode;
        }

        public ExpressionNode parseExpression(StatementsNode root)
        {
            ExpressionNode variableNode = parseVariableOrNumber();
            if(variableNode != null)
            {
                root.addNode(variableNode);
            }
            else
            {
                errorLine.Text = "На позиции " + this.pos + " ожидалась переменная";
                return null;
            }
            if (this.require(tokenTypeList["CONSTANT"]) == null)
            {
                errorLine.Text = "На позиции " + this.pos + " ожидалась constant";
                return null;
            }
            else
            {
                root.addNode(new ConstantNode());
            }

            ExpressionNode dataType = parseDataType();
            var assignOperator = match(tokenTypeList["ASSIGMENT"]);
            if(assignOperator != null)
            {
                var rightFormulaNode = this.parseFormula();
                if( rightFormulaNode == null)
                {
                    return null;
                }
                var binaryNode = new BinOperationNode(assignOperator, variableNode, rightFormulaNode);
                root.addNode(dataType);
                return binaryNode;
            }
            return dataType;
        }

        public ExpressionNode parseCode()
        {
            StatementsNode root = new StatementsNode();
            while (this.pos < this.tokens.Count)
            {
                var codeStringNode = this.parseExpression(root);
                if (codeStringNode == null)
                {
                    break;
                }
                if (this.require(this.tokenTypeList["SEMICOLON"]) == null)
                {
                    break;
                }
                root.addNode(codeStringNode);
            }
            return root;
        }
    }
}
