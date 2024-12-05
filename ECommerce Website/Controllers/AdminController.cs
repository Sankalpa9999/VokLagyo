using ECommerce_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerce_Website.Controllers
{
    public class AdminController : Controller
    {
        private myContext _context;
        private IWebHostEnvironment _env;
        public AdminController(myContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            string admin_session = HttpContext.Session.GetString("admin_session");
            if(
                admin_session != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
            
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string adminEmail, string adminPassword)
        {
            var row = _context.tbl_admin.FirstOrDefault(a=>a.admin_email == adminEmail);
            if (row != null && row.admin_password == adminPassword)
            {
                HttpContext.Session.SetString("admin_session", row.admin_id.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Incorrect Username or Password";
                return View();
            }
            
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("admin_session");
            return RedirectToAction("login");
        }
        public IActionResult Profile()
        {
            var adminId = HttpContext.Session.GetString("admin_session");

            if (string.IsNullOrEmpty(adminId))
            {
                return RedirectToAction("Login"); // Redirect to login if the session is null or empty
            }

            int parsedAdminId;
            if (!int.TryParse(adminId, out parsedAdminId))
            {
                return RedirectToAction("Login"); // Redirect if the session value is not a valid integer
            }

            var row = _context.tbl_admin.Where(a => a.admin_id == parsedAdminId).ToList();
            return View(row);
        }

        [HttpPost]
        public IActionResult Profile(Admin updatedAdmin)
        {
            // Fetch the existing admin record
            var existingAdmin = _context.tbl_admin.FirstOrDefault(a => a.admin_id == updatedAdmin.admin_id);

            if (existingAdmin != null)
            {
                // Update only the fields that are part of the form
                existingAdmin.admin_name = updatedAdmin.admin_name;
                existingAdmin.admin_email = updatedAdmin.admin_email;
                existingAdmin.admin_password = updatedAdmin.admin_password;

                // Save the changes
                _context.SaveChanges();

                TempData["Message"] = "Profile updated successfully!";
            }
            else
            {
                TempData["Error"] = "Admin not found!";
            }

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public IActionResult ChangeProfileImage(IFormFile admin_image, int admin_id)
        {
            var existingAdmin = _context.tbl_admin.FirstOrDefault(a => a.admin_id == admin_id);

            if (existingAdmin != null && admin_image != null)
            {
                string imagePath = Path.Combine(_env.WebRootPath, "admin_image", admin_image.FileName);
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    admin_image.CopyTo(fileStream);
                }

                existingAdmin.admin_image = admin_image.FileName;

                _context.SaveChanges();

                TempData["Message"] = "Profile image updated successfully!";
            }
            else
            {
                TempData["Error"] = "Error updating profile image!";
            }

            return RedirectToAction("Profile");
        }




        /*        [HttpPost]
                public IActionResult ChangeProfileImage(IFormFile admin_image, Admin admin)
                {
                    string ImagePath = Path.Combine(_env.WebRootPath, "admin_image", admin_image.FileName);
                    FileStream fs = new FileStream(ImagePath, FileMode.Create);
                    admin_image.CopyTo(fs);
                    admin.admin_image = admin_image.FileName;
                    _context.tbl_admin.Update(admin);
                    _context.SaveChanges();
                    return RedirectToAction("Profile");
                }*/
        //[HttpPost]
        //public IActionResult ChangeProfileImage(IFormFile admin_image,Admin admin)
        //{
        //    string ImagePath = Path.Combine(_env.WebRootPath, "admin_image",admin_image.FileName);
        //    FileStream fs = new FileStream(ImagePath,FileMode.Create);
        //    admin_image.CopyTo(fs);
        //    admin.admin_image = admin_image.FileName;
        //    _context.tbl_admin.Update(admin);
        //    _context.SaveChanges();
        //    return RedirectToAction("Profile");
        //}

        /*        [HttpPost]
                public IActionResult ChangeProfileImage(IFormFile admin_image, Admin admin)
                {
                    // Find the existing admin by ID
                    var existingAdmin = _context.tbl_admin.Find(admin.admin_id);
                    if (existingAdmin == null)
                    {
                        // Handle case where admin does not exist
                        return NotFound("Admin not found.");
                    }

                    // Check if a new image file was uploaded
                    if (admin_image != null && admin_image.Length > 0)
                    {
                        // Build file path for the new image
                        string folderPath = Path.Combine(_env.WebRootPath, "admin_image");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath); // Ensure directory exists
                        }

                        string imagePath = Path.Combine(folderPath, admin_image.FileName);
                        using (var fs = new FileStream(imagePath, FileMode.Create))
                        {
                            admin_image.CopyTo(fs); // Save the file
                        }

                        // Update the admin's image field
                        existingAdmin.admin_image = admin_image.FileName;
                    }

                    // Save changes to other admin details (if necessary)
                    existingAdmin.admin_name = admin.admin_name;
                    existingAdmin.admin_email = admin.admin_email;
                    existingAdmin.admin_password = admin.admin_password;

                    // Save the changes to the database
                    _context.tbl_admin.Update(existingAdmin);
                    _context.SaveChanges();

                    return RedirectToAction("Profile");
                }*/

        public IActionResult fetchCustomer()
        {
            return View(_context.tbl_customer.ToList());
        }

        public IActionResult customerDetails(int id)
        {  
            return View(_context.tbl_customer.FirstOrDefault(c => c.customer_id == id));
        }
        
        public IActionResult updateCustomer(int id)
        {  
            return View(_context.tbl_customer.Find(id));
        }
        //[HttpPost]
        //public IActionResult updateCustomer(Customer customer,IFormFile customer_image)
        //{
        //    string ImagePath = Path.Combine(_env.WebRootPath,"customer_images", customer_image.FileName);
        //    FileStream fs = new FileStream(ImagePath, FileMode.Create);
        //    customer_image.CopyTo(fs);
        //    customer.customer_image = customer_image.FileName;
        //    _context.tbl_customer.Update(customer);
        //    _context.SaveChanges();
        //    return RedirectToAction("fetchCustomer");
        //}
        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer, IFormFile customer_image)
        {
            var existingCustomer = _context.tbl_customer.Find(customer.customer_id);
            if (existingCustomer == null)
            {
                // Handle case where customer doesn't exist
                return NotFound();
            }

            // Update the customer's fields
            existingCustomer.customer_name = customer.customer_name;
            existingCustomer.customer_phone = customer.customer_phone;
            existingCustomer.customer_email = customer.customer_email;
            existingCustomer.customer_password = customer.customer_password;
            existingCustomer.customer_gender = customer.customer_gender;
            existingCustomer.customer_country = customer.customer_country;
            existingCustomer.customer_city = customer.customer_city;
            existingCustomer.customer_address = customer.customer_address;


            // Handle file upload
            if (customer_image != null && customer_image.Length > 0)
            {
                string folderPath = Path.Combine(_env.WebRootPath, "customer_images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string imagePath = Path.Combine(folderPath, customer_image.FileName);
                using (var fs = new FileStream(imagePath, FileMode.Create))
                {
                    customer_image.CopyTo(fs);
                }

                existingCustomer.customer_image = customer_image.FileName; // Save the new file name
            }

            // Save changes
            _context.tbl_customer.Update(existingCustomer);
            _context.SaveChanges();

            return RedirectToAction("fetchCustomer");
        }

        public ActionResult deletePermission(int id)
        {
            return View(_context.tbl_customer.FirstOrDefault(c => c.customer_id == id));
        }

        public IActionResult deleteCustomer(int id)
        {
            var customer = _context.tbl_customer.Find(id);
            _context.tbl_customer.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("fetchCustomer");
        }

        public IActionResult fetchCategory()
        {
            return View(_context.tbl_category.ToList());
        }
        public IActionResult addCategory()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult addCategory(Category cat)
        //{
        //    _context.tbl_category.Add(cat);
        //    _context.SaveChanges();
        //    return RedirectToAction("fetchCategory");
        //}
        [HttpPost]
        public IActionResult addCategory(Category cat)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace(cat.category_name))
            {
                ViewBag.ErrorMessage = "Category name is required.";
                return View("AddCategory"); // Return the same view to display the error
            }

            // Add category to the database
            _context.tbl_category.Add(cat);
            _context.SaveChanges();
            return RedirectToAction("fetchCategory");
        }

        public IActionResult updateCategory(int id)
        {
            var category = _context.tbl_category.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult updateCategory(Category cat)
        {
            _context.tbl_category.Update(cat);
            _context.SaveChanges();
            return RedirectToAction("fetchCategory");
        }

        public ActionResult deletePermissionCategory(int id)
        {
            return View(_context.tbl_category.FirstOrDefault(c => c.category_id == id));
        }

        public IActionResult deleteCategory(int id)
        {
            var category = _context.tbl_category.Find(id);
            _context.tbl_category.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("fetchCategory");
        }

        public IActionResult fetchProduct()
        {
            return View(_context.tbl_product.ToList());
        }

        public IActionResult addProduct()
        {
            List<Category> categories = _context.tbl_category.ToList();
            ViewData["categories"] = categories;
            return View();
        }
        [HttpPost]
        public IActionResult addProduct(Product prod,IFormFile product_image)
        {
            string imageName = Path.GetFileName(product_image.FileName);
            string imagePath = Path.Combine(_env.WebRootPath,"product_images",imageName);
            FileStream fs = new FileStream(imagePath,FileMode.Create);
            product_image.CopyTo(fs);
            prod.product_image = imageName;
            _context.tbl_product.Add(prod);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }

        public IActionResult productDetails(int id)
        {
            return View(_context.tbl_product.Include(p=>p.Category).FirstOrDefault(p=>p.product_id==id));
        }

        public ActionResult deletePermissionProduct(int id)
        {
            return View(_context.tbl_product.FirstOrDefault(p => p.product_id == id));
        }

        public IActionResult deleteProduct(int id)
        {
            var product = _context.tbl_product.Find(id);
            _context.tbl_product.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }
        public IActionResult updateProduct(int id)
        {
            List<Category> categories = _context.tbl_category.ToList();
            ViewData["categories"] = categories;
            var product = _context.tbl_product.Find(id);

            ViewBag.selectedCategoryId = product.cat_id;

            return View(product);
        }

        [HttpPost]
        public IActionResult updateProduct(Product product)
        {
            _context.tbl_product.Update(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }

        [HttpPost]
        public IActionResult ChangeProductImage(IFormFile product_image, Product product)
        {
            string ImagePath = Path.Combine(_env.WebRootPath, "product_images", product_image.FileName);
            FileStream fs = new FileStream(ImagePath, FileMode.Create);
            product_image.CopyTo(fs);
            product.product_image = product_image.FileName;
            _context.tbl_product.Update(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }


        public IActionResult fetchFeedback()
        {
            
            return View(_context.tbl_feedback.ToList());
        }

        public ActionResult deletePermissionFeedback(int id)
        {
            return View(_context.tbl_feedback.FirstOrDefault(f => f.feedback_id == id));
        }
        public IActionResult deleteFeedback(int id)
        {
            var feedback = _context.tbl_feedback.Find(id);
            _context.tbl_feedback.Remove(feedback);
            _context.SaveChanges();
            return RedirectToAction("fetchFeedback");
        }

        public IActionResult fetchcart()
        {
            var cart = _context.tbl_cart.Include(c=>c.products).Include(c=>c.customers).ToList();
            return View(cart);
        }
        public ActionResult deletePermissionCart(int id)
        {
            return View(_context.tbl_cart.FirstOrDefault(c => c.cart_id == id));
        }

        public IActionResult deleteCart(int id)
        {
            var cart = _context.tbl_cart.Find(id);
            _context.tbl_cart.Remove(cart);
            _context.SaveChanges();
            return RedirectToAction("fetchCart");
        }
        public IActionResult updateCart(int id)
        {
            var cart = _context.tbl_cart.Find(id);
            return View(cart);
        }
        [HttpPost]
        public IActionResult updateCart(int cart_status, Cart cart)
        {
            cart.cart_status = cart_status;
            _context.tbl_cart.Update(cart);
            _context.SaveChanges();
            return RedirectToAction("fetchCart");
        }

        public IActionResult ViewOrders()
        {
            // Retrieve pending orders only
            var pendingOrders = _context.tbl_cart
                .Include(c => c.products)
                .Include(c => c.customers)
                .Where(c => c.cart_status == 0) // Only fetch pending orders
                .ToList();

            return View(pendingOrders);
        }


        // Mark an order as completed
        public IActionResult CompleteOrder(int id)
        {
            var order = _context.tbl_cart.FirstOrDefault(c => c.cart_id == id);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            // Mark as completed
            order.cart_status = 1;
            _context.SaveChanges();

            TempData["Message"] = "Order marked as completed.";
            return RedirectToAction("ViewOrders");
        }

        // Remove an order
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.tbl_cart.Find(id);
            if (order == null) return NotFound("Order not found.");

            _context.tbl_cart.Remove(order);
            _context.SaveChanges();

            TempData["Message"] = "Order has been deleted.";
            return RedirectToAction("ViewOrders");
        }

        public IActionResult OrderHistory()
        {
            // Retrieve completed orders
            var completedOrders = _context.tbl_cart
                .Include(c => c.products)
                .Include(c => c.customers)
                .Where(c => c.cart_status == 1) // Only fetch completed orders
                .ToList();

            return View(completedOrders);
        }
    }
}

