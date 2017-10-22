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
            Bezirk Braunau = new Bezirk{Bezirk_Id = 1, Bezirksname = "Braunau" };
            Bezirk Eferding = new Bezirk { Bezirk_Id = 2, Bezirksname = "Eferding" };
            Bezirk Freistadt = new Bezirk { Bezirk_Id = 3, Bezirksname = "Freistadt" };
            Bezirk Gmunden = new Bezirk { Bezirk_Id = 4, Bezirksname = "Gmunden" };
            Bezirk Grieskirchen = new Bezirk { Bezirk_Id = 5, Bezirksname = "Grieskirchen" };
            Bezirk Kirchdorf = new Bezirk { Bezirk_Id = 6, Bezirksname = "Kirchdorf" };
            Bezirk Linz = new Bezirk { Bezirk_Id = 7, Bezirksname = "Linz" };
            Bezirk Linz_Land = new Bezirk { Bezirk_Id = 8, Bezirksname = "Linz-Land" };
            Bezirk Perg = new Bezirk { Bezirk_Id = 9, Bezirksname = "Perg" };
            Bezirk Ried = new Bezirk { Bezirk_Id = 10, Bezirksname = "Ried" };
            Bezirk Steyr = new Bezirk { Bezirk_Id = 11, Bezirksname = "Steyr" };
            Bezirk Steyr_Land = new Bezirk { Bezirk_Id = 12, Bezirksname = "Steyr-Land" };
            Bezirk Rohrbach = new Bezirk { Bezirk_Id = 13, Bezirksname = "Rohrbach" };
            Bezirk Schärding = new Bezirk { Bezirk_Id = 14, Bezirksname = "Schärding" };
            Bezirk Urfahr_Umgebung = new Bezirk { Bezirk_Id = 15, Bezirksname = "Urfahr-Umgebung" };
            Bezirk Vöcklabruck = new Bezirk { Bezirk_Id = 16, Bezirksname = "Vöcklabruck" };
            Bezirk Wels = new Bezirk { Bezirk_Id = 17, Bezirksname = "Wels" };
            Bezirk Wels_Land = new Bezirk { Bezirk_Id = 18, Bezirksname = "Wels-Land" };
            #endregion----------------------------------------------------------------------------------------

            #region---------------------------------------------------------------Admins erzeugen
            Admintable a1 = new Admintable { Admin_Id = 1 ,Username = "testadmin1", Password = "hallo123", Can_create_acc = true, Can_delete_acc = true, Can_edit_acc = true };
            Admintable a2 = new Admintable { Admin_Id = 2, Username = "testadmin2", Password = "test123", Can_create_acc = true, Can_delete_acc = true, Can_edit_acc = false };
            Admintable a3 = new Admintable { Admin_Id = 3, Username = "testadmin3", Password = "htlgkr123", Can_create_acc = true, Can_delete_acc = false, Can_edit_acc = false };
            Admintable a4 = new Admintable { Admin_Id = 4, Username = "testadmin4", Password = "affe909", Can_create_acc = false, Can_delete_acc = false, Can_edit_acc = false };
            #endregion---------------------------------------------------------------------------------

            #region--------------------------------------------------Typendefinitionen erzeugen
            //Fragen
            Typendefinition F_T = new Typendefinition { Typ_Id = 1, Typ = "F_T" };
            Typendefinition F_TB = new Typendefinition { Typ_Id = 2, Typ = "F_T+B" };
            Typendefinition F_TV = new Typendefinition { Typ_Id = 3, Typ = "F_T+V" };
            //Info
            Typendefinition I_T = new Typendefinition { Typ_Id = 4, Typ = "I_T" };
            Typendefinition I_TB = new Typendefinition { Typ_Id = 5, Typ = "I_T+B" };
            Typendefinition I_TV = new Typendefinition { Typ_Id = 6, Typ = "I_T+V" };
            Typendefinition I_TT = new Typendefinition { Typ_Id = 7, Typ = "I_TT" };
            Typendefinition I_TBB = new Typendefinition { Typ_Id = 8, Typ = "I_T+BB" };
            Typendefinition I_TTVV = new Typendefinition { Typ_Id = 9, Typ = "I_TT+VV" };
            Typendefinition I_TBBV = new Typendefinition { Typ_Id = 10, Typ = "I_T+BB+V" };
            //Antworten
            Typendefinition A_T = new Typendefinition { Typ_Id = 11, Typ = "A_T" };
            Typendefinition A_S = new Typendefinition { Typ_Id = 12, Typ = "A_S" };
            Typendefinition A_DP = new Typendefinition { Typ_Id = 13, Typ = "A_DP" };
            Typendefinition A_CBxT = new Typendefinition { Typ_Id = 14, Typ = "A_CB:T" };
            Typendefinition A_CBxB = new Typendefinition { Typ_Id = 15, Typ = "A_CB:B" };
            Typendefinition A_RBxT = new Typendefinition { Typ_Id = 16, Typ = "A_RB:T" };
            Typendefinition A_RBxB = new Typendefinition { Typ_Id = 17, Typ = "A_RB:B" };
            Typendefinition A_VxTTuM = new Typendefinition { Typ_Id = 18, Typ = "A_V:T-T?M" };
            Typendefinition A_VxBTuM = new Typendefinition { Typ_Id = 29, Typ = "A_V:B-T?M" };
            Typendefinition A_VxBBuM = new Typendefinition { Typ_Id = 20, Typ = "A_V:B-B?M" };
            Typendefinition A_VxTTuV = new Typendefinition { Typ_Id = 21, Typ = "A_V:T-T?V" };
            Typendefinition A_VxBTuV = new Typendefinition { Typ_Id = 22, Typ = "A_V:B-T?V" };
            Typendefinition A_VxBBuV = new Typendefinition { Typ_Id = 23, Typ = "A_V:B-B?V" };
            #endregion----------------------------------------------------------------------------

            #region-----------------------------------------------Stufen erzeugen
            Stufe Bronze = new Stufe { Stufe_Id = 1, Stufenname = "Bronze" };
            Stufe Silver = new Stufe { Stufe_Id = 2, Stufenname = "Silver" };
            Stufe Gold = new Stufe { Stufe_Id = 3, Stufenname = "Gold" };
            #endregion-------------------------------------------------

            #region---------------------------------------Orte erzeugen
            Ort Eggerding = new Ort { Ort_Id = 1 ,Ortsname = "Eggerding", Bezirk = Schärding };
            Ort Hof = new Ort { Ort_Id = 2, Ortsname = "Hof", Bezirk = Schärding };
            Ort Maasbach = new Ort { Ort_Id = 3, Ortsname = "Maasbach", Bezirk = Schärding };
            Ort Hartkirchen = new Ort { Ort_Id = 4, Ortsname = "Hartkirchen", Bezirk = Eferding };
            #endregion---------------------------------------



            //Station st1 = new Station { };





            #region-----------------------------------------------------------Bezike in DB einfügen
            context.Bezirke.AddOrUpdate(Braunau);
            context.Bezirke.AddOrUpdate(Eferding);
            context.Bezirke.AddOrUpdate(Freistadt);
            context.Bezirke.AddOrUpdate(Gmunden);
            context.Bezirke.AddOrUpdate(Grieskirchen);
            context.Bezirke.AddOrUpdate(Kirchdorf);
            context.Bezirke.AddOrUpdate(Linz);
            context.Bezirke.AddOrUpdate(Linz_Land);
            context.Bezirke.AddOrUpdate(Perg);
            context.Bezirke.AddOrUpdate(Ried);
            context.Bezirke.AddOrUpdate(Steyr);
            context.Bezirke.AddOrUpdate(Steyr_Land);
            context.Bezirke.AddOrUpdate(Rohrbach);
            context.Bezirke.AddOrUpdate(Schärding);
            context.Bezirke.AddOrUpdate(Urfahr_Umgebung);
            context.Bezirke.AddOrUpdate(Vöcklabruck);
            context.Bezirke.AddOrUpdate(Wels);
            context.Bezirke.AddOrUpdate(Wels_Land);
            #endregion------------------------------------------------------------------------------

            #region--------------------------------------------------------------------Andmins in DB einfügen
            context.Admins.AddOrUpdate(a1);
            context.Admins.AddOrUpdate(a2);
            context.Admins.AddOrUpdate(a3);
            context.Admins.AddOrUpdate(a4);
            #endregion----------------------------------------------------------------------

            #region--------------------------------------------Typendefinitionen in DB einfügen
            context.Typendefinitionen.AddOrUpdate(F_T);
            context.Typendefinitionen.AddOrUpdate(F_TB);
            context.Typendefinitionen.AddOrUpdate(F_TV);
            context.Typendefinitionen.AddOrUpdate(I_T);
            context.Typendefinitionen.AddOrUpdate(I_TB);
            context.Typendefinitionen.AddOrUpdate(I_TV);
            context.Typendefinitionen.AddOrUpdate(I_TT);
            context.Typendefinitionen.AddOrUpdate(I_TBB);
            context.Typendefinitionen.AddOrUpdate(I_TTVV);
            context.Typendefinitionen.AddOrUpdate(I_TBBV);
            context.Typendefinitionen.AddOrUpdate(A_T);
            context.Typendefinitionen.AddOrUpdate(A_S);
            context.Typendefinitionen.AddOrUpdate(A_DP);
            context.Typendefinitionen.AddOrUpdate(A_CBxT);
            context.Typendefinitionen.AddOrUpdate(A_CBxB);
            context.Typendefinitionen.AddOrUpdate(A_RBxT);
            context.Typendefinitionen.AddOrUpdate(A_RBxB);
            context.Typendefinitionen.AddOrUpdate(A_VxTTuM);
            context.Typendefinitionen.AddOrUpdate(A_VxBTuM);
            context.Typendefinitionen.AddOrUpdate(A_VxBBuM);
            context.Typendefinitionen.AddOrUpdate(A_VxTTuV);
            context.Typendefinitionen.AddOrUpdate(A_VxBTuV);
            context.Typendefinitionen.AddOrUpdate(A_VxBBuV);
            #endregion-------------------------------------------------------------------

            #region-----------------------------------------------------------Stufen in DB einfügen
            context.Stufen.AddOrUpdate(Bronze);
            context.Stufen.AddOrUpdate(Silver);
            context.Stufen.AddOrUpdate(Gold);
            #endregion-----------------------------------------------------

            #region---------------------------------------------------Orte in DB einfügen
            context.Orte.AddOrUpdate(Eggerding);
            context.Orte.AddOrUpdate(Hof);
            context.Orte.AddOrUpdate(Maasbach);
            context.Orte.AddOrUpdate(Hartkirchen);
            #endregion-----------------------------------------------------------

            context.Typendefinitionen.AddOrUpdate();


            context.SaveChanges();



        }
    }
}
