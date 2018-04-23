using System.Runtime.CompilerServices;
using Interpreter.Expressions;
using System.Collections.Generic;

namespace Interpreter
{
    public class Evaluator
    {
        private static Dictionary<Symbol, Expression> environment;

        private Evaluator instance = null;
        
        public Evaluator Instance
        {
            get
            {
                if (instance == null)
                    instance = new Evaluator();
                return instance;
            }
        }
        
        private Evaluator() {}
        
        public Expression EvaluateExpression(TokenTree expression)
        {
            if (expression is Token)
                return ExpressionFactory.ParseExpression(expression.ToString());
            else
            {
                TokenNode expr = expression as TokenNode;
                var list = expr.children;

                if (expr.children.Count == 0)
                    throw new InvalidEmptyExpression();

                if (list[0] == "def")
                {
                    if (list.Count != 3)
                        throw new InvalidExpression("def", expr.ToString());

                    var value = list[2].Evaluate(environment);
                    var symbol = list[1].Evaluate(environment) as Symbol;
                    
                    environment[symbol] = value;
                    
                    return Void.Instance;
                }
                if (list[0] == "if")
                {
                    if (list.Count != 4)
                        throw new InvalidExpression("if", expr.ToString());

                    // TODO: casting to bool
                    var cond = list[1].Evaluate(environment);

                    if (!(cond is Bool) || (cond as Bool).GetValue())
                        return list[2].Evaluate(environment);
                    else
                        return list[3].Evaluate(environment);
                }
                if (list[0] == "and")
                {
                    for (int i = 1; i < list.Count; i++)
                        if ((list[i].Evaluate(environment) is Bool) && 
                           !(list[i].Evaluate(environment) as Bool).GetValue())
                            return new Bool(false);                    

                    return new Bool(true);
                }
                if (list[0] == "or")
                {
                    for (int i = 1; i < list.Count; i++)
                        if (!(list[i].Evaluate(environment) is Bool) || 
                             (list[i].Evaluate(environment) as Bool).GetValue())
                            return new Bool(true);

                    return new Bool(false);
                }
            }
        }
    }
}