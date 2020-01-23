namespace NCIP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AffectedCommunity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        CommunityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Community", t => t.CommunityID)
                .ForeignKey("dbo.Project", t => t.ProjectID)
                .Index(t => t.ProjectID)
                .Index(t => t.CommunityID);
            
            CreateTable(
                "dbo.Community",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sitio = c.String(nullable: false),
                        Population = c.Int(nullable: false),
                        Representative = c.String(nullable: false),
                        BarangayID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Barangay", t => t.BarangayID)
                .Index(t => t.BarangayID);
            
            CreateTable(
                "dbo.Barangay",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BarangayName = c.String(nullable: false),
                        Classification = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        projectTitle = c.String(nullable: false),
                        projectType = c.String(nullable: false),
                        reference = c.String(nullable: false),
                        timeStamp = c.DateTime(nullable: false),
                        StageID = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
                        PersonID = c.Int(nullable: false),
                        FileDirectory = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.PersonID)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .ForeignKey("dbo.Stage", t => t.StageID)
                .Index(t => t.StageID)
                .Index(t => t.CompanyID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Landline = c.String(),
                        Mobile = c.String(),
                        Businesstype = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Lastname = c.String(),
                        Firstname = c.String(),
                        Address = c.String(),
                        landline = c.String(),
                        Mobile = c.String(),
                        CompanyID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Meeting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DateHeld = c.DateTime(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        StageID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Project", t => t.ProjectID)
                .ForeignKey("dbo.Stage", t => t.StageID)
                .Index(t => t.ProjectID)
                .Index(t => t.StageID);
            
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Lastname = c.String(nullable: false),
                        Firstname = c.String(nullable: false),
                        MeetingID = c.Int(nullable: false),
                        CommunityID = c.Int(nullable: false),
                        EthnicGroupID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Community", t => t.CommunityID)
                .ForeignKey("dbo.EthnicGroup", t => t.EthnicGroupID)
                .ForeignKey("dbo.Meeting", t => t.MeetingID)
                .Index(t => t.MeetingID)
                .Index(t => t.CommunityID)
                .Index(t => t.EthnicGroupID);
            
            CreateTable(
                "dbo.EthnicGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Stage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Projectfile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Filename = c.String(),
                        Filelocation = c.String(),
                        ProjectID = c.Int(nullable: false),
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .ForeignKey("dbo.Project", t => t.ProjectID)
                .Index(t => t.ProjectID)
                .Index(t => t.ApplicationUserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(nullable: false, maxLength: 255),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.LoggedAction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ActionDone = c.String(),
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .Index(t => t.ApplicationUserID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [LastName], [FirstName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'42aff7dc-9690-4b8b-bcc1-4d9b36b38796', N'North', N'Peter', N'admin@admin.com', 0, N'ABcQx0H/V2rmm2A0SoITpY8hX1YcpV3wi0wnoJkMeEtOL11otBkkqdOZh3sJteiU4g==', N'c81ae0ed-3ef7-4ff0-a752-c66a40c78365', NULL, 0, 0, NULL, 1, 0, N'admin@admin.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [LastName], [FirstName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c5400d62-fc31-4b9f-b2a7-f722979ea249', N'Trump', N'Donald', N'encoder@encoder.com', 0, N'ACXC8Z2ekoXXnuJLV5I+yJkoRLnVq/TKLcrqpC0dT3HlnLbsiCjDEzljnW9jHKzLzA==', N'e67bc1bc-effb-4df8-9d70-9d8a7ed0fdd2', NULL, 0, 0, NULL, 1, 0, N'encoder@encoder.com')

            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'0ce3b0a5-4a64-4593-aeb5-5a906815a689', N'Admin')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6552a762-6eca-45cd-9bfd-d88a24ea94db', N'Encoder')

            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'42aff7dc-9690-4b8b-bcc1-4d9b36b38796', N'0ce3b0a5-4a64-4593-aeb5-5a906815a689')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c5400d62-fc31-4b9f-b2a7-f722979ea249', N'6552a762-6eca-45cd-9bfd-d88a24ea94db')
             
            SET IDENTITY_INSERT [dbo].[Company] ON
            INSERT INTO [dbo].[Company] ([ID], [Name], [Address], [Landline], [Mobile], [Businesstype]) VALUES (1, N' n/a', N' n/a', N' n/a', N' n/a', N' n/a')
            SET IDENTITY_INSERT [dbo].[Company] OFF");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LoggedAction", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projectfile", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projectfile", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Meeting", "StageID", "dbo.Stage");
            DropForeignKey("dbo.Project", "StageID", "dbo.Stage");
            DropForeignKey("dbo.Meeting", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.Attendance", "MeetingID", "dbo.Meeting");
            DropForeignKey("dbo.Attendance", "EthnicGroupID", "dbo.EthnicGroup");
            DropForeignKey("dbo.Attendance", "CommunityID", "dbo.Community");
            DropForeignKey("dbo.Project", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.Project", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Person", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.AffectedCommunity", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.AffectedCommunity", "CommunityID", "dbo.Community");
            DropForeignKey("dbo.Community", "BarangayID", "dbo.Barangay");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LoggedAction", new[] { "ApplicationUserID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Projectfile", new[] { "ApplicationUserID" });
            DropIndex("dbo.Projectfile", new[] { "ProjectID" });
            DropIndex("dbo.Attendance", new[] { "EthnicGroupID" });
            DropIndex("dbo.Attendance", new[] { "CommunityID" });
            DropIndex("dbo.Attendance", new[] { "MeetingID" });
            DropIndex("dbo.Meeting", new[] { "StageID" });
            DropIndex("dbo.Meeting", new[] { "ProjectID" });
            DropIndex("dbo.Person", new[] { "CompanyID" });
            DropIndex("dbo.Project", new[] { "PersonID" });
            DropIndex("dbo.Project", new[] { "CompanyID" });
            DropIndex("dbo.Project", new[] { "StageID" });
            DropIndex("dbo.Community", new[] { "BarangayID" });
            DropIndex("dbo.AffectedCommunity", new[] { "CommunityID" });
            DropIndex("dbo.AffectedCommunity", new[] { "ProjectID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LoggedAction");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Projectfile");
            DropTable("dbo.Stage");
            DropTable("dbo.EthnicGroup");
            DropTable("dbo.Attendance");
            DropTable("dbo.Meeting");
            DropTable("dbo.Person");
            DropTable("dbo.Company");
            DropTable("dbo.Project");
            DropTable("dbo.Barangay");
            DropTable("dbo.Community");
            DropTable("dbo.AffectedCommunity");
        }
    }
}
