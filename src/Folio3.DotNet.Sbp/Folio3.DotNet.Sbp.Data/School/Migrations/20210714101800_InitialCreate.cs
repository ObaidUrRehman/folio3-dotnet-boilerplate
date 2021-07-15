using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Folio3.DotNet.Sbp.Data.School.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Budget = table.Column<decimal>(type: "money", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstructorID = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                    table.ForeignKey(
                        name: "FK_Departments_Instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfficeAssignments",
                columns: table => new
                {
                    InstructorID = table.Column<long>(type: "bigint", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeAssignments", x => x.InstructorID);
                    table.ForeignKey(
                        name: "FK_OfficeAssignments_Instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<long>(type: "bigint", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Courses_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseAssignments",
                columns: table => new
                {
                    InstructorID = table.Column<long>(type: "bigint", nullable: false),
                    CourseID = table.Column<long>(type: "bigint", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAssignments", x => new { x.CourseID, x.InstructorID });
                    table.ForeignKey(
                        name: "FK_CourseAssignments_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseAssignments_Instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<long>(type: "bigint", nullable: false),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "ID", "Created", "FirstName", "HireDate", "LastName", "Updated", "Version" },
                values: new object[,]
                {
                    { 1L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kim", new DateTime(1995, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abercrombie", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fadi", new DateTime(2002, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fakhouri", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roger", new DateTime(1998, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harui", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Candace", new DateTime(2001, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kapoor", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roger", new DateTime(2004, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zheng", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "ID", "Created", "EnrollmentDate", "FirstName", "LastName", "Updated", "Version" },
                values: new object[,]
                {
                    { 1L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2010, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carson", "Alexander", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Meredith", "Alonso", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2013, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arturo", "Anand", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gytis", "Barzdukas", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yan", "Li", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2011, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peggy", "Justice", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2013, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laura", "Norman", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2005, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nino", "Olivetto", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentID", "Budget", "Created", "InstructorID", "Name", "StartDate", "Updated", "Version" },
                values: new object[,]
                {
                    { 1L, 350000m, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "English", new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2L, 100000m, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "Mathematics", new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3L, 350000m, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, "Engineering", new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4L, 100000m, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L, "Economics", new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "OfficeAssignments",
                columns: new[] { "InstructorID", "Created", "Location", "Updated", "Version" },
                values: new object[,]
                {
                    { 2L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith 17", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gowan 27", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thompson 304", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseID", "Created", "Credits", "DepartmentID", "Title", "Updated", "Version" },
                values: new object[,]
                {
                    { 2021L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1L, "Composition", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2042L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1L, "Literature", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 1045L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2L, "Calculus", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3141L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2L, "Trigonometry", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 1050L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3L, "Chemistry", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4022L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4L, "Microeconomics", new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "CourseAssignments",
                columns: new[] { "CourseID", "InstructorID", "Created", "Updated", "Version" },
                values: new object[,]
                {
                    { 2021L, 1L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4022L, 5L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2042L, 1L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 1045L, 2L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3141L, 3L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 1050L, 3L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 1050L, 4L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentID", "CourseID", "Created", "Grade", "StudentID", "Updated", "Version" },
                values: new object[,]
                {
                    { 8L, 1050L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4L, new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6L, 1050L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3L, new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 1L, 1050L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1L, new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4L, 3141L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2L, new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3L, 1045L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2L, new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 10L, 2042L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6L, new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 9L, 2021L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5L, new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5L, 2021L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2L, new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2L, 4022L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1L, new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7L, 4022L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3L, new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseAssignments_InstructorID",
                table: "CourseAssignments",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentID",
                table: "Courses",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InstructorID",
                table: "Departments",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseID",
                table: "Enrollments",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentID",
                table: "Enrollments",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseAssignments");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "OfficeAssignments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Instructors");
        }
    }
}
