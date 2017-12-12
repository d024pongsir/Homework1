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
            return View(repo.All());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(string search_input)
        {
            if (string.IsNullOrEmpty(search_input))
            {
                return RedirectToAction("Index");
            }

            var data = repo.All().Where(p => p.客戶名稱.Contains(search_input) || p.統一編號.Contains(search_input) ||
                p.電話.Contains(search_input) || p.傳真.Contains(search_input) ||
                p.地址.Contains(search_input) || p.Email.Contains(search_input)
            );
            ViewBag.search_input = search_input;
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