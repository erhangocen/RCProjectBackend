using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class RCProjectDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=RCProjectDB;Trusted_Connection=true");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<ProfilePhoto> ProfilePhotos { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Contact> Contacts { get; set;}
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<BidImage> BidImages { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Favorite> Favorites { get; set; } 
        public DbSet<Token> Tokens { get; set; }
        public DbSet<TokenOperation> TokenOperations { get; set; }
    }
}