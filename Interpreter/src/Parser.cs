using System;
using System.Runtime.CompilerServices;
using Interpreter.Expressions;
using System.Collections.Generic;

namespace Interpreter
{
    public class Parser
    {
        private static Parser instance = null;
        
        public static Parser Instance
        {
            get
            {
                if (instance == null)
                    instance = new Parser();
                return instance;
            }
        }

        public Expression ParseExpression(TokenTree expression)
        {
            if (expression is Token)
                return ExpressionFactory.ParseValue(expression.ToString());
            else
            {
                var list = (expression as TokenNode).children;

                if (list.Count == 0)
                    throw new InvalidEmptyExpression();
                // special form
                if (list[0] is Token keyword)
                {
                    switch (keyword.token)
                    {
                        case "'":
                        {    
                            if (list.Count != 2)
                                throw new SpecialFormArityMismatch(keyword.token, 
                                                                   2, 
                                                                   list.Count);
                            if (list[1] is Token value)
                                return ExpressionFactory.ParseValue(list[1].ToString());
                            else if (list[1] is TokenTree ls)
                            {
                                //var tempList = new List<Expression>();
                                // TODO:
                                throw new NotImplementedException("list quoting"); 
                            }
                            
                            //TODO: quoting (preferably create a list)
                            break;
                        }
                        case "def":
                        {
                            if (list.Count != 3)
                                throw new SpecialFormArityMismatch(keyword.token,
                                                                   3,
                                                                   list.Count);

                            if (!(list[1] is Token sym))
                                throw new InvalidUseOfSpecialForm(keyword.token, 
                                                                  expression.ToString());

                            var symbol = ExpressionFactory.ParseValue(sym.token);
                            
                            if (!(symbol is Symbol s))
                                throw new InvalidUseOfSpecialForm(keyword.token,
                                                                  expression.ToString());
                            
                            var value = ParseExpression(list[2]);
                            
                            return new Definition(s, value);
                        }
                    }
                }
                // another combination
                else
                {
                    
                }
            }

            return null;
        }
    }
}