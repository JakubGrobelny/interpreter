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
                        case "set!":
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

                            return new Set(s, value);
                        }
                        case "if":
                        {
                            if (list.Count != 4)
                                throw new SpecialFormArityMismatch(keyword.token,
                                                                   4,
                                                                   list.Count);
                            var cond = ParseExpression(list[1]);
                            var ifTrue = ParseExpression(list[2]);
                            var ifFalse = ParseExpression(list[3]);

                            return new If(cond, ifTrue, ifFalse);
                        }
                        case "and":
                        {
                            var expressions = new List<Expression>();

                            for (int i = 1; i < list.Count; i++)
                                expressions.Add(ParseExpression(list[i]));

                            return new And(expressions);
                        }
                        case "or":
                        {
                            var expressions = new List<Expression>();

                            for (int i = 1; i < list.Count; i++)
                                expressions.Add(ParseExpression(list[i]));

                            return new Or(expressions);
                        }
                        case "cond":
                        {
                            var clauses = new List<List<Expression>>();
                            // (cond (p1 e1) (p2 e2) ... (pn en) (else en))
                            for (int i = 1; i < list.Count; i++)
                            {
                                if (!(list[i] is TokenNode clause) || clause.children.Count != 2)
                                    throw new InvalidUseOfSpecialForm(keyword.token,
                                                                      expression.ToString());

                                var cond = ParseExpression(clause.children[0]);
                                var expr = ParseExpression(clause.children[1]);
                                
                                clauses.Add(new List<Expression> {cond, expr});
                            }

                            return new Cond(clauses);
                        }
                        case "defclass":
                        {
                            throw new NotImplementedException();
                        }
                        case "defarray":
                        {
                            throw new NotImplementedException();                            
                        }
                        case "lambda*":
                        case "lambda":
                        {
                            if (list.Count != 3)
                                throw new SpecialFormArityMismatch(keyword.token,
                                                                   3,
                                                                   list.Count);
                            
                            if (!(list[1] is TokenNode parameters))
                                throw new InvalidUseOfSpecialForm(keyword.token,
                                                                  expression.ToString());

                            var paramList = new List<Symbol>();

                            foreach (var param in parameters.children)
                            {
                                var paramExpr = ParseExpression(param);
                                if (!(paramExpr is Symbol paramSymbol))
                                    throw new InvalidUseOfSpecialForm(keyword.token,
                                                                      expression.ToString());
                                paramList.Add(paramSymbol);
                            }

                            var expressions = new List<Expression>();
                            if (list[2] is Token singleExpression)
                                expressions.Add(ParseExpression(singleExpression));
                            else if (list[2] is TokenNode multipleExpressions)
                            {
                                foreach (var expr in multipleExpressions.children)
                                    expressions.Add(ParseExpression(expr));
                            }
                            
                            if (keyword.token == "lambda")
                                return new Lambda(paramList, expressions);
                            else
                                return new VariadicLambda(paramList, expressions);
                        }
                        //TODO: let, let*
                    }
                }
                // function application
                else
                {
                    //TODO: application
                }
            }   

            throw new InvalidExpression(expression.ToString());
        }
    }
}