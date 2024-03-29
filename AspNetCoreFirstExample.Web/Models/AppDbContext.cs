﻿using Microsoft.EntityFrameworkCore;
using AspNetCoreFirstExample.Web.Models;

namespace AspNetCoreFirstExample.Web.Models
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        
        public DbSet<Visitor> Visitors{ get; set; }
        
        public DbSet<AspNetCoreFirstExample.Web.Models.Category> Category { get; set; }
    }
}
