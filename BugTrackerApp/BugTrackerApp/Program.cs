using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Print.ShowWelcomeMsg();

            while (true)
            {
                Print.ShowMenu();
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Print.ShowExitMsg();
                        return;

                    case "1":
                        CreateProject();
                        break;

                    case "2":
                        CreateIssue();
                        break;

                    case "3":
                        ShowProjects();
                        break;

                    case "4":
                        ShowIssues();
                        break;

                    case "5":
                        LeaveComment();
                        break;

                    default:
                        Print.ShowIncorrectInputMsg();
                        break;
                }
            }
        }

        static void CreateProject()
        {
            Console.WriteLine("\nCreating a new project...");
            Console.WriteLine("-------------------------\n");
            Console.Write("Project's Name: ");
            var projectName = Console.ReadLine();

            Console.Write("Project's description: ");
            var projectDescription = Console.ReadLine();

            if (projectName != "" && projectDescription != "")
            {
                var proj = Dashboard.CreateProject(projectName, projectDescription);
                Console.WriteLine("\nProject was successfully created\n");
            }
            else
            {
                Console.WriteLine("\nCannot create a new project with no name or description\n");
            }
        }

        static void CreateIssue()
        {
            Console.WriteLine("\nCreating a new issue...");
            Console.WriteLine("-------------------------");
            var projectsExist = ShowProjects();
            if (projectsExist)
            {
                Console.Write("Specify project id to create an issue: ");
                var projId = Convert.ToInt32(Console.ReadLine());
                var proj = Dashboard.GetProjectById(projId);

                if (proj != null)
                {
                    Console.Write("\nName of the issue: ");
                    var issueName = Console.ReadLine();

                    Console.Write("Description of the issue: ");
                    var issueDescription = Console.ReadLine();

                    Console.Write("Email address of assignee: ");
                    var assignee = Console.ReadLine();

                    var label = Enum.GetNames(typeof(IssueLabel));

                    var issueLabel = PrintEnum(label);

                    Issue issue = Dashboard.CreateIssue(projId, issueName, issueDescription, assignee, (IssueLabel)(issueLabel - 1));
                    Console.WriteLine("Issue was successfully created.\n");
                }
                else
                {
                    Print.AskForChoosingCorrectProject();
                }
            }
        }

        static int PrintEnum(string[] label)
        {
            bool wrongLabel = true;
            while (wrongLabel)
            {
                Print.AskForCorrectInput();

                for (var i = 0; i < label.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {label[i]}");
                }

                Console.Write("\nLabel: ");
                var issueStatusInput = Console.ReadLine();
                Console.WriteLine("-----------------------------");

                var isNumeric = int.TryParse(issueStatusInput, out var issueLabel);

                if (isNumeric && issueLabel <= label.Length && issueLabel > 0)
                {
                    return issueLabel;
                }

                else
                {
                    Console.WriteLine("---------------------------");
                    Print.ShowIncorrectInputMsg();
                    return 1;
                }
            }
            return 1;
        }

        static bool ShowProjects()
        {
            Console.WriteLine("\n### List of all projects ###\n");
            var projects = Dashboard.ShowAllProjects();
            if (projects.Count == 0)
            {
                Console.WriteLine("There are no projects yet.");
                Console.WriteLine("-------------------------");
                return false;
            }
            else
            {
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Project Id   | Project Name   | Project Description     ");
                Console.WriteLine("--------------------------------------------------------");
                foreach (var proj in projects)
                {
                    Console.WriteLine($"{proj.ProjectId}            | {proj.ProjectTitle}      | {proj.ProjectDescription}");
                }
                Console.WriteLine("--------------------------------------------------------\n");
                return true;
            }
        }

        static bool ShowIssues()
        {
            var projectsExist = ShowProjects();

            if (projectsExist)
            {
                var projectId = Print.AskForProjectId();

                var issues = Dashboard.ShowAllIssues(projectId);
                var IssuesExsist = issues.Count() >= 1;

                if (IssuesExsist)
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Issue Id   | Issue Name          | Issue Assignee   | Issue Status   | Issue Label   | Issue Description   | Issue Date    ");
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------");

                    foreach (var oneissue in issues)
                    {
                        Console.WriteLine($"{oneissue.IssueId}          | {oneissue.IssueTitle}    | {oneissue.IssueAssignee}      | {oneissue.IssueStatus}       | {oneissue.IssueLabel}          | {oneissue.IssueDescription}             | {oneissue.IssueDate}    ");
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");

                        var comments = Dashboard.GetCommentsByIssueId(oneissue.IssueId);

                        if (comments.Count() > 0)
                        {
                            ShowComments(comments);
                        }
                        else {
                            Console.WriteLine("There are no comments yet");
                        }
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------\n");
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("\nThis project doesn't have issues yet or there is no project with provided id.");
                    Console.WriteLine("------------------------------------------------------------------------------\n");
                    return false;
                }
            }
            return false;
        }

        static void ShowComments(List<Comment> comments)
        {
            Console.WriteLine("# Issue Comments: ");
            foreach (var cmnt in comments)
            {
                Console.WriteLine($"Comment Id: {cmnt.CommentId}     | Comment body: {cmnt.CommentBody}     | Comment Date: {cmnt.CommentTime}");
            }
        }

        static void LeaveComment()
        {
            var IssueExsist = ShowIssues();
            if (IssueExsist)
            {
                Console.WriteLine("For which issue whould you like to leave a comment?");
                Console.Write("Issue ID: ");
                var issueId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Write comment: ");
                var comment = Console.ReadLine();
                Dashboard.LeaveCommentForIssue(issueId, comment);
                Console.WriteLine("Comment was successfully added");
            }
        }
    }
}
