namespace DbWorker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Guid = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Patronymic = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Position = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Login = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Employee", t => t.Guid)
                .Index(t => t.Guid);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Guid = c.Guid(nullable: false, identity: true),
                        ChatGuid = c.Guid(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                        Content = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Chat", t => t.ChatGuid, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserGuid, cascadeDelete: true)
                .Index(t => t.ChatGuid)
                .Index(t => t.UserGuid);
            
            CreateTable(
                "dbo.Chat",
                c => new
                    {
                        Guid = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.MessageStatus",
                c => new
                    {
                        MessageGuid = c.Guid(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.MessageGuid, t.UserGuid })
                .ForeignKey("dbo.Message", t => t.MessageGuid, cascadeDelete: true)
                .Index(t => t.MessageGuid);
            
            CreateTable(
                "dbo.Party",
                c => new
                    {
                        ChatGuid = c.Guid(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChatGuid, t.UserGuid })
                .ForeignKey("dbo.Chat", t => t.ChatGuid, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserGuid, cascadeDelete: true)
                .Index(t => t.ChatGuid)
                .Index(t => t.UserGuid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Party", "UserGuid", "dbo.User");
            DropForeignKey("dbo.Party", "ChatGuid", "dbo.Chat");
            DropForeignKey("dbo.Message", "UserGuid", "dbo.User");
            DropForeignKey("dbo.MessageStatus", "MessageGuid", "dbo.Message");
            DropForeignKey("dbo.Message", "ChatGuid", "dbo.Chat");
            DropForeignKey("dbo.User", "Guid", "dbo.Employee");
            DropIndex("dbo.Party", new[] { "UserGuid" });
            DropIndex("dbo.Party", new[] { "ChatGuid" });
            DropIndex("dbo.MessageStatus", new[] { "MessageGuid" });
            DropIndex("dbo.Message", new[] { "UserGuid" });
            DropIndex("dbo.Message", new[] { "ChatGuid" });
            DropIndex("dbo.User", new[] { "Guid" });
            DropTable("dbo.Party");
            DropTable("dbo.MessageStatus");
            DropTable("dbo.Chat");
            DropTable("dbo.Message");
            DropTable("dbo.User");
            DropTable("dbo.Employee");
        }
    }
}
