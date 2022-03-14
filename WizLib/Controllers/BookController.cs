using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;
using WizLib_Model.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            List<Book> objList = _db.Books.Include(u => u.Publisher).ToList();
            //foreach (var obj in objList)
            //{
            //   // Least Efficient
            //    obj.Publisher = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == obj.Publisher_Id);

            //   // Explicit Loading More Efficient
            //    _db.Entry(obj).Reference(u => u.Publisher).Load();
            //}
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

        public IActionResult Details(int? id)
        {
            BookVM obj = new BookVM();
            

            if (id == null)
            {
                return View(obj);
            }
            else
            {
                //Edit
                obj.Book = _db.Books.Include(u => u.BookDetail).FirstOrDefault(u => u.Book_Id == id);
               //obj.Book.BookDetail = _db.BookDetails.FirstOrDefault(u => u.BookDetail_Id == obj.Book.BookDetail_Id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookVM obj)
        {

            if (obj.Book.BookDetail.BookDetail_Id == 0)
            {
                //this is create
                _db.BookDetails.Add(obj.Book.BookDetail);
                _db.SaveChanges();

                var BookFromDb = _db.Books.FirstOrDefault(u => u.Book_Id == obj.Book.Book_Id);
                BookFromDb.BookDetail_Id = obj.Book.BookDetail.BookDetail_Id;
                _db.SaveChanges();

            }
            else
            {
                //this is update
                _db.BookDetails.Update(obj.Book.BookDetail);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));

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

