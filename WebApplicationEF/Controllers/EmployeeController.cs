using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using WebApplicationEF.Models;

namespace WebApplicationEF.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: Employee
        public ActionResult Index()
        {

            var data = db.Employees.Include(e => e.Experiences).ToList();

            if (Request.IsAjaxRequest())

                return PartialView(data);

            return View(data);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee, string command = "")
        {


            if (command == "add")
            {
                if (employee.Experiences is null)
                    employee.Experiences = new List<Experience>();
                employee.Experiences.Add(new Experience());
                return View(employee);
            }

            else if (command.StartsWith("delete"))
            {
                int index = Convert.ToInt32(command.Replace("delete-", string.Empty));
                employee.Experiences.RemoveAt(index);
                return View(employee);
            }
            if (ModelState.IsValid)
            {
                if (employee.ImageUpload != null)
                {
                    employee.ImageUrl = "/Images/" + Guid.NewGuid() + Path.GetExtension(employee.ImageUpload.FileName);

                    employee.ImageUpload.SaveAs(Server.MapPath(employee.ImageUrl));
                }


                db.Employees.Add(employee);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        [ChildActionOnly]
        public ActionResult ExperienceEntry(Employee employee)
        {
            if (employee is null)
                employee = new Employee();
            if (employee.Experiences is null)
                employee.Experiences = new List<Experience>();

            return PartialView("ExperienceEntry", employee);
        }




        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Include(e => e.Experiences).FirstOrDefault(e => e.ID == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,Email,Salary,Permanent,ImageUrl, ImageUpload,Experiences")] Employee employee, string command = "")
        {

            if (command == "add")
            {
                if (employee.Experiences is null)
                    employee.Experiences = new List<Experience>();
                employee.Experiences.Add(new Experience());
                return View(employee);
            }

            else if (command.StartsWith("delete"))
            {
                int index = Convert.ToInt32(command.Replace("delete-", string.Empty));
                employee.Experiences.RemoveAt(index);
                return View(employee);
            }
            if (ModelState.IsValid)
            {
                if (employee.ImageUpload != null)
                {
                    employee.ImageUrl = "/Images/" + Guid.NewGuid() + Path.GetExtension(employee.ImageUpload.FileName);

                    employee.ImageUpload.SaveAs(Server.MapPath(employee.ImageUrl));
                }


                foreach (var exp in employee.Experiences)
                {

                    if (exp.EmployeeID != employee.ID)
                    {
                        exp.EmployeeID = employee.ID;
                        db.Entry(exp).State = EntityState.Added;
                    }
                }
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpDelete]
        public ActionResult AjaxDelete(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
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
