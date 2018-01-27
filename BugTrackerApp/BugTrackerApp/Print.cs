using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerApp
{
    public static class Print
    {

        public static void ShowWelcomeMsg()
        {
            Console.WriteLine("*******************************");
            Console.WriteLine("Welcome to Bug Tracking System");
            Console.WriteLine("*******************************");
        }
        public static void ShowMenu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Create a new project");
            Console.WriteLine("2. Create a new issue");
            Console.WriteLine("3. Display all projects");
            Console.WriteLine("4. Display issues for a project");
            Console.WriteLine("5. Leave a comment for an issue");
            Console.WriteLine("--------------------------------------");
            Console.Write("Please choose one opition from above: ");
        }

        public static void ShowExitMsg()
        {
            Console.WriteLine("\nThank you for visiting. Have a nice day!");
            Console.WriteLine("----------------------------------------");
        }

        public static void ShowIncorrectInputMsg()
        {
            Console.WriteLine("Incorrect input. Please try again.");
            Console.WriteLine("----------------------------------");
        }

        public static void AskForCorrectInput()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Please choose correct label.");
            Console.WriteLine("-----------------------------");
        }

        public static void AskForChoosingCorrectProject()
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Choose existing project");
            Console.WriteLine("-----------------------");
        }

        public static int AskForProjectId()
        {
            Console.WriteLine("Issues for what project whould you like to see?");
            Console.Write("Project ID: ");
            var projectId = Convert.ToInt32(Console.ReadLine());
            return projectId;
        }
    }
}
