using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class StudentSemTeacherDepContext:IdentityDbContext<ApplicationUser>
    {
        public StudentSemTeacherDepContext(DbContextOptions<StudentSemTeacherDepContext> options):base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Semester> Semesters { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Department> Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //modelBuilder.Entity<Student>().HasKey(s => s.StudentId);
            //modelBuilder.Entity<Semester>().HasKey(s => s.SemesterId);
            //modelBuilder.Entity<Student>().HasOne(s => s.Semester).WithMany(s => s.Students)
            //    .HasForeignKey(s => s.SemesterId).OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Teacher>().HasKey(t => t.TeacherId);
            //modelBuilder.Entity<Department>().HasKey(d => d.DepartmentId);
            //modelBuilder.Entity<Teacher>().HasOne(d => d.Department).WithMany(t => t.Teachers)
            //    .HasForeignKey(d => d.DepartmentId).OnDelete(DeleteBehavior.Restrict);
        }
        
        
    }
}
