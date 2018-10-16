﻿namespace MyCompany.MyProject.Common.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class LambdaExpressionExtensions
    {
        public static Expression<Func<T, bool>> GetBinaryLambdaExpression<T, TK>(this Expression<Func<T, TK>> selector, TK value)
        {
            var exp = selector.Body.CreateKeyComparisonExpression(Expression.Constant(value));
            var lambda = (Expression<Func<T, bool>>)Expression.Lambda(exp, false, selector.GetParameterExpression());
            return lambda;
        }

        public static string GetPropertyName(this LambdaExpression selector)
        {
            MemberExpression me;

            if (selector.Body is MemberExpression expression)
            {
                me = expression;
            }
            else
            {
                var op = ((UnaryExpression)selector.Body).Operand;
                me = (MemberExpression)op;
            }

            return ((PropertyInfo)me.Member).Name;
        }

        public static Type GetPropertyType(this LambdaExpression selector)
        {
            MemberExpression me;

            if (selector.Body is MemberExpression expression)
            {
                me = expression;
            }
            else
            {
                var op = ((UnaryExpression)selector.Body).Operand;
                me = (MemberExpression)op;
            }

            return ((PropertyInfo)me.Member).PropertyType;
        }

        private static BinaryExpression CreateKeyComparisonExpression(this Expression leftExpression, Expression rightExpression)
        {
            if (leftExpression.Type == rightExpression.Type)
            {
                return Expression.Equal(leftExpression, rightExpression);
            }

            if (leftExpression.Type.IsNullableType())
            {
                rightExpression = Expression.Convert(rightExpression, leftExpression.Type);
            }
            else
            {
                leftExpression = Expression.Convert(leftExpression, rightExpression.Type);
            }

            return Expression.Equal(leftExpression, rightExpression);
        }

        private static ParameterExpression GetParameterExpression(this LambdaExpression expression)
        {
            var memberExp = expression.Body;
            while (true)
            {
                if (memberExp is MemberExpression exp)
                {
                    switch (exp.Expression)
                    {
                        case ParameterExpression exp2:
                            return exp2;
                        case MemberExpression exp3:
                            memberExp = exp3;
                            continue;
                    }
                }

                if (memberExp is UnaryExpression exp4)
                {
                    switch (exp4.Operand)
                    {
                        case ParameterExpression exp5:
                            return exp5;
                        case MemberExpression exp6:
                            memberExp = exp6;
                            continue;
                    }
                }

                break;
            }

            throw new InvalidOperationException("No parameter expression found in provided expression");
        }
    }
}
