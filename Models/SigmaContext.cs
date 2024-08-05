using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SigmaBackend.Models;

public partial class SigmaContext : DbContext
{
    public SigmaContext()
    {
    }

    public SigmaContext(DbContextOptions<SigmaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grade__54F87A57B0C8E337");

            entity.ToTable("Grade");

            entity.Property(e => e.GradeId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Grade1).HasColumnName("Grade").IsRequired();

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentId");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Grades)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherId");
            entity.HasOne(d => d.Subject).WithMany(p => p.Grades)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_SubjectId");
            entity.Property(e => e.GradeType)
                .IsRequired();
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52B99528597A3");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.StudentLastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .IsRequired();
            entity.Property(e => e.Password)
                .IsRequired();
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subject__AC1BA3A830C937D9");

            entity.ToTable("Subject");

            entity.Property(e => e.SubjectId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__Teacher__EDF25964097EBF57");

            entity.ToTable("Teacher");

            entity.Property(e => e.TeacherId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.TeacherLastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TeacherName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TeacherSubject)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .IsRequired();
            entity.Property(e => e.Password)
                .IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
