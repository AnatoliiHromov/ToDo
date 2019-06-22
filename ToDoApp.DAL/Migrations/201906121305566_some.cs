namespace ToDoApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class some : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsAccepted = c.Boolean(nullable: false),
                        FromUser = c.Int(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        SecondName = c.String(),
                        Phone = c.String(),
                        Comment = c.String(),
                        uPhoto = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        IsActivated = c.Boolean(nullable: false),
                        Cookies = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ToDoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        State = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        GeoLong = c.Double(nullable: false),
                        GeoLat = c.Double(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.SubToDoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        State = c.Int(nullable: false),
                        ToDo_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ToDoes", t => t.ToDo_ID)
                .Index(t => t.ToDo_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoes", "User_ID", "dbo.Users");
            DropForeignKey("dbo.SubToDoes", "ToDo_ID", "dbo.ToDoes");
            DropForeignKey("dbo.Friends", "User_ID", "dbo.Users");
            DropIndex("dbo.SubToDoes", new[] { "ToDo_ID" });
            DropIndex("dbo.ToDoes", new[] { "User_ID" });
            DropIndex("dbo.Friends", new[] { "User_ID" });
            DropTable("dbo.SubToDoes");
            DropTable("dbo.ToDoes");
            DropTable("dbo.Users");
            DropTable("dbo.Friends");
        }
    }
}
