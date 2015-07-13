namespace University.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using University.Models;
    using University.Models.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<University.Models.DAL.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SchoolContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Students.AddOrUpdate(
              p => p.StudentID,
                new Student { StudentID = 1, FirstMidName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Parse("2005-09-01") },
                new Student { StudentID = 2, FirstMidName = "Meredith", LastName = "Alonso", EnrollmentDate = DateTime.Parse("2002-09-01") },
                new Student { StudentID = 3, FirstMidName = "Arturo", LastName = "Anand", EnrollmentDate = DateTime.Parse("2003-09-01") },
                new Student { StudentID = 4, FirstMidName = "Gytis", LastName = "Barzdukas", EnrollmentDate = DateTime.Parse("2002-09-01") },
                new Student { StudentID = 5, FirstMidName = "Yan", LastName = "Li", EnrollmentDate = DateTime.Parse("2002-09-01") },
                new Student { StudentID = 6, FirstMidName = "Peggy", LastName = "Justice", EnrollmentDate = DateTime.Parse("2001-09-01") },
                new Student { StudentID = 7, FirstMidName = "Laura", LastName = "Norman", EnrollmentDate = DateTime.Parse("2003-09-01") },
                new Student { StudentID = 8, FirstMidName = "Nino", LastName = "Olivetto", EnrollmentDate = DateTime.Parse("2005-09-01") }
            );

            context.Courses.AddOrUpdate(
              p => p.CourseID,
                new Course { CourseID = 1050, CourseName = "Chemistry", InstructorFirstName = "Bill", InstructorLastName = "Murrey" },
                new Course { CourseID = 4022, CourseName = "Microeconomics", InstructorFirstName = "Bruce", InstructorLastName = "Poll" },
                new Course { CourseID = 4041, CourseName = "Macroeconomics", InstructorFirstName = "Vane", InstructorLastName = "Jones" },
                new Course { CourseID = 1045, CourseName = "Calculus", InstructorFirstName = "John", InstructorLastName = "Smith" },
                new Course { CourseID = 3141, CourseName = "Trigonometry", InstructorFirstName = "Tom", InstructorLastName = "Pane" },
                new Course { CourseID = 2021, CourseName = "Composition", InstructorFirstName = "Scott", InstructorLastName = "Mills" },
                new Course { CourseID = 2042, CourseName = "Literature", InstructorFirstName = "Math", InstructorLastName = " Lebovsky" }
            );

            context.Enrollments.AddOrUpdate(
                new Enrollment { StudentID=1, CourseID=1050,Grade=Grade.A},
                new Enrollment { StudentID=1, CourseID=4022,Grade=Grade.C},
                new Enrollment { StudentID=1, CourseID=4041,Grade=Grade.B},
                new Enrollment { StudentID=2, CourseID=1045,Grade=Grade.B},
                new Enrollment { StudentID=2, CourseID=3141,Grade=Grade.F},
                new Enrollment { StudentID=2, CourseID=2021,Grade=Grade.F},
                new Enrollment { StudentID=3, CourseID=1050,Grade=Grade.C},
                new Enrollment { StudentID=4, CourseID=1050,Grade=Grade.A},
                new Enrollment { StudentID=4, CourseID=4022,Grade=Grade.F},
                new Enrollment { StudentID=5, CourseID=4041,Grade=Grade.C},
                new Enrollment { StudentID=6, CourseID=1045,Grade=Grade.F},
                new Enrollment { StudentID=7, CourseID=3141,Grade=Grade.A}
            );
           
        }
    }
}
