﻿using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        //GET 
        public IActionResult Create()
        {
            return View();
        }
        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrdeer.ToString())
            {
                ModelState.AddModelError("name", "The Display Ordeer cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Created successfullly";
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET -> Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();

            }
            return View(categoryFromDb);
        }
        //Post -> Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrdeer.ToString())
            {
                ModelState.AddModelError("name", "The Display Ordeer cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Updated successfullly";
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET -> Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();

            }
            return View(categoryFromDb);
        }
        //Post ->Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
           var obj = _db.Categories.Find(id); 
            if (obj == null) 
            {
                return NotFound();
            }
            _db.Categories.Remove(obj); 
            _db.SaveChanges();
            TempData["Success"] = "Category deleted successfullly";
            return RedirectToAction("Index");  
        }


    }
}
