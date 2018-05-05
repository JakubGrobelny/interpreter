using System;
using System.Collections.Generic;
using Interpreter.Expressions;
using Void = Interpreter.Expressions.Void;

namespace Interpreter
{
    public static class Interpreter
    {
        private static bool quit = false;

        public static void Main(string[] args)
        {
            var input = Console.ReadLine();

            try
            {
                var list = Lexer.Instance.Tokenize(input);
                
                var globalEnv = InitGlobalEnv();
                
                //TODO: replace this temporary loop with read-eval-write loop
                //TODO: while not quit
                foreach (var expr in list)
                {
                    var expression = Parser.Instance.ParseExpression(expr);
                    var value = expression.Evaluate(globalEnv);
                    if (!(value is Void))
                        Console.WriteLine(value.ToString());
                }
                //    Console.WriteLine(Parser.Instance.ParseExpression(expr));                
            }
            catch (InternalException exc)
            {
                Console.WriteLine("Error!: " + exc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Interpreter error!\n" +
                                  "If this has happened then you should " +
                                  "report the bug.\n" + exc);                
            }
            
            Console.ReadKey();
        }

        // Default built-in values in environment.
        private static Dictionary<Symbol, Expression> InitGlobalEnv()
        {
            var env = new Dictionary<Symbol, Expression>();

            env[new Symbol("+")] = new InternalClosure("+",
                (arguments, environment) =>
                {
                    Number sum = new Rational(0);

                    foreach (var expr in arguments)
                    {
                        var number = expr.Evaluate(environment);
                        if (!(number is Number num))
                            throw new InvalidArgument("+", number.ToString());
                        sum = sum + num;
                    }

                    return sum;
                });

            env[new Symbol("-")] = new InternalClosure("-",
            (arguments, environment) =>
                {
                    if (arguments.Count == 0)
                        throw new ArityMismatch("-", 1, 0);

                    var firstArg = arguments[0].Evaluate(environment);
                    if (!(firstArg is Number dif))
                        throw new InvalidArgument("-", firstArg.ToString());

                    for (int i = 1; i < arguments.Count; i++)
                    {
                        var number = arguments[i].Evaluate(environment);
                        if (!(number is Number num))
                            throw new InvalidArgument("-", number.ToString());
                        dif -= num;
                    }

                    return dif;
                });

            env[new Symbol("*")] = new InternalClosure("*",
                (arguments, environment) =>
                {
                    Number prod = new Rational(1);

                    foreach (var expr in arguments)
                    {
                        var number = expr.Evaluate(environment);
                        if (!(number is Number num))
                            throw new InvalidArgument("*", number.ToString());
                        prod = prod * num;
                    }

                    return prod;
                });

            env[new Symbol("/")] = new InternalClosure("/",
                (arguments, environment) =>
                {
                    if (arguments.Count == 0)
                        throw new ArityMismatch("/", 1, 0);

                    var first = arguments[0].Evaluate(environment);
                    if (!(first is Number quot))
                        throw new InvalidArgument("/", first.Evaluate(environment).ToString());

                    // (/ <arg>) === 1/<arg>
                    if (arguments.Count == 1)
                    {
                        var result = new Rational(1);
                        return result / quot;
                    }
                    // Else the result is first arg divided by rest of them

                    for (int i = 1; i < arguments.Count; i++)
                    {
                        var number = arguments[i].Evaluate(environment);
                        if (!(number is Number num))
                            throw new InvalidArgument("/", number.ToString());
                        quot = quot / num;
                    }

                    return quot;

                });

            // Applies function to the arguments from the list.
            //TODO: test it when i have lists implemented
            env[new Symbol("apply")] = new InternalClosure("apply",
                (arguments, environment) =>
                {
                    if (arguments.Count != 2)
                        throw new ArityMismatch("apply", 2, arguments.Count);
                    if (!(arguments[0].Evaluate(environment) is Function fun))
                        throw new InvalidArgument("apply", arguments[0].Evaluate(environment).ToString());

                    var args = arguments[1].Evaluate(environment);
                    if (!Pair.IsList(args))
                        throw new InvalidArgument("apply", arguments[1].Evaluate(environment).ToString());
                    var argsList = Pair.CastToList(args);

                    return fun.Call(argsList, environment);
                });

            // Used for exiting the program.
            env[new Symbol("exit")] = new InternalClosure("exit",
                (arguments, environment) =>
                {
                    quit = true;
                    return Void.Instance;
                });

            return env;
        }
    }
}
