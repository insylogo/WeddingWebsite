using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingWebsite.Models;

namespace WeddingWebsite.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RSVP()
        {
            var unparsedId = Request?.Cookies["rsvp_id"]?.Value;
            int id;

            if (unparsedId != null && int.TryParse(unparsedId, out id))
            {
                 using (var db = new WeddingDbContext())
                {
                    var model = db.RSVPs.Find(id);
                    if (model == null)
                    {
                        var cookie = Response.Cookies["rsvp_id"];
                        cookie.Expires = DateTime.MinValue;
                        Response.Cookies.Set(cookie);
                        return View();
                    }
                    return View("SubmitRSVP", model);
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult ResubmitRSVP(int id)
        {
            using (var db = new WeddingDbContext())
            {
                var model = db.RSVPs.Find(id);
                if (model == null)
                {
                    var cookie = Response.Cookies["rsvp_id"];
                    cookie.Expires = DateTime.MinValue;
                    Response.Cookies.Set(cookie);
                    return RedirectToAction("RSVP");
                }
                return View("RSVP", model);
            }            
        }

        public ActionResult SubmitRSVP(RSVP rsvp)
        {
            using (var db = new WeddingDbContext())
            {
                if (rsvp.Id == default(int))
                {
                    db.RSVPs.Add(rsvp);
                    db.SaveChanges();
                }
                else
                {
                    var existing = db.RSVPs.Attach(rsvp);
                    db.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();                    
                }
            }
            
            var idCookie = new HttpCookie("rsvp_id", rsvp.Id.ToString());
            idCookie.Expires = DateTime.MaxValue;
            Response.SetCookie(idCookie);

            return PartialView(rsvp);
        }

        public ActionResult ListRSVPs()
        {
            using (var db = new WeddingDbContext())
            {
                return View(db.RSVPs.ToList());
            }
        }
    }
}