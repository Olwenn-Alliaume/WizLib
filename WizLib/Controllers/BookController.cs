using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class BookController : Controller
    {
        private readonly AplicationDbContext _db;

        public BookController(AplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Book> objList = _db.Books.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            Book obj = new Book();
            if( id  == null )
            {
                return View(obj);
            }
            else
            {
                //Edit
                obj = _db.Books.FirstOrDefault(u => u.Book_Id == id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Book obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Book_Id == 0)
                {
                    //this is create
                    _db.Books.Add(obj);

                }
                else
                {
                    //this is update
                    _db.Books.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(obj);

        }

        public IActionResult Delete(int id)
        {

            var ojbFromDb = _db.Books.FirstOrDefault( u => u.Book_Id == id );
            _db.Books.Remove(ojbFromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple2()
        {

            List<Book> catList = new List<Book>();
            for(int i = 1; i <= 2; i++)
            {
                catList.Add(new Book { Title = Guid.NewGuid().ToString() });
                //_db.Books.Add(new Book { Name = Guid.NewGuid().ToString() });
            }

            _db.Books.AddRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple5()
        {
            List<Book> catList = new List<Book>();
            for (int i = 1; i <= 5; i++)
            {
                catList.Add(new Book { Title = Guid.NewGuid().ToString() });
                //_db.Books.Add(new Book { Name = Guid.NewGuid().ToString() });
            }

            _db.Books.AddRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveMultiple2()
        {

            IEnumerable<Book> catList = _db.Books.OrderByDescending( u => u.Book_Id ).Take(2).ToList();
            

            _db.Books.RemoveRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple5()
        {
            IEnumerable<Book> catList = _db.Books.OrderByDescending(u => u.Book_Id).Take(5).ToList();


            _db.Books.RemoveRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }

    

}

