using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Folio3.DotNet.Sbp.Data.School.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Instructors",
                table => new
                {
                    ID = table.Column<long>("bigint")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HireDate = table.Column<DateTime>("datetime2"),
                    Version = table.Column<int>("int"),
                    Created = table.Column<DateTime>("datetime2"),
                    Updated = table.Column<DateTime>("datetime2"),
                    LastName = table.Column<string>("nvarchar(50)", maxLength: 50),
                    FirstName = table.Column<string>("nvarchar(50)", maxLength: 50)
                },
                constraints: table => { table.PrimaryKey("PK_Instructors", x => x.ID); });

            migrationBuilder.CreateTable(
                "Students",
                table => new
                {
                    ID = table.Column<long>("bigint")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentDate = table.Column<DateTime>("datetime2"),
                    Version = table.Column<int>("int"),
                    Created = table.Column<DateTime>("datetime2"),
                    Updated = table.Column<DateTime>("datetime2"),
                    LastName = table.Column<string>("nvarchar(50)", maxLength: 50),
                    FirstName = table.Column<string>("nvarchar(50)", maxLength: 50)
                },
                constraints: table => { table.PrimaryKey("PK_Students", x => x.ID); });

            migrationBuilder.CreateTable(
                "Departments",
                table => new
                {
                    DepartmentID = table.Column<long>("bigint")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true),
                    Budget = table.Column<decimal>("money"),
                    StartDate = table.Column<DateTime>("datetime2"),
                    InstructorID = table.Column<long>("bigint", nullable: true),
                    RowVersion = table.Column<byte[]>("rowversion", rowVersion: true, nullable: true),
                    Version = table.Column<int>("int"),
                    Created = table.Column<DateTime>("datetime2"),
                    Updated = table.Column<DateTime>("datetime2")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                    table.ForeignKey(
                        "FK_Departments_Instructors_InstructorID",
                        x => x.InstructorID,
                        "Instructors",
                        "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "OfficeAssignments",
                table => new
                {
                    InstructorID = table.Column<long>("bigint"),
                    Location = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true),
                    Version = table.Column<int>("int"),
                    Created = table.Column<DateTime>("datetime2"),
                    Updated = table.Column<DateTime>("datetime2")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeAssignments", x => x.InstructorID);
                    table.ForeignKey(
                        "FK_OfficeAssignments_Instructors_InstructorID",
                        x => x.InstructorID,
                        "Instructors",
                        "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Courses",
                table => new
                {
                    CourseID = table.Column<long>("bigint"),
                    Title = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true),
                    Credits = table.Column<int>("int"),
                    DepartmentID = table.Column<long>("bigint"),
                    Version = table.Column<int>("int"),
                    Created = table.Column<DateTime>("datetime2"),
                    Updated = table.Column<DateTime>("datetime2")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                    table.ForeignKey(
                        "FK_Courses_Departments_DepartmentID",
                        x => x.DepartmentID,
                        "Departments",
                        "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "CourseAssignments",
                table => new
                {
                    InstructorID = table.Column<long>("bigint"),
                    CourseID = table.Column<long>("bigint"),
                    Version = table.Column<int>("int"),
                    Created = table.Column<DateTime>("datetime2"),
                    Updated = table.Column<DateTime>("datetime2")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAssignments", x => new {x.CourseID, x.InstructorID});
                    table.ForeignKey(
                        "FK_CourseAssignments_Courses_CourseID",
                        x => x.CourseID,
                        "Courses",
                        "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_CourseAssignments_Instructors_InstructorID",
                        x => x.InstructorID,
                        "Instructors",
                        "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Enrollments",
                table => new
                {
                    EnrollmentID = table.Column<long>("bigint")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<long>("bigint"),
                    StudentID = table.Column<long>("bigint"),
                    Grade = table.Column<int>("int", nullable: true),
                    Version = table.Column<int>("int"),
                    Created = table.Column<DateTime>("datetime2"),
                    Updated = table.Column<DateTime>("datetime2")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentID);
                    table.ForeignKey(
                        "FK_Enrollments_Courses_CourseID",
                        x => x.CourseID,
                        "Courses",
                        "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Enrollments_Students_StudentID",
                        x => x.StudentID,
                        "Students",
                        "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "Instructors",
                new[] {"ID", "Created", "FirstName", "HireDate", "LastName", "Updated", "Version"},
                new object[,]
                {
                    {
                        1L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kim",
                        new DateTime(1995, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abercrombie",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        2L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fadi",
                        new DateTime(2002, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fakhouri",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        3L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roger",
                        new DateTime(1998, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harui",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        4L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Candace",
                        new DateTime(2001, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kapoor",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        5L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roger",
                        new DateTime(2004, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zheng",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    }
                });

            migrationBuilder.InsertData(
                "Students",
                new[] {"ID", "Created", "EnrollmentDate", "FirstName", "LastName", "Updated", "Version"},
                new object[,]
                {
                    {
                        1L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2010, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carson", "Alexander",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        2L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Meredith", "Alonso",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        3L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2013, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arturo", "Anand",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        4L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gytis", "Barzdukas",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        5L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yan", "Li",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        6L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2011, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peggy", "Justice",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        7L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2013, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laura", "Norman",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        8L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2005, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nino", "Olivetto",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    }
                });

            migrationBuilder.InsertData(
                "Departments",
                new[] {"DepartmentID", "Budget", "Created", "InstructorID", "Name", "StartDate", "Updated", "Version"},
                new object[,]
                {
                    {
                        1L, 350000m, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "English",
                        new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        2L, 100000m, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "Mathematics",
                        new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        3L, 350000m, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, "Engineering",
                        new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        4L, 100000m, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L, "Economics",
                        new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    }
                });

            migrationBuilder.InsertData(
                "OfficeAssignments",
                new[] {"InstructorID", "Created", "Location", "Updated", "Version"},
                new object[,]
                {
                    {
                        2L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith 17",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        3L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gowan 27",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        4L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thompson 304",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    }
                });

            migrationBuilder.InsertData(
                "Courses",
                new[] {"CourseID", "Created", "Credits", "DepartmentID", "Title", "Updated", "Version"},
                new object[,]
                {
                    {
                        2021L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1L, "Composition",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        2042L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1L, "Literature",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        1045L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2L, "Calculus",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        3141L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2L, "Trigonometry",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        1050L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3L, "Chemistry",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        4022L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4L, "Microeconomics",
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    }
                });

            migrationBuilder.InsertData(
                "CourseAssignments",
                new[] {"CourseID", "InstructorID", "Created", "Updated", "Version"},
                new object[,]
                {
                    {
                        2021L, 1L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        4022L, 5L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        2042L, 1L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        1045L, 2L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        3141L, 3L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        1050L, 3L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        1050L, 4L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    }
                });

            migrationBuilder.InsertData(
                "Enrollments",
                new[] {"EnrollmentID", "CourseID", "Created", "Grade", "StudentID", "Updated", "Version"},
                new object[,]
                {
                    {
                        8L, 1050L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4L,
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        6L, 1050L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3L,
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        1L, 1050L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1L,
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        4L, 3141L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2L,
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        3L, 1045L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2L,
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        10L, 2042L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6L,
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        9L, 2021L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5L,
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        5L, 2021L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2L,
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        2L, 4022L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1L,
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    },
                    {
                        7L, 4022L, new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3L,
                        new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1
                    }
                });

            migrationBuilder.CreateIndex(
                "IX_CourseAssignments_InstructorID",
                "CourseAssignments",
                "InstructorID");

            migrationBuilder.CreateIndex(
                "IX_Courses_DepartmentID",
                "Courses",
                "DepartmentID");

            migrationBuilder.CreateIndex(
                "IX_Departments_InstructorID",
                "Departments",
                "InstructorID");

            migrationBuilder.CreateIndex(
                "IX_Enrollments_CourseID",
                "Enrollments",
                "CourseID");

            migrationBuilder.CreateIndex(
                "IX_Enrollments_StudentID",
                "Enrollments",
                "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "CourseAssignments");

            migrationBuilder.DropTable(
                "Enrollments");

            migrationBuilder.DropTable(
                "OfficeAssignments");

            migrationBuilder.DropTable(
                "Courses");

            migrationBuilder.DropTable(
                "Students");

            migrationBuilder.DropTable(
                "Departments");

            migrationBuilder.DropTable(
                "Instructors");
        }
    }
}