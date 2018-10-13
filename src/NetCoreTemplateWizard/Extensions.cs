using EnvDTE;
using EnvDTE100;
using EnvDTE80;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace NetCoreTemplateWizard
{
    public static class Extensions
    {
        internal const string VsProjectItemKindSolutionFolder = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";

        public static Project GetProject(this Solution4 solution, string projectName) =>
            solution.GetAllProjects().FirstOrDefault(p => p.Name.Equals(projectName));

        private static IEnumerable<Project> GetAllProjects(this Solution4 solution)
        {
            return solution.Projects
                .Cast<Project>()
                .SelectMany(GetChildProjects)
                .Union(solution.Projects.Cast<Project>())
                .Where(p => !string.IsNullOrEmpty(p.FullName ?? string.Empty));
        }

        private static IEnumerable<Project> GetChildProjects(Project parent)
        {
            try
            {
                if (parent.Kind != VsProjectItemKindSolutionFolder && parent.Collection == null)
                    return Enumerable.Empty<Project>();

                if (!string.IsNullOrEmpty(parent.FullName))
                    return new[] { parent };
            }
            catch (COMException)
            {
                return Enumerable.Empty<Project>();
            }

            return parent.ProjectItems
                .Cast<ProjectItem>()
                .Where(p => p.SubProject != null)
                .SelectMany(p => GetChildProjects(p.SubProject));
        }

        public static Project AddProject(this Solution4 solution, string destination, string projectName, string templateName)
        {
            var projectPath = Path.Combine(destination, projectName);
            var templatePath = solution.GetProjectTemplate(templateName, "CSharp");

            solution.AddFromTemplate(templatePath, projectPath, projectName, false);

            return solution.GetProject(projectName);
        }

        public static Project AddProject(this SolutionFolder solutionFolder, string destination, string projectName, string templateName)
        {
            var projectPath = Path.Combine(destination, projectName);
            var templatePath = ((Solution4)solutionFolder.DTE.Solution).GetProjectTemplate(templateName, "CSharp");

            solutionFolder.AddFromTemplate(templatePath, projectPath, projectName);

            return ((Solution4)solutionFolder.DTE.Solution).GetProject(projectName);
        }

        public static SolutionFolder AddSolutionFolderEx(this Solution4 solution, string folderName)
        {
            var folder = solution.GetSolutionFolderEx(folderName) ??
                ((Solution4)solution).AddSolutionFolder(folderName).Object;

            return folder;
        }

        public static SolutionFolder GetSolutionFolderEx(this Solution4 solution, string folderName)
        {
            var solutionFolder = ((Solution2)solution)
                .Projects
                .OfType<Project>()
                .FirstOrDefault(p => p.Name == folderName);

            return solutionFolder?.Object;
        }
    }
}
