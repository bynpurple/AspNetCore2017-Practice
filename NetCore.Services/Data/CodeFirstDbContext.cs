using Microsoft.EntityFrameworkCore;
using NetCore.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Services.Data
{
    // Fluent API
    // 상속
    // CodeFirstDbContext: 자식클래스
    // DbContext: 부모클래스
    public class CodeFirstDbContext: DbContext
    {
        // 생성자 상속
        public CodeFirstDbContext(DbContextOptions<CodeFirstDbContext> options): base(options)
        {

        }

        // DB 테이블 리스트 지정
        public DbSet<User> Users { get; set; }

        // 메소드 상속
        // 메소드 상속은 override 키워드 사용
        // 부모클래스에서 OnModelCreating 메소드가 virtual
        // DbContext virtual 메소드 상속받은 CodeFirstDbContext override 메소드
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 4 가지 작업
            // DB 테이블 변경
            modelBuilder.Entity<User>().ToTable(name: "User");

            // 복합키 지정
            modelBuilder.Entity<UserRolesByUser>().HasKey(c => new { c.UserId, c.RoleId });

            // 컬럼 기본값 지정
            modelBuilder.Entity<User>(e =>
            {
                e.Property(c => c.IsMemberShipWithDrawn).HasDefaultValue(value: false);
            });

            // Index 지정
            modelBuilder.Entity<User>().HasIndex(c => new { c.UserEmail });

        }

    }
}
