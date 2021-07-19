﻿// <auto-generated />
using System;
using Folio3.Sbp.Data.AuditLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Folio3.DotNet.Sbp.Data.Migrations
{
    [DbContext(typeof(AuditLogDbContext))]
    [Migration("20210716111541_AddAuditEntities")]
    partial class AddAuditEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Folio3.DotNet.Sbp.Data.AuditLogging.Entities.AuditLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EntityState")
                        .HasColumnType("int");

                    b.Property<string>("RecordId")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("RecordId");

                    b.HasIndex("TableName");

                    b.HasIndex("TimeStamp");

                    b.HasIndex("UserEmail");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("Folio3.DotNet.Sbp.Data.AuditLogging.Entities.AuditLogChange", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AuditLogId")
                        .HasColumnType("bigint");

                    b.Property<string>("Current")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Original")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Property")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuditLogId");

                    b.ToTable("AuditLogChanges");
                });

            modelBuilder.Entity("Folio3.DotNet.Sbp.Data.AuditLogging.Entities.AuditLogChange", b =>
                {
                    b.HasOne("Folio3.DotNet.Sbp.Data.AuditLogging.Entities.AuditLog", "AuditLog")
                        .WithMany("AuditLogChanges")
                        .HasForeignKey("AuditLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuditLog");
                });

            modelBuilder.Entity("Folio3.DotNet.Sbp.Data.AuditLogging.Entities.AuditLog", b =>
                {
                    b.Navigation("AuditLogChanges");
                });
#pragma warning restore 612, 618
        }
    }
}