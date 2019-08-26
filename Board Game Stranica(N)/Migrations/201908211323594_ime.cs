namespace Board_Game_Stranica_N_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Ime", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Prezime", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "DatumRodenja", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Opis", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Opis");
            DropColumn("dbo.AspNetUsers", "DatumRodenja");
            DropColumn("dbo.AspNetUsers", "Prezime");
            DropColumn("dbo.AspNetUsers", "Ime");
        }
    }
}
