using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerApp
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentBody { get; set; }
        public DateTime CommentTime { get; set; } = DateTime.Now;
        public string Email { get; set; }
        public bool Deleted { get; set; } = false;

        [ForeignKey("Issue")]
        public int IssueId { get; set; }
        public virtual Issue Issue { get; set; }
    }
}
