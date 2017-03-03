using Common.Configuration;

namespace Common.ConfigurationResolvers
{
    public interface IConfigurationResolver<T> where T : BaseConfiguration
    {
        T Resolve();
    }
}