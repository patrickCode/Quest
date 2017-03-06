using System;

namespace Azure.DocumentDb.Utility
{
    public interface ICollectionNameResolver
    {
        string Resolve(Type documentType);
    }
}