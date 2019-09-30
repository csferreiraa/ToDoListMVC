using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListMVC.Models;

namespace ToDoListMVC.Database
{
    public class ToDoListContext : DbContext
    {

        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options)
        {

        }

        public DbSet<Tasks> Tasks { get; set; }

    }
}
