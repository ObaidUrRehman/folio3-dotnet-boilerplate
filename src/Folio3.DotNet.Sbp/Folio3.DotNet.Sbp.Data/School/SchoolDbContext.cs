﻿using Folio3.DotNet.Sbp.Data.Common;
using Folio3.DotNet.Sbp.Data.School.Entities;
using Folio3.DotNet.Sbp.Data.School.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Folio3.DotNet.Sbp.Data.School
{
	// https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-5.0
	public class SchoolDbContext : IdentityDbContext<User>
	{
		public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
		{
		}

		public virtual DbSet<Course> Courses { get; set; }
		public virtual DbSet<Enrollment> Enrollments { get; set; }
		public virtual DbSet<Student> Students { get; set; }
		public virtual DbSet<Department> Departments { get; set; }
		public virtual DbSet<Instructor> Instructors { get; set; }
		public virtual DbSet<OfficeAssignment> OfficeAssignments { get; set; }
		public virtual DbSet<CourseAssignment> CourseAssignments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Seed();

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CourseAssignment>()
				.HasKey(c => new { c.CourseID, c.InstructorID });
		}

		public override int SaveChanges()
		{
			return SaveChanges(acceptAllChangesOnSuccess: true);
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			return Task.Run(async () => await SaveChangesAsync(acceptAllChangesOnSuccess)).Result;
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			return await SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
		}

		public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			TrackableHelpers.PopulateTrackableFields(changes: ChangeTracker);

			int result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

			return result;
		}
	}
}
