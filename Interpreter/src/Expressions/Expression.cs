using System;
using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public abstract class Expression : ICloneable
    {
        public abstract object Clone();
        
        public abstract Expression Evaluate(Dictionary<Symbol, Expression> env);
    }

    public abstract class Value : Expression
    {
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            return this;
        }
    }

    public abstract class Combination : Expression
    {        
        //TODO: probably implement cloning for real.
        public override object Clone() => this;
    }

    public abstract class SpecialForm : Combination {}
    
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
                return Rational.Add(((Rational)a), ((Rational)b));
            else
            {
                return new Real((double)a + (double)b);
            }
        }

        public static Number operator-(Number a, Number b)
        {
            if (a is Rational && b is Rational)
                return Rational.Substract(((Rational)a), ((Rational)b));
            else
            {
                return new Real((double)a - (double)b);
            }
            
        }
        
        public static Number operator*(Number a, Number b)
        {
            if (a is Rational && b is Rational)
                return Rational.Multiply(((Rational)a), ((Rational)b));
            else
            {
                return new Real((double)a * (double)b);
            }
            
        }

        public static Number operator/(Number a, Number b)
        {
            if (a is Rational && b is Rational)
                return Rational.Divide(((Rational)a), ((Rational)b));
            else
            {
                return new Real((double)a / (double)b);
            }
        }
    }
}