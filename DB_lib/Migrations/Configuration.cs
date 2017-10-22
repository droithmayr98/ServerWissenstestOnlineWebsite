namespace DB_lib.Migrations
{
    using DB_lib.Tables;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DB_lib.TestDB_Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        protected override void Seed(DB_lib.TestDB_Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            #region----------------------------------------------------------------------Bezirke erzeugen
            Bezirk b01 = new Bezirk{Bezirk_Id = 1, Bezirksname = "Braunau" };
            Bezirk b02 = new Bezirk { Bezirk_Id = 2, Bezirksname = "Eferding" };
            Bezirk b03 = new Bezirk { Bezirk_Id = 3, Bezirksname = "Freistadt" };
            Bezirk b04 = new Bezirk { Bezirk_Id = 4, Bezirksname = "Gmunden" };
            Bezirk b05 = new Bezirk { Bezirk_Id = 5, Bezirksname = "Grieskirchen" };
            Bezirk b06 = new Bezirk { Bezirk_Id = 6, Bezirksname = "Kirchdorf" };
            Bezirk b07 = new Bezirk { Bezirk_Id = 7, Bezirksname = "Linz" };
            Bezirk b08 = new Bezirk { Bezirk_Id = 8, Bezirksname = "Linz-Land" };
            Bezirk b09 = new Bezirk { Bezirk_Id = 9, Bezirksname = "Perg" };
            Bezirk b10 = new Bezirk { Bezirk_Id = 10, Bezirksname = "Ried" };
            Bezirk b11 = new Bezirk { Bezirk_Id = 11, Bezirksname = "Steyr" };
            Bezirk b12 = new Bezirk { Bezirk_Id = 11, Bezirksname = "Steyr-Land" };
            Bezirk b13 = new Bezirk { Bezirk_Id = 11, Bezirksname = "Rohrbach" };
            Bezirk b14 = new Bezirk { Bezirk_Id = 11, Bezirksname = "Schärding" };
            Bezirk b15 = new Bezirk { Bezirk_Id = 11, Bezirksname = "Urfahr-Umgebung" };
            Bezirk b16 = new Bezirk { Bezirk_Id = 11, Bezirksname = "Vöcklabruck" };
            Bezirk b17 = new Bezirk { Bezirk_Id = 11, Bezirksname = "Wels" };
            Bezirk b18 = new Bezirk { Bezirk_Id = 11, Bezirksname = "Wels-Land" };
            #endregion----------------------------------------------------------------------------------------

            Admintable a1 = new Admintable { Admin_Id = 1 ,Username = "testadmin1", Password = "hallo123", Can_create_acc = true, Can_delete_acc = true, Can_edit_acc = true };
            Admintable a2 = new Admintable { Admin_Id = 2, Username = "testadmin2", Password = "test123", Can_create_acc = true, Can_delete_acc = true, Can_edit_acc = false };
            Admintable a3 = new Admintable { Admin_Id = 3, Username = "testadmin3", Password = "htlgkr123", Can_create_acc = true, Can_delete_acc = false, Can_edit_acc = false };
            Admintable a4 = new Admintable { Admin_Id = 4, Username = "testadmin4", Password = "affe909", Can_create_acc = false, Can_delete_acc = false, Can_edit_acc = false };

            #region-----------------------------------------------------------Bezike in die Datenbank einfügen
            context.Bezirke.AddOrUpdate(b01);
            context.Bezirke.AddOrUpdate(b02);
            context.Bezirke.AddOrUpdate(b03);
            context.Bezirke.AddOrUpdate(b04);
            context.Bezirke.AddOrUpdate(b05);
            context.Bezirke.AddOrUpdate(b06);
            context.Bezirke.AddOrUpdate(b07);
            context.Bezirke.AddOrUpdate(b08);
            context.Bezirke.AddOrUpdate(b09);
            context.Bezirke.AddOrUpdate(b10);
            context.Bezirke.AddOrUpdate(b11);
            context.Bezirke.AddOrUpdate(b12);
            context.Bezirke.AddOrUpdate(b13);
            context.Bezirke.AddOrUpdate(b14);
            context.Bezirke.AddOrUpdate(b15);
            context.Bezirke.AddOrUpdate(b16);
            context.Bezirke.AddOrUpdate(b17);
            context.Bezirke.AddOrUpdate(b18);
            #endregion------------------------------------------------------------------------------

            context.Admins.AddOrUpdate(a1);
            context.Admins.AddOrUpdate(a2);
            context.Admins.AddOrUpdate(a3);
            context.Admins.AddOrUpdate(a4);


            context.SaveChanges();



        }
    }
}
