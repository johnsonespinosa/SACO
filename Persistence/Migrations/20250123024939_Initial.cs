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
                name: "SISTEMA");

            migrationBuilder.CreateSequence(
                name: "IDGEN",
                schema: "SISTEMA");

            migrationBuilder.CreateSequence(
                name: "LOG_SEQUENCE",
                schema: "SISTEMA");

            migrationBuilder.CreateSequence(
                name: "Login_SEQ",
                schema: "SISTEMA");

            migrationBuilder.CreateSequence(
                name: "SEQ_CIRCULADOLC_ID",
                schema: "SISTEMA",
                cyclic: true);

            migrationBuilder.CreateSequence(
                name: "SEQ_CUB_INADM",
                schema: "SISTEMA",
                cyclic: true);

            migrationBuilder.CreateSequence(
                name: "SEQ_DOC_INAC",
                schema: "SISTEMA",
                cyclic: true);

            migrationBuilder.CreateSequence(
                name: "SQ_Login",
                schema: "SISTEMA");

            migrationBuilder.CreateTable(
                name: "Circulations",
                schema: "SISTEMA",
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
                name: "CODPAIS",
                schema: "SISTEMA",
                columns: table => new
                {
                    NumericCode = table.Column<decimal>(type: "numeric", nullable: false),
                    Code = table.Column<string>(type: "character varying(3)", unicode: false, maxLength: 3, nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C001162978", x => x.NumericCode);
                });

            migrationBuilder.CreateTable(
                name: "Expirations",
                schema: "SISTEMA",
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
                schema: "SISTEMA",
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
                schema: "SISTEMA",
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
                schema: "SISTEMA",
                table: "Circulations",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[,]
                {
                    { new Guid("13a533c7-a9e3-42a2-b386-87657ba60871"), "PE", "INTERES MIGRATORIO" },
                    { new Guid("1449e21f-a31a-49dd-8b7a-77b3d8b30aef"), "DG", "DROGAS" },
                    { new Guid("32dd31bc-a1db-4182-b063-82b187c3e17e"), "PE", "PERDIDA DOCUMENTO" },
                    { new Guid("48e5e961-2e4c-4f79-a224-29ce34194952"), "AE", "AVISO ENTRADA" },
                    { new Guid("5420ab8f-a599-4dfb-8d3f-607c80a43357"), "AS", "AVISO SALIDA" },
                    { new Guid("8cc773f2-6ca4-47f9-8f61-04e1330154c2"), "DET", "DETENCION" },
                    { new Guid("91da3adc-3b4b-48ac-9a82-203807209c6f"), "PE", "AVISO ENT/SAL" },
                    { new Guid("9ce39c36-dd03-43b4-8410-b89474e7307b"), "PE", "PROHIBICION ENTRADA" },
                    { new Guid("f4b42694-5f71-46d9-8243-caede3cf7a50"), "PS", "PROHIBICION SALIDA" }
                });

            migrationBuilder.InsertData(
                schema: "SISTEMA",
                table: "Expirations",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("3a228fee-e56a-4570-86d3-3ed1e245db32"), "90" },
                    { new Guid("6c5401c1-20e3-4500-b77b-72e328a9c454"), "1" },
                    { new Guid("7bf5dfb4-7759-4c89-b835-4bc998ec1201"), "21" }
                });

            migrationBuilder.InsertData(
                schema: "SISTEMA",
                table: "Organs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("16a057bf-141f-4023-b5fd-a11afb5a7cc2"), "JEF-HO" },
                    { new Guid("1a487931-43f3-4b1c-9f72-e05ef23ed76f"), "DSP" },
                    { new Guid("1cf6bef6-dd5a-40c4-974d-740fa90d81b5"), "DGI" },
                    { new Guid("2c5c1707-a311-48e0-be55-45d27814059f"), "CII" },
                    { new Guid("30a6a713-983d-4912-8532-aa1d46ead667"), "JEF-ME" },
                    { new Guid("3cf2a73d-b92a-4b18-b7a7-a1de942d449d"), "JEF-AR" },
                    { new Guid("3eafa6b4-c835-44e7-a7e1-8004e733442d"), "JEF-MA" },
                    { new Guid("439c1596-5072-4d38-84cd-c2c92ade9c40"), "FMTAR" },
                    { new Guid("46007422-aab0-4987-949c-12123032dfe0"), "PTI" },
                    { new Guid("48f8fc89-c2fd-4e22-9afe-ad51fce437ea"), "JEF-SS" },
                    { new Guid("4e22905c-9efc-446e-9e25-4ba0af0f7752"), "PNR" },
                    { new Guid("5d46a206-ca3a-4a7c-9557-20d5bc4495b8"), "JEF-AV" },
                    { new Guid("6930eb2c-ac02-41be-8184-25bb5fe9471e"), "DCIM" },
                    { new Guid("6eda93cf-d1dc-40ad-850b-c9c85c23d81e"), "DNA" },
                    { new Guid("7dcde639-dfcd-4322-9ed4-27f022381723"), "FGR" },
                    { new Guid("8f811725-ec6d-43c7-ae0c-0b05c9cf5776"), "TSP" },
                    { new Guid("914273b0-be73-40ff-abc5-08949fd09072"), "PNR" },
                    { new Guid("be4da09d-6301-4abc-adfe-54f32d77e03b"), "DICO" },
                    { new Guid("da08c3ea-318e-4627-9765-9fbe5430d37b"), "JEF-GU" },
                    { new Guid("dbeacaba-8b77-4698-9eb6-039f12d15975"), "JEF-SC" },
                    { new Guid("df6cdcd4-7533-48f6-8501-86b417b364e2"), "JEF-ME" },
                    { new Guid("dfc67d51-ee8f-43d0-b9ea-c85511969460"), "JEF-CM" },
                    { new Guid("e641df40-6510-4707-94bc-a795c81b6ac7"), "JEF-TU" },
                    { new Guid("f440c502-9fea-411d-864c-f54b198ca87c"), "DGCI" },
                    { new Guid("f4446b55-8295-4820-823f-6ddc956361a4"), "JEF-GR" },
                    { new Guid("f4967882-71fe-4ee8-9461-73e51a382a31"), "JEF-PR" },
                    { new Guid("faca4fae-93f9-4dc9-9eaa-170090a0cbef"), "JEF-VC" },
                    { new Guid("fbe537cd-b11d-4921-b8ce-0adc4b131adc"), "JEF-CF" },
                    { new Guid("ffaf6f6b-a7a2-4182-bd4a-f951c6b4f16b"), "JEF-MB" }
                });

            migrationBuilder.CreateIndex(
                name: "OperationalCirculation_BirthDate",
                schema: "SISTEMA",
                table: "OperationalCirculations",
                column: "BirthDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Circulations",
                schema: "SISTEMA");

            migrationBuilder.DropTable(
                name: "CODPAIS",
                schema: "SISTEMA");

            migrationBuilder.DropTable(
                name: "Expirations",
                schema: "SISTEMA");

            migrationBuilder.DropTable(
                name: "OperationalCirculations",
                schema: "SISTEMA");

            migrationBuilder.DropTable(
                name: "Organs",
                schema: "SISTEMA");

            migrationBuilder.DropSequence(
                name: "IDGEN",
                schema: "SISTEMA");

            migrationBuilder.DropSequence(
                name: "LOG_SEQUENCE",
                schema: "SISTEMA");

            migrationBuilder.DropSequence(
                name: "Login_SEQ",
                schema: "SISTEMA");

            migrationBuilder.DropSequence(
                name: "SEQ_CIRCULADOLC_ID",
                schema: "SISTEMA");

            migrationBuilder.DropSequence(
                name: "SEQ_CUB_INADM",
                schema: "SISTEMA");

            migrationBuilder.DropSequence(
                name: "SEQ_DOC_INAC",
                schema: "SISTEMA");

            migrationBuilder.DropSequence(
                name: "SQ_Login",
                schema: "SISTEMA");
        }
    }
}
