using BugTrackerApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerUI.Models.ViewModels
{
    public class TasksModel
    {
        public List<Issue> Issues { get; set; }
        public int ProjectId { get; set; }
    }
}