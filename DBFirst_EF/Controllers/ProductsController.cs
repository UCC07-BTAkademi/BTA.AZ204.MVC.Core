using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBFirst_EF.Models;

namespace DBFirst_EF.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthwindContext _context;

        public ProductsController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            // LINQ : Language Integrated Query

            var northwindContext = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier);

            return View(await northwindContext.ToListAsync());

            // LINQ : Previous

            //var products= from p in _context.Products
            //              join c in _context.Categories on p.CategoryId equals c.CategoryId
            //              join s in _context.Suppliers on p.SupplierId equals s.SupplierId
            //              select new
            //              {
            //                ProductName = p.ProductName,
            //                CategoryName = c.CategoryName,  
            //                QuantityPerUnit = p.QuantityPerUnit,
            //                  UnitPrice = p.UnitPrice,
            //                  UnitsInStock = p.UnitsInStock,
            //                  UnitsOnOrder = p.UnitsOnOrder,
            //                  ReorderLevel = p.ReorderLevel,
            //                  Discontinued = p.Discontinued,
            //                  Category = p.CategoryId,
            //                  Supplier = p.SupplierId,
            //                  SupplierName = s.CompanyName
            //              };

            //return View(await products);

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", product.SupplierId);
            return View(product);
        }

        [NonAction]
        private dynamic ToCategoriesSelectList(DbSet<Category> categories, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>(); // view tarafına gidecek kategori listesi

            foreach (var item in categories)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.CategoryName,
                    Value = item.CategoryId.ToString()
                });
            }

            return new SelectList(list, "Value", "Text");

        }

        [NonAction]
        private dynamic ToSuppliersSelectList(DbSet<Supplier> suppliers, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>(); // view tarafına gidecek kategori listesi

            foreach (var item in suppliers)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.CompanyName,
                    Value = item.SupplierId.ToString()
                });
            }

            return new SelectList(list, "Value", "Text");

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // edit sayfasında dropdown içinde gelen id değerlerinin gerçek tanımlarını öğrenmek.
            var categories = _context.Categories.ToList(); // categories tablosunu bir liste haline getiriyor.

            if (categories != null)
            {
                ViewBag.CategoryList = ToCategoriesSelectList(_context.Categories, "CategoryId", "CategoryName");
            }

            // supplier dropdown için (viewdaki)
            var suppliers = _context.Suppliers.ToList(); // categories tablosunu bir liste haline getiriyor.

            if (suppliers != null)
            {
                ViewBag.SupplierList = ToSuppliersSelectList(_context.Suppliers, "SupplierId", "CompanyName");
            }







            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id); // gelen id parametresine göre products tableından eşleşen kayıdı buluyor.

            if (product == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", product.SupplierId);
            
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", product.SupplierId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'NorthwindContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
