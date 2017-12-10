using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class View_1Controller : Controller
    {
        // GET: View_1
        public ActionResult Index()
        {
            List<View_1> viewresult = new List<View_1>();
            using (var db = new ClientEntities())
            {
                viewresult = db.View_1.ToList();
            }

            return View(viewresult);
        }
    }
}