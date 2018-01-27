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
        private static int lastProjectNumber = 0;
        #region Constructor
        public Project()
        {
            ProjectId = ++lastProjectNumber;
        }
        #endregion
        #region Properties
        public int ProjectId { get; private set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public List<Issue> Issues = new List<Issue>();
        #endregion
    }
}
