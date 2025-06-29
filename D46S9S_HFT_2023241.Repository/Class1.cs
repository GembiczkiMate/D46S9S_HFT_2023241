﻿using D46S9S_HFT_2023241.Models;
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

            try
            {
                this.Database.EnsureCreated();
            }
            catch { }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
               builder
                .UseSqlite("Data Source = data.db");
            }
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Products)
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithOne(o => o.Products)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>().HasData(new Order[]
            {
                new Order{OrderId=1,UserId=1,ProductId=1,OrderDate=DateTime.Parse("2023.10.11") },
                new Order{OrderId=2,UserId=2,ProductId=2, OrderDate=DateTime.Parse("2023.11.01") },
                new Order{OrderId=3,UserId=3,ProductId=3, OrderDate=DateTime.Parse("2023.09.01") },
                new Order{OrderId=4,UserId=5,ProductId=5, OrderDate=DateTime.Parse("2023.07.21") },
                new Order{OrderId=5,UserId=5,ProductId=4, OrderDate=DateTime.Parse("2023.05.12") },
                new Order{OrderId=6,UserId=4,ProductId=2, OrderDate=DateTime.Parse("2023.11.11") },
                new Order{OrderId=7,UserId=2,ProductId=3, OrderDate=DateTime.Parse("2023.04.05") },
                new Order{OrderId=8,UserId=1,ProductId=1, OrderDate=DateTime.Parse("2023.03.01") },
                new Order{OrderId=9,UserId=3,ProductId=4, OrderDate=DateTime.Parse("2023.12.01") },
                new Order{OrderId=10,UserId=2,ProductId=5, OrderDate=DateTime.Parse("2023.08.30") },
                new Order{OrderId=11,UserId=5,ProductId=2, OrderDate=DateTime.Parse("2023.11.11") },
                new Order{OrderId=12,UserId=2,ProductId=5, OrderDate=DateTime.Parse("2023.11.11") },





            }
            );
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User{UserId=1,Username="Bob" },
                new User{UserId=2,Username="John"},
                new User{UserId=3,Username="Adam"},
                new User{UserId=4,Username="Bill"},
                new User{UserId=5,Username="Josh"},

            });
            modelBuilder.Entity<Product>().HasData(new Product[]
            {

                new Product{ProductId=1,ProductName="screws",Price= 200},
                new Product{ProductId=2, ProductName="bolts" ,Price = 600},
                new Product{ProductId=3, ProductName="hammer" ,Price = 600},
                new Product{ProductId=4, ProductName="screwdriver" ,Price = 600},
                new Product{ProductId=5, ProductName="nuts" ,Price = 600}


            });
        }


    }
}
