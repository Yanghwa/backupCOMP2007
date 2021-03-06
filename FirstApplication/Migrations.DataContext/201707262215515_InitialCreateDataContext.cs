namespace FirstApplication.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateDataContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameGenres",
                c => new
                    {
                        GameGenreId = c.String(nullable: false, maxLength: 128),
                        GenreId = c.String(nullable: false, maxLength: 128),
                        GameId = c.String(nullable: false, maxLength: 128),
                        CreateDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GameGenreId)
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .Index(t => t.GenreId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 250),
                        IsMultiplayer = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        GameId = c.String(nullable: false, maxLength: 128),
                        Rank = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 250),
                        CreateDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameGenres", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Ratings", "GameId", "dbo.Games");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GameGenres", "GameId", "dbo.Games");
            DropIndex("dbo.Ratings", new[] { "GameId" });
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropIndex("dbo.GameGenres", new[] { "GameId" });
            DropIndex("dbo.GameGenres", new[] { "GenreId" });
            DropTable("dbo.Genres");
            
            DropTable("dbo.Ratings");
            DropTable("dbo.Games");
            DropTable("dbo.GameGenres");
        }
    }
}
