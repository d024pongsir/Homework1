using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

using WebApplication1.Models.CustomResults;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace WebApplication1.Controllers
{
    public class 客戶銀行資訊Controller : Controller
    {
        private ClientEntities db = new ClientEntities();

        private JArray GetExportData(List<客戶銀行資訊> data)
        {
            JArray jObjects = new JArray();

            foreach (var item in data)
            {
                var jo = new JObject();
                jo.Add("Id", item.Id);
                jo.Add("客戶Id", item.客戶Id);
                jo.Add("銀行名稱", item.銀行名稱);
                jo.Add("銀行代碼", item.銀行代碼);
                jo.Add("分行代碼", item.分行代碼);
                jo.Add("帳戶名稱", item.帳戶名稱);
                jo.Add("帳戶號碼", item.帳戶號碼);
                jObjects.Add(jo);
            }
            return jObjects;
        }

        public ActionResult GetAllExcelDataPengVersion(string search_input)
        {
            var data = db.客戶銀行資訊.Include(path => path.客戶資料).Where(p => !p.IsDeleted).ToList();
            if (!string.IsNullOrEmpty(search_input))
            {
                data = data.Where(p => p.銀行名稱.Contains(search_input)).ToList();
            }

            var exportSpource = this.GetExportData(data);
            var dt = JsonConvert.DeserializeObject<DataTable>(exportSpource.ToString());

            var exportFileName = string.Concat(
                "客戶銀行資訊_",
                DateTime.Now.ToString("yyyyMMddHHmmss"),
                ".xlsx");

            ExportExcelPeng nv = new ExportExcelPeng("客戶銀行資料", exportFileName, dt);
            return nv.取得FileStreamResult();
        }

        // GET: 客戶銀行資訊
        public ActionResult Index()
        {
            var 客戶銀行資訊 = db.客戶銀行資訊.Include(客 => 客.客戶資料).Where(p => ! p.IsDeleted);
            return View(客戶銀行資訊.ToList());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(string search_input)
        {
            var data = db.客戶銀行資訊.Include(path => path.客戶資料).Where(p => !p.IsDeleted).ToList();
            if (!string.IsNullOrEmpty(search_input))
            {
                data = data.Where(p => p.銀行名稱.Contains(search_input)).ToList();
            }

            ViewBag.search_input = search_input;
            return View(data.ToList());
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                db.客戶銀行資訊.Add(客戶銀行資訊);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶銀行資訊).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);

            // db.客戶銀行資訊.Remove(客戶銀行資訊);
            客戶銀行資訊.IsDeleted = true;

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
