using D46S9S_HFT_2023241.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;

namespace D46S9S_HFT_2023241.Repository
{
    public class OrderDB : DbContext
    {
        public DbSet<Order> orders { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
        public OrderDB()
        {

            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gembiczki Máté\Source\Repos\D46S9S_HFT_2023241\D46S9S_HFT_2023241.Repository\Database1.mdf;Integrated Security=True";
                builder
                .UseSqlServer(conn);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(order => order
            .HasOne<User>().WithMany().HasForeignKey(order => order.OrderId).OnDelete(DeleteBehavior.Cascade));
            modelBuilder.Entity<Order>(order => order
            .HasOne<Product>().WithMany().HasForeignKey(order => order.ProductId).OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Order>().HasData(new Order[]
            {
                new Order{OrderId=1,UserId=1,ProductId=1,OrderDate=DateTime.Parse("2023.10.11") },
                new Order{OrderId=2,UserId=2,ProductId=2, OrderDate=DateTime.Parse("2023.11.01") }



            }
            );
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User{UserId=1,Username="Bob" },
                new User{UserId=2,Username="John" }
            });
            modelBuilder.Entity<Product>().HasData(new Product[]
            {

                new Product{ProductId=1,ProductName="alma"},
                new Product{ProductId=2, ProductName="körte"}
            });
        }


    }
}
