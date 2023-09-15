using Microsoft.EntityFrameworkCore;
using System;

namespace KisiselAdresDefteri.Web.Models;

    public class AppDbContext:DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Kisiler> kisiler { get; set; }
}