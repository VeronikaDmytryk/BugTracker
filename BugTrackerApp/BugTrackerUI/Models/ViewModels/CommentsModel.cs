using BugTrackerApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerUI.Models.ViewModels
{
    public class CommentsModel
    {
        public List<Comment> Comments { get; set; }
        public int IssueId { get; set; }
    }
}