using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class 客戶資料Controller : Controller
    {
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();

        // GET: 客戶資料
        public ActionResult Index()
        {
            var 客戶分類repo = RepositoryHelper.Get客戶分類Repository();
            var data = new SelectList(客戶分類repo.All(), "Id", "客戶分類說明");
            ViewBag.tester = data;
 
            return View(repo.All());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(string search_input, string dropdown_selected)
        {
            if (string.IsNullOrEmpty(search_input) && string.IsNullOrEmpty(dropdown_selected))
            {
                return RedirectToAction("Index");
            }

            var data = repo.資料篩選邏輯寫在repo類別內(search_input, dropdown_selected);

            ViewBag.search_input = search_input;

            var 客戶分類repo = RepositoryHelper.Get客戶分類Repository();
            var ddlitems = new SelectList(客戶分類repo.All(), "Id", "客戶分類說明", dropdown_selected);
            ViewBag.tester = ddlitems;

            return View(data);
        }

        public ActionResult Create()
        {
            var 客戶分類repo = RepositoryHelper.Get客戶分類Repository();
            ViewBag.客戶分類Id = new SelectList(客戶分類repo.All(), "Id", "客戶分類說明");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(客戶資料 data)
        {
            if (ModelState.IsValid)
            {
                repo.Add(data);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            var 客戶分類repo = RepositoryHelper.Get客戶分類Repository();
            ViewBag.客戶分類Id = new SelectList(客戶分類repo.All(), "Id", "客戶分類說明");
            return View(data);
        }

        public ActionResult Edit(int id)
        {
            var item = repo.All().FirstOrDefault(p => p.Id == id);

            var 客戶分類repo = RepositoryHelper.Get客戶分類Repository();
            ViewBag.客戶分類Id = new SelectList(客戶分類repo.All(), "Id", "客戶分類說明", item.客戶分類Id);
            return View(item);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, 客戶資料 data)
        {
            if (ModelState.IsValid)
            {
                var item = repo.All().FirstOrDefault(p => p.Id == id);

                item.客戶名稱 = data.客戶名稱;
                item.統一編號 = data.統一編號;
                item.電話 = data.電話;
                item.傳真 = data.傳真;
                item.地址 = data.地址;
                item.Email = data.Email;

                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            var 客戶分類repo = RepositoryHelper.Get客戶分類Repository();
            ViewBag.客戶分類Id = new SelectList(客戶分類repo.All(), "Id", "客戶分類說明");
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var item = repo.Find(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public ActionResult Delete(int id)
        {
            var item = repo.Find(id);
            repo.Delete(item);

            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }
    }
}