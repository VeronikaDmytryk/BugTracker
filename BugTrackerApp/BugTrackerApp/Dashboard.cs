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
                ProjectDescription = projectDescription
            };
            db.Projects.Add(project);
            db.SaveChanges();
            return project;
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
            return db.Comments.Where(c => c.IssueId == issueId).OrderByDescending(c => c.CommentTime).ToList();
        }

        public static List<Project> ShowAllProjects()
        {
            return db.Projects.ToList();
        }

        public static Project GetProjectById(int projectId)
        {
            return db.Projects.Where(proj => proj.ProjectId == projectId).FirstOrDefault();
        }
      
        public static List<Issue> ShowAllIssues(int projectId)
        {
            return db.Issues.Where(issue => issue.ProjectId == projectId).OrderByDescending(issue=>issue.IssueDate).ToList();
        }

        public static List<Issue> GetIssuesByAssignee(string email)
        {
            return db.Issues.Where(issue => issue.IssueAssignee == email).ToList();
        }
    }
}
