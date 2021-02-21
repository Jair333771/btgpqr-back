﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using btg_pqr_back.Infrastructure.Context;

namespace btg_pqr_back.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210221062521_UpdatePqrActive")]
    partial class UpdatePqrActive
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("btg_pqr_back.Core.Entities.ClaimEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClaimId")
                        .HasColumnType("int");

                    b.Property<int>("PqrId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("claims");
                });

            modelBuilder.Entity("btg_pqr_back.Core.Entities.PqrEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateRequest")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateResponse")
                        .HasColumnType("datetime2");

                    b.Property<string>("MessageUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseAdmin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("pqrs");
                });
#pragma warning restore 612, 618
        }
    }
}
