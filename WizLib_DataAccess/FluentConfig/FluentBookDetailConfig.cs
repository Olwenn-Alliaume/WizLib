﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookDetailConfig : IEntityTypeConfiguration<Fluent_BookDetail>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookDetail> modelBuilder)
        {
            //Name of Table

            //BookDetails
            modelBuilder.HasKey(b => new { b.BookDetail_Id });
            modelBuilder.Property(b => b.NumberOfChapters).IsRequired();
        }
    }
}
