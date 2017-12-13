using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        ClientEntities db = new ClientEntities();

        // GET: 客戶聯絡人
        public ActionResult Index()
        {
            var data = db.客戶聯絡人.Where(p => !p.IsDeleted).ToList();

            var listsrc = db.客戶聯絡人.Select(p => p.職稱).Distinct();
            List<SelectListItem> 職稱list = new List<SelectListItem>();
            foreach (var 職稱 in listsrc)
            {
                職稱list.Add(new SelectListItem() {  Text = 職稱, Value = 職稱  });
            }

            ViewBag.tester = new SelectList(職稱list, "Value", "Text"); ;

            return View(data);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(string search_input, string dropdown_selected, string sortby)
        {
            var data = db.客戶聯絡人.Where(p => !p.IsDeleted).Where(p =>
            p.職稱.Contains(search_input) || p.姓名.Contains(search_input) || p.Email.Contains(search_input) ||
            p.手機.Contains(search_input) || p.電話.Contains(search_input)).ToList();

            if (!string.IsNullOrEmpty(dropdown_selected))
            {
                data = data.Where(p => p.職稱 == dropdown_selected).ToList();
            }

            if (!string.IsNullOrEmpty(sortby))
            {
                if ("1asc".Equals(sortby)) data = data.OrderBy(p => p.職稱).ToList();
                if ("1desc".Equals(sortby)) data = data.OrderByDescending(p => p.職稱).ToList();
                if ("2asc".Equals(sortby)) data = data.OrderBy(p => p.姓名).ToList();
                if ("2desc".Equals(sortby)) data = data.OrderByDescending(p => p.姓名).ToList();
                if ("3asc".Equals(sortby)) data = data.OrderBy(p => p.Email).ToList();
                if ("3desc".Equals(sortby)) data = data.OrderByDescending(p => p.Email).ToList();
                if ("4asc".Equals(sortby)) data = data.OrderBy(p => p.手機).ToList();
                if ("4desc".Equals(sortby)) data = data.OrderByDescending(p => p.手機).ToList();
                if ("5asc".Equals(sortby)) data = data.OrderBy(p => p.電話).ToList();
                if ("5desc".Equals(sortby)) data = data.OrderByDescending(p => p.電話).ToList();
                if ("6asc".Equals(sortby)) data = data.OrderBy(p => p.客戶資料.客戶名稱).ToList();
                if ("6desc".Equals(sortby)) data = data.OrderByDescending(p => p.客戶資料.客戶名稱).ToList();
            }

            ViewBag.search_input = search_input;

            var listsrc = db.客戶聯絡人.Select(p => p.職稱).Distinct();
            List<SelectListItem> 職稱list = new List<SelectListItem>();
            foreach (var 職稱 in listsrc)
            {
                職稱list.Add(new SelectListItem() { Text = 職稱, Value = 職稱 });
            }

            ViewBag.tester = new SelectList(職稱list, "Value", "Text", dropdown_selected); 

            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(客戶聯絡人 data)
        {
            if (ModelState.IsValid)
            {
                db.客戶聯絡人.Add(data);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View(data);
        }

        public ActionResult Edit(int id)
        {
            var item = db.客戶聯絡人.Find(id);

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View(item);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, 客戶聯絡人 data)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶聯絡人.Find(id);

                item.客戶Id = data.客戶Id;
                item.職稱 = data.職稱;
                item.姓名 = data.姓名;
                item.Email = data.Email;
                item.手機 = data.手機;
                item.電話 = data.電話;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View(data);
        }

        public ActionResult Details(int id)
        {
            return View(db.客戶聯絡人.Find(id));
        }

        public ActionResult Delete(int id)
        {
            var item = db.客戶聯絡人.Find(id);

            item.IsDeleted = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}