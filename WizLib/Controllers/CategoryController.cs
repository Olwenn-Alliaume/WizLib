using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AplicationDbContext _db;

        public CategoryController(AplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objList = _db.Categories.ToList();
            return View(objList);
        }

        public IActionResult Upsrt(int? id)
        {
            Category obj = new Category();
            if( id  == null )
            {
                return View(obj);
            }
            else
            {
                //Edit
                obj = _db.Categories.First(u => u.Category_Id == id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
        }
    }
}
