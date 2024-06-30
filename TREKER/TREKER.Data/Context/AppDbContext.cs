using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities;
using TREKER.Core.Entities.UserModels;

namespace TREKER.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Trekking> Trekkings { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<TrekkingFacility> TrekkingFacilities { get; set; }
        public DbSet<TrekkingFeature> TrekkingFeatures { get; set; }
        public DbSet<TrekkingImage> TrekkingImages { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
