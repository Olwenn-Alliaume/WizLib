﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizLib_DataAccess.FluentConfig;
using WizLib_Model.Models;

namespace WizLib_DataAccess.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<BookDetailsFromView> BookDetailsFromViews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        public DbSet<Fluent_Publisher> Fluent_Publishers { get; set; }
        public DbSet<Fluent_BookDetail> Fluent_BookDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //we configure fluent API


            //Category yable name and column name
            modelBuilder.Entity<Category>().ToTable("tbl_category");
            modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnName("CategoryName");


            //composite key
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.Author_Id , ba.Book_Id });

            modelBuilder.ApplyConfiguration(new FluentBookConfig());
            modelBuilder.ApplyConfiguration(new FluentBookAuthorConfig());
            modelBuilder.ApplyConfiguration(new FluentBookDetailConfig());
            modelBuilder.ApplyConfiguration(new FluentPublisherConfig());
            modelBuilder.ApplyConfiguration(new FluentAuthorConfig());

            modelBuilder.Entity<BookDetailsFromView>().HasNoKey().ToView("GetOnlyBookDetails");
        }

    }
}
