using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;
using WizLib_Model.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            BookVM obj = new BookVM();
            obj.PublisherList = _db.Publishers.Select(i => new SelectListItem
            {

                Text = i.Name,
                Value = i.Publisher_Id.ToString()

            });

            if( id  == null )
            {
                return View(obj);
            }
            else
            {
                //Edit
                obj.Book = _db.Books.FirstOrDefault(u => u.Book_Id == id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM obj)
        {

                if(obj.Book.Book_Id == 0)
                {
                    //this is create
                    _db.Books.Add(obj.Book);

                }
                else
                {
                    //this is update
                    _db.Books.Update(obj.Book);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int id)
        {

            var ojbFromDb = _db.Books.FirstOrDefault( u => u.Book_Id == id );
            _db.Books.Remove(ojbFromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        
    }

    

}

