using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Workers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using(var db = new WorkersdbEntities())
            {
                var workers = db.Workers.ToList();
                return View(workers);
            }            
        }

        [HttpGet]
        public ActionResult AddWorker()
        {
            WorkersdbEntities db = new WorkersdbEntities();
            List<Worker> worker = db.Workers.ToList();
            ViewBag.WorkersList = new SelectList(worker, "Id", "Firstname");
            return PartialView("_AddWorker");
        }

        [HttpPost]
        public ActionResult AddWorker(Worker worker)
        {
            if(worker.Firstname != null && worker.Surname !=null && worker.Patronymic != null && worker.Position != null)
            {
                worker.Id = Guid.NewGuid();
                using (var db = new WorkersdbEntities())
                {
                    db.Workers.Add(worker);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
          
        }

        [HttpGet]
        public ActionResult EditWorker(Guid id)
        {
            WorkersdbEntities db = new WorkersdbEntities();
            List<Worker> workerlist = db.Workers.ToList();
            ViewBag.WorkersList = new SelectList(workerlist, "Id", "Firstname");
            Worker worker = db.Workers.Find(id);

            return PartialView("_EditWorker", worker);
        }

        [HttpPost]
        public ActionResult EditWorker(Worker worker)
        {
            if(worker.Firstname != null && worker.Surname != null && worker.Patronymic != null && worker.Position != null)
            {
                using (var db = new WorkersdbEntities())
                {
                    db.Entry(worker).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            } else { return RedirectToAction("Index"); }
            
        }

        [HttpGet]
        public ActionResult DeleteWorker(Guid id)
        {
            WorkersdbEntities db = new WorkersdbEntities();
            Worker worker = db.Workers.Find(id);
            return PartialView("_DeleteWorker" , worker);
        }

        [HttpPost]
        public ActionResult DeleteWorker(Worker worker)
        {
            WorkersdbEntities db = new WorkersdbEntities();

            db.Workers.Attach(worker);
            db.Workers.Remove(worker);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}