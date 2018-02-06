using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerApp
{
    public static class Dashboard
    {
        private static BugTrackerModel db = new BugTrackerModel();

        public static Project CreateProject(string projectTitle,
                                     string projectDescription)
        {
            Project project = new Project
            {
                ProjectTitle = projectTitle,
                ProjectDescription = projectDescription,
                Deleted = false
            };
            db.Projects.Add(project);
            db.SaveChanges();
            return project;
        }

        public static Project EditProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("account", "Invalid Account");
            }
            Project oldproject = GetProjectById(project.ProjectId);

            oldproject.ProjectTitle = project.ProjectTitle;
            oldproject.ProjectDescription = project.ProjectDescription;

            db.SaveChanges();

            return project;
        }

        public static Project ArchiveProject(int id)
        {
            Project currentProject = GetProjectById(id);

            if (currentProject == null)
            {
                throw new ArgumentNullException("account", "Invalid Account");
            }

            currentProject.Deleted = true;

            db.SaveChanges();

            return currentProject;
        }

        public static Issue CreateIssue(int projectId,
                                     string issueTitle,
                                     string issueDescription,
                                     string issueAssignee,
                                     IssueLabel issueLabel = IssueLabel.Task,
                                     IssueStatus issueStatus = IssueStatus.ToDo
                                     )
        {
            Issue issue = new Issue
            {
                ProjectId = projectId,
                IssueTitle = issueTitle,
                IssueDescription = issueDescription,
                IssueStatus = issueStatus,
                IssueLabel = issueLabel,
                IssueAssignee = issueAssignee,
            };
            db.Issues.Add(issue);
            db.SaveChanges();
            return issue;
        }

        public static void LeaveCommentForIssue(int issueId, string commentBody, string email)
        {
            var comment = new Comment
            {
                IssueId = issueId,
                CommentBody = commentBody,
                Email = email
            };
            db.Comments.Add(comment);
            db.SaveChanges();
        }

        public static List<Comment> GetCommentsByIssueId(int issueId)
        {
            return db.Comments.Where(c => c.IssueId == issueId && !c.Deleted ).OrderByDescending(c => c.CommentTime).ToList();
        }

        public static List<Project> ShowAllProjects()
        {
            return db.Projects.Where(proj => !proj.Deleted).ToList();
        }

        public static Project GetProjectById(int projectId)
        {
            return db.Projects.Where(proj => proj.ProjectId == projectId && proj.Deleted != true).FirstOrDefault();
        }
      
        public static List<Issue> ShowAllIssues(int projectId)
        {
            return db.Issues.Where(issue => issue.ProjectId == projectId && !issue.Deleted ).OrderByDescending(issue=>issue.IssueDate).ToList();
        }

        public static List<Issue> GetIssuesByAssignee(string email)
        {
            return db.Issues.Where(issue => issue.IssueAssignee == email && !issue.Deleted).ToList();
        }
    }
}
