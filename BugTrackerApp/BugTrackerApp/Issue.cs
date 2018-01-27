using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerApp
{

    public enum IssueStatus
    {
        ToDo,
        InProcess,
        Done
    };

    public enum IssueLabel
    {
        Bug,
        Task,
        Crash,
        Feature,
        Documentation
    }
    /// <summary>
    /// This is a ticket
    /// </summary>
    public class Issue
    {
        private static int lastIssueId = 0;
        #region Constructor
        public Issue()
        {
            IssueId = ++lastIssueId;
        }
        #endregion
        #region Props
        public int IssueId { get; private set; }
        public DateTime IssueDate { get; private set; } = DateTime.Now;
        public string IssueTitle { get; set; }
        public string IssueDescription { get; set; }
        public IssueStatus IssueStatus { get; set; }
        public IssueLabel IssueLabel { get; set; }
        public string IssueAssignee { get; set; }
        public int ProjectId { get; set; }
        #endregion

    }
}