using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Labb3_CallDatabase.Models;

namespace Labb3_CallDatabase.Data
{
    public partial class Labb3Context : DbContext
    {
        public Labb3Context()
        {
        }

        public Labb3Context(DbContextOptions<Labb3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<StaffCourseConnection> StaffCourseConnections { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=GREENMECHANOID; Initial Catalog=Labb2-SchoolDatabase;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.Coursename)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Schoolyear)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.Grade1)
                    .HasMaxLength(10)
                    .HasColumnName("Grade")
                    .IsFixedLength();

                entity.Property(e => e.GradingDate).HasColumnType("date");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Grades_Course");
            });

            modelBuilder.Entity<StaffCourseConnection>(entity =>
            {
                entity.HasKey(e => e.ConnectionId)
                    .HasName("PK__StaffCou__404A64F326357E63");

                entity.ToTable("StaffCourseConnection");

                entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StaffCourseConnections)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_StaffCourseConnection_Course");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffCourseConnections)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_StaffCourseConnection_Staff");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.Adress)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Classnumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Dateofbirth).HasColumnType("datetime");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.Adress)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Dateofbirth).HasColumnType("datetime");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Occupation)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
