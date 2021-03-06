﻿using System;
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
        #region Props
        [Key]
        public int IssueId { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.Now;
        [Required, MaxLength(50, ErrorMessage = "Title shouldn't be longer than 50 characters ")]
        public string IssueTitle { get; set; }
        [Required, MaxLength(500, ErrorMessage = "Description shouldn't be longer than 500 characters ")]
        public string IssueDescription { get; set; }
        [Required]
        public IssueStatus IssueStatus { get; set; }
        [Required]
        public IssueLabel IssueLabel { get; set; }
        public string IssueAssignee { get; set; }
        public bool Deleted { get; set; } = false;


        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        #endregion

    }
}