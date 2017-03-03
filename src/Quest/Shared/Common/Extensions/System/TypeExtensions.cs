using System.Linq;
using System.Collections.Generic;

namespace System.Reflection
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetBaseClasesAndInterfaces(this Type type)
        {
            if (type.GetTypeInfo().BaseType == null)
                return type.GetInterfaces();

            return Enumerable.Repeat(type.GetTypeInfo().BaseType, 1)
                .Concat(type.GetInterfaces())
                .Concat(type.GetInterfaces().SelectMany(GetBaseClasesAndInterfaces))
                .Concat(type.GetTypeInfo().BaseType.GetBaseClasesAndInterfaces());
        }

        public static Type GetSubclass(this Type type, Type toCheck)
        {
            var cur = type;
            while (cur != null && cur != typeof(object))
            {
                if (cur.IsConstructedGenericType && cur == toCheck)
                {   
                        return cur;
                }
                cur = cur.GetTypeInfo().BaseType;
                //if (toCheck.IsConstructedGenericType)
                //{
                //    if (toCheck == cur)
                //    {
                //        return cur;
                //    }
                //}
                //else
                //{
                //    toCheck = toCheck.GetTypeInfo().BaseType;
                //}
                //var cur = toCheck.IsConstructedGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                //if (toCheck == cur)
                //{
                //    return cur;
                //}
                //toCheck = type.GetTypeInfo().BaseType;
            }
            return null;
        }
    }
}