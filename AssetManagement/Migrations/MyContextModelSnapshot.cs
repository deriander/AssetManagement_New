﻿// <auto-generated />
using System;
using AssetManagement.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AssetManagement.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AssetManagement.Model.Borrow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Approval_1");

                    b.Property<bool>("Approval_2");

                    b.Property<DateTimeOffset>("Borrow_Date");

                    b.Property<int>("Item_Id");

                    b.Property<string>("Status_Approval");

                    b.Property<int>("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("Item_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("TB_T_Borrow");
                });

            modelBuilder.Entity("AssetManagement.Model.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand");

                    b.Property<string>("Cpu");

                    b.Property<DateTimeOffset>("Create_Date");

                    b.Property<DateTimeOffset?>("Delete_Date");

                    b.Property<string>("Display");

                    b.Property<string>("Gpu");

                    b.Property<bool>("Is_Delete");

                    b.Property<string>("Os");

                    b.Property<string>("Ram");

                    b.Property<bool>("Status");

                    b.Property<string>("Storage");

                    b.Property<DateTimeOffset?>("Update_Date");

                    b.HasKey("Id");

                    b.ToTable("TB_M_Item");
                });

            modelBuilder.Entity("AssetManagement.Model.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Approval_1");

                    b.Property<bool>("Approval_2");

                    b.Property<string>("Brand");

                    b.Property<string>("Cpu");

                    b.Property<string>("Display");

                    b.Property<string>("Gpu");

                    b.Property<string>("Os");

                    b.Property<string>("Ram");

                    b.Property<DateTimeOffset>("Request_Date");

                    b.Property<string>("Status_Approval");

                    b.Property<string>("Storage");

                    b.Property<int>("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("User_Id");

                    b.ToTable("TB_T_Request");
                });

            modelBuilder.Entity("AssetManagement.Model.Return", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Condition");

                    b.Property<int>("Item_Id");

                    b.Property<DateTimeOffset>("Return_Date");

                    b.Property<string>("Status");

                    b.Property<int>("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("Item_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("TB_T_Return");
                });

            modelBuilder.Entity("AssetManagement.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<DateTime>("Birth_Date");

                    b.Property<string>("Email");

                    b.Property<string>("First_Name");

                    b.Property<string>("Last_Name");

                    b.Property<string>("Password");

                    b.Property<string>("Phone_Number");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.ToTable("TB_M_User");
                });

            modelBuilder.Entity("AssetManagement.Model.Borrow", b =>
                {
                    b.HasOne("AssetManagement.Model.Item", "Item")
                        .WithMany()
                        .HasForeignKey("Item_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AssetManagement.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AssetManagement.Model.Request", b =>
                {
                    b.HasOne("AssetManagement.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AssetManagement.Model.Return", b =>
                {
                    b.HasOne("AssetManagement.Model.Item", "Item")
                        .WithMany()
                        .HasForeignKey("Item_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AssetManagement.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
