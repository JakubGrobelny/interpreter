using System;
using System.Collections.Generic;
using Interpreter.Expressions;
using Void = Interpreter.Expressions.Void;

namespace Interpreter
{
    public static class Interpreter
    {
        private static Random randomizer = new Random();

        private static bool quit = false;

        public static void Main(string[] args)
        {
            var globalEnv = InitGlobalEnv();
            var input = "";
            List<TokenTree> list;

            while (!quit)
            {
                // TODO: make if possible to get previous input with ctrl+up

                try
                {
                    if (input == "")
                        Console.Write("|=> ");
                    else
                    {
                        Console.Write("    ");
                        input += ' ';
                    }

                    input += Console.ReadLine() + '\n';

                    if (!Lexer.Instance.IsComplete(input))
                        continue;

                    list = Lexer.Instance.Tokenize(input);
                    input = "";

                    foreach (var expr in list)
                    {
                        var expression = Parser.Instance.ParseExpression(expr);
                        var value = expression.Evaluate(globalEnv);
                        Printer.Instance.Print(value);
                    }
                }
                catch (InternalException exc)
                {
                    Console.WriteLine("Error!: " + exc.Message);
                    input = "";
                }
                catch (Exception exc)
                {
                    Console.WriteLine("CRITICAL ERROR!\n" +
                                      "If this has happened then you should " +
                                      "report the bug.\n" + exc);
                }

            }
        }

