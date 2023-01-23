using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> _options) : base(_options)
        {
        }
        //////////Classes
        /// 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StGroup> StGroups { get; set; }
        // public DbSet<GroupDiscussion> GroupDiscussions { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<PlayListGroup> PlayListGroups { get; set; }
        //  public DbSet<CourseReview> CourseReviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Right> Rights { get; set; }
        public DbSet<RoleRight> RoleRights { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(a => a.IsActive);


            modelBuilder.Entity<UserRole>().HasKey(table => new
            {
                table.UserId,
                table.RoleId
            });

            modelBuilder.Entity<RoleRight>().HasKey(table => new
            {
                table.RoleId,
                table.RightId
            });

            //////UserCourse

            modelBuilder.Entity<UserCourse>(_UserCourse =>
            {
                _UserCourse.ToTable("UserCourse");
                _UserCourse.HasKey(table => new
                {
                    table.UserId,
                    table.CourseId
                });
                _UserCourse.HasOne(v => v.User).WithMany().HasForeignKey(v => v.UserId).OnDelete(DeleteBehavior.NoAction);
                _UserCourse.HasOne(v => v.Course).WithMany().HasForeignKey(v => v.CourseId).OnDelete(DeleteBehavior.NoAction);

                _UserCourse.Property(p => p.Id)
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            //modelBuilder.Entity<UserCourse>().HasKey(table => new
            //{
            //    table.UserId,
            //    table.CourseId
            //});
            ///
            modelBuilder.Entity<PlayListGroup>(_PlayListGroup =>
            {
                _PlayListGroup.ToTable("PlayListGroups");
                _PlayListGroup.HasKey(table => new
                {
                    table.PlayListId,
                    table.StGroupId
                });
                _PlayListGroup.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            //    modelBuilder.Entity<PlayListGroup>().HasKey(table => new
            //{
            //    table.PlayListId,
            //    table.StGroupId
            //  });


            ///UserGroup
            modelBuilder.Entity<UserGroup>().HasKey(table => new
            {
                table.UserId,
                table.StGroupId
            });

            ///Right
            modelBuilder.Entity<Right>().HasIndex(r => r.Name).IsUnique();

            ////Role
            modelBuilder.Entity<Role>().HasIndex(r => r.Name).IsUnique();

            ////CourseReview
            //modelBuilder.Entity<CourseReview>(_CourseReview =>
            //{
            //    _CourseReview.ToTable("CourseReview");
            //    _CourseReview.HasOne(v => v.User).WithMany().HasForeignKey(v => v.UserId).OnDelete(DeleteBehavior.NoAction);
            //    _CourseReview.HasOne(v => v.Course).WithMany().HasForeignKey(v => v.CourseId).OnDelete(DeleteBehavior.NoAction);
            //});
            //////////Call the Base
            base.OnModelCreating(modelBuilder);
        }



    }
}
