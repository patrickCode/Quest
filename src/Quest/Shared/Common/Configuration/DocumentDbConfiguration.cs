using Common.ConfigurationResolvers;

namespace Common.Configuration
{
    public class DocumentDbConfiguration: BaseConfiguration
    {
        public DocumentDbConfiguration() { }
        public DocumentDbConfiguration(IConfigurationResolver<DocumentDbConfiguration> resolver)
        {
            var config = resolver.Resolve();
            Endpoint = config.Endpoint;
            PrimaryKey = config.PrimaryKey;
            Database = config.Database;
            QuestionCollection = config.QuestionCollection;
        }
        public string Endpoint { get; set; }
        public string PrimaryKey { get; set; }
        public string Database { get; set; }
        public string QuestionCollection { get; set; }
        public string CategoriesCollection { get; set; }
    }
}