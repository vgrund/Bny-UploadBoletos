﻿// <auto-generated />
using System;
using Bny.UploadBoletos.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bny.UploadBoletos.Infra.Data.Migrations
{
    [DbContext(typeof(BoletosContext))]
    [Migration("20220516024935_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Bny.UploadBoletos.Domain.OperacoesAggregate.Operacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodigoAtivo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CodigoCliente")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Corretora")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdBolsa")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Mensagem")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("StatusBoleto")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal?>("ValorDesconto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorFinanceiro")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Operacoes");
                });
#pragma warning restore 612, 618
        }
    }
}