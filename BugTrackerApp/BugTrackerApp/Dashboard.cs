using BugTrackerApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerApp
{
    public static class Dashboard
    {
        private static List<Project> Projects = new List<Project>();

        public static Project CreateProject(string projectTitle,
                                     string projectDescription)
        {
            Project project = new Project
            {
                ProjectTitle = projectTitle,
                ProjectDescription = projectDescription
            };
            Projects.Add(project);
            return project;
        }

        public static Issue CreateIssue(int projectId,
                                     string issueTitle,
                                     string issueDescription,
                                     string issueAssignee,
                                     IssueStatus issueStatus = IssueStatus.ToDo,
                                     IssueLabel issueLabel = IssueLabel.Task
                                     )
        {
            var project = Projects.Where(p => p.ProjectId == projectId).FirstOrDefault();
            Issue issue = new Issue
            {
                ProjectId = projectId,
                IssueTitle = issueTitle,
                IssueDescription = issueDescription,
                IssueStatus = issueStatus,
                IssueLabel = issueLabel,
                IssueAssignee = issueAssignee,
            };
            project.Issues.Add(issue);
            return issue;
        }

        public static List<Project> ShowAllProjects()
        {
            return Projects;
        }

        public static Project GetProjectById(int projectId)
        {
            var project = Projects.Where(proj => proj.ProjectId == projectId).FirstOrDefault();
            return project;
        }

        public static List<Issue> ShowAllIssues(int projectId)
        {
            var project = Projects.Where(p => p.ProjectId == projectId).FirstOrDefault();
            return project.Issues;
        }
    }
}
