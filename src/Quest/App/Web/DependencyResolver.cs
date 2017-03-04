﻿using Autofac;
using Services;
using Common.Model;
using Autofac.Core;
using Common.Domain;
using Common.Services;
using Azure.DocumentDb;
using Common.Interfaces;
using Common.Configuration;
using System.Collections.Generic;
using Common.ConfigurationResolvers;
using Microsoft.Extensions.Configuration;
using Services.QuestionServices.Commands;
using Services.QuestionServices.Processors;
using Services.QuestionServices.QueryServices;
using Common.ConfigurationResolvers.ApplicationResolvers;

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
            RegisterQueryService(builder);

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

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(IEnumerable<CommandProcessor>) && pi.Name == "processors",
                        (pi, ctx) => ctx.Resolve<IEnumerable<CommandProcessor>>()))
                .SingleInstance();
        }

        private void RegisterQueryService(ContainerBuilder builder)
        {
            builder.RegisterType<QuestionsQueryService>()
                .As<IQuestionsQueryService>()
                .As<IQueryService<QuestionDto>>();
        }
    }
}