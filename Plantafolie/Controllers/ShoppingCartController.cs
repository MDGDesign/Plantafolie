using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plantafolie.Data;
using Plantafolie.Models;
using Plantafolie.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Plantafolie.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext storeDB;
        public ShoppingCartController(ApplicationDbContext context)
        {
            storeDB = context;
        }

        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, storeDB);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the product from the database
            var ajoutProduit = storeDB.Produits
                .Include(a => a.Nom)
                .Include(a => a.Categorie)
                .FirstOrDefault(produit => produit.ProduitID == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext, storeDB);

            cart.AddToCart(ajoutProduit);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext, storeDB);

            // Get the name of the product to display confirmation
            string produitNom = storeDB.Carts.Include("Produit")
                .Single(item => item.RecordId == id).Produit.Nom;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(storeDB, id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = HttpUtility.HtmlEncode(produitNom) + " a été retiré de votre panier.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary

        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, storeDB);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}
