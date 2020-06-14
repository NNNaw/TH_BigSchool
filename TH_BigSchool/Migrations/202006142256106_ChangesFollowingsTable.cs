namespace TH_BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesFollowingsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Followings", "Course_Id", "dbo.Courses");
            DropIndex("dbo.Followings", new[] { "Course_Id" });
            DropColumn("dbo.Followings", "Course_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Followings", "Course_Id", c => c.Int());
            CreateIndex("dbo.Followings", "Course_Id");
            AddForeignKey("dbo.Followings", "Course_Id", "dbo.Courses", "Id");
        }
    }
}
