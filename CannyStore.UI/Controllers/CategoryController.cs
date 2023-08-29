using CannyStore.Core.Contracts;
using CannyStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CannyStore.UI.Controllers
{
    public class CategoryController : Controller
    {
        IRepository<ProductCategory> _repository;
        public CategoryController(IRepository<ProductCategory> categoryContext)
        {
            _repository = categoryContext;
        }

        // GET: Category
        public ActionResult Index()
        {
            List<ProductCategory> categories = _repository.Collection().ToList();
            return View();
        }
        public ActionResult Create() 
        {
            ProductCategory category = new ProductCategory();
            return View(category);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }
            else
            {
                _repository.Insert(category);
                _repository.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id) 
        {
            ProductCategory category = _repository.Find(Id);
            if (category == null)
            {
                return HttpNotFound();
            }
            else
                return View(category);
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory category,string Id)
        {
            ProductCategory c = _repository.Find(Id);
            if (c == null)
            {
                return HttpNotFound();
            }
            else
            {
                c.Category = category.Category;
                _repository.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            ProductCategory c = _repository.Find(Id);
            if (c == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(c);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory c = _repository.Find(Id);
            if (c == null)
            { return HttpNotFound(); }
            else
            {
                _repository.Delete(Id); 
                return RedirectToAction("Index");
            }
        }
    }
}