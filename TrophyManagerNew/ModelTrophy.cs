namespace TrophyManagerNew
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ModelTrophy : DbContext
    {
        // Your context has been configured to use a 'ModelTrophy' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TrophyManagerNew.ModelTrophy' database on your LocalDb instance. 
        public ModelTrophy(string connectionString)
            : base(connectionString)
        {
        }

        // If you wish to target a different database and/or database provider, modify the 'ModelTrophy' 
        // connection string in the application configuration file.
        public ModelTrophy()
            : base("name=ModelTrophy")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Version> Versions { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SkillDictionary> SkillsDictionary { get; set; }
        public virtual DbSet<Player> Players { get; set; }
    }

    public class Version
    {
        public long Id { get; set; }
        public System.DateTime Data { get; set; }
        public string FileName { get; set; }
        public string DateWeekYear { get; set; }
    }

    public class Skill
    {
        public long Id { get; set; }
        public long VerId { get; set; }
        public long SkillDictId { get; set; }
        public double SkillValue { get; set; }
        public long PlayerId { get; set; }

    }

    public class SkillDictionary
    {
        public long Id { get; set; }
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
        public string SkillVisName { get; set; }

    }

    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PositionCode { get; set; }
        public string TrophyCode { get; set; }
    }
}