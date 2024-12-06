using ECommerce_Website.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerce_Website.Controllers
{
    public class CustomerController : Controller
    {
        private myContext _context;
        private IWebHostEnvironment _env;
        public CustomerController(myContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Category> category = _context.tbl_category.ToList();
            ViewData["category"] = category;

            List<Product> products = _context.tbl_product.ToList();
            ViewData["product"] = products;

            

            ViewBag.checkSession = HttpContext.Session.GetString("customerSession");
            return View();
        }
        public IActionResult customerLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult customerLogin(String customerEmail, String customerPassword)
        {
            var customer = _context.tbl_customer.FirstOrDefault(c=>c.customer_email==customerEmail);
            if(customer!=null && customer.customer_password==customerPassword)
            {
                HttpContext.Session.SetString("customerSession", customer.customer_id.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Incorrect Username or Password";
                return View();
            }
 
            
        }


        public IActionResult CustomerRegistration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerRegistration(Customer customer)
        {
            _context.tbl_customer.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("customerLogin");
        }


        public IActionResult customerLogout()
        {
            HttpContext.Session.Remove("customerSession");
            return RedirectToAction("index");
        }

        public IActionResult customerProfile()
        {

            if(String.IsNullOrEmpty(HttpContext.Session.GetString("customerSession")))
            {
                return RedirectToAction("customerLogin");
            }
            else
            {
                List<Category> category = _context.tbl_category.ToList();
                ViewData["category"] = category;
                var customerId = HttpContext.Session.GetString("customerSession");
                var row = _context.tbl_customer.Where(c => c.customer_id == int.Parse(customerId)).ToList();
                ViewBag.checkSession = HttpContext.Session.GetString("customerSession");
                return View(row);

            }
        }
        [HttpPost]
        public IActionResult updateCustomerProfile(Customer customer)
        {
            _context.tbl_customer.Update(customer);
            _context.SaveChanges();
  
            return RedirectToAction("customerProfile");
        }
        /*        public IActionResult changeProfileImage(Customer customer, IFormFile customer_image)
                {
                    string ImagePath = Path.Combine(_env.WebRootPath, "customer_images", customer_image.FileName);
                    FileStream fs = new FileStream(ImagePath, FileMode.Create);
                    customer_image.CopyTo(fs);
                    customer.customer_image = customer_image.FileName;
                    _context.tbl_customer.Update(customer);
                    _context.SaveChanges();

                    return RedirectToAction("customerProfile");
                }*/


        public IActionResult ChangeProfileImage(int customer_id, IFormFile customer_image)
        {
            // Check if the image is null or empty
            if (customer_image == null || customer_image.Length == 0)
            {
                ModelState.AddModelError("", "Please upload a valid image.");
                return RedirectToAction("CustomerProfile");  // Redirect back to the profile page if the image is invalid
            }

            // Fetch the existing customer from the database
            var existingCustomer = _context.tbl_customer.FirstOrDefault(c => c.customer_id == customer_id);
            if (existingCustomer == null)
            {
                return NotFound("Customer not found.");
            }

            // Save the image to the server
            string imageDirectory = Path.Combine(_env.WebRootPath, "customer_images");
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);  // Create directory if it does not exist
            }

            string fileName = Path.GetFileName(customer_image.FileName);
            string filePath = Path.Combine(imageDirectory, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                customer_image.CopyTo(fileStream);
            }

            // Update the customer's image path in the database
            existingCustomer.customer_image = fileName;
            _context.tbl_customer.Update(existingCustomer);
            _context.SaveChanges();

            return RedirectToAction("CustomerProfile");  // Redirect back to the customer profile page
        }


        /*        public IActionResult changeProfileImage(int customer_id, IFormFile customer_image)
                {
                    // Retrieve the existing customer record
                    var customer = _context.tbl_customer.FirstOrDefault(c => c.customer_id == customer_id);
                    if (customer == null)
                    {
                        return NotFound("Customer not found.");
                    }

                    // Save the new image
                    if (customer_image != null)
                    {
                        string imagePath = Path.Combine(_env.WebRootPath, "customer_images", customer_image.FileName);

                        using (var fs = new FileStream(imagePath, FileMode.Create))
                        {
                            customer_image.CopyTo(fs);
                        }

                        customer.customer_image = customer_image.FileName;
                    }

                    // Update the database
                    _context.tbl_customer.Update(customer);
                    _context.SaveChanges();

                    return RedirectToAction("customerProfile");
                }*/
        public IActionResult feedback()
        {
            ViewBag.checkSession = HttpContext.Session.GetString("customerSession");
            return View();
        }
        [HttpPost]
        public IActionResult feedback(Feedback feedback)
        {
            TempData["message"] = "Thank You For Your Feedback";
            _context.tbl_feedback.Add(feedback);
            _context.SaveChanges();
            return RedirectToAction("feedback");
        }

        public IActionResult fetchAllProducts()
        {
            /*            List<Category> category = _context.tbl_category.ToList();
            ViewData["category"] = category;*/

            List<Product> products = _context.tbl_product.ToList();
            ViewData["product"] = products;
            ViewBag.checkSession = HttpContext.Session.GetString("customerSession");
            return View();
        }
        public IActionResult productDetails(int id)
        {
            /*            List<Category> category = _context.tbl_category.ToList();
                           ViewData["category"] = category;*/

            var products = _context.tbl_product.Where(p => p.product_id == id).ToList();
            ViewBag.checkSession = HttpContext.Session.GetString("customerSession");
            return View(products);
        }

        public IActionResult AddToCart(int prod_id, Cart cart)
        {
            string isLogin = HttpContext.Session.GetString("customerSession");
            if(isLogin != null)
            {
                cart.prod_id = prod_id;
                cart.cust_id = int.Parse(isLogin);
                cart.product_quantity = 1;
                cart.cart_status = 0;
                _context.tbl_cart.Add(cart);
                _context.SaveChanges();
                TempData["message"] = "Product Successfully Added in Cart";
                return RedirectToAction("fetchAllProducts");
            }
            else
            {
                return RedirectToAction("customerLogin");
                
            }
        }

        public IActionResult fetchCart()
        {
            string customerId = HttpContext.Session.GetString("customerSession");
            if (customerId != null)
            {
                var cart = _context.tbl_cart
                    .Where(c => c.cust_id == int.Parse(customerId) && !c.is_checked_out) // Filter out checked-out items
                    .Include(c => c.products)
                    .ToList();
                ViewBag.checkSession = HttpContext.Session.GetString("customerSession");
                return View(cart);
            }
            else
            {
                return RedirectToAction("customerLogin");
            }
        }

        public IActionResult removeProduct(int id)
        {
            var product = _context.tbl_cart.Find(id);
            _context.tbl_cart.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("fetchCart");
        }




        public IActionResult CheckoutProduct(int id)
        {
            // Retrieve the cart item along with its related product and customer details
            var cartItem = _context.tbl_cart
                .Include(c => c.products)
                .Include(c => c.customers)
                .FirstOrDefault(c => c.cart_id == id);

            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            // Pass the cart item to the view
            ViewBag.checkSession = HttpContext.Session.GetString("customerSession");
            return View(cartItem);
        }


        [HttpPost]
        public IActionResult ConfirmCheckout(int cart_id)
        {
            // Retrieve the cart item
            var cartItem = _context.tbl_cart
                .Include(c => c.products)
                .Include(c => c.customers)
                .FirstOrDefault(c => c.cart_id == cart_id);

            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            // Create an order for the cart item
            var order = new Order
            {
                cart_id = cartItem.cart_id,
                order_status = 0, // Pending
            };

            // Add the order to the database
            _context.tbl_order.Add(order);
            _context.SaveChanges();

            // Mark the cart item as checked out
            cartItem.is_checked_out = true; // Set is_checked_out to true
            _context.SaveChanges();

            // Confirmation message
            TempData["Message"] = "Your checkout has been successfully confirmed!";
            ViewBag.checkSession = HttpContext.Session.GetString("customerSession");

            return RedirectToAction("UserOrderHistory"); // Redirect to order history
        }




        /*
                [HttpPost]
                public IActionResult ConfirmCheckout(int cart_id)
                {
                    // Retrieve the cart item
                    var cartItem = _context.tbl_cart
                        .Include(c => c.products)
                        .Include(c => c.customers)
                        .FirstOrDefault(c => c.cart_id == cart_id);

                    if (cartItem == null)
                    {
                        return NotFound("Cart item not found.");
                    }

                    // Create an order for the cart item
                    var order = new Order
                    {
                        cart_id = cartItem.cart_id,
                        order_status = 0, // Pending

                    };

                    // Add the order to the database
                    _context.tbl_order.Add(order);
                    _context.SaveChanges();

                    // Confirmation message
                    TempData["Message"] = "Your checkout has been successfully confirmed!";

                    return RedirectToAction("UserOrderHistory"); // Redirect to order history
                }*/




        public IActionResult onlinePayment()
        {
            ViewBag.checkSession = HttpContext.Session.GetString("customerSession");
            return View();
        }

        public IActionResult UserOrderHistory()
        {
            string customerId = HttpContext.Session.GetString("customerSession");
            if (customerId != null)
            {
                // Fetch orders related to the logged-in customer
                var orders = _context.tbl_order
                    .Include(o => o.cart) // Assuming tbl_order is linked to tbl_cart
                    .ThenInclude(c => c.products) // Assuming tbl_cart has a relationship with products
                    .Where(o => o.cart.cust_id == int.Parse(customerId))
                    .ToList();
                ViewBag.checkSession = HttpContext.Session.GetString("customerSession");
                return View(orders);
            }
            else
            {
                return RedirectToAction("customerLogin");
            }
        }

    }


}
