using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizLib_Model.Models;

namespace WizLib_DataAccess.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
            
        }

        //public DbSet<Category> Categories { get; set; }
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

            //BookDetails
            modelBuilder.Entity<Fluent_BookDetail>().HasKey(b => new { b.BookDetail_Id});
            modelBuilder.Entity<Fluent_BookDetail>().Property(b => b.NumberOfChapters).IsRequired();


            //Book
            modelBuilder.Entity<Fluent_Book>().HasKey(b => new { b.Book_Id });
            modelBuilder.Entity<Fluent_Book>().Property(b => b.Title).IsRequired();
            modelBuilder.Entity<Fluent_Book>().Property(b => b.ISBN).IsRequired().HasMaxLength(15);
            modelBuilder.Entity<Fluent_Book>().Property(b => b.Price).IsRequired();

            //one to one realtion between book and book detail
            modelBuilder.Entity<Fluent_Book>()
                .HasOne(b => b.Fluent_BookDetail)
                .WithOne(b => b.Fluent_Book).HasForeignKey<Fluent_Book>("BookDetail_Id");

            //one to one realtion between book and book publisher
            modelBuilder.Entity<Fluent_Book>()
                .HasOne(b => b.Fluent_Publisher)
                .WithMany(b => b.Fluent_Books).HasForeignKey(b=>b.Publisher_Id);

            //many to many relation

            modelBuilder.Entity<Fluent_BookAuthor>().HasKey(ba => new { ba.Author_Id, ba.Book_Id });

            modelBuilder.Entity<Fluent_BookAuthor>()
                .HasOne(b => b.Fluent_Book)
                .WithMany(b => b.Fluent_BookAuthors).HasForeignKey(b => b.Book_Id);

            modelBuilder.Entity<Fluent_BookAuthor>()
                .HasOne(b => b.Fluent_Author)
                .WithMany(b => b.Fluent_BookAuthors).HasForeignKey(b => b.Author_Id);

            //Author
            modelBuilder.Entity<Fluent_Author>().HasKey(a => new { a.Author_Id });
            modelBuilder.Entity<Fluent_Author>().Property(a => a.FirstName).IsRequired();
            modelBuilder.Entity<Fluent_Author>().Property(a => a.LastName).IsRequired();
            modelBuilder.Entity<Fluent_Author>().Ignore(a => a.FullName);

            //Publisher
            modelBuilder.Entity<Fluent_Publisher>().HasKey(a => new { a.Publisher_Id });
            modelBuilder.Entity<Fluent_Publisher>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Fluent_Publisher>().Property(a => a.Location).IsRequired();




        }

    }
}
