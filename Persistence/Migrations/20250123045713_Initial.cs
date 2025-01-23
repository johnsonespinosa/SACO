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
                name: "Circulations",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(4)", unicode: false, maxLength: 4, nullable: false),
                    Description = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Circulations", x => x.Id);
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
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expirations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationalCirculations",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileNumber = table.Column<string>(type: "character varying(36)", unicode: false, maxLength: 36, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(12)", unicode: false, maxLength: 12, nullable: false),
                    SecondName = table.Column<string>(type: "character varying(12)", unicode: false, maxLength: 12, nullable: false),
                    LastName1 = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName2 = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", unicode: false, maxLength: 6, nullable: false),
                    Citizenship = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    CirculationType = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    CirculationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", unicode: false, maxLength: 6, nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", unicode: false, maxLength: 6, nullable: false),
                    Organ = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Section = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Official = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Phone1 = table.Column<string>(type: "character varying(8)", unicode: false, maxLength: 8, nullable: false),
                    Phone2 = table.Column<string>(type: "character varying(8)", unicode: false, maxLength: 8, nullable: false),
                    Instruction = table.Column<string>(type: "character varying(40)", unicode: false, maxLength: 40, nullable: false),
                    Observations = table.Column<string>(type: "character varying(40)", unicode: false, maxLength: 40, nullable: false),
                    ReasonForCirculation = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Deleted = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationalCirculations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organs",
                schema: "System",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organs", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "System",
                table: "Circulations",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[,]
                {
                    { new Guid("1bdd31bc-f387-411e-80ff-32a40f02e509"), "AE", "AVISO ENTRADA" },
                    { new Guid("2d198bd0-f9dd-4f57-8729-11eeaa628ca9"), "PE", "INTERES MIGRATORIO" },
                    { new Guid("716c8e89-8f87-43c6-83f2-3a536710a1ae"), "PS", "PROHIBICION SALIDA" },
                    { new Guid("719cec57-9fd0-4e3e-8bee-f41100668075"), "PE", "PROHIBICION ENTRADA" },
                    { new Guid("8ccee024-1fb2-4066-b111-86c7217c4bb2"), "PE", "PERDIDA DOCUMENTO" },
                    { new Guid("b95f4ea3-2e2f-478d-ae9c-41ac35bbe05b"), "AS", "AVISO SALIDA" },
                    { new Guid("baa5a92d-0029-4f00-9689-31212ccd7020"), "PE", "AVISO ENT/SAL" },
                    { new Guid("c6aa80b2-14c7-446c-90ad-1eb5619e4b99"), "DET", "DETENCION" },
                    { new Guid("cced3da1-57bd-4223-8304-f8fbdf976052"), "DG", "DROGAS" }
                });

            migrationBuilder.InsertData(
                schema: "System",
                table: "Citizenships",
                columns: new[] { "Id", "Abbreviation", "Description" },
                values: new object[,]
                {
                    { new Guid("005c6651-e6c2-47d5-bc73-13e79d795517"), "DEU", "Alemán/a" },
                    { new Guid("03d61cca-1513-46db-958c-24fcf90adf27"), "CHN", "Chino/a" },
                    { new Guid("07406b70-a8d4-42dc-9a01-45819fa3ea91"), "ROU", "Rumano/rumana" },
                    { new Guid("0a55d7ba-c5b3-4584-b70d-1b9dd9c5a131"), "SAU", "Árabe Saudita" },
                    { new Guid("228f3508-6b19-4385-aa0a-31e7bc072839"), "COL", "Colombiano/a" },
                    { new Guid("25d97325-d9b4-4879-aeff-93d91bf6e4be"), "SCO", "Escocés/escocesa" },
                    { new Guid("2b1f556b-1991-4f57-815e-6d5dac62b3f3"), "NLD", "Holandés/holandesa" },
                    { new Guid("2d017839-f2b1-4b8c-b823-b84b621f78b7"), "USA", "Estadounidense" },
                    { new Guid("2d917ae9-994a-4135-aed6-b42b3abf5054"), "SLV", "Salvadoreño/a" },
                    { new Guid("35f2d774-7cde-4c64-a26c-fe06dd9aa2a2"), "NIC", "Nicaragüense" },
                    { new Guid("3cc97ead-ce62-4f55-8cb9-2d40f01bb18e"), "FIN", "Finlandés/finlandesa" },
                    { new Guid("46008117-8e96-4ab2-b536-af27dc44a94e"), "PHL", "Filipino/a" },
                    { new Guid("4e27a64f-24c2-4ef4-a82b-714e589f32b9"), "HND", "Hondureño/a" },
                    { new Guid("5bc7dadd-104a-43a1-9c86-1359f582fad1"), "DNK", "Danés/danesa" },
                    { new Guid("606740fc-a2ea-49be-884b-70cf9cabcceb"), "CAN", "Canadiense" },
                    { new Guid("62285431-878d-453c-a1a0-9480322e13b2"), "BEL", "Belga" },
                    { new Guid("6411072c-8a85-4ff7-a89b-b567d8d47183"), "NOR", "Noruego/noruega" },
                    { new Guid("66e03d93-7eb8-4974-a5e5-dfa91f1dc0bd"), "PRI", "Puertorriqueño/puertorriqueña" },
                    { new Guid("6facfa8e-6579-4dcc-8a8b-6d30660b1af2"), "BRA", "Brasileño/a" },
                    { new Guid("724e233e-e0b3-4d89-bb07-b7dc880acadb"), "BOL", "Boliviano/a" },
                    { new Guid("74263ac6-4b0f-4d94-b2c5-c7c2e872530e"), "ETH", "Etíope" },
                    { new Guid("74810d29-4677-434d-b1a2-972d170b283d"), "GTM", "Guatemalteco/a" },
                    { new Guid("81d7db5a-7947-429f-8ac9-d0c21b95d9f9"), "DOM", "Dominicano/dominicana" },
                    { new Guid("91bde1c9-98d1-493c-9a33-00b012161fe9"), "POL", "Polaco/polaca" },
                    { new Guid("96b03b07-e00a-4efa-915a-72008d8a05da"), "ECU", "Ecuatoriano/a" },
                    { new Guid("9abca330-29d7-495c-b3fb-bb254502a569"), "CUB", "Cubano/a" },
                    { new Guid("9aca8e69-e9cf-4be5-b92f-a89dafef30df"), "SWE", "Sueco/sueca" },
                    { new Guid("a29d99f8-77b4-4581-9aa9-aaf6043d1b89"), "WAL", "Galés/galesa" },
                    { new Guid("a33fa963-f784-4b6c-879e-6619703067da"), "AFG", "Afgano/a" },
                    { new Guid("a3bc6c11-d6dd-4305-bc99-fba7e00fb337"), "EGY", "Egipcio/a" },
                    { new Guid("a539eb0f-0ab9-488f-b11a-1086aafd4c3f"), "PAN", "Panameño/a" },
                    { new Guid("a909208b-a940-4315-ba39-4c752b4affa4"), "CRI", "Costarricense" },
                    { new Guid("adcdd75b-13bd-4bfd-a75d-4da5142cf09c"), "AUS", "Australiano/a" },
                    { new Guid("b4619a44-c126-4196-977f-c4ad53b0cd74"), "GRC", "Griego/a" },
                    { new Guid("bdfdd288-bfeb-458b-9a41-c8bd527b0e07"), "FRA", "Francés/francesa" },
                    { new Guid("c19c7482-d845-43f2-b0fd-dae07ce060ed"), "MYS", "Malayo/malaya" },
                    { new Guid("c5442f42-938d-4711-904e-a271819fd3d8"), "CHL", "Chileno/a" },
                    { new Guid("c71ef18b-f0a8-46ed-88b6-8eb4d7e25d2b"), "PRT", "Portugués/portuguesa" },
                    { new Guid("ce8f6da8-679c-47e1-8ef1-8f4230349f86"), "MEX", "Mexicano/a" },
                    { new Guid("d1fb6f8c-50fb-4b68-a33e-d7b9aa259329"), "MAR", "Marroquí/marroquí" },
                    { new Guid("e0fac894-11e0-4e98-9181-e6c270233f26"), "KOR", "Surcoreano/a" },
                    { new Guid("e22f691f-a621-4cb5-b6cf-2006fa0b85f7"), "ESP", "Español/a" },
                    { new Guid("e7243554-7cc8-474e-9eb3-bfb85c9f6581"), "KHM", "Camboyano/a" },
                    { new Guid("e99380dc-1c5f-4502-9b7d-668a699c512d"), "ARG", "Argentino/a" },
                    { new Guid("eb793580-a2de-4978-bf27-8ca8d1e26c72"), "NZL", "Neozelandés/neozelandesa" },
                    { new Guid("f9b3aa6e-1cba-43d5-8a0b-fea7944e7f28"), "EST", "Estonio/a" },
                    { new Guid("fb4e044c-1099-41bb-a0ca-07cff6bc08c5"), "PER", "Peruano/a" },
                    { new Guid("fb892225-08e4-418a-a9f3-f3a0721dcc0f"), "RUS", "Ruso/rusa" },
                    { new Guid("fe09ffb7-198c-4f43-935f-24860b90928b"), "HTI", "Haitiano/a" },
                    { new Guid("ffa7fb73-02d6-4d5c-bfdb-912f3fa2f2a7"), "PRY", "Paraguayo/a" }
                });

            migrationBuilder.InsertData(
                schema: "System",
                table: "Expirations",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("1d0fa6a9-27d2-46c8-b61d-ac0da92eb3e7"), "21" },
                    { new Guid("4ef0ee21-bdce-4664-acd3-96da9462c408"), "90" },
                    { new Guid("5583fd6b-1801-4571-80df-ba9d59dff535"), "1" }
                });

            migrationBuilder.InsertData(
                schema: "System",
                table: "Organs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0534eb14-3bb6-4446-a2ae-f239df13387a"), "JEF-MA" },
                    { new Guid("14dc5d56-88c1-4948-9abd-3b46d6b34be3"), "JEF-PR" },
                    { new Guid("1cca1b5e-0872-41f1-9ae8-0af2bf66422b"), "JEF-GR" },
                    { new Guid("29f6e1fe-fabe-472f-8a85-0930d1f35a9d"), "JEF-SS" },
                    { new Guid("2db4ce92-243a-42a1-8092-8cc4e525ce7e"), "DGI" },
                    { new Guid("37261ea7-8218-450d-8dcf-770ab9728b81"), "PTI" },
                    { new Guid("37bbeecf-97b9-40b6-b3f5-526a18f281a0"), "JEF-HO" },
                    { new Guid("3deecb6a-ac6a-4c94-b4f3-1749ff261d89"), "JEF-ME" },
                    { new Guid("42ea6d67-1393-4faa-9458-1b17b1a1c383"), "DGCI" },
                    { new Guid("436932a9-9c6e-4f0b-9392-e90111c99e13"), "JEF-AV" },
                    { new Guid("43a8fb74-1f67-4800-909e-b28b987fdd55"), "PNR" },
                    { new Guid("468c93ef-aa8f-4d89-93a0-8c8d9e631ca2"), "JEF-AR" },
                    { new Guid("47acb459-5499-47d7-b349-b32663ce012e"), "DICO" },
                    { new Guid("4c8614e7-97ec-413c-b4b8-6ad96d1f0b72"), "JEF-MB" },
                    { new Guid("4c9220fd-72a6-4f55-983b-14af6460a48f"), "JEF-TU" },
                    { new Guid("5e766101-dde9-4978-b27a-cf84a5e10444"), "JEF-VC" },
                    { new Guid("61c898d1-195b-45f5-9b89-0fcbd45ddc8d"), "JEF-CM" },
                    { new Guid("7524f6e0-2b28-4ea0-9a5d-bbc7bccc998a"), "DSP" },
                    { new Guid("82cb9c16-a1a3-4173-94b7-6ac3cfcfa1e4"), "JEF-GU" },
                    { new Guid("86fbeaf5-beb4-4267-91b2-e7d038596468"), "JEF-SC" },
                    { new Guid("87fafff0-05db-47b7-8f7e-7b1c82f7aef9"), "DCIM" },
                    { new Guid("89258072-fb9b-49be-a442-19ed6860af25"), "PNR" },
                    { new Guid("99f374e6-372e-454a-9085-e0be8356c0d7"), "JEF-ME" },
                    { new Guid("a15bbb25-af61-4f9a-b9f3-8e74f5dadcf0"), "DNA" },
                    { new Guid("accd0205-b24d-4abd-9dd4-bf5baacec6e7"), "CII" },
                    { new Guid("adc0dea4-a88e-4974-817f-85d7947d3c7b"), "FGR" },
                    { new Guid("b1e32c7d-2a1c-4164-8d93-f77b12bc30ab"), "JEF-CF" },
                    { new Guid("c0190a05-1fab-47c2-925c-49a81d27ea9d"), "FMTAR" },
                    { new Guid("c7bf384c-904d-4b2f-a2c0-4e8a7f956625"), "TSP" }
                });

            migrationBuilder.CreateIndex(
                name: "OperationalCirculation_BirthDate",
                schema: "System",
                table: "OperationalCirculations",
                column: "BirthDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Circulations",
                schema: "System");

            migrationBuilder.DropTable(
                name: "Citizenships",
                schema: "System");

            migrationBuilder.DropTable(
                name: "Expirations",
                schema: "System");

            migrationBuilder.DropTable(
                name: "OperationalCirculations",
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
