﻿using ECommerce_Website.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult changeProfileImage(Customer customer, IFormFile customer_image)
        {
            string ImagePath = Path.Combine(_env.WebRootPath, "customer_images", customer_image.FileName);
            FileStream fs = new FileStream(ImagePath, FileMode.Create);
            customer_image.CopyTo(fs);
            customer.customer_image = customer_image.FileName;
            _context.tbl_customer.Update(customer);
            _context.SaveChanges();

            return RedirectToAction("customerProfile");
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

            return View();
        }
        public IActionResult productDetails(int id)
        {
            /*            List<Category> category = _context.tbl_category.ToList();
                           ViewData["category"] = category;*/

            var products = _context.tbl_product.Where(p => p.product_id == id).ToList();
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


    }


}
