using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
    public class eLContext : DbContext
    {
        public eLContext()
            : base("eLibrary")
        {

            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<eLContext, eLConfiguration>());
        }

       // public DbSet<Chapter> Chapters { get; set; }
       // public DbSet<Course> Courses { get; set; }
        public DbSet<Level> Level { get; set; }
        public DbSet<TextBook> TextBooks { get; set; }
        public DbSet<LibraryUser> LibraryUsers { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AdditionalChapterText> AdditionalChapterTexts { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Row> Rows { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<MyRole> MyRoles { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }

        public DbSet<SubjectArea> SubjectAreas { get; set; }
        public DbSet<BorrowedItem> BorrowedItems { get; set; }
        public DbSet<Finance> Finance { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chapter>().Property(p => p.FileData).HasColumnType("image");
            modelBuilder.Entity<AdditionalChapterText>().Property(p => p.FileData).HasColumnType("image");
            modelBuilder.Entity<TextBook>().Property(p => p.FileData).HasColumnType("image");
            modelBuilder.Entity<Book>().Property(p => p.FileData).HasColumnType("image");
            base.OnModelCreating(modelBuilder);

        }
    }
}
