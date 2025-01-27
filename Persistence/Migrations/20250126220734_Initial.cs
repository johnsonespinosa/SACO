using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "System");

            migrationBuilder.CreateSequence(
                name: "IDGEN",
                schema: "System");

            migrationBuilder.CreateSequence(
                name: "LOG_SEQUENCE",
                schema: "System");

            migrationBuilder.CreateSequence(
                name: "Login_SEQ",
                schema: "System");

            migrationBuilder.CreateSequence(
                name: "SEQ_CIRCULADOLC_ID",
                schema: "System",
                cyclic: true);

            migrationBuilder.CreateSequence(
                name: "SEQ_CUB_INADM",
                schema: "System",
                cyclic: true);

            migrationBuilder.CreateSequence(
                name: "SEQ_DOC_INAC",
                schema: "System",
                cyclic: true);

            migrationBuilder.CreateSequence(
                name: "SQ_Login",
                schema: "System");

            migrationBuilder.CreateTable(
                name: "CirculationTypes",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    Description = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CirculationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Citizenships",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(3)", unicode: false, maxLength: 3, nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizenships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expirations",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expirations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organs",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Circulations",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SecondName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CitizenshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    CirculationTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CirculationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpirationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganId = table.Column<Guid>(type: "uuid", nullable: false),
                    Section = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Official = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Phone1 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Phone2 = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Instruction = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Observations = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ReasonForCirculation = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: false),
                    Deleted = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Circulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Circulations_CirculationTypes_CirculationTypeId",
                        column: x => x.CirculationTypeId,
                        principalSchema: "System",
                        principalTable: "CirculationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Circulations_Citizenships_CitizenshipId",
                        column: x => x.CitizenshipId,
                        principalSchema: "System",
                        principalTable: "Citizenships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Circulations_Expirations_ExpirationId",
                        column: x => x.ExpirationId,
                        principalSchema: "System",
                        principalTable: "Expirations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Circulations_Organs_OrganId",
                        column: x => x.OrganId,
                        principalSchema: "System",
                        principalTable: "Organs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "System",
                table: "CirculationTypes",
                columns: new[] { "Id", "Abbreviation", "Description" },
                values: new object[,]
                {
                    { new Guid("109f0cd1-28a1-4cfe-aec9-db26c030f737"), "PE", "INTERES MIGRATORIO" },
                    { new Guid("1f610708-d1ae-4b2b-a7f4-46bacff268bd"), "DG", "DROGAS" },
                    { new Guid("5ab8562c-2de5-4c96-a673-20a7b32fefbe"), "PS", "PROHIBICION SALIDA" },
                    { new Guid("5b200913-2ef1-404a-9bb5-d10de63eb10f"), "PE", "AVISO ENT/SAL" },
                    { new Guid("79489eae-62fa-4278-879b-e441a26bc572"), "DET", "DETENCION" },
                    { new Guid("9cb20464-174f-490c-950a-f0a433033d12"), "PE", "PROHIBICION ENTRADA" },
                    { new Guid("acbc1ddb-e8ee-452e-b8e2-57e0c3631557"), "PE", "PERDIDA DOCUMENTO" },
                    { new Guid("bca08ff3-f585-44b5-addc-d27399bf6dbd"), "AS", "AVISO SALIDA" },
                    { new Guid("c6f5c452-734b-4603-a9b1-10171b2d50d5"), "AE", "AVISO ENTRADA" }
                });

            migrationBuilder.InsertData(
                schema: "System",
                table: "Citizenships",
                columns: new[] { "Id", "Abbreviation", "Description" },
                values: new object[,]
                {
                    { new Guid("057656cd-a89e-4ee4-937b-40b5b1d4264c"), "PER", "Peruano/a" },
                    { new Guid("05d09da6-5fac-4cc0-8358-4732e73bc73c"), "DNK", "Danés/danesa" },
                    { new Guid("09cfb083-9edb-453d-90ff-87c0aa4f76fb"), "MAR", "Marroquí/marroquí" },
                    { new Guid("1fd12829-4bb7-4889-8542-6176bc6a974c"), "COL", "Colombiano/a" },
                    { new Guid("21ef8495-604c-4b0d-89a0-47246d80b633"), "KHM", "Camboyano/a" },
                    { new Guid("2304e933-78f2-4006-81dc-978eb33ed616"), "NIC", "Nicaragüense" },
                    { new Guid("2473cf73-303e-4177-b057-f363f95cfef0"), "ESP", "Español/a" },
                    { new Guid("28e193e6-c06c-4ba1-9d0f-06c8d960889e"), "KOR", "Surcoreano/a" },
                    { new Guid("302ddb87-874b-45a0-9350-3898a8aac678"), "RUS", "Ruso/rusa" },
                    { new Guid("3130862f-fe23-4c39-8290-af08dcea08a1"), "FRA", "Francés/francesa" },
                    { new Guid("339277e2-38aa-467f-9601-17b331cfd90f"), "SCO", "Escocés/escocesa" },
                    { new Guid("3830c231-4fe2-4dd9-8abd-8a4b220bb765"), "NLD", "Holandés/holandesa" },
                    { new Guid("3c7e8c7b-420e-42f3-a234-afb2e8444ca5"), "MEX", "Mexicano/a" },
                    { new Guid("42972d1d-eecc-45ad-816e-107c08a66a8d"), "HND", "Hondureño/a" },
                    { new Guid("46cf1d6d-3df5-41f8-866a-2a21891ab0d8"), "SLV", "Salvadoreño/a" },
                    { new Guid("4c2aa7a2-ac94-4501-a36b-d17ca349ea8f"), "MYS", "Malayo/malaya" },
                    { new Guid("4c5766ce-000b-4b4c-a4e5-f2745230d927"), "AUS", "Australiano/a" },
                    { new Guid("5fcc300d-f86d-41c7-9c20-8e96750fe50e"), "NOR", "Noruego/noruega" },
                    { new Guid("65b12e34-6f79-47e3-95af-178d8485d77b"), "PAN", "Panameño/a" },
                    { new Guid("6b28addb-20a9-4f2e-a8f8-647dfd2ee300"), "BOL", "Boliviano/a" },
                    { new Guid("70bf3d60-f937-4fb2-a9f4-fdca76432b20"), "NZL", "Neozelandés/neozelandesa" },
                    { new Guid("760c93a1-19fa-401e-90a3-4e6a3540a9e9"), "AFG", "Afgano/a" },
                    { new Guid("7b7d05f1-fc8e-4380-a0b0-81c753ad8e17"), "DOM", "Dominicano/dominicana" },
                    { new Guid("7de38f35-fb73-4db2-afe4-b0ed3a2b5c0e"), "ECU", "Ecuatoriano/a" },
                    { new Guid("81233b8e-5e5f-4796-aef0-8981f446c023"), "PRY", "Paraguayo/a" },
                    { new Guid("8923e927-8195-4858-8453-d9dd29831c7b"), "WAL", "Galés/galesa" },
                    { new Guid("95311a20-3593-4e82-b982-77dde7971141"), "BEL", "Belga" },
                    { new Guid("9f98fb4f-4ce9-4a23-b166-11b37a4006eb"), "EGY", "Egipcio/a" },
                    { new Guid("a45b93d3-78b7-487d-ba06-4d40f0a65a71"), "DEU", "Alemán/a" },
                    { new Guid("a4dcad00-69a8-4fe7-8189-0a847ddff42f"), "GTM", "Guatemalteco/a" },
                    { new Guid("aa2fc645-89c3-4244-8dea-91a9fcd4acd5"), "GRC", "Griego/a" },
                    { new Guid("ab6e050f-02fc-4226-9ef0-886b6533dc6b"), "SWE", "Sueco/sueca" },
                    { new Guid("b8901589-f599-4325-9cfc-5e1b69395f26"), "HTI", "Haitiano/a" },
                    { new Guid("bc515ba8-c7ab-4a64-915a-0085240d2b9c"), "PRT", "Portugués/portuguesa" },
                    { new Guid("c1780c40-cc95-4f0a-a423-1ef4a038def9"), "FIN", "Finlandés/finlandesa" },
                    { new Guid("c4a531f8-7fb2-4d2d-83d1-069d8e110ff5"), "EST", "Estonio/a" },
                    { new Guid("c6f686c2-8fac-4c26-8ed4-4216ebe17baa"), "CUB", "Cubano/a" },
                    { new Guid("cfe868f6-b86c-4615-b945-0c625f7cb579"), "BRA", "Brasileño/a" },
                    { new Guid("d7053f1d-fb86-4981-a9c4-5bff3a4a819b"), "CAN", "Canadiense" },
                    { new Guid("deea262c-cde1-4e43-b0ca-8f5d97dbe3cf"), "ROU", "Rumano/rumana" },
                    { new Guid("e0532d85-4842-44b1-9e3a-9df292a85cbb"), "CHN", "Chino/a" },
                    { new Guid("e491b15b-4f30-40b7-967c-1e54225195d4"), "ETH", "Etíope" },
                    { new Guid("e4f82a02-0c1f-4800-9bf9-d18f89dbf6a0"), "SAU", "Árabe Saudita" },
                    { new Guid("e82e2453-881d-4175-bbbd-50a9fc0eb264"), "PRI", "Puertorriqueño/puertorriqueña" },
                    { new Guid("e9bc1ac5-8f40-4a98-855a-3982d8adbb61"), "POL", "Polaco/polaca" },
                    { new Guid("ea69f972-6187-4276-9783-44037cd9121b"), "ARG", "Argentino/a" },
                    { new Guid("eb817c9d-6abe-4953-996d-7d4d8530d8f7"), "CHL", "Chileno/a" },
                    { new Guid("edb33ef0-0ebc-40c2-b8a6-533e859b049a"), "CRI", "Costarricense" },
                    { new Guid("f756e3bd-3847-4071-9071-9ecda63ccc6d"), "PHL", "Filipino/a" },
                    { new Guid("f9b77cae-de8d-4c95-87f1-1a4eead0340f"), "USA", "Estadounidense" }
                });

            migrationBuilder.InsertData(
                schema: "System",
                table: "Expirations",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("11ab35ff-cb80-493a-bfc6-591404f064cc"), "90" },
                    { new Guid("74ba8f43-9618-48eb-a5d0-94b7ccdbbea8"), "21" },
                    { new Guid("96dc2d4d-6fdc-4af1-ba61-1b3eb9714311"), "1" }
                });

            migrationBuilder.InsertData(
                schema: "System",
                table: "Organs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("03fb1cf0-1f09-474f-8b27-b12af623d207"), "JEF-CM" },
                    { new Guid("0d830f46-06e6-4f8f-943b-f7308a00005f"), "JEF-AV" },
                    { new Guid("15fb9d84-3d73-470d-a6fb-d3d2e843cfbe"), "JEF-TU" },
                    { new Guid("1ecdfc3e-8514-4449-a905-93ec51c3de6b"), "JEF-ME" },
                    { new Guid("1f609388-dbe5-48a4-9f9c-cfbbe5adeeed"), "DGI" },
                    { new Guid("26094401-cee2-4fb1-9f04-3462995fef7e"), "JEF-AR" },
                    { new Guid("3185e85e-12a0-4275-822e-b7e4a2e9b9aa"), "JEF-PR" },
                    { new Guid("363b4f69-d24c-4fdd-9b0b-4fc10266c85c"), "JEF-VC" },
                    { new Guid("3df3411c-5ac0-4de9-9826-86182ce2ec8a"), "DNA" },
                    { new Guid("44b7e3af-0bce-420e-8b97-cccb1e1dbb75"), "JEF-MA" },
                    { new Guid("45a08086-1dc9-4060-b872-21d8ee3db1e1"), "JEF-SC" },
                    { new Guid("487c22e9-6e73-43d2-8561-bbb4ba3b2be5"), "JEF-SS" },
                    { new Guid("4f69a27d-41d8-4cc5-bd21-cc2c9a31e62a"), "JEF-GU" },
                    { new Guid("642f674f-1989-4698-871d-481709f54ae9"), "DSP" },
                    { new Guid("6d4e0d7d-1bdb-41ee-b6b3-498df19d1f47"), "JEF-MB" },
                    { new Guid("6e89b8de-0de8-4e5f-8450-427ac74a53b2"), "FGR" },
                    { new Guid("705bb7db-3405-4a4d-9090-5a13956fdf60"), "DICO" },
                    { new Guid("85a4e9dd-a6a4-46e4-a4d9-8efdfa1e6de2"), "DGCI" },
                    { new Guid("8d31738d-2a1c-4bdd-9e3d-fd4081155091"), "JEF-GR" },
                    { new Guid("963729bc-a83e-4558-b5fb-c98635833e6f"), "JEF-HO" },
                    { new Guid("9e487360-d593-44f0-ac47-79123819e20e"), "PNR" },
                    { new Guid("a2f5bd59-3ad8-4f31-8987-e85d2f930d47"), "FMTAR" },
                    { new Guid("a9c35fdc-205a-4941-9860-5face7f17d60"), "TSP" },
                    { new Guid("b91fe38f-5d2f-42de-a134-95854b0e5817"), "JEF-ME" },
                    { new Guid("bfe91010-bf36-4739-a5f8-4ad1d365d884"), "DCIM" },
                    { new Guid("d722bebd-092c-45f0-9a6c-64540ab5ff8d"), "PNR" },
                    { new Guid("d9258bd5-a2f0-4288-9998-0ab5bfb89cc4"), "JEF-CF" },
                    { new Guid("db515b03-98d4-4b24-b2ef-74e6c4c618f0"), "PTI" },
                    { new Guid("e51ebdd7-e30c-48ff-bdb2-d4921f890b11"), "CII" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Circulations_BirthDate",
                schema: "System",
                table: "Circulations",
                column: "BirthDate");

            migrationBuilder.CreateIndex(
                name: "IX_Circulations_CirculationTypeId",
                schema: "System",
                table: "Circulations",
                column: "CirculationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Circulations_CitizenshipId",
                schema: "System",
                table: "Circulations",
                column: "CitizenshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Circulations_ExpirationId",
                schema: "System",
                table: "Circulations",
                column: "ExpirationId");

            migrationBuilder.CreateIndex(
                name: "IX_Circulations_FirstName",
                schema: "System",
                table: "Circulations",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Circulations_LastName1",
                schema: "System",
                table: "Circulations",
                column: "LastName1");

            migrationBuilder.CreateIndex(
                name: "IX_Circulations_LastName2",
                schema: "System",
                table: "Circulations",
                column: "LastName2");

            migrationBuilder.CreateIndex(
                name: "IX_Circulations_OrganId",
                schema: "System",
                table: "Circulations",
                column: "OrganId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Circulations",
                schema: "System");

            migrationBuilder.DropTable(
                name: "CirculationTypes",
                schema: "System");

            migrationBuilder.DropTable(
                name: "Citizenships",
                schema: "System");

            migrationBuilder.DropTable(
                name: "Expirations",
                schema: "System");

            migrationBuilder.DropTable(
                name: "Organs",
                schema: "System");

            migrationBuilder.DropSequence(
                name: "IDGEN",
                schema: "System");

            migrationBuilder.DropSequence(
                name: "LOG_SEQUENCE",
                schema: "System");

            migrationBuilder.DropSequence(
                name: "Login_SEQ",
                schema: "System");

            migrationBuilder.DropSequence(
                name: "SEQ_CIRCULADOLC_ID",
                schema: "System");

            migrationBuilder.DropSequence(
                name: "SEQ_CUB_INADM",
                schema: "System");

            migrationBuilder.DropSequence(
                name: "SEQ_DOC_INAC",
                schema: "System");

            migrationBuilder.DropSequence(
                name: "SQ_Login",
                schema: "System");
        }
    }
}
