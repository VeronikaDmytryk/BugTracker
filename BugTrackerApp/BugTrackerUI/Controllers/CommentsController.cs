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
    public class CommentsController : Controller
    {
        private BugTrackerModel db = new BugTrackerModel();

        // GET: Comments
        public ActionResult Index(int? id)
        {
            var comments = Dashboard.GetCommentsByIssueId(id.Value);
            CommentsModel CommentsData = new CommentsModel
            {
                Comments = comments,
                IssueId = id.Value
            };
            return View(CommentsData);
        }


        // GET: Comments/Create
        public ActionResult Create(int? id)
        {
            var IssueId = id.Value;
            var comment = new Comment()
            {
                IssueId = id.Value
            };
            return View(comment);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "CommentId,CommentBody,Email,IssueId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                Dashboard.LeaveCommentForIssue(comment.IssueId,comment.CommentBody, HttpContext.User.Identity.Name);
                return RedirectToAction("Index", new {id = comment.IssueId});
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = Dashboard.GetCommentById(id.Value);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "CommentId,CommentBody,CommentTime,Email,Deleted,IssueId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                Dashboard.EditComment(comment, HttpContext.User.Identity.Name);
                return RedirectToAction("Index", new { id = comment.IssueId});
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = Dashboard.GetCommentById(id.Value);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int? id)
        {
            Comment comment = Dashboard.GetCommentById(id.Value);
            Dashboard.ArchiveComment(id.Value, HttpContext.User.Identity.Name);
            return RedirectToAction("Index", new { id = comment.IssueId});
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
