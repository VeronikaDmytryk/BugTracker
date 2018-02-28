using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTrackerApp;
using BugTrackerUI.Models.ViewModels;

namespace BugTrackerUI.Controllers
{
    public class IssuesController : Controller
    {
        private BugTrackerModel db = new BugTrackerModel();

        // GET: Issues
        public ActionResult Index(int? id)
        {
            var issues = Dashboard.ShowAllIssues(id.Value);
            var Data = new TasksModel
            {
                Issues = issues,
                ProjectId = id.Value
            };

            return View(Data);
        }

        // GET: Issues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = Dashboard.GetIssueById(id.Value);

            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // GET: Issues/Create
        public ActionResult Create(int? id)
        {
            var issue = new Issue()
            {
                ProjectId = id.Value
            };
            return View(issue);
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId, IssueTitle,IssueDescription,IssueStatus,IssueLabel,IssueAssignee")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                Dashboard.CreateIssue(issue.ProjectId, issue.IssueTitle, issue.IssueDescription, issue.IssueAssignee, issue.IssueLabel, issue.IssueStatus);
                return RedirectToAction("Index", new { id = issue.ProjectId });
            }
            return View(issue);
        }

        // GET: Issues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = Dashboard.GetIssueById(id.Value);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IssueId, IssueTitle, IssueDescription, IssueStatus, IssueLabel, IssueAssignee, ProjectId")] Issue issue, int ProjectId)
        {
            if (ModelState.IsValid)
            {
                Dashboard.EditIssue(issue);
                return RedirectToAction("Index", new { id = ProjectId});
            }
            return View(issue);
        }

        // GET: Issues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = Dashboard.GetIssueById(id.Value);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? projectId)
        {
            Dashboard.ArchiveIssue(id);
            return RedirectToAction("Index", new { id = projectId.Value });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
