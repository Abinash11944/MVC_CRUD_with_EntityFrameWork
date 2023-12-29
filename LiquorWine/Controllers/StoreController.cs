using LiquorWine.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiquorWine.Controllers
{
    public class StoreController : Controller
    {
        LiquorEntities1 liquorDB = new LiquorEntities1();
        // GET: ShopDetail
        public ActionResult Index()
        {
            var shops = liquorDB.ShopTbs.ToList();
            //shops = shops.Take(4).ToList();
         
           
           
            return View(shops);
        }
        [HttpPost]
        public ActionResult Index(string search) 
        {
            var shops = liquorDB.ShopTbs.ToList();
            if (search != null)
            {
                shops = liquorDB.ShopTbs.Where(x => x.ShopName.Contains(search)||x.ShopEmail.Contains(search)||x.OwnerName.Contains(search)).ToList();
            }
            
            return View(shops);
        }
    
       


        // GET: ShopDetail
        public ActionResult Details(int id) 
        {
            var shop = liquorDB.ShopTbs.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(ShopTb shop)
        {
            var shops = liquorDB.ShopTbs.Where(x => x.ShopEmail == shop.ShopEmail && x.ShopPhone == shop.ShopPhone).Count();
            if (shops > 0)
            {
                return RedirectToAction("Index");
            }
            return View(shop);
        }
        public ActionResult UserUpdate(int id)
        {
            var shop = liquorDB.ShopTbs.Find(id);
            return View(shop);
        }
        [HttpPost]
        public ActionResult UserUpdate(ShopTb shop ,string CSharp, string Java,string Python)
        {
            if (CSharp!=null )
            {
                shop.Language = CSharp;
            }
            else
            {
                HttpNotFound();
            }
            if (Java != null)
            {
                shop.Language = Java;
            }
            else
            {
                HttpNotFound();
            }
            if (Python != null)
            {
                shop.Language = Python;
            }
            else  
            {
                HttpNotFound();
            }
            liquorDB.Entry(shop).State=System.Data.Entity.EntityState.Modified;
            liquorDB.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Create()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult Create(ShopTb shop)
        {
            liquorDB.ShopTbs.Add(shop);
            liquorDB.SaveChanges();
            return RedirectToAction("login");
            //return RedirectToAction("Details", new { id = shop.ShopId });
        }
        public ActionResult Edit(int? id) 
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            ShopTb ShopTbFromDb = liquorDB.ShopTbs.Find(id);
            if (ShopTbFromDb == null)
            {
                return HttpNotFound();
            } 

            return View(ShopTbFromDb);
        }
        [HttpPost]
        public ActionResult Edit(int id, ShopTb shop)
        {

            if (id != shop.ShopId)
            {
                return HttpNotFound();
            }
           
            liquorDB.ShopTbs.AddOrUpdate(shop);
            liquorDB.SaveChanges();
            return RedirectToAction("Index");


            // If ModelState is not valid, return to the view with validation errors.

        }
        public ActionResult Delete(int? id)
        {
            if(id== null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            ShopTb shop=liquorDB.ShopTbs.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }

            return View(shop);
        }
       [HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public ActionResult DeleteConfirmed(int id)
{
    if (!ModelState.IsValid)
    {
        return View("Delete", liquorDB.ShopTbs.Find(id));
    }

    ShopTb shop = liquorDB.ShopTbs.Find(id);
    if (shop == null)
    {
        return HttpNotFound();
    }
    liquorDB.ShopTbs.Remove(shop);
    liquorDB.SaveChanges();
    return RedirectToAction("Index");
}
    }
   
}
