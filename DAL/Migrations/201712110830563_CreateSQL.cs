namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSQL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorityModel",
                c => new
                    {
                        AuthorityID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AddTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuthorityID);
            
            CreateTable(
                "dbo.AuthorityItemModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuthorityID = c.Int(nullable: false),
                        MenuID = c.Int(nullable: false),
                        AddTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BookMOD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AddTime = c.DateTime(nullable: false),
                        Name = c.String(),
                        ManagerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourseRecordMOD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AddTime = c.DateTime(nullable: false),
                        CourseManagerID = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        CourseCount = c.Int(nullable: false),
                        CourseStatus = c.Int(nullable: false),
                        ManagerID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ManagerModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LoginName = c.String(),
                        ManagerName = c.String(),
                        Phone = c.String(),
                        Password = c.String(),
                        AuthorityID = c.Int(nullable: false),
                        AddTime = c.DateTime(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Operate = c.Int(nullable: false),
                        TimeZone = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ManagerLogModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ManagerID = c.Int(nullable: false),
                        AddTime = c.DateTime(nullable: false),
                        LogType = c.Int(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MenuModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MenuName = c.String(),
                        ParentID = c.Int(nullable: false),
                        LinkUrl = c.String(),
                        Grade = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        MenuImg = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductMOD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AddTime = c.DateTime(nullable: false),
                        ProductName = c.String(),
                        Frequency = c.Int(nullable: false),
                        LimitDate = c.Int(nullable: false),
                        CourseCount = c.Int(nullable: false),
                        LeaveCount = c.Int(nullable: false),
                        PriceMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsEnable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudentMOD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AddTime = c.DateTime(nullable: false),
                        LoginName = c.String(),
                        Phone = c.String(),
                        Password = c.String(),
                        TimeZone = c.Int(nullable: false),
                        Supervisor = c.Int(nullable: false),
                        ChinaName = c.String(),
                        EnglishName = c.String(),
                        HomePhone = c.String(),
                        WorkPhone = c.String(),
                        FamilyPhone = c.String(),
                        Email = c.String(),
                        Skype = c.String(),
                        BackupEmail = c.String(),
                        FamilyEmail = c.String(),
                        Wechat = c.String(),
                        s_HomePhone = c.String(),
                        s_WorkPhone = c.String(),
                        s_FamilyPhone = c.String(),
                        s_Email = c.String(),
                        s_Skype = c.String(),
                        s_BackupEmail = c.String(),
                        s_FamilyEmail = c.String(),
                        s_Wechat = c.String(),
                        Six = c.Boolean(nullable: false),
                        Birthday = c.String(),
                        IsNonage = c.Boolean(nullable: false),
                        Country = c.String(),
                        City = c.String(),
                        NativeLanguage = c.String(),
                        IsToChina = c.Boolean(nullable: false),
                        FamilyName = c.String(),
                        Character = c.String(),
                        LearningAttitude = c.String(),
                        Hobbies = c.String(),
                        TabooTopic = c.String(),
                        FamilyRelationship = c.String(),
                        Vitae = c.String(),
                        LearningNeeds = c.String(),
                        LearningContent = c.String(),
                        LearningMethod = c.String(),
                        ManagerID = c.Int(nullable: false),
                        IsFreeProbation = c.Boolean(nullable: false),
                        FreeProbationCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudentCourseRecordMOD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AddTime = c.DateTime(nullable: false),
                        StudentID = c.Int(nullable: false),
                        CourseManagerID = c.Int(nullable: false),
                        CourseRecordID = c.Int(nullable: false),
                        StudentProductID = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        CourseCount = c.Int(nullable: false),
                        StudentCourseStatus = c.Int(nullable: false),
                        StudentCourseStatusChinaName = c.String(),
                        IsEffective = c.Boolean(nullable: false),
                        StudentCourseStatusEnglishName = c.String(),
                        NowBookID = c.Int(nullable: false),
                        NowBookName = c.String(),
                        NextBookID = c.Int(nullable: false),
                        NextBookName = c.String(),
                        BookBuyInfo = c.String(),
                        CourseContent = c.String(),
                        Task = c.String(),
                        CourseQuestion = c.String(),
                        CourseEmphasis = c.String(),
                        StudentPerformed = c.String(),
                        CourseManagerFeedback = c.String(),
                        StudentFeeback = c.String(),
                        IsChangeBook = c.Boolean(nullable: false),
                        ChangeBookCause = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudentCourseStatusMOD",
                c => new
                    {
                        StatusID = c.Int(nullable: false),
                        AddTime = c.DateTime(nullable: false),
                        ChinaName = c.String(),
                        IsEffective = c.Boolean(nullable: false),
                        EnglishName = c.String(),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.StudentProductMOD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AddTime = c.DateTime(nullable: false),
                        StudentID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        ProductName = c.String(),
                        Frequency = c.Int(nullable: false),
                        LimitDate = c.Int(nullable: false),
                        TotalCourseCount = c.Int(nullable: false),
                        LeaveCount = c.Int(nullable: false),
                        PriceMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductStatus = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        RestOfCourseCount = c.Int(nullable: false),
                        UseLeaveCount = c.Int(nullable: false),
                        IsInstallment = c.Boolean(nullable: false),
                        UnliquidatedMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NextPayDate = c.DateTime(nullable: false),
                        NextPayMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudentProductPayRecordMOD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AddTime = c.DateTime(nullable: false),
                        StudentProductID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        PayDate = c.DateTime(nullable: false),
                        PayMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ManagerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudentSuspendSchoolingRecordMOD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AddTime = c.DateTime(nullable: false),
                        StudentID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SystemParameMOD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AddTime = c.DateTime(nullable: false),
                        Name = c.String(),
                        Value = c.Int(nullable: false),
                        IsEnable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TimeZoneMOD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UTCName = c.String(),
                        UTCHours = c.Int(nullable: false),
                        UTCMinutes = c.Int(nullable: false),
                        DSTName = c.String(),
                        DST = c.Int(nullable: false),
                        DSTStartTime = c.DateTime(),
                        DSTEndTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TimeZoneMOD");
            DropTable("dbo.SystemParameMOD");
            DropTable("dbo.StudentSuspendSchoolingRecordMOD");
            DropTable("dbo.StudentProductPayRecordMOD");
            DropTable("dbo.StudentProductMOD");
            DropTable("dbo.StudentCourseStatusMOD");
            DropTable("dbo.StudentCourseRecordMOD");
            DropTable("dbo.StudentMOD");
            DropTable("dbo.ProductMOD");
            DropTable("dbo.MenuModel");
            DropTable("dbo.ManagerLogModel");
            DropTable("dbo.ManagerModel");
            DropTable("dbo.CourseRecordMOD");
            DropTable("dbo.BookMOD");
            DropTable("dbo.AuthorityItemModel");
            DropTable("dbo.AuthorityModel");
        }
    }
}
