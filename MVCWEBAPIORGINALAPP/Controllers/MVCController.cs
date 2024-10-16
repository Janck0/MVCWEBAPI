using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCWEBAPIORGINALAPP;
using System.Net.Http;

namespace MVCWEBAPIORGINALAPP.Controllers
{
    public class MVCController : Controller
    {
        private APIEntities db = new APIEntities();

        // GET: MVC
        public ActionResult Index()
        {
            IEnumerable<TABLE_EMPLOYEE> employees = null;
            using (var Client = new HttpClient())
            {
                

                Client.BaseAddress = new Uri("http://localhost:57233/api/API/");
                //HTTP POST

                var resTask = Client.GetAsync("getapi");
                resTask.Wait();
                
                var result = resTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var ReadTask = result.Content.ReadAsAsync<IList<TABLE_EMPLOYEE>>();
                    ReadTask.Wait();
                    employees = ReadTask.Result;
                }
                else
                {
                    employees = Enumerable.Empty<TABLE_EMPLOYEE>();
                }
                return View(employees);
            }
            
        }

        // GET: MVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TABLE_EMPLOYEE emp = null;
            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57233/api/API/");
                var res = client.GetAsync($"getwithid/{id}");
                res.Wait();
                var result = res.Result;
                if (result.IsSuccessStatusCode)
                {
                    var ReadTask = result.Content.ReadAsAsync<TABLE_EMPLOYEE>();
                    ReadTask.Wait();
                    emp = ReadTask.Result;
                }
                else
                {
                    emp = new TABLE_EMPLOYEE();
                }
                
            }
            return View(emp);
        }

        // GET: MVC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MVC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,AGE,MARK")] TABLE_EMPLOYEE tABLE_EMPLOYEE)
        {
            if (ModelState.IsValid)
            {
                using (var Client = new HttpClient())
                {
                    Client.BaseAddress = new Uri("http://localhost:57233/api/API/");
                    //HTTP POST
                    var postTask = Client.PostAsJsonAsync<TABLE_EMPLOYEE>("postwebapitab", tABLE_EMPLOYEE);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                //db.TABLE_EMPLOYEE.Add(tABLE_EMPLOYEE);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tABLE_EMPLOYEE);
        }

        // GET: MVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TABLE_EMPLOYEE emp = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57233/api/API/");
                var res = client.GetAsync($"getwithid/{id}");
                res.Wait();
                var result = res.Result;
                if (result.IsSuccessStatusCode)
                {
                    var ReadTask = result.Content.ReadAsAsync<TABLE_EMPLOYEE>();
                    ReadTask.Wait();
                    emp = ReadTask.Result;
                }
                else
                {
                    emp = new TABLE_EMPLOYEE();
                }

            }
            return View(emp);
        }

        // POST: MVC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,AGE,MARK")] TABLE_EMPLOYEE tABLE_EMPLOYEE)
        {
            if (ModelState.IsValid)
            {
                using (var Client = new HttpClient())
                {
                    Client.BaseAddress = new Uri("http://localhost:57233/api/API/");
                    //HTTP POST
                    var postTask = Client.PutAsJsonAsync<TABLE_EMPLOYEE>($"Edit{tABLE_EMPLOYEE.ID}", tABLE_EMPLOYEE);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                
            }
            return View(tABLE_EMPLOYEE);
        }

        // GET: MVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TABLE_EMPLOYEE tABLE_EMPLOYEE = db.TABLE_EMPLOYEE.Find(id);
            if (tABLE_EMPLOYEE == null)
            {
                return HttpNotFound();
            }
            return View(tABLE_EMPLOYEE);
        }

        // POST: MVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //TABLE_EMPLOYEE tABLE_EMPLOYEE = db.TABLE_EMPLOYEE.Find(id);
            //db.TABLE_EMPLOYEE.Remove(tABLE_EMPLOYEE);
            //db.SaveChanges();
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri("http://localhost:57233/api/API/");
                //HTTP POST
                var postTask = Client.DeleteAsync($"delete/{id}");
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Delete");
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
