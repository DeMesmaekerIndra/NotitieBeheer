using System.Data.Entity;

namespace IndraDeMesmaeker.NotitieBeheer.Data
{
    public class NotitieBeheerContext : DbContext
    {
        #region properties
        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
        #endregion

        public NotitieBeheerContext() : base("dbIndraDeMesmaekerNotitieBeheer")
        {
            Database.SetInitializer(new NotitieBeheerContextDropCreateAlways());
            //Database.SetInitializer(new NotitieBeheerContextDropCreateDatabaseIfModelChanges());
        }
    }
}
