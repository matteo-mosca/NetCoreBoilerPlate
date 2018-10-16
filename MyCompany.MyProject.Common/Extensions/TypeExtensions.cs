﻿namespace MyCompany.MyProject.Common.Extensions
{
    using System;
    using System.Reflection;

    public static class TypeExtensions
    {
        public static bool IsNullableType(this Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return !typeInfo.IsValueType
                   || (typeInfo.IsGenericType
                   && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
