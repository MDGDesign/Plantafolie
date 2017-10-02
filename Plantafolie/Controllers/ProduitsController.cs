using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plantafolie.Data;
using Plantafolie.Models.Produit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore.Query;
using PagedList.Mvc;
using PagedList;

namespace Plantafolie.Controllers
{
    // [Authorize(Roles = "Administrator, Manager")]
    public class ProduitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduitsController(ApplicationDbContext context)
        {
            _context = context;
        }




        // GET: Produits
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchType, string searchString, int? page)
        {
            // Order de trie pour les liens
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ProdIDSortParam = String.IsNullOrEmpty(sortOrder) ? "ProdID_desc" : "";
            ViewBag.CatSortParam    = sortOrder == "Categorie" ? "Cat_desc" : "Categorie";
            ViewBag.DateSortParam   = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.NomSortParam    = sortOrder == "Nom" ? "Nom_desc" : "Nom";

            // IEnumerable<Produit> listeDesProduits = _context.Produits.Include(p => p.Categorie).Include(p => p.Etat);
            var listeDesProduits = _context.Produits.Include(p => p.Categorie).Include(p => p.Etat);

            if (!String.IsNullOrEmpty(searchString))
            {
                page = 1;
                
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                listeDesProduits = SearchProduct(searchType, searchString, listeDesProduits);
            }

            // Effectue l'ordre de trie
            listeDesProduits = SortList(listeDesProduits, sortOrder);

            // Pagination
            int pageNumber = (page ?? 1);
            var pageOfProducts = listeDesProduits.ToPagedList(pageNumber, 25);
            int pageSize = 10;

            ViewBag.PageOfProducts = pageOfProducts;

            return View(await listeDesProduits.ToListAsync());
            // return View(listeDesProduits.ToPagedList(pageNumber, pageSize));
        }

    




        // GET: Produits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits
                .Include(p => p.Categorie)
                .Include(p => p.Etat)
                .SingleOrDefaultAsync(m => m.ProduitID == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }




        // GET: Produits/Create
        public IActionResult Create()
        {
            ViewData["EtatID"] = new SelectList(_context.Etats, "EtatID", "EtatNom");
            ViewData["CategorieID"] = new SelectList(_context.Categories, "CategorieID", "CategorieNom");
            return View();
        }




        // POST: Produits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProduitID,Nom,Description,PrixDemande,PrixDeVente,Image,Quantite,Disponible,Poids,EtatID,CategorieID")] Produit produit)
        {
            // Comparaison de PrixDemandé et PrixDeVente dans le model Produit

            var fileTempPath = Path.GetTempFileName(); // Chemin sur le serveur
            
            

            if (produit.Image != null)
            {
                if (CheckFileType(produit.Image))
                {
                     using (var stream = new FileStream("wwwroot/images/produits", FileMode.Create))
                    // using (var stream = new FileStream("C:\\Dev", FileMode.Create))
                    {
                        await produit.Image.CopyToAsync(stream);
                    }
                }

                produit.ImageName = produit.Image.FileName;
                
                
            }
            
            
            // Ajout de la date de création de la fiche
            produit.DateDeCreation = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EtatID"] = new SelectList(_context.Etats, "EtatID", "EtatNom", produit.EtatID);
            ViewData["CategorieID"] = new SelectList(_context.Categories, "CategorieID", "CategorieNom", produit.CategorieID);
            
            return View(produit);
        }




        // GET: Produits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits.SingleOrDefaultAsync(m => m.ProduitID == id);
            if (produit == null)
            {
                return NotFound();
            }
            ViewData["EtatID"] = new SelectList(_context.Etats, "EtatID", "EtatNom", produit.EtatID);
            ViewData["CategorieID"] = new SelectList(_context.Categories, "CategorieID", "CategorieNom", produit.CategorieID);
            return View(produit);
        }




        // POST: Produits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProduitID,Nom,Description,PrixDemande,PrixDeVente,ImagePath,DateDeCreation,Quantite,Disponible,Poids,EtatID,CategorieID")] Produit produit)
        {
            if (id != produit.ProduitID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitExists(produit.ProduitID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EtatID"] = new SelectList(_context.Etats, "EtatID", "EtatNom", produit.EtatID);
            ViewData["CategorieID"] = new SelectList(_context.Categories, "CategorieID", "CategorieID", produit.CategorieID);
            return View(produit);
        }




        // GET: Produits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits
                .Include(p => p.Categorie)
                .SingleOrDefaultAsync(m => m.ProduitID == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }




        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produit = await _context.Produits.SingleOrDefaultAsync(m => m.ProduitID == id);
            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        private bool ProduitExists(int id)
        {
            return _context.Produits.Any(e => e.ProduitID == id);
        }


        private bool CheckFileType(IFormFile file)
        {
            if (file != null)
            {
                if (file.ContentType.ToLower() != "image/jpg" &&
                    file.ContentType.ToLower() != "image/jpeg" &&
                    file.ContentType.ToLower() != "image/pjpeg" &&
                    file.ContentType.ToLower() != "image/x-png" &&
                    file.ContentType.ToLower() != "image/png")
                {
                    return false;
                }


            }

            return true;
        }

        // MÉTHODE DE TRIE DE LA PAGE INDEX - IIncludableQueryable<Produit, Etat>
        private IIncludableQueryable<Produit, Etat> SortList(IIncludableQueryable<Produit, Etat> listeDesProduits, string sortOrder)
        {
            switch (sortOrder)
            {
                case "Nom":
                    listeDesProduits = listeDesProduits.OrderBy(p => p.Nom).Include(p => p.Categorie).Include(p => p.Etat);
                    break;

                case "Nom_desc":
                    listeDesProduits = listeDesProduits.OrderByDescending(p => p.Nom).Include(p => p.Categorie).Include(p => p.Etat);
                    break;

                case "Categorie":
                    listeDesProduits = listeDesProduits.OrderBy(p => p.Categorie.CategorieNom).Include(p => p.Categorie).Include(p => p.Etat);
                    break;

                case "Cat_desc":
                    listeDesProduits = listeDesProduits.OrderByDescending(p => p.Categorie.CategorieNom).Include(p => p.Categorie).Include(p => p.Etat);
                    break;

                case "Date":
                    listeDesProduits = listeDesProduits.OrderBy(p => p.DateDeCreation).Include(p => p.Categorie).Include(p => p.Etat);
                    break;

                case "Date_desc":
                    listeDesProduits = listeDesProduits.OrderByDescending(p => p.DateDeCreation).Include(p => p.Categorie).Include(p => p.Etat);
                    break;

                case "ProdID_desc":
                    listeDesProduits = listeDesProduits.OrderByDescending(p => p.ProduitID).Include(p => p.Categorie).Include(p => p.Etat);
                    break;

                default:
                    listeDesProduits = listeDesProduits.OrderBy(p => p.ProduitID).Include(p => p.Categorie).Include(p => p.Etat);
                    break;
            }

            return listeDesProduits;
        }

        // RECHERCHE - IIncludableQueryable<Produit, Etat> 
        private IIncludableQueryable<Produit, Etat> SearchProduct(string searchType, string searchString, IIncludableQueryable<Produit, Etat> listeDesProduits)
        {
           

            // Effectue la recherche
            // listeDesProduits = listeDesProduits.Where(p => p.Nom.ToLower().Contains(searchString.ToLower())).ToList();
            


            switch (searchType)
            {
                case "Date":
                    // DateTime.TryParse(searchString, out )
                    // listeDesProduits = listeDesProduits.Where(p => p.DateDeCreation.Equals(searchString));
                    break;
                case "ProduitID":
                    break;

                case "Categorie":
                    break;

                case "Description":
                    break;

                default:
                    // listeDesProduits = listeDesProduits.Where(p => p.Nom.ToLower().Contains(searchString.ToLower()));
                    break;
            }



            // var listeProd = listeDesProduits.Where




            return listeDesProduits;
        }














    }
}
