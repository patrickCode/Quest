using Autofac;
using Autofac.Core;
using Azure.DocumentDb;
using Common.Configuration;
using Common.ConfigurationResolvers;
using Common.ConfigurationResolvers.ApplicationResolvers;
using Common.Domain;
using Common.Interfaces;
using Common.Services;
using Microsoft.Extensions.Configuration;
using Services;
using Services.QuestionServices.Commands;
using Services.QuestionServices.Processors;
using System.Collections.Generic;

namespace Web
{
    public class DependencyResolver: Module
    {
        private readonly IConfigurationRoot _configuration;
        public DependencyResolver(IConfigurationRoot Configuration)
            : base()
        {
            _configuration = Configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            RegisterConfigurations(builder);
            RegisterInfrastructure(builder);
            RegisterCommandProcessors(builder);

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(IEnumerable<CommandProcessor>) && pi.Name == "processors",
                        (pi, ctx) => ctx.Resolve<IEnumerable<CommandProcessor>>()))
                .SingleInstance();

            base.Load(builder);
        }

        private void RegisterConfigurations(ContainerBuilder builder)
        {
            builder.RegisterType<DocumentDbAppConfigurationResolver>()
                .AsSelf()
                .As<IConfigurationResolver<DocumentDbConfiguration>>()
                .WithParameter(
                new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IConfigurationSection) && pi.Name == "conifigurationSection",
                    (pi, ctx) => _configuration.GetSection("Azure").GetSection("DocumentDb")));
            builder.RegisterType<DocumentDbConfiguration>()
                .AsSelf();
        }

        private void RegisterInfrastructure(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(DocumentReader<>))
                .As(typeof(IDocumentReader<>))
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DocumentWriter<>))
                .As(typeof(IDocumentWriter<>))
                .InstancePerLifetimeScope();
        }

        private void RegisterCommandProcessors(ContainerBuilder builder)
        {
            builder.RegisterType<AddQuestionProcessor>()
                .As<CommandProcessor>()
                .As<CommandProcessor<AddQuestion>>();
        }
    }
}