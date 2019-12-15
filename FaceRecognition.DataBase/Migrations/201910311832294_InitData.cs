namespace FaceRecognition.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        hash_Pass = c.String(nullable: false, maxLength: 256),
                        salt_Pass = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 256),
                        Id_User = c.Int(nullable: false),
                        Id_Role = c.Int(nullable: false),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Id_Role, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Id_User, cascadeDelete: true)
                .Index(t => t.Id_User)
                .Index(t => t.Id_Role);
            
            CreateTable(
                "dbo.Imagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Account = c.Int(nullable: false),
                        Img = c.String(),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Id_Account, cascadeDelete: true)
                .Index(t => t.Id_Account);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        FullName = c.String(nullable: false, maxLength: 255),
                        Gender = c.Boolean(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfInputs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfTrainings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "Id_User", "dbo.Users");
            DropForeignKey("dbo.Accounts", "Id_Role", "dbo.Roles");
            DropForeignKey("dbo.Imagers", "Id_Account", "dbo.Accounts");
            DropIndex("dbo.Imagers", new[] { "Id_Account" });
            DropIndex("dbo.Accounts", new[] { "Id_Role" });
            DropIndex("dbo.Accounts", new[] { "Id_User" });
            DropTable("dbo.TypeOfTrainings");
            DropTable("dbo.TypeOfInputs");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Imagers");
            DropTable("dbo.Accounts");
        }
    }
}
