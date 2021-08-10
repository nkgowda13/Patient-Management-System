﻿// <auto-generated />
using System;
using IPTreatment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IPTreatment.Migrations
{
    [DbContext(typeof(TreatmentContext))]
    [Migration("20210802042201_m1")]
    partial class m1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IPTreatment.Models.PatientDetail", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Age")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ailment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TreatmentCommencementDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TreatmentPackageName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("IPTreatment.Models.TreatmentPlan", b =>
                {
                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<string>("PackageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("SpecialistName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TreatmentCommencementDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TreatmentEndDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PlanId");

                    b.ToTable("Plans");
                });
#pragma warning restore 612, 618
        }
    }
}