        // Default built-in values in environment.
        private static Dictionary<Symbol, Expression> InitGlobalEnv()
        {
            var env = new Dictionary<Symbol, Expression>();

            // TODO: symbol->string, open-file

            // TODO: change the evaluation rules for InternalClosures (they kinda use lazy evaluation so I need to cast arguments every time)

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

            // Compares two numbers (use eq? for everything else)
            env[new Symbol("=")] = new InternalClosure("=",
                (arguments, environment) =>
                {
                    if (arguments.Count != 2)
                        throw new ArityMismatch("=", 2, arguments.Count);

                    var num1expr = arguments[0].Evaluate(environment);
                    var num2expr = arguments[1].Evaluate(environment);

                    if (!(num1expr is Number num1))
                        throw new InvalidArgument("=", arguments[0].ToString());
                    if (!(num2expr is Number num2))
                        throw new InvalidArgument("=", arguments[1].ToString());

                    return new Bool(num1 == num2);
                });

            env[new Symbol("print")] = new InternalClosure("print",
                (arguments, environment) =>
                {
                    if (arguments.Count != 1)
                        throw new ArityMismatch("print", 1, arguments.Count);

                    if (!(arguments[0] is StringLiteral str))
                        throw new InvalidArgument("print", arguments[0].ToString());

                    Console.Write(str.Value);
                    return Void.Instance;
                });

            env[new Symbol("expt")] = new InternalClosure("expt",
                (arguments, environment) =>
                {
                    if (arguments.Count != 2)
                        throw new ArityMismatch("expt", 2, arguments.Count);

                    var num1expr = arguments[0].Evaluate(environment);
                    var num2expr = arguments[1].Evaluate(environment);

                    if (!(num1expr is Number num1))
                        throw new InvalidArgument("=", arguments[0].ToString());
                    if (!(num2expr is Number num2))
                        throw new InvalidArgument("=", arguments[1].ToString());

                    return new Real(Math.Pow((double) num1, (double) num2));
                });

            env[new Symbol("clear")] = new InternalClosure("clear",
                (arguments, environment) =>
                {
                    if (arguments.Count != 0)
                        throw new ArityMismatch("clear", 0, arguments.Count);
                    Console.Clear();
                    return Void.Instance;
                });

            env[new Symbol("list")] = new InternalClosure("list",
                (arguments, environment) =>
                {
                    var evaluatedList = new List<Expression>();
                    foreach (var arg in arguments)
                        evaluatedList.Add(arg.Evaluate(environment));

                    return Pair.CreateList(evaluatedList);
                });

            env[new Symbol("cdr")] = new InternalClosure("cdr",
                (arguments, environment) =>
                {
                    if (arguments.Count != 1)
                        throw new ArityMismatch("cdr", 1, arguments.Count);

                    var arg = arguments[0].Evaluate(environment);

                    if (!(arg is Pair pair))
                        throw new InvalidArgument("cdr", arg.ToString());

                    return pair.Second;
                });

            env[new Symbol("car")] = new InternalClosure("car",
                (arguments, environment) =>
                {
                    if (arguments.Count != 1)
                        throw new ArityMismatch("car", 1, arguments.Count);

                    var arg = arguments[0].Evaluate(environment);

                    if (!(arg is Pair pair))
                        throw new InvalidArgument("car", arg.ToString());

                    return pair.First;
                });

            env[new Symbol("null?")] = new InternalClosure("null?",
                (arguments, environment) =>
                {
                    if (arguments.Count != 1)
                        throw new ArityMismatch("null?", 1, arguments.Count);

                    return new Bool(arguments[0].Evaluate(environment) is Null);
                });

            env[new Symbol("pair?")] = new InternalClosure("pair?",
                (arguments, environment) =>
                {
                    if (arguments.Count != 1)
                        throw new ArityMismatch("pair?", 1, arguments.Count);

                    return new Bool(arguments[0].Evaluate(environment) is Pair);
                });

            env[new Symbol("list?")] = new InternalClosure("list?",
                (arguments, environment) =>
                {
                    if (arguments.Count != 1)
                        throw new ArityMismatch("list?", 1, arguments.Count);

                    return new Bool(Pair.IsList(arguments[0].Evaluate(environment)));
                });

            env[new Symbol("cons")] = new InternalClosure("cons",
                (arguments, environment) =>
                {
                    if (arguments.Count != 2)
                        throw new ArityMismatch("cons", 2, arguments.Count);

                    var car = arguments[0].Evaluate(environment);
                    var cdr = arguments[1].Evaluate(environment);

                    return new Pair(car, cdr);
                });

            env[new Symbol(">")] = new InternalClosure(">",
                (arguments, environment) =>
                {
                    if (arguments.Count != 2)
                        throw new ArityMismatch(">", 2, arguments.Count);

                    var num1expr = arguments[0].Evaluate(environment);
                    var num2expr = arguments[1].Evaluate(environment);

                    if (!(num1expr is Number num1))
                        throw new InvalidArgument("=",num1expr.ToString());
                    if (!(num2expr is Number num2))
                        throw new InvalidArgument("=", num2expr.ToString());

                    return new Bool((double) num1 > (double) num2);
                });

            env[new Symbol("random")] = new InternalClosure("random",
                (arguments, environment) =>
                {
                    if (arguments.Count != 2)
                        throw new ArityMismatch("random", 2, arguments.Count);

                    var arg1 = arguments[0].Evaluate(environment);
                    var arg2 = arguments[1].Evaluate(environment);

                    if (!(arg1 is Number start))
                        throw new InvalidArgument("random", arg1.ToString());
                    if (!(arg2 is Number end))
                        throw new InvalidArgument("random", arg2.ToString());

                    try
                    {
                        return new Rational(randomizer.Next((int) (double) start, (int) (double) end));
                    }
                    catch (ArgumentOutOfRangeException exc)
                    {
                        throw new InvalidArgument("random", arg2.ToString());
                    }
                });

            env[new Symbol("string->list")] = new InternalClosure("string->list",
                (arguments, environment) =>
                {
                    if (arguments.Count != 1)
                        throw new ArityMismatch("string->list", 1, arguments.Count);

                    var str = (StringLiteral)arguments[0].Evaluate(environment);
                    if (str == null)
                        throw new InvalidArgument("string->list", arguments[0].Evaluate(environment).ToString());

                    var list = new List<Expression>();

                    foreach (var c in str.Value)
                        list.Add(new StringLiteral(c));

                    return Pair.CreateList(list);
                });

            env[new Symbol("string->symbol")] = new InternalClosure("string->symbol",
                (arguments, environment) =>
                {
                    if (arguments.Count != 1)
                        throw new ArityMismatch("string->symbol", 1, arguments.Count);

                    var str = (StringLiteral)arguments[0].Evaluate(environment);
                    if (str == null)
                        throw new InvalidArgument("string->symbol", arguments[0].Evaluate(environment).ToString());

                    return new Symbol(str.Value);
                });

            env[new Symbol("val->string")] = new InternalClosure("val->string",
                (arguments, environment) =>
                {
                    if (arguments.Count != 1)
                        throw new ArityMismatch("val->string", 1, arguments.Count);

                    return new StringLiteral("\"" + arguments[0].Evaluate(environment) + "\"");
                });

            env[new Symbol("string->val")] = new InternalClosure("string->val",
                (arguments, environment) =>
                {
                    if (arguments.Count != 1)
                        throw new ArityMismatch("string->val", 1, arguments.Count);

                    if (!(arguments[0].Evaluate(environment) is StringLiteral str))
                        throw new InvalidArgument("string->val", arguments[0].Evaluate(environment).ToString());

                    //TODO: special rules for lists
                    return ExpressionFactory.ParseValue(str.Value);
                });

            env[new Symbol("list->string")] = new InternalClosure("list->string",
                (arguments, environment) =>
                {
                    if (arguments.Count != 1)
                        throw new ArityMismatch("list->string", 1, arguments.Count);

                    Expression list = arguments[0].Evaluate(environment);

                    if (!Pair.IsList(list))
                        throw new InvalidArgument("list->string", list.ToString());

                    if (list is Null)
                        return new StringLiteral("\"\"");

                    list = (Pair) list;
                    var finalString = "";

                    var castedList = Pair.CastToList(list);

                    foreach (var expr in castedList)
                    {
                        if (!(expr.Evaluate(environment) is StringLiteral str))
                            throw new InvalidArgument("list->string", expr.ToString());

                        finalString += str.Value;
                    }

                    return new StringLiteral("\"" + finalString + "\"");
                });

            return env;
        }
    }
}
