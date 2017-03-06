using Common.Configuration;
using Microsoft.Extensions.Configuration;

namespace Common.ConfigurationResolvers.ApplicationResolvers
{
    public class DocumentDbAppConfigurationResolver : IConfigurationResolver<DocumentDbConfiguration>
    {
        private readonly IConfigurationSection _configurationSection;
        public DocumentDbAppConfigurationResolver(IConfigurationSection conifigurationSection)
        {
            _configurationSection = conifigurationSection;
        }
        public DocumentDbConfiguration Resolve()
        {
            return new DocumentDbConfiguration()
            {
                Endpoint = _configurationSection["Endpoint"],
                PrimaryKey = _configurationSection["PrimaryKey"],
                Database = _configurationSection["Database"],
                QuestionCollection = _configurationSection["QuestionsCollection"],
                CategoriesCollection = _configurationSection["CategoriesCollection"],
            };
        }
    }
}