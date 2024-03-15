using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.src
{
    internal class Lexer
    {
        public string code;
        public int pos;
        private List<Token> tokenList = new List<Token>();
        private Dictionary<string, TokenType> tokenTypesValues = new tokensList().tokenTypeList;
        Label errorLine;


        public Lexer(string code, Label errorLine)
        {
            this.code = code;
            this.errorLine = errorLine;
        }

        public List<Token> lexerAnalysis()
        {
            while (nextToken()) { }
            return tokenList;
        }

        bool nextToken()
        {
            if(pos >= code.Length)
                return false;

            foreach (var value in tokenTypesValues)
            {
                var tokenTypeKey = value.Key;
                var tokenTypeValue = value.Value;
                var regex = new Regex('^' + tokenTypeValue.regexp);
                var result = code.Substring(pos).Trim().Split(' ')[0];
                
                if (result != null)
                {
                    if (regex.IsMatch(result))
                    {
                        Token token = new Token(tokenTypeValue, result, pos);
                        Debug.WriteLine(result); 
                        Debug.WriteLine("Совпало");
                        pos += result.Length+1;
                        tokenList.Add(token);
                        errorLine.Text = "";
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine(result);
                        errorLine.Text="На позиции "+pos+" обнаружена ошибка";
                    }
                }
            }
            return false;
        }

    }
}
