using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plantafolie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantafolie.Controllers
{
    public class StoreController : Controller
    {
        ApplicationDbContext storeDB;

        public StoreController(ApplicationDbContext context)
        {
            storeDB = context;
        }

        //
        // GET: /Store/GenreMenu
        public ActionResult GenreMenu()
        {
            var genres = storeDB.Categories.ToList();
            return PartialView(genres);
        }

        public ActionResult Index()
        {
            var genres = storeDB.Categories.ToList();
            return View(genres);
        }




        public ActionResult Browse(string categorie)
        {
            // Retrieve Categories and its Associated Products from database
            var categorieModel = storeDB.Categories.Include("Produit")
                .Single(g => g.CategorieNom == categorie);

            return View(categorieModel);
        }

        // GET: StoreManager/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await storeDB.Produits
                .Include(a => a.Nom)
                .Include(a => a.Categorie)
                .SingleOrDefaultAsync(m => m.ProduitID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }
    }
}
