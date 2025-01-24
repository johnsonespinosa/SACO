﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("System")
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("IDGEN");

            modelBuilder.HasSequence("LOG_SEQUENCE");

            modelBuilder.HasSequence("Login_SEQ");

            modelBuilder.HasSequence("SEQ_CIRCULADOLC_ID")
                .IsCyclic();

            modelBuilder.HasSequence("SEQ_CUB_INADM")
                .IsCyclic();

            modelBuilder.HasSequence("SEQ_DOC_INAC")
                .IsCyclic();

            modelBuilder.HasSequence("SQ_Login");

            modelBuilder.Entity("Domain.Entities.Circulation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("character varying(4)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("character varying(25)");

                    b.HasKey("Id");

                    b.ToTable("Circulations", "System");

                    b.HasData(
                        new
                        {
                            Id = new Guid("59771e04-b80d-4900-b183-b754284ce431"),
                            Abbreviation = "PE",
                            Description = "PROHIBICION ENTRADA"
                        },
                        new
                        {
                            Id = new Guid("c2b7639c-406e-48df-acd0-6e52cfd56d7e"),
                            Abbreviation = "PS",
                            Description = "PROHIBICION SALIDA"
                        },
                        new
                        {
                            Id = new Guid("29ba72d1-aa90-4c52-8f30-1f6762822877"),
                            Abbreviation = "DET",
                            Description = "DETENCION"
                        },
                        new
                        {
                            Id = new Guid("9d6e1cb4-a7d6-478b-becc-f360fc7b5311"),
                            Abbreviation = "AE",
                            Description = "AVISO ENTRADA"
                        },
                        new
                        {
                            Id = new Guid("e91ec0f5-60af-4a48-9703-80f8ce60c860"),
                            Abbreviation = "AS",
                            Description = "AVISO SALIDA"
                        },
                        new
                        {
                            Id = new Guid("fe551100-6117-471d-8281-558c7c48355b"),
                            Abbreviation = "DG",
                            Description = "DROGAS"
                        },
                        new
                        {
                            Id = new Guid("7205bde0-3f3d-4104-98a6-4daa9247ed7d"),
                            Abbreviation = "PE",
                            Description = "AVISO ENT/SAL"
                        },
                        new
                        {
                            Id = new Guid("b9a75f68-d7fb-45ba-99d1-7708874286f2"),
                            Abbreviation = "PE",
                            Description = "INTERES MIGRATORIO"
                        },
                        new
                        {
                            Id = new Guid("8455d912-da80-4345-b753-03540175fccf"),
                            Abbreviation = "PE",
                            Description = "PERDIDA DOCUMENTO"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Citizenship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Citizenships", "System");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4e08c263-d201-4750-8999-86731c7aac3b"),
                            Abbreviation = "AFG",
                            Description = "Afgano/a"
                        },
                        new
                        {
                            Id = new Guid("f5450c55-cf4f-4320-a9f7-79be825377e3"),
                            Abbreviation = "DEU",
                            Description = "Alemán/a"
                        },
                        new
                        {
                            Id = new Guid("5cbbd5e1-9f53-440a-8455-1969f8ced713"),
                            Abbreviation = "SAU",
                            Description = "Árabe Saudita"
                        },
                        new
                        {
                            Id = new Guid("fc71c134-2c24-4a96-b157-c21834863ca5"),
                            Abbreviation = "ARG",
                            Description = "Argentino/a"
                        },
                        new
                        {
                            Id = new Guid("badadf6c-8641-4bda-b22c-d0f2892a6597"),
                            Abbreviation = "AUS",
                            Description = "Australiano/a"
                        },
                        new
                        {
                            Id = new Guid("6fc265ee-5418-4993-aed3-3c38caab73cf"),
                            Abbreviation = "BEL",
                            Description = "Belga"
                        },
                        new
                        {
                            Id = new Guid("59e9498f-8705-4e07-b6c0-d660807c492a"),
                            Abbreviation = "BOL",
                            Description = "Boliviano/a"
                        },
                        new
                        {
                            Id = new Guid("df739998-6b38-415a-95d5-ed01f2a99410"),
                            Abbreviation = "BRA",
                            Description = "Brasileño/a"
                        },
                        new
                        {
                            Id = new Guid("08655c54-6612-451c-95b2-3f5dc9fd7082"),
                            Abbreviation = "KHM",
                            Description = "Camboyano/a"
                        },
                        new
                        {
                            Id = new Guid("105bf6b7-76b0-4818-9034-fe5cc3fa2344"),
                            Abbreviation = "CAN",
                            Description = "Canadiense"
                        },
                        new
                        {
                            Id = new Guid("0468abb0-05fc-49d7-ab75-08f67b659b5c"),
                            Abbreviation = "CHL",
                            Description = "Chileno/a"
                        },
                        new
                        {
                            Id = new Guid("75f4ad7e-785d-4c8b-88c9-d62b11cfc83f"),
                            Abbreviation = "CHN",
                            Description = "Chino/a"
                        },
                        new
                        {
                            Id = new Guid("84539023-4198-498b-9772-1cca8a45c48b"),
                            Abbreviation = "COL",
                            Description = "Colombiano/a"
                        },
                        new
                        {
                            Id = new Guid("4fe63243-38f9-49b3-b21f-a1994270f15a"),
                            Abbreviation = "KOR",
                            Description = "Surcoreano/a"
                        },
                        new
                        {
                            Id = new Guid("0def6c60-782c-45d3-8c2f-1bf347e5edd7"),
                            Abbreviation = "CRI",
                            Description = "Costarricense"
                        },
                        new
                        {
                            Id = new Guid("d7f934c4-b51f-47bb-a612-458c36fc18c4"),
                            Abbreviation = "CUB",
                            Description = "Cubano/a"
                        },
                        new
                        {
                            Id = new Guid("93a90e33-7c04-4076-b104-ab4a0056f0e1"),
                            Abbreviation = "DNK",
                            Description = "Danés/danesa"
                        },
                        new
                        {
                            Id = new Guid("0b21aa1f-ab42-4860-9151-79513d7ef614"),
                            Abbreviation = "ECU",
                            Description = "Ecuatoriano/a"
                        },
                        new
                        {
                            Id = new Guid("0e41b9fb-a5e7-4622-bc42-87931d3e0efb"),
                            Abbreviation = "EGY",
                            Description = "Egipcio/a"
                        },
                        new
                        {
                            Id = new Guid("8c9e0759-4b5e-4be0-8630-b91079b6bc78"),
                            Abbreviation = "SLV",
                            Description = "Salvadoreño/a"
                        },
                        new
                        {
                            Id = new Guid("b2bb97fe-4cd6-4fb6-b584-17a0ee64d0b5"),
                            Abbreviation = "SCO",
                            Description = "Escocés/escocesa"
                        },
                        new
                        {
                            Id = new Guid("20892379-045e-4043-b3fd-a9bf6a7c2a5d"),
                            Abbreviation = "ESP",
                            Description = "Español/a"
                        },
                        new
                        {
                            Id = new Guid("1dcebbd0-9b9d-4f6d-969b-99055998487e"),
                            Abbreviation = "USA",
                            Description = "Estadounidense"
                        },
                        new
                        {
                            Id = new Guid("4cf91a09-3eb5-49a3-8e5c-9d6db8180575"),
                            Abbreviation = "EST",
                            Description = "Estonio/a"
                        },
                        new
                        {
                            Id = new Guid("87b7b89f-8282-4459-bba0-04431691889e"),
                            Abbreviation = "ETH",
                            Description = "Etíope"
                        },
                        new
                        {
                            Id = new Guid("e2d52059-0057-4a89-9ddc-e0303380f143"),
                            Abbreviation = "PHL",
                            Description = "Filipino/a"
                        },
                        new
                        {
                            Id = new Guid("6f237d15-32b1-4f68-a9a5-58b4b180ae7c"),
                            Abbreviation = "FIN",
                            Description = "Finlandés/finlandesa"
                        },
                        new
                        {
                            Id = new Guid("29686beb-2770-4114-9209-1cd39f5b922b"),
                            Abbreviation = "FRA",
                            Description = "Francés/francesa"
                        },
                        new
                        {
                            Id = new Guid("780d8f3f-ae48-4d27-a5e6-757f4cfa3e9c"),
                            Abbreviation = "WAL",
                            Description = "Galés/galesa"
                        },
                        new
                        {
                            Id = new Guid("a295b2aa-c388-4ec7-9491-6ea6b0c334c3"),
                            Abbreviation = "GRC",
                            Description = "Griego/a"
                        },
                        new
                        {
                            Id = new Guid("cec74dfe-3afb-4812-8aa0-41325751ef5e"),
                            Abbreviation = "GTM",
                            Description = "Guatemalteco/a"
                        },
                        new
                        {
                            Id = new Guid("e955c293-e60f-4a93-92c7-089b0acddf28"),
                            Abbreviation = "HTI",
                            Description = "Haitiano/a"
                        },
                        new
                        {
                            Id = new Guid("a16a2435-2837-47ca-b15b-dcf7cdf52a26"),
                            Abbreviation = "NLD",
                            Description = "Holandés/holandesa"
                        },
                        new
                        {
                            Id = new Guid("d6bca850-777a-48b9-b372-7137f9acccf9"),
                            Abbreviation = "HND",
                            Description = "Hondureño/a"
                        },
                        new
                        {
                            Id = new Guid("f80a03fc-6c1c-4870-adbe-8884324e2c5a"),
                            Abbreviation = "MYS",
                            Description = "Malayo/malaya"
                        },
                        new
                        {
                            Id = new Guid("ea73e8da-343a-4558-9534-04a237984c52"),
                            Abbreviation = "MAR",
                            Description = "Marroquí/marroquí"
                        },
                        new
                        {
                            Id = new Guid("3a2de1bf-a3ca-438b-8455-c20d2aca672f"),
                            Abbreviation = "MEX",
                            Description = "Mexicano/a"
                        },
                        new
                        {
                            Id = new Guid("fd8cc854-5061-4a6b-8ebc-18e2ef5dc138"),
                            Abbreviation = "NIC",
                            Description = "Nicaragüense"
                        },
                        new
                        {
                            Id = new Guid("5c993ce2-fce9-46bd-9379-5af321764ed0"),
                            Abbreviation = "NOR",
                            Description = "Noruego/noruega"
                        },
                        new
                        {
                            Id = new Guid("2f8004e2-b40e-4b98-a8f6-63ce8e28a18d"),
                            Abbreviation = "NZL",
                            Description = "Neozelandés/neozelandesa"
                        },
                        new
                        {
                            Id = new Guid("60734c94-0be1-4df4-9730-04c414eb0098"),
                            Abbreviation = "PAN",
                            Description = "Panameño/a"
                        },
                        new
                        {
                            Id = new Guid("287f863c-24ac-4b71-b76a-ef441e92b8ec"),
                            Abbreviation = "PRY",
                            Description = "Paraguayo/a"
                        },
                        new
                        {
                            Id = new Guid("d20cb6ff-ee71-4f7f-841a-f57eca10dd62"),
                            Abbreviation = "PER",
                            Description = "Peruano/a"
                        },
                        new
                        {
                            Id = new Guid("5dcb4065-aae8-4852-bc14-e73194896846"),
                            Abbreviation = "POL",
                            Description = "Polaco/polaca"
                        },
                        new
                        {
                            Id = new Guid("6a393284-0810-4acb-b74c-9ec1562175c7"),
                            Abbreviation = "PRT",
                            Description = "Portugués/portuguesa"
                        },
                        new
                        {
                            Id = new Guid("653d00ba-2986-4465-8420-52a179fa913b"),
                            Abbreviation = "PRI",
                            Description = "Puertorriqueño/puertorriqueña"
                        },
                        new
                        {
                            Id = new Guid("a2cce456-8ff2-4675-8439-8040801cc058"),
                            Abbreviation = "DOM",
                            Description = "Dominicano/dominicana"
                        },
                        new
                        {
                            Id = new Guid("a4f72bdc-f254-4202-9833-44980d92f31e"),
                            Abbreviation = "ROU",
                            Description = "Rumano/rumana"
                        },
                        new
                        {
                            Id = new Guid("a5fabe1c-a0cd-4167-80c6-d0b0c8ff11c8"),
                            Abbreviation = "RUS",
                            Description = "Ruso/rusa"
                        },
                        new
                        {
                            Id = new Guid("92de9667-4bd5-4cae-a475-4a68bc4ad799"),
                            Abbreviation = "SWE",
                            Description = "Sueco/sueca"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Expiration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Expirations", "System");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5e16c1f0-bd07-4c2b-a466-e10c495590cc"),
                            Description = "1"
                        },
                        new
                        {
                            Id = new Guid("b472871c-f0c6-40b5-8bda-3f68b11e33de"),
                            Description = "21"
                        },
                        new
                        {
                            Id = new Guid("0547dd97-383f-4153-b3ee-65f8a6ebed4e"),
                            Description = "90"
                        });
                });

            modelBuilder.Entity("Domain.Entities.OperationalCirculation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("BirthDate")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("CirculationDate")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CirculationType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Citizenship")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset?>("Deleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("ExpirationDate")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FileNumber")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("character varying(36)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("character varying(12)");

                    b.Property<string>("Instruction")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("character varying(40)");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName1")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName2")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Observations")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("Official")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Organ")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Phone1")
                        .IsRequired()
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("character varying(8)");

                    b.Property<string>("Phone2")
                        .IsRequired()
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("character varying(8)");

                    b.Property<string>("ReasonForCirculation")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("character varying(12)");

                    b.Property<string>("Section")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "BirthDate" }, "OperationalCirculation_BirthDate");

                    b.ToTable("OperationalCirculations", "System");

                    b.HasData(
                        new
                        {
                            Id = new Guid("004b8933-68d6-4c15-a5a7-a2dbda5e3821"),
                            BirthDate = new DateTimeOffset(new DateTime(1999, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            CirculationDate = new DateTimeOffset(new DateTime(2025, 1, 23, 15, 37, 38, 472, DateTimeKind.Unspecified).AddTicks(9890), new TimeSpan(0, -5, 0, 0, 0)),
                            CirculationType = "PROHIBICION ENTRADA",
                            Citizenship = "Cubano/a",
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatedBy = "test",
                            DeletedBy = "",
                            ExpirationDate = new DateTimeOffset(new DateTime(2025, 1, 23, 15, 37, 38, 472, DateTimeKind.Unspecified).AddTicks(9949), new TimeSpan(0, -5, 0, 0, 0)),
                            FileNumber = "70ee705f-8211-4fb8-8796-eb5a3695111f",
                            FirstName = "Pablo",
                            Instruction = "",
                            LastModifiedBy = "",
                            LastName1 = "Johnson",
                            LastName2 = "Espinosa",
                            Observations = "",
                            Official = "Enerieda",
                            Organ = "PTI",
                            Phone1 = "54541079",
                            Phone2 = "",
                            ReasonForCirculation = "",
                            SecondName = "Enrique",
                            Section = ""
                        });
                });

            modelBuilder.Entity("Domain.Entities.Organ", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Organs", "System");

                    b.HasData(
                        new
                        {
                            Id = new Guid("129e6704-0615-4ded-ad2f-27b89baea359"),
                            Name = "PTI"
                        },
                        new
                        {
                            Id = new Guid("fc9d20be-cad6-4df7-bffc-33e56c38a49f"),
                            Name = "DNA"
                        },
                        new
                        {
                            Id = new Guid("fbbcedf2-5595-4e87-bd3e-0f534b65d27e"),
                            Name = "DGI"
                        },
                        new
                        {
                            Id = new Guid("a69a0c22-83eb-4ac7-a7c8-21174165a508"),
                            Name = "DGCI"
                        },
                        new
                        {
                            Id = new Guid("91a0c454-7a70-4967-b416-ab48e34cd6c8"),
                            Name = "PNR"
                        },
                        new
                        {
                            Id = new Guid("5910a473-e699-44c9-a9c4-c74d816a785e"),
                            Name = "DCIM"
                        },
                        new
                        {
                            Id = new Guid("3c6f29d8-ed99-41d4-929f-6dd9413972a0"),
                            Name = "DICO"
                        },
                        new
                        {
                            Id = new Guid("1b6a9b08-eb51-4d58-bc21-67bc29f196c6"),
                            Name = "CII"
                        },
                        new
                        {
                            Id = new Guid("36d34613-41d0-4d99-880c-fd1f2134e9b8"),
                            Name = "DSP"
                        },
                        new
                        {
                            Id = new Guid("226b7f8e-6bf8-437e-9f5f-7bd618608e92"),
                            Name = "JEF-AR"
                        },
                        new
                        {
                            Id = new Guid("42ef9b88-164a-470b-9eff-c1287b0e658f"),
                            Name = "JEF-MB"
                        },
                        new
                        {
                            Id = new Guid("d8c3bbc3-a8aa-4da9-8259-7d4c71bf6458"),
                            Name = "JEF-PR"
                        },
                        new
                        {
                            Id = new Guid("2be74bac-4060-4fb2-8ea0-ccb26f40933b"),
                            Name = "JEF-MA"
                        },
                        new
                        {
                            Id = new Guid("8c7404e2-267f-4565-ac60-ba25e8acddf1"),
                            Name = "JEF-VC"
                        },
                        new
                        {
                            Id = new Guid("9fedcf2a-4fa7-4010-88b7-ce0388def5b5"),
                            Name = "JEF-CF"
                        },
                        new
                        {
                            Id = new Guid("5c2d580e-0251-49fd-8090-780fcc0f8e8a"),
                            Name = "JEF-SS"
                        },
                        new
                        {
                            Id = new Guid("2a24ed16-b780-4628-ad18-7786e9936b55"),
                            Name = "JEF-AV"
                        },
                        new
                        {
                            Id = new Guid("ffcb3acf-ede1-4718-be50-b360e8771f18"),
                            Name = "JEF-CM"
                        },
                        new
                        {
                            Id = new Guid("38c6a092-e3fd-40d8-965f-dc84403c6f94"),
                            Name = "JEF-GR"
                        },
                        new
                        {
                            Id = new Guid("f17d935a-ef6c-41fc-a772-38d86dd22f25"),
                            Name = "JEF-GU"
                        },
                        new
                        {
                            Id = new Guid("5404e96f-c246-4312-b808-9cf16a43bc86"),
                            Name = "JEF-TU"
                        },
                        new
                        {
                            Id = new Guid("a8fdc706-4204-4070-8238-612fe66641d5"),
                            Name = "JEF-ME"
                        },
                        new
                        {
                            Id = new Guid("3247f57b-1cb2-410e-a400-32d306816611"),
                            Name = "JEF-ME"
                        },
                        new
                        {
                            Id = new Guid("15d466cc-5761-4eea-9c3b-1c3daec9e8aa"),
                            Name = "JEF-HO"
                        },
                        new
                        {
                            Id = new Guid("2a9d999e-d7b5-4c22-9433-58f518a98418"),
                            Name = "JEF-SC"
                        },
                        new
                        {
                            Id = new Guid("8cc3d04e-2282-4a0f-9302-7a250b705157"),
                            Name = "PNR"
                        },
                        new
                        {
                            Id = new Guid("0db74239-28de-4231-950f-3a6eeb4e99aa"),
                            Name = "FGR"
                        },
                        new
                        {
                            Id = new Guid("a0185ca5-91e8-4970-8393-d58287404d58"),
                            Name = "TSP"
                        },
                        new
                        {
                            Id = new Guid("5cb762a6-994b-4fac-8473-4156a30e2b3a"),
                            Name = "FMTAR"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
