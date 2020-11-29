using ScienceBook.Web.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.DAL
{
    public class ScienceBookContext : DbContext
    {
        public ScienceBookContext()
            : base("DefaultConnection")
        { 
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<ScienceClub> ScienceClubs { get; set; }
        public DbSet<CategoryOfScienceClub> CategoriesOfScienceClub { get; set; }
        public DbSet<FieldOfStudy> FieldsOfStudies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<CategoryOfTask> CategoriesOfTask { get; set; }
        public DbSet<TaskState> TaskStates { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ScienceClub_Member_Role> ScienceClubs_Members_Roles { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<OptionsInElection> OptionsInElections { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<MailingList> MailingList { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<MembershipDeclaration> MembershipDeclarations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Task>()
                .HasRequired(t => t.ScienceClub)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasRequired(t => t.ScienceClub)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventParticipant>()
               .HasRequired(t => t.Members)
               .WithMany()
               .WillCascadeOnDelete(false);
        }
    }
}