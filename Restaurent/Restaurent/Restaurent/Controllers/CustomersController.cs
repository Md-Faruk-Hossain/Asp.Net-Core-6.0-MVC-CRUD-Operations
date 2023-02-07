using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurent.Models;
using Restaurent.Models.ViewModel;

namespace Restaurent.Controllers
{
	[Authorize(Roles = "Account Officer,Manager,Admin")]
	//[Authorize(Policy = "EmployeeOnly")]
	public class CustomersController : Controller
    {
        private readonly FoodDbContext _context;
        private readonly IWebHostEnvironment _he;

        public CustomersController(FoodDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.Include(x => x.OrderItems).ThenInclude(b => b.FoodItem).ToListAsync());
			
		}

        public IActionResult AddNewFoodItem(int? id)
        {
            ViewBag.FoodItems = new SelectList(_context.FoodItems.ToList(), "FoodItemId", "FoodName", id.ToString() ?? "");
            return PartialView("_AddNewFoodItem");
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerVM customerVM, int[] FoodItemId)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer()
                {
                    CustomerName = customerVM.CustomerName,
                    CustomerPhone = customerVM.CustomerPhone,
                    Address = customerVM.Address,
                    IsOrder = customerVM.IsOrder
                };
                //Img
                string webroot = _he.WebRootPath;
                string folder = "Images";
                string imgFileName = Path.GetFileName(customerVM.PicturFile.FileName);
                string fileToWrite = Path.Combine(webroot, folder, imgFileName);

                using (var stream = new FileStream(fileToWrite, FileMode.Create))
                {
                    await customerVM.PicturFile.CopyToAsync(stream);
                    customer.Picture = "/" + folder + "/" + imgFileName;
                }
                foreach (var item in FoodItemId)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        Customer = customer,
                        CustomerId = customer.CustomerId,
                        FoodItemId = item
                    };
                    _context.OrderItems.Add(orderItem);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            Customer customer = _context.Customers.First(x => x.CustomerId == id);
            var FoodItemList = await _context.OrderItems.Where(x => x.CustomerId == id).Select(x => x.FoodItemId).ToListAsync();


            CustomerVM customerVM = new CustomerVM
            {
                CustomerId = customer.CustomerId,
                CustomerPhone= customer.CustomerPhone,
                CustomerName = customer.CustomerName,
                Address = customer.Address,
                IsOrder = customer.IsOrder,
                Picture=customer.Picture,
                FoodItemList = FoodItemList
            };
            return View(customerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerVM customerVM, int[] FoodItemId)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer()
                {
                    CustomerId =customerVM.CustomerId,
                    CustomerName = customerVM.CustomerName,
                    CustomerPhone = customerVM.CustomerPhone,
                    Address = customerVM.Address,
                    IsOrder = customerVM.IsOrder,
                    Picture =customerVM.Picture
                    
                };
                //Img
                if (customerVM.PicturFile != null)
                {
					string webroot = _he.WebRootPath;
					string folder = "Images";
					string imgFileName = Path.GetFileName(customerVM.PicturFile.FileName);
					string fileToWrite = Path.Combine(webroot, folder, imgFileName);

					using (var stream = new FileStream(fileToWrite, FileMode.Create))
					{
						await customerVM.PicturFile.CopyToAsync(stream);
						customer.Picture = "/" + folder + "/" + imgFileName;
					}
				}
               
				//exists FoodItemList
				var existsFoodItem = _context.OrderItems.Where(x => x.CustomerId == customerVM.CustomerId).ToList();
                foreach (var item in existsFoodItem)
                {
                    _context.OrderItems.Remove(item);
                }
				//add new FoodItemList
				foreach (var item in FoodItemId)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        CustomerId = customer.CustomerId,
                        FoodItemId = item
                    };
                    _context.OrderItems.Add(orderItem);
                }
                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));               				
			}
			return View();

		}

        public async Task<IActionResult> Delete(int? id)
        {
            Customer customer = _context.Customers.First(x => x.CustomerId == id);
            var FoodItemList = _context.OrderItems.Where(x => x.CustomerId == id).ToList();

            foreach (var item in FoodItemList)
            {
                _context.OrderItems.Remove(item);
            }
            _context.Entry(customer).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }		

	}
}
