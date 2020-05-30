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
                "dbo.Chat",
                c => new
                    {
                        Guid = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                        PartyGuid = c.Guid(nullable: false),
                        MessageGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Message", t => t.MessageGuid, cascadeDelete: true)
                .ForeignKey("dbo.Party", t => t.PartyGuid, cascadeDelete: true)
                .Index(t => t.PartyGuid)
                .Index(t => t.MessageGuid);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Guid = c.Guid(nullable: false, identity: true),
                        ChatGuid = c.Guid(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                        Contect = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        MessageStatusGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.MessageStatus", t => t.MessageStatusGuid, cascadeDelete: true)
                .Index(t => t.MessageStatusGuid);
            
            CreateTable(
                "dbo.MessageStatus",
                c => new
                    {
                        MessageGuid = c.Guid(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MessageGuid);
            
            CreateTable(
                "dbo.Party",
                c => new
                    {
                        ChatGuid = c.Guid(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ChatGuid);
            
            CreateTable(
                "dbo.MessageStatusUsers",
                c => new
                    {
                        MessageStatus_MessageGuid = c.Guid(nullable: false),
                        User_Guid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.MessageStatus_MessageGuid, t.User_Guid })
                .ForeignKey("dbo.MessageStatus", t => t.MessageStatus_MessageGuid, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Guid, cascadeDelete: true)
                .Index(t => t.MessageStatus_MessageGuid)
                .Index(t => t.User_Guid);
            
            CreateTable(
                "dbo.MessageUsers",
                c => new
                    {
                        Message_Guid = c.Guid(nullable: false),
                        User_Guid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Message_Guid, t.User_Guid })
                .ForeignKey("dbo.Message", t => t.Message_Guid, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Guid, cascadeDelete: true)
                .Index(t => t.Message_Guid)
                .Index(t => t.User_Guid);
            
            CreateTable(
                "dbo.PartyUsers",
                c => new
                    {
                        Party_ChatGuid = c.Guid(nullable: false),
                        User_Guid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Party_ChatGuid, t.User_Guid })
                .ForeignKey("dbo.Party", t => t.Party_ChatGuid, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Guid, cascadeDelete: true)
                .Index(t => t.Party_ChatGuid)
                .Index(t => t.User_Guid);
            
            CreateTable(
                "dbo.ChatUsers",
                c => new
                    {
                        Chat_Guid = c.Guid(nullable: false),
                        User_Guid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Chat_Guid, t.User_Guid })
                .ForeignKey("dbo.Chat", t => t.Chat_Guid, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Guid, cascadeDelete: true)
                .Index(t => t.Chat_Guid)
                .Index(t => t.User_Guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "Guid", "dbo.Employee");
            DropForeignKey("dbo.ChatUsers", "User_Guid", "dbo.User");
            DropForeignKey("dbo.ChatUsers", "Chat_Guid", "dbo.Chat");
            DropForeignKey("dbo.PartyUsers", "User_Guid", "dbo.User");
            DropForeignKey("dbo.PartyUsers", "Party_ChatGuid", "dbo.Party");
            DropForeignKey("dbo.Chat", "PartyGuid", "dbo.Party");
            DropForeignKey("dbo.MessageUsers", "User_Guid", "dbo.User");
            DropForeignKey("dbo.MessageUsers", "Message_Guid", "dbo.Message");
            DropForeignKey("dbo.MessageStatusUsers", "User_Guid", "dbo.User");
            DropForeignKey("dbo.MessageStatusUsers", "MessageStatus_MessageGuid", "dbo.MessageStatus");
            DropForeignKey("dbo.Message", "MessageStatusGuid", "dbo.MessageStatus");
            DropForeignKey("dbo.Chat", "MessageGuid", "dbo.Message");
            DropIndex("dbo.ChatUsers", new[] { "User_Guid" });
            DropIndex("dbo.ChatUsers", new[] { "Chat_Guid" });
            DropIndex("dbo.PartyUsers", new[] { "User_Guid" });
            DropIndex("dbo.PartyUsers", new[] { "Party_ChatGuid" });
            DropIndex("dbo.MessageUsers", new[] { "User_Guid" });
            DropIndex("dbo.MessageUsers", new[] { "Message_Guid" });
            DropIndex("dbo.MessageStatusUsers", new[] { "User_Guid" });
            DropIndex("dbo.MessageStatusUsers", new[] { "MessageStatus_MessageGuid" });
            DropIndex("dbo.Message", new[] { "MessageStatusGuid" });
            DropIndex("dbo.Chat", new[] { "MessageGuid" });
            DropIndex("dbo.Chat", new[] { "PartyGuid" });
            DropIndex("dbo.User", new[] { "Guid" });
            DropTable("dbo.ChatUsers");
            DropTable("dbo.PartyUsers");
            DropTable("dbo.MessageUsers");
            DropTable("dbo.MessageStatusUsers");
            DropTable("dbo.Party");
            DropTable("dbo.MessageStatus");
            DropTable("dbo.Message");
            DropTable("dbo.Chat");
            DropTable("dbo.User");
            DropTable("dbo.Employee");
        }
    }
}
