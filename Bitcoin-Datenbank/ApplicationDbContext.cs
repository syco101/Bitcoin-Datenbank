using AksicDavid_LB_M295.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace AksicDavid_LB_M295
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<BitcoinHolding> BitcoinHoldings { get; set; }
    }
}