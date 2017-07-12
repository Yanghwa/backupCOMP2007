using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstApplication.Models;

namespace FirstApplication.Controllers
{
    public class GenresController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Genres
        public ActionResult Index()
        {
            var genres = db.Genres.AsQueryable();

            genres = genres.OrderBy(x => x.Name).AsQueryable();

            return View(db.Genres.ToList());
        }

        // GET: Genres/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            Genre model = new Genre();
            ViewBag.Games = new MultiSelectList(db.Games.ToList(), "GameId", "Name", model.Games.Select(x => x.GameId).ToArray());
            return View(model);
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreId,Name, GameIds")] Genre genre, string[] GameIds)
        {
            if (ModelState.IsValid)
            {
                Genre checkGenre = db.Genres.SingleOrDefault(x => x.Name == genre.Name);
                if(checkGenre == null)
                {
                    genre.GenreId = Guid.NewGuid().ToString();
                    genre.CreateDate = DateTime.Now;
                    genre.EditDate = genre.CreateDate;
                    db.Genres.Add(genre);
                    db.SaveChanges();

                    if(GameIds != null)
                    {
                        foreach (string gameId in GameIds)
                        {
                            GameGenre gameGenre = new GameGenre();

                            gameGenre.GameGenreId = Guid.NewGuid().ToString();
                            gameGenre.CreateDate = DateTime.Now;
                            gameGenre.EditDate = gameGenre.CreateDate;

                            gameGenre.GenreId = genre.GenreId;
                            gameGenre.GameId = gameId;
                            db.GameGenres.Add(gameGenre);
                        }
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Duplicate Genre detected.");
                }
            }

            ViewBag.Games = new MultiSelectList(db.Games.ToList(), "GenreId", "Name", GameIds);
            return View(genre);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            ViewBag.Games = new MultiSelectList(db.Games.ToList(), "GameId", "Name", genre.Games.Select(x => x.GameId).ToArray());
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenreId,Name, GameIds")] Genre genre, string[] GameIds)
        {
            if (ModelState.IsValid)
            {
                Genre checkgenre = db.Genres.SingleOrDefault(x => x.Name == genre.Name && x.GenreId != genre.GenreId);
                if(checkgenre ==null)
                {
                    var tmpgenre = db.Genres.Find(genre.GenreId);
                    tmpgenre.Name = genre.Name;
                    tmpgenre.EditDate = DateTime.Now;
                    db.Entry(tmpgenre).State = EntityState.Modified;

                    //items to remove
                    var removeItems = tmpgenre.Games.Where(x => !GameIds.Contains(x.GameId)).ToList();
                    foreach (var removeItem in removeItems)
                    {
                        db.Entry(removeItem).State = EntityState.Deleted;
                    }
                    //items to add
                    if (GameIds != null)
                    {
                        var addItems = GameIds.Where(x => !tmpgenre.Games.Select(y => y.GameId).Contains(x));
                        foreach (var addItem in addItems)
                        {
                            GameGenre gameGenre = new GameGenre();

                            gameGenre.GameGenreId = Guid.NewGuid().ToString();
                            gameGenre.CreateDate = DateTime.Now;
                            gameGenre.EditDate = gameGenre.CreateDate;

                            gameGenre.GenreId = tmpgenre.GenreId;
                            gameGenre.GameId = addItem;
                            db.GameGenres.Add(gameGenre);
                        }
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Duplicate Genre detected.");
                }
            }

            ViewBag.Games = new MultiSelectList(db.Games.ToList(), "GenreId", "Name", GameIds);
            return View(genre);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Genre genre = db.Genres.Find(id);
            foreach (var gamegenre in genre.Games.ToList())
            {
                db.GameGenres.Remove(gamegenre);
            }
            db.Genres.Remove(genre);
            var removed = db.ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
