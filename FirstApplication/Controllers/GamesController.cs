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
    [Authorize]
    public class GamesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Games
        [AllowAnonymous]
        public ActionResult Index()
        {
            //var games = db.Games.Include(g => g.Genre);
            var games = db.Games.AsQueryable();

            games = games.OrderBy(x => x.Name).AsQueryable();

            return View(games.ToList());
        }

        // GET: Games/Details/5
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            //Game game = db.Games.Include(x => x.GameGenres).SingleOrDefault(y => y.GameId == id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            Game model = new Game();
            //ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", model.Genres.Select(x=>x.GenreId).ToArray());
            return View(model);
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,IsMultiplayer, GenreIds")] Game game, string[] GenreIds)
        {
            
            if (ModelState.IsValid)
            {
                Game checkgame = db.Games.SingleOrDefault(x => x.Name == game.Name && x.IsMultiplayer == game.IsMultiplayer);
                if (checkgame == null)
                {
                    //game.GameId = Guid.NewGuid().ToString();
                    //game.CreateDate = DateTime.Now;
                    //game.EditDate = game.CreateDate;
                    db.Games.Add(game);
                    db.SaveChanges();

                    if (GenreIds != null)
                    {

                        foreach (string genreId in GenreIds)
                        {
                            GameGenre gameGenre = new GameGenre();

                            //gameGenre.GameGenreId = Guid.NewGuid().ToString();
                           // gameGenre.CreateDate = DateTime.Now;
                            //gameGenre.EditDate = gameGenre.CreateDate;

                            gameGenre.GameId = game.GameId;
                            gameGenre.GenreId = genreId;
                            game.Genres.Add(gameGenre);
                        }
                        db.Entry(game).State = EntityState.Modified; 
                        db.SaveChanges();
                    }

                    
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Duplicate Game detected.");
                }

            }
            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", GenreIds);
            //ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", game.GenreId);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            //ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", game.GenreId);
            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", game.Genres.Select(x => x.GenreId).ToArray());
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,Name,IsMultiplayer, GenreIds")] Game game, string[] GenreIds)
        {
            if (ModelState.IsValid)
            {
                Game tmpgame = db.Games.Find(game.GameId);
                if (tmpgame != null)
                {
                    Game checkgame = db.Games.SingleOrDefault(x => x.Name == game.Name && x.IsMultiplayer == game.IsMultiplayer && x.GameId != game.GameId);
                    if(checkgame == null)
                    {
                        tmpgame.Name = game.Name;
                        tmpgame.EditDate = DateTime.Now;
                        tmpgame.IsMultiplayer = game.IsMultiplayer;

                        db.Entry(tmpgame).State = EntityState.Modified;

                        //items to remove
                        var removeItems = tmpgame.Genres.Where(x => !GenreIds.Contains(x.GenreId)).ToList();
                        foreach (var removeItem in removeItems)
                        {
                            db.Entry(removeItem).State = EntityState.Deleted;
                        }
                        //items to add
                        if(GenreIds !=null)
                        {
                            var addItems = GenreIds.Where(x => !tmpgame.Genres.Select(y => y.GenreId).Contains(x));
                            foreach (var addItem in addItems)
                            {
                                GameGenre gameGenre = new GameGenre();

                                gameGenre.GameGenreId = Guid.NewGuid().ToString();
                                gameGenre.CreateDate = DateTime.Now;
                                gameGenre.EditDate = gameGenre.CreateDate;

                                gameGenre.GameId = tmpgame.GameId;
                                gameGenre.GenreId = addItem;
                                db.GameGenres.Add(gameGenre);
                                //db.Entry(addItem).State = EntityState.Added;
                            }
                        }

                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Duplicate Game detected.");
                    }
                }
            }
            //ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", game.GenreId);
            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", GenreIds);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            //Game game = db.Games.Include(x => x.GameGenres).SingleOrDefault(y => y.GameId == id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Game game = db.Games.Find(id);
            //Game game = db.Games.Include(x => x.GameGenres).SingleOrDefault(y => y.GameId == id);
            //delete foreignKey objects
            foreach (var gamegenre in game.Genres.ToList())
            {
                db.GameGenres.Remove(gamegenre);
            }
            //remove the game
            db.Games.Remove(game);
            var removed = db.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
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
