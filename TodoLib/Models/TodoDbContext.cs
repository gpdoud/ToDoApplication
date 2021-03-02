using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoLib.Models {
    
    public class TodoDbContext : DbContext {

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            if(!builder.IsConfigured) {
                var connStr = "server=localhost\\sqlexpress;" +
                                "database=todo;" +
                                "trusted_connection=true;";
                builder.UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder) {

        }

        public TodoDbContext() { }
    }
}
