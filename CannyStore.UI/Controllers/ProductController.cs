using CannyStore.Core.Contracts;
using CannyStore.Core.Models;
using CannyStore.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CannyStore.UI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        IRepository<Product> context;
        IRepository<ProductCategory> productCategory;
        public ProductController(IRepository<Product> productContext, IRepository<ProductCategory> categoryContext)
        {
            context = productContext;
            productCategory = categoryContext;
        }
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create() 
        {
            ProductVM viewModel = new ProductVM();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategory.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product) 
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null) 
                return HttpNotFound();
            else
            {
                ProductVM viewModel = new ProductVM();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategory.Collection();
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id) 
        {
            Product p = context.Find(Id);
            if(p == null)
                return HttpNotFound();
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                p.Category = product.Category;
                p.Description = product.Description;
                p.Name = product.Name;
                p.Price = product.Price;
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            Product p = context.Find(Id);
            if (p == null)
                return HttpNotFound();
            else
            {
                return View(p);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product p = context.Find(Id);
            if (p == null) 
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
            }
            return RedirectToAction("Index");
        }

    }
}