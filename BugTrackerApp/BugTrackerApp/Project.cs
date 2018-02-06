using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerApp
{
    
    public class Project
    {

        #region Properties
        [Key]
        public int ProjectId { get; set; }
        [Required, MinLength(1), MaxLength(50)]
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public bool Deleted { get; set; } = false;
        #endregion
    }
}
