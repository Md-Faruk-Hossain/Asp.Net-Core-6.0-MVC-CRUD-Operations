using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurent.Models;
using Restaurent.Models.ViewModel;

namespace Restaurent.Controllers
{
    public class FoodItemsController : Controller
    {
        private readonly FoodDbContext _context;
        private readonly IWebHostEnvironment _he;
        public FoodItemsController(FoodDbContext context,IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }
       

        // GET: FoodItems
        public async Task<IActionResult> Index()
        {
              return View(await _context.FoodItems.ToListAsync());
        }

       

        // GET: FoodItems/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodItemVM itemVM)
        {
            
            if (ModelState.IsValid)
            {
                FoodItem food = new FoodItem()
                {
                    FoodName = itemVM.FoodName,
                    EntryDate = itemVM.EntryDate,
                    IsAvailable = itemVM.IsAvailable,
                    Quantity = itemVM.Quantity,
                    Price =itemVM.Price
                };
                //Img
                string webroot = _he.WebRootPath;
                string folder = "ImagesFood";
                string imgFileName = Path.GetFileName(itemVM.Picturefile.FileName);
                string fileToWrite = Path.Combine(webroot, folder, imgFileName);

                using (var stream = new FileStream(fileToWrite, FileMode.Create))
                {
                    await itemVM.Picturefile.CopyToAsync(stream);
                    food.Picture = "/" + folder + "/" + imgFileName;
                }
                _context.FoodItems.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public async Task<IActionResult> Edit(int? id)
        {
            FoodItem foodItem = _context.FoodItems.First(x => x.FoodItemId == id);
            var OrderItemsList = await _context.OrderItems.Where(x => x.FoodItemId == id).Select(x => x.FoodItemId).ToListAsync();


            FoodItemVM foodItemVM = new FoodItemVM
            {
                //FoodItemId = foodItem.FoodItemId,
                FoodName = foodItem.FoodName,
                EntryDate = foodItem.EntryDate,
                IsAvailable = foodItem.IsAvailable,
                Quantity = foodItem.Quantity,
                OrderItemsList = OrderItemsList
            };
            return View(foodItemVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FoodItemVM itemVM)
        {

            if (ModelState.IsValid)
            {
                FoodItem food = new FoodItem()
                {
                    FoodItemId = itemVM.FoodItemId,
                    FoodName = itemVM.FoodName,
                    EntryDate = itemVM.EntryDate,
                    IsAvailable = itemVM.IsAvailable,
                    Quantity = itemVM.Quantity,
                    Price = itemVM.Price
                };
                //Img
                string webroot = _he.WebRootPath;
                string folder = "ImagesFood";
                string imgFileName = Path.GetFileName(itemVM.Picturefile.FileName);
                string fileToWrite = Path.Combine(webroot, folder, imgFileName);

                using (var stream = new FileStream(fileToWrite, FileMode.Create))
                {
                    await itemVM.Picturefile.CopyToAsync(stream);
                    food.Picture = "/" + folder + "/" + imgFileName;
                }
                _context.Entry(food).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            FoodItem foodItem = _context.FoodItems.First(x => x.FoodItemId == id);
                      
            _context.Entry(foodItem).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
