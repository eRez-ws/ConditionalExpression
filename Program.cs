using System;
using System.Linq.Expressions;
using Nancy;
using Nancy.Cryptography;
using Nancy.Security;

namespace ConditionalExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            CsrfApplicationStartup UpdateConditional(System.Linq.Expressions.ConditionalExpression c, Expression test, Expression ifTrue, Expression ifFalse)
            {
                if (test != c.Test || ifTrue != c.IfTrue || ifFalse != c.IfFalse)
                {
                    Console.WriteLine("calling vul element!");
                    return new CsrfApplicationStartup(CryptographyConfiguration.NoEncryption, new DefaultObjectSerializer(), new DefaultCsrfTokenValidator(CryptographyConfiguration.NoEncryption));
                }
                Console.WriteLine("this should not invoke vulnerable element");
                return new CsrfApplicationStartup(CryptographyConfiguration.NoEncryption, new DefaultObjectSerializer(), new DefaultCsrfTokenValidator(CryptographyConfiguration.NoEncryption));
            }

            var num = 100;
            Expression conditionExpr = Expression.Condition(
                Expression.Constant(num > 10),
                Expression.Constant("num is greater than 10"),
                Expression.Constant("num is smaller than 10")
            );
            CsrfApplicationStartup csrfApplicationStartup = UpdateConditional
                (System.Linq.Expressions.ConditionalExpression.Condition
                    (Expression.Constant(num > 10),
                    Expression.Constant("num is greater than 10"),
                    Expression.Constant("num is smaller than 10")),conditionExpr,conditionExpr,conditionExpr);
        }
    }
}