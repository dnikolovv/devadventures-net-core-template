using EnvDTE;
using EnvDTE100;
using EnvDTE80;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NetCoreTemplateWizard
{
    public class Wizard : IWizard
    {
        private string _destinationDirectory;
        private string _safeProjectName;
        private Solution4 _solution;

        // This method is called before opening any item that
        // has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        // This method is only called for item templates
        // not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        // This method is called after the project is created
        public void RunFinished()
        {
            var currentDirectory = _destinationDirectory;
            var generatedProjectPaths = Directory.GetDirectories(currentDirectory);

            var srcFolderPath = Path.Combine(Directory.GetParent(currentDirectory).FullName, "src");
            var srcServerFolderPath = Path.Combine(srcFolderPath, "server");
            var testsFolderPath = Path.Combine(Directory.GetParent(currentDirectory).FullName, "tests");

            CreateDirectories(srcFolderPath, srcServerFolderPath, testsFolderPath);

            // We will be moving the test projects into the ./tests folder and the rest into the ./src/server folder.
            MoveProjectsToCorrespondingFolders(generatedProjectPaths, srcServerFolderPath, testsFolderPath);

            AddConfiguration(srcFolderPath);

            DeleteUnneededFolders(currentDirectory);
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            _safeProjectName = replacementsDictionary["$safeprojectname$"];
            _destinationDirectory = replacementsDictionary["$destinationdirectory$"];
            _solution = ((automationObject as DTE2).Solution as Solution4);
        }

        // This method is only called for item templates,
        // not for project templates.
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        private static void AddConfiguration(string srcFolderPath)
        {
            var configurationFolder = Path.Combine(srcFolderPath, "configuration");

            var assetsRootPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets");
            var styleCopJson = File.ReadAllLines(Path.Combine(assetsRootPath, "stylecop.json"));
            var rulesetFile = File.ReadAllLines(Path.Combine(assetsRootPath, "analyzers.ruleset"));

            Directory.CreateDirectory(configurationFolder);
            File.WriteAllText(Path.Combine(configurationFolder, "stylecop.json"), string.Join(Environment.NewLine, styleCopJson));
            File.WriteAllText(Path.Combine(configurationFolder, "analyzers.ruleset"), string.Join(Environment.NewLine, rulesetFile));
        }

        private static void CreateDirectories(params string[] paths)
        {
            foreach (var path in paths)
            {
                Directory.CreateDirectory(path);
            }
        }

        private void DeleteUnneededFolders(string directory)
        {
            var oldFolderPath = Path.Combine(Directory.GetParent(directory).FullName, _safeProjectName);
            Directory.Delete(oldFolderPath, true);
        }

        private void MoveProjectsToCorrespondingFolders(IEnumerable<string> projectPaths, string sourceFolderPath, string testsFolderPath)
        {
            var srcSolutionFolder = _solution.AddSolutionFolderEx("src");
            var testsSolutionFolder = _solution.AddSolutionFolderEx("tests");

            foreach (var projectPath in projectPaths)
            {
                var projectFolderName = projectPath.Split('\\').Last().Trim();

                var isTestProject = projectPath.EndsWith("tests", StringComparison.OrdinalIgnoreCase);

                var physicalDestinationFolder = isTestProject ?
                    Path.Combine(testsFolderPath, projectFolderName) :
                    Path.Combine(sourceFolderPath, projectFolderName);

                var solutionDestinationFolder = isTestProject ?
                    testsSolutionFolder :
                    srcSolutionFolder;

                Directory.Move(projectPath, physicalDestinationFolder);

                var project = _solution.GetProject(projectFolderName);
                _solution.Remove(project);

                var projectCsprojPath = Path.Combine(physicalDestinationFolder, $"{projectFolderName}.csproj");
                solutionDestinationFolder.AddFromFile(projectCsprojPath);
            }
        }
    }
}