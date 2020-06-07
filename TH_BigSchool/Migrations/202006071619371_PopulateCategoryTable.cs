namespace TH_BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategoryTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Categories (Id, Name) values (1,'Develop')");
            Sql("Insert into Categories (Id, Name) values (2,'Business')");
            Sql("Insert into Categories (Id, Name) values (3,'Makerting')");
        }
        
        public override void Down()
        {
        }
    }
}
