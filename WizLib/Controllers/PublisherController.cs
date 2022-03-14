using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class PublisherController : Controller
    {
        private readonly AplicationDbContext _db;

        public PublisherController(AplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Publisher> objList = _db.Publishers.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            Publisher obj = new Publisher();
            if( id  == null )
            {
                return View(obj);
            }
            else
            {
                //Edit
                obj = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Publisher_Id == 0)
                {
                    //this is create
                    _db.Publishers.Add(obj);

                }
                else
                {
                    //this is update
                    _db.Publishers.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(obj);

        }

        public IActionResult Delete(int id)
        {

            var ojbFromDb = _db.Publishers.FirstOrDefault( u => u.Publisher_Id == id );
            _db.Publishers.Remove(ojbFromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple2()
        {

            List<Publisher> catList = new List<Publisher>();
            for(int i = 1; i <= 2; i++)
            {
                catList.Add(new Publisher { Name = Guid.NewGuid().ToString() });
                //_db.Publishers.Add(new Publisher { Name = Guid.NewGuid().ToString() });
            }

            _db.Publishers.AddRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple5()
        {
            List<Publisher> catList = new List<Publisher>();
            for (int i = 1; i <= 5; i++)
            {
                catList.Add(new Publisher { Name = Guid.NewGuid().ToString() });
                //_db.Publishers.Add(new Publisher { Name = Guid.NewGuid().ToString() });
            }

            _db.Publishers.AddRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveMultiple2()
        {

            IEnumerable<Publisher> catList = _db.Publishers.OrderByDescending( u => u.Publisher_Id ).Take(2).ToList();
            

            _db.Publishers.RemoveRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple5()
        {
            IEnumerable<Publisher> catList = _db.Publishers.OrderByDescending(u => u.Publisher_Id).Take(5).ToList();


            _db.Publishers.RemoveRange(catList);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }

    

}

