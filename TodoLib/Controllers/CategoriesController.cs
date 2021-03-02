
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using ToDoLib.Models;

namespace ToDoLib.Controllers {
    
    public class CategoriesController {
        private readonly TodoDbContext _context;

        public async Task<IEnumerable<Category>> GetAll() {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByPK(int id) {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> Create(Category category) {
            if(category == null) {
                throw new Exception("Input cannot be null");
            }
            if(category.Id != 0) {
                throw new Exception("Input must have Id set to zero");
            }
            _context.Categories.Add(category);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Create failed!");
            }
            return category;
        }

        public async Task Change(Category category) {
            if(category == null) {
                throw new Exception("Input cannot be null");
            }
            if(category.Id == 0) {
                throw new Exception("Input must have Id greater than zero");
            }
            _context.Entry(category).State = EntityState.Modified;
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Change failed!");
            }
            return;
        }

        public async Task<Category> Remove(int id) {
            var category = await _context.Categories.FindAsync(id);
            if(category == null) {
                throw new Exception("Not found");
            }
            _context.Categories.Remove(category);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Remove failed!");
            }
            return category;
        }

        public CategoriesController() {
            _context = new TodoDbContext();
        }
    }
}
