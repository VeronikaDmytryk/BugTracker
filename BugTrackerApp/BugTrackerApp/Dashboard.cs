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

        public static Issue EditIssue(Issue issue)
        {
            if (issue == null)
            {
                throw new ArgumentNullException("account", "Invalid Account");
            }
            Issue oldIssue = GetIssueById(issue.IssueId);

            oldIssue.IssueTitle = issue.IssueTitle;
            oldIssue.IssueDescription = issue.IssueDescription;
            oldIssue.IssueStatus = issue.IssueStatus;
            oldIssue.IssueLabel = issue.IssueLabel;
            oldIssue.IssueAssignee = issue.IssueAssignee;

            db.SaveChanges();

            return issue;
        }

        public static Comment EditComment(Comment comment, string email)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment", "Invalid Comment");
            }
            Comment oldComment = GetCommentById(comment.CommentId);

            if (oldComment.Email != email)
            {
                throw new ArgumentException("comment", "You don't have permissions to edit the comment");
            }
            
            oldComment.CommentBody = comment.CommentBody;

            db.SaveChanges();

            return comment;
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

        public static Issue ArchiveIssue(int id)
        {
            Issue currentIssue = GetIssueById(id);

            if (currentIssue == null)
            {
                throw new ArgumentNullException("issue", "Invalid Issue");
            }

            currentIssue.Deleted = true;

            db.SaveChanges();

            return currentIssue;
        }

        public static Comment ArchiveComment(int id, string email)
        {
            Comment currentComment = GetCommentById(id);

            if (currentComment == null)
            {
                throw new ArgumentNullException("comment", "Invalid Comment");
            }
            if (currentComment.Email != email)
            {
                throw new ArgumentException("comment", "You don't have permissions to delete the comment");
            }
            else
            {
                currentComment.Deleted = true;
                db.SaveChanges();
                return currentComment;
            }
           
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
                Deleted = false
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

        public static Comment GetCommentById(int id)
        {
           return db.Comments.Where(comment => comment.CommentId == id && !comment.Deleted).FirstOrDefault();
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

        public static Issue GetIssueById(int id)
        {
            return db.Issues.Where(issue => issue.IssueId == id && !issue.Deleted).FirstOrDefault();
        }
    }
}
