//using Microsoft.AspNetCore.Mvc;
//using Plantafolie.Data;
//using Plantafolie.Models;
//using Plantafolie.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;

//namespace Plantafolie.Controllers
//{
//    public class ShoppingCartController : Controller
//    {
//        ApplicationDbContext storeDB;
//        public ShoppingCartController(ApplicationDbContext context)
//        {
//            storeDB = context;
//        }

//        //
//        // GET: /ShoppingCart/
//        public ActionResult Index()
//        {
//            var cart = ShoppingCart.GetCart(this.HttpContext);

//            // Set up our ViewModel
//            var viewModel = new ShoppingCartViewModel
//            {
//                CartItems = cart.GetCartItems(storeDB),
//                CartTotal = cart.GetTotal()
//            };
//            // Return the view
//            return View(viewModel);
//        }
//        //
//        // GET: /Store/AddToCart/5
//        public ActionResult AddToCart(int id)
//        {
//            // Retrieve the album from the database
//            var addedAlbum = storeDB.Produit.Single(album => album.AlbumId == id);

//            // Add it to the shopping cart
//            var cart = ShoppingCart.GetCart(this.HttpContext);

//            cart.AddToCart(storeDB, addedAlbum);

//            // Go back to the main store page for more shopping
//            return RedirectToAction("Index");
//        }
//        //
//        // AJAX: /ShoppingCart/RemoveFromCart/5
//        [HttpPost]
//        public ActionResult RemoveFromCart(int id)
//        {
//            // Remove the item from the cart
//            var cart = ShoppingCart.GetCart(this.HttpContext);

//            // Get the name of the album to display confirmation
//            string albumName = storeDB.Carts.Single(item => item.RecordId == id).Produit.Nom;

//            // Remove from cart
//            int itemCount = cart.RemoveFromCart(id);

//            // Display the confirmation message
//            var results = new ShoppingCartRemoveViewModel
//            {
//                Message = HttpUtility.HtmlEncode(albumName) + " has been removed from your shopping cart.",
//                CartTotal = cart.GetTotal(),
//                CartCount = cart.GetCount(),
//                ItemCount = itemCount,
//                DeleteId = id
//            };
//            return Json(results);
//        }
//        //
//        // GET: /ShoppingCart/CartSummary

//        public ActionResult CartSummary()
//        {
//            var cart = ShoppingCart.GetCart(this.HttpContext);

//            ViewData["CartCount"] = cart.GetCount();
//            return PartialView("CartSummary");
//        }
//    }
//}
