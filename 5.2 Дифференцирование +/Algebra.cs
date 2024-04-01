using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflection.Differentiation
{
    public static class Algebra
    {
        public static Expression<Func<double, double>> Differentiate(Expression<Func<double, double>> function)
        {
            return Expression.Lambda<Func<double, double>>(DifferentiateExpression(function.Body), function.Parameters);
        }

        private static Expression DifferentiateExpression(Expression expression)
        {
            switch (expression)
            {
                case ConstantExpression _:
                    return Expression.Constant(0.0);
                case ParameterExpression _:
                    return Expression.Constant(1.0);
                case BinaryExpression binaryExpression:
                    return DifferentiateBinaryExpression(binaryExpression);
                case UnaryExpression unaryExpression when unaryExpression.NodeType == ExpressionType.Convert:
                    return DifferentiateExpression(unaryExpression.Operand);
                case UnaryExpression unaryExpression:
                    throw new ArgumentException($"Unsupported unary expression: {unaryExpression}");
                case MethodCallExpression methodCallExpression when methodCallExpression.Method.Name == "Sin":
                    if (methodCallExpression.Arguments.Count == 1)
                    {
                        var arg = methodCallExpression.Arguments[0];
                        return Expression.Multiply(Expression.Call(typeof(Math).GetMethod("Cos"), arg), DifferentiateExpression(arg));
                    }
                    throw new ArgumentException($"Unsupported function: {methodCallExpression.Method.Name}");
                case MethodCallExpression methodCallExpression when methodCallExpression.Method.Name == "Cos":
                    if (methodCallExpression.Arguments.Count == 1)
                    {
                        var arg = methodCallExpression.Arguments[0];
                        return Expression.Multiply(Expression.Negate(Expression.Call(typeof(Math).GetMethod("Sin"), arg)), DifferentiateExpression(arg));
                    }
                    throw new ArgumentException($"Unsupported function: {methodCallExpression.Method.Name}");
                case MemberExpression memberExpression when memberExpression.Member.Name == "ToString":
                    throw new ArgumentException($"Unsupported unary expression: {memberExpression.Member.Name}");
                case MemberExpression memberExpression:
                    throw new ArgumentException($"Unsupported expression type: {memberExpression.Expression}");
                default:
                    throw new ArgumentException($"Unsupported expression: {expression}");
            }
        }

        private static Expression DifferentiateBinaryExpression(BinaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Add:
                    return Expression.Add(DifferentiateExpression(expression.Left), DifferentiateExpression(expression.Right));
                case ExpressionType.Subtract:
                    return Expression.Subtract(DifferentiateExpression(expression.Left), DifferentiateExpression(expression.Right));
                case ExpressionType.Multiply:
                    return Expression.Add(
                        Expression.Multiply(expression.Left, DifferentiateExpression(expression.Right)),
                        Expression.Multiply(DifferentiateExpression(expression.Left), expression.Right)
                    );
                case ExpressionType.Divide:
                    throw new ArgumentException("Division is not supported");
                default:
                    throw new ArgumentException($"Unsupported expression: {expression}");
            }
        }
    }
}