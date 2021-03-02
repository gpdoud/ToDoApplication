using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using ToDoLib.Models;

namespace ToDoLib.Controllers {
    
    public class TodosController {
        private readonly TodoDbContext _context;

        public async Task<IEnumerable<Todo>> GetAll() {
            return await _context.Todos
                                .Include(x => x.Category)
                                .ToListAsync();
        }

        public async Task<Todo> GetByPK(int id) {
            return await _context.Todos
                                .Include(x => x.Category)
                                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Todo> Create(Todo todo) {
            if(todo == null) {
                throw new Exception("Input cannot be null");
            }
            if(todo.Id != 0) {
                throw new Exception("Input must have Id set to zero");
            }
            _context.Todos.Add(todo);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Create failed!");
            }
            return todo;
        }

        public async Task Change(Todo todo) {
            if(todo == null) {
                throw new Exception("Input cannot be null");
            }
            if(todo.Id == 0) {
                throw new Exception("Input must have Id greater than zero");
            }
            _context.Entry(todo).State = EntityState.Modified;
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Change failed!");
            }
            return;
        }

        public async Task<Todo> Remove(int id) {
            var todo = await _context.Todos.FindAsync(id);
            if(todo == null) {
                throw new Exception("Not found");
            }
            _context.Todos.Remove(todo);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Remove failed!");
            }
            return todo;
        }

        public TodosController() {
            _context = new TodoDbContext();
        }
    }
}
