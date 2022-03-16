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
            List<Book> objList = _db.Books.Include(u => u.Publisher)
                                    .Include( u=>u.BookAuthors).ThenInclude(u=>u.Author).ToList();
            //foreach (var obj in objList)
            //{
            //    //   // Least Efficient
            //    //    obj.Publisher = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == obj.Publisher_Id);

            //    //   // Explicit Loading More Efficient
            //    _db.Entry(obj).Reference(u => u.Publisher).Load();
            //    _db.Entry(obj).Collection(u => u.BookAuthors).Load();
            //    foreach(var bookAuth in obj.BookAuthors)
            //    {
            //        _db.Entry(bookAuth).Reference(u => u.Author).Load();

            //    }
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

        public IActionResult ManageAuthors(int id)
        {
            BookAuthorVM obj = new BookAuthorVM
            {
                BookAuthorList = _db.BookAuthors.Include(u => u.Author).Include(u => u.Book)
                                        .Where(u => u.Book_Id == id).ToList(),
                BookAuthor = new BookAuthor()
                {
                    Book_Id = id
                },
                Book = _db.Books.FirstOrDefault(u => u.Book_Id == id)
            };
            List<int> tempListOfAssignedAuthors = obj.BookAuthorList.Select( u=> u.Author_Id).ToList();
            //NOT IN CLAUSE IN LINQ
            //get all the authors wohs id is not in tempListOfAssignedAuthors
            var tempList = _db.Authors.Where(u => !tempListOfAssignedAuthors.Contains(u.Author_Id)).ToList();

            obj.AuthorList = tempList.Select(i => new SelectListItem
            {
                Text = i.FullName,
                Value = i.Author_Id.ToString()
            });
            return View(obj);

        }

        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if(bookAuthorVM.BookAuthor.Book_Id != 0 && bookAuthorVM.BookAuthor.Author_Id != 0)
            {
                _db.BookAuthors.Add(bookAuthorVM.BookAuthor);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.Book_Id } );

        }

        [HttpPost]
        public IActionResult RemoveAuthors(int authorId , BookAuthorVM bookAuthorVM)
        {
            int book_id = bookAuthorVM.Book.Book_Id;
            BookAuthor bookAuthor = _db.BookAuthors.FirstOrDefault(
                u => u.Author_Id == authorId && u.Book_Id == book_id
                );

            _db.BookAuthors.Remove(bookAuthor);
            _db.SaveChanges();
            return RedirectToAction(nameof(ManageAuthors), new { @id = book_id } );


        }


        public IActionResult PlayGround()
        {
            //var bookTemp = _db.Books.FirstOrDefault();
            //bookTemp.Price = 100;

            //var bookCollection = _db.Books;
            //double totalPrice = 0;

            //foreach (var book in bookCollection)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = _db.Books.ToList();
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _db.Books;
            //var bookCount1 = bookCollection2.Count();

            //var bookCount2 = _db.Books.Count();
            //return RedirectToAction(nameof(Index));

            //Queryable > Enumerable

            //Updated relat4ed data

            //var bookTemp1 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.Book_Id == 6);
            //bookTemp1.BookDetail.NumberOfChapters = 2222;
            //_db.Books.Update(bookTemp1);
            //_db.SaveChanges();


            //var bookTemp2 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.Book_Id == 7);
            //bookTemp1.BookDetail.Weight = "123";
            //_db.Books.Attach(bookTemp2);
            //_db.SaveChanges();


            //Views
            var viewList = _db.BookDetailsFromViews.ToList();
            var viewList1 = _db.BookDetailsFromViews.FirstOrDefault();
            var viewList2 = _db.BookDetailsFromViews.Where(u=>u.Price > 500);

            //RAW SQL

            var bookRaw = _db.Books.FromSqlRaw("Select * from dbo.books").ToList();
            //SQL injection attack prone
            int id = 6;
            var bookTemp1 = _db.Books.FromSqlRaw($"Select * from dbo.books where Book_Id=({id})").ToList();

            var booksSproc = _db.Books.FromSqlInterpolated($" EXEC dbo.getAllBookDetails { id}").ToList();

            var BookFilter1 = _db.Books.Include(e => e.BookAuthors.Where(p => p.Author_Id == 1)).ToList();
            var BookFilter2 = _db.Books.Include(e => e.BookAuthors.OrderByDescending(p => p.Author_Id).Take(2)).ToList();


            return RedirectToAction(nameof(Index));

            
            //.NET 5 only

        }


    }

    

}

