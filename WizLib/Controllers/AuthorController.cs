using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AplicationDbContext _db;

        public AuthorController(AplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Author> objList = _db.Authors.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            Author obj = new Author();
            if( id  == null )
            {
                return View(obj);
            }
            else
            {
                //Edit
                obj = _db.Authors.FirstOrDefault(u => u.Author_Id == id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Author obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Author_Id == 0)
                {
                    //this is create
                    _db.Authors.Add(obj);

                }
                else
                {
                    //this is update
                    _db.Authors.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(obj);

        }

        public IActionResult Delete(int id)
        {

            var ojbFromDb = _db.Authors.FirstOrDefault( u => u.Author_Id == id );
            _db.Authors.Remove(ojbFromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple2()
        {

            List<Author> catList = new List<Author>();
            for(int i = 1; i <= 2; i++)
            {
                catList.Add(new Author { FirstName = Guid.NewGuid().ToString() });
                //_db.Authors.Add(new Author { Name = Guid.NewGuid().ToString() });
            }

            _db.Authors.AddRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple5()
        {
            List<Author> catList = new List<Author>();
            for (int i = 1; i <= 5; i++)
            {
                catList.Add(new Author { FirstName = Guid.NewGuid().ToString() });
                //_db.Authors.Add(new Author { Name = Guid.NewGuid().ToString() });
            }

            _db.Authors.AddRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveMultiple2()
        {

            IEnumerable<Author> catList = _db.Authors.OrderByDescending( u => u.Author_Id ).Take(2).ToList();
            

            _db.Authors.RemoveRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple5()
        {
            IEnumerable<Author> catList = _db.Authors.OrderByDescending(u => u.Author_Id).Take(5).ToList();


            _db.Authors.RemoveRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }

    

}

