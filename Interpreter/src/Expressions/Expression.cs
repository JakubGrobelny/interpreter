using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public abstract class Expression
    { //TODO: implement Cloneable?
        public abstract Expression Evaluate(Dictionary<Symbol, Expression> env);
    }

    public abstract class Value : Expression
    {
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            return this;
        }
    }

    public abstract class Combination : Expression {}

    public abstract class Number : Value
    {
        protected abstract double ToDouble();
        
        public static explicit operator double(Number num)
        {
            return num.ToDouble();
        }
        
        public static Number operator+(Number a, Number b)
        {
            if (a is Rational && b is Rational)
                return Rational.Add((a as Rational), (b as Rational));
            else
            {
                return new Real((double)a + (double)b);
            }
        }

        public static Number operator-(Number a, Number b)
        {
            if (a is Rational && b is Rational)
                return Rational.Substract((a as Rational), (b as Rational));
            else
            {
                return new Real((double)a - (double)b);
            }
            
        }
        
        public static Number operator*(Number a, Number b)
        {
            if (a is Rational && b is Rational)
                return Rational.Multiply((a as Rational), (b as Rational));
            else
            {
                return new Real((double)a * (double)b);
            }
            
        }

        public static Number operator/(Number a, Number b)
        {
            if (a is Rational && b is Rational)
                return Rational.Divide((a as Rational), (b as Rational));
            else
            {
                return new Real((double)a / (double)b);
            }
        }
    }
}