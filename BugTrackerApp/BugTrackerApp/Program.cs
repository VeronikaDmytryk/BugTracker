using BugTrackerApp;
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
            Project project = null;
            Console.WriteLine("*******************************");
            Console.WriteLine("Welcome to Bug Tracking System");
            Console.WriteLine("*******************************");
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Create a new project");
                Console.WriteLine("2. Create a new issue");
                Console.WriteLine("3. Display all projects");
                Console.WriteLine("4. Display issues for a project");
                Console.WriteLine("--------------------------------------");
                Console.Write("Please choose one opition from above: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        Console.WriteLine(" ");
                        Console.WriteLine("Thank you for visiting. Have a nice day!");
                        Console.WriteLine("----------------------------------------");
                        return;
                    case "1":
                        Console.WriteLine(" ");
                        Console.WriteLine("Creating a new project...");
                        Console.WriteLine("-------------------------");
                        Console.Write("Project's Name: ");
                        var projectName = Console.ReadLine();
                        Console.Write("Project's description: ");
                        var projectDescription = Console.ReadLine();
                        if (projectName != "" && projectDescription != "")
                        {
                            project = Dashboard.CreateProject(projectName, projectDescription);
                            Console.WriteLine(" ");
                        }
                        else
                        {
                            Console.WriteLine(" ");
                            Console.WriteLine("Cannot create a new project with no name or description");
                            Console.WriteLine(" ");
                        }
                        break;
                    case "2":
                        Console.WriteLine(" ");
                        Console.WriteLine("Creating a new issue...");
                        Console.WriteLine(" ");
                        var projectsExist = ShowProjects();
                        if (projectsExist)
                        {
                            Console.Write("Specify project id to create an issue: ");
                            var projId = Convert.ToInt32(Console.ReadLine());
                            var proj = Dashboard.GetProjectById(projId);
                            if (proj != null)
                            {
                                Console.Write("Name of the issue: ");
                                var issueName = Console.ReadLine();
                                Console.Write("Description of the issue: ");
                                var issueDescription = Console.ReadLine();
                                Console.Write("Email address of assignee: ");
                                var assignee = Console.ReadLine();
                                var status = Enum.GetNames(typeof(IssueStatus));
                                bool wrongStatus = true;

                                while (wrongStatus)
                                {
                                    Console.WriteLine("-----------------------------");
                                    Console.WriteLine("Please choose correct status.");
                                    Console.WriteLine("-----------------------------");
                                    for (var i = 0; i < status.Length; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {status[i]}");
                                    }
                                    Console.WriteLine("Status: ");
                                    var issueStatusInput = Console.ReadLine();
                                    Console.WriteLine("-----------------------------");
                                    var isNumeric = int.TryParse(issueStatusInput, out var issueStatus);
                                    if (isNumeric && issueStatus <= status.Length && issueStatus > 0)
                                    {
                                        Issue issue = Dashboard.CreateIssue(projId, issueName, issueDescription, assignee, (IssueStatus)(issueStatus - 1));
                                        wrongStatus = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("---------------------------");
                                        Console.WriteLine("Incorrect input. Try again.");
                                        Console.WriteLine("---------------------------");
                                    }
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("-----------------------");
                                Console.WriteLine("Choose existing project");
                                Console.WriteLine("-----------------------");
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    case "3":
                        Console.WriteLine(" ");
                        ShowProjects();
                        Console.WriteLine(" ");
                        break;
                    case "4":
                        Console.WriteLine(" ");
                        projectsExist = ShowProjects();
                        if (projectsExist)
                        {
                            Console.WriteLine("Issues for what project whould you like to see?");
                            Console.Write("Project ID: ");
                            var projectId = Console.ReadLine();
                            var issues = Dashboard.ShowAllIssues(Convert.ToInt32(projectId));
                            if (issues.Count() >= 1)
                            {
                                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
                                Console.WriteLine("Issue Id   | Issue Name  | Issue Assignee   | Issue Status   | Issue Label   | Issue Description   | Issue Date   ");
                                foreach (var oneissue in issues)
                                {
                                    Console.WriteLine($"{oneissue.IssueId}          | {oneissue.IssueTitle}    | {oneissue.IssueAssignee}      | {oneissue.IssueStatus}       | {oneissue.IssueLabel}          | {oneissue.IssueDescription}             | {oneissue.IssueDate}   ");
                                }
                                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
                                break;
                            }
                            else
                            {
                                Console.WriteLine(" ");
                                Console.WriteLine("This project doesn't have issues yet or there is no project with provided id.");
                                Console.WriteLine("-------------------------------------");
                                Console.WriteLine(" ");
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    default:
                        Console.WriteLine("Incorrect input. Please try again.");
                        Console.WriteLine("----------------------------------");
                        break;
                }
            }
        }
        static bool ShowProjects()
        {
            Console.WriteLine("### List of all projects ###");
            Console.WriteLine(" ");
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
                foreach (var proj in projects)
                {
                    Console.WriteLine($"{proj.ProjectId}            | {proj.ProjectTitle}     | {proj.ProjectDescription}");
                }
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine(" ");
                return true;
            }
        }
    }
}
