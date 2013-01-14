﻿using Kudu.Contracts.Settings;
using System;
using System.Globalization;
using System.IO.Abstractions;

namespace Kudu.Core.Deployment.Generator
{
    public class NodeSiteBuilder : BaseBasicBuilder
    {
        public NodeSiteBuilder(IEnvironment environment, IDeploymentSettingsManager settings, IBuildPropertyProvider propertyProvider, string repositoryPath, string projectPath)
            : base(environment, settings, propertyProvider, repositoryPath, projectPath, "--node")
        {
        }

        protected override void PostDeployScript(DeploymentContext context)
        {
            base.PostDeployScript(context);
            SelectNodeVersion(context);
        }

        private void SelectNodeVersion(DeploymentContext context)
        {
            var fileSystem = new FileSystem();

            ILogger innerLogger = context.Logger.Log(Resources.Log_SelectNodeJsVersion);

            string sourcePath = String.IsNullOrEmpty(ProjectPath) ? RepositoryPath : ProjectPath;
            NodeSiteEnabler.SelectNodeVersion(fileSystem, Environment.ScriptPath, sourcePath, context.OutputPath, DeploymentSettings, context.Tracer, innerLogger);
        }
    }
}