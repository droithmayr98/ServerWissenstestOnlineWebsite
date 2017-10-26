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
            Bezirk Braunau = new Bezirk{ Bezirksname = "Braunau" };
            Bezirk Eferding = new Bezirk { Bezirksname = "Eferding" };
            Bezirk Freistadt = new Bezirk { Bezirksname = "Freistadt" };
            Bezirk Gmunden = new Bezirk { Bezirksname = "Gmunden" };
            Bezirk Grieskirchen = new Bezirk { Bezirksname = "Grieskirchen" };
            Bezirk Kirchdorf = new Bezirk { Bezirksname = "Kirchdorf" };
            Bezirk Linz = new Bezirk { Bezirksname = "Linz" };
            Bezirk Linz_Land = new Bezirk { Bezirksname = "Linz-Land" };
            Bezirk Perg = new Bezirk { Bezirksname = "Perg" };
            Bezirk Ried = new Bezirk { Bezirksname = "Ried" };
            Bezirk Steyr = new Bezirk { Bezirksname = "Steyr" };
            Bezirk Steyr_Land = new Bezirk { Bezirksname = "Steyr-Land" };
            Bezirk Rohrbach = new Bezirk { Bezirksname = "Rohrbach" };
            Bezirk Schärding = new Bezirk { Bezirksname = "Schärding" };
            Bezirk Urfahr_Umgebung = new Bezirk { Bezirksname = "Urfahr-Umgebung" };
            Bezirk Vöcklabruck = new Bezirk { Bezirksname = "Vöcklabruck" };
            Bezirk Wels = new Bezirk { Bezirksname = "Wels" };
            Bezirk Wels_Land = new Bezirk { Bezirksname = "Wels-Land" };
            #endregion----------------------------------------------------------------------------------------

            #region---------------------------------------------------------------Admins erzeugen
            Admintable a1 = new Admintable { Username = "testadmin1", Password = "hallo123", Can_create_acc = true, Can_delete_acc = true, Can_edit_acc = true };
            Admintable a2 = new Admintable { Username = "testadmin2", Password = "test123", Can_create_acc = true, Can_delete_acc = true, Can_edit_acc = false };
            Admintable a3 = new Admintable { Username = "testadmin3", Password = "htlgkr123", Can_create_acc = true, Can_delete_acc = false, Can_edit_acc = false };
            Admintable a4 = new Admintable { Username = "testadmin4", Password = "affe909", Can_create_acc = false, Can_delete_acc = false, Can_edit_acc = false };
            #endregion---------------------------------------------------------------------------------

            #region--------------------------------------------------Typendefinitionen erzeugen
            //Fragen
            Typendefinition F_T = new Typendefinition { Typ = "F_T" };
            Typendefinition F_TB = new Typendefinition { Typ = "F_T+B" };
            Typendefinition F_TV = new Typendefinition { Typ = "F_T+V" };
            //Info
            Typendefinition I_T = new Typendefinition { Typ = "I_T" };
            Typendefinition I_TB = new Typendefinition { Typ = "I_T+B" };
            Typendefinition I_TV = new Typendefinition { Typ = "I_T+V" };
            Typendefinition I_TT = new Typendefinition { Typ = "I_TT" };
            Typendefinition I_TBB = new Typendefinition { Typ = "I_T+BB" };
            Typendefinition I_TTVV = new Typendefinition { Typ = "I_TT+VV" };
            Typendefinition I_TBBV = new Typendefinition { Typ = "I_T+BB+V" };
            //Antworten
            Typendefinition A_T = new Typendefinition { Typ = "A_T" };
            Typendefinition A_S = new Typendefinition { Typ = "A_S" };
            Typendefinition A_DP = new Typendefinition { Typ = "A_DP" };
            Typendefinition A_CBxT = new Typendefinition { Typ = "A_CB:T" };
            Typendefinition A_CBxB = new Typendefinition { Typ = "A_CB:B" };
            Typendefinition A_RBxT = new Typendefinition { Typ = "A_RB:T" };
            Typendefinition A_RBxB = new Typendefinition { Typ = "A_RB:B" };
            Typendefinition A_VxTTuM = new Typendefinition { Typ = "A_V:T-T?M" };
            Typendefinition A_VxBTuM = new Typendefinition { Typ = "A_V:B-T?M" };
            Typendefinition A_VxBBuM = new Typendefinition { Typ = "A_V:B-B?M" };
            Typendefinition A_VxTTuV = new Typendefinition { Typ = "A_V:T-T?V" };
            Typendefinition A_VxBTuV = new Typendefinition { Typ = "A_V:B-T?V" };
            Typendefinition A_VxBBuV = new Typendefinition { Typ = "A_V:B-B?V" };
            #endregion----------------------------------------------------------------------------

            #region-----------------------------------------------Stufen erzeugen
            Stufe Bronze = new Stufe { Stufenname = "Bronze" };
            Stufe Silver = new Stufe { Stufenname = "Silver" };
            Stufe Gold = new Stufe { Stufenname = "Gold" };
            #endregion-------------------------------------------------

            #region---------------------------------------Orte erzeugen
            Ort Eggerding = new Ort { Ortsname = "Eggerding", Bezirk = Schärding };
            Ort Hof = new Ort { Ortsname = "Hof", Bezirk = Schärding };
            Ort Maasbach = new Ort { Ortsname = "Maasbach", Bezirk = Schärding };
            Ort Hartkirchen = new Ort { Ortsname = "Hartkirchen", Bezirk = Eferding };
            #endregion---------------------------------------

            #region--------------------------------------------------------Stationen erzeugen
            Station Allgemeinwissen = new Station { Stationsname = "Allgemeinwissen" };
            Station Dienstgrade = new Station { Stationsname = "Dienstgrade" };
            Station Geräte = new Station { Stationsname = "Wasserführende Amateuren + technische Geräte" };
            Station VorbeugenderBrandschutz = new Station { Stationsname = "Vorbeugender Brandschutz" };
            Station Seilknoten = new Station { Stationsname = "Seilknoten" };
            Station Nachrichtenübermittlung = new Station { Stationsname = "Nachrichtenübermittlung" };
            Station Verkehrserziehung = new Station { Stationsname = "Verkehrserziehung und Absichern von Einsatzstellen" };
            Station ErsteHilfe = new Station { Stationsname = "Erste Hilfe" };
            Station Taktik = new Station { Stationsname = "Taktik" };
            Station GefährlicheStoffe = new Station { Stationsname = "Gefährliche Stoffe" };
            Station Atemschutz = new Station { Stationsname = "Atem- und Körperschutz" };
            #endregion-------------------------------------------------------

            #region---------------------------------------------------Fragen erzeugen
            Frage fText1 = new Frage {Typ = F_T, Fragetext = "Zu welchen Bezirk gehört deine Gemeinde?"}; //Antwort Text
            Frage fText2 = new Frage { Typ = F_T, Fragetext = "Welchen Dienstgrad hat der Gerätewart?" }; //Antwort Text
            Frage fSlider1 = new Frage { Typ = F_T, Fragetext = "Wie lange ist ein C-Druckschlauch?" }; //Antwort Slider
            Frage fSlider2 = new Frage { Typ = F_T, Fragetext = "Wie lange ist ein B-Druckschlauch?" }; //Antwort Slider
            Frage fCheckBox1 = new Frage { Typ = F_T, Fragetext = "Welche Feuerwehrfahrzeuge besitzt deine Feuerwehr?" }; //Antwort CheckBox
            Frage fCheckBox2 = new Frage { Typ = F_T, Fragetext = "Welche Mitglieder hat die Feuerwehr?" }; //Antwort CheckBox
            Frage fRadioButtons1 = new Frage { Typ = F_T, Fragetext = "Wer ist der Schutzpatron der Feuerwehr?" }; //Antwort RadioButtons
            Frage fRadioButtons2 = new Frage { Typ = F_T, Fragetext = "Wie viele Bezirke hat OÖ?" }; //Antwort RadioButtons
            Frage fDatePicker1 = new Frage { Typ = F_T, Fragetext = "Wann habe ich Geburtstag?" }; //Antwort DatePicker
            Frage fDatePicker2 = new Frage { Typ = F_T, Fragetext = "Wann ist Weihnachten?" }; //Antwort DatePicker
            Frage fVerbinden1 = new Frage { Typ = F_T, Fragetext = "Gleiche Buchstaben Memory?" }; //Antwort Verbinden
            Frage fVerbinden2 = new Frage { Typ = F_T, Fragetext = "Gleiche Zahlen Memory?" }; //Antwort Verbinden
            #endregion----------------------------------------------

            #region-----------------------------------Zusatzinfo erzeugen
            Zusatzinfo zusatzinfoText1 = new Zusatzinfo { Typ = I_T};
            Zusatzinfo zusatzinfoText2 = new Zusatzinfo { Typ = I_T };
            Zusatzinfo zusatzinfoText3 = new Zusatzinfo { Typ = I_T };
            Zusatzinfo zusatzinfoText4 = new Zusatzinfo { Typ = I_T };
            Zusatzinfo zusatzinfoText5 = new Zusatzinfo { Typ = I_T };
            #endregion---------------------------------------------

            #region-----------------------------------InfoContent erzeugen
            InfoContent info1_zusatzinfoText1 = new InfoContent { Zusatzinfo = zusatzinfoText1, Heading = "Allgemeines", Info_Content= "Oberösterreich ist ein österreichisches Bundesland; Landeshauptstadt ist Linz. Oberösterreich ist mit 11.982 Quadratkilometern flächenmäßig das viertgrößte und mit 1,47 Millionen Einwohnern bevölkerungsmäßig das drittgrößte Bundesland Österreichs. Es grenzt an Bayern (Deutschland), Südböhmen (Tschechien) sowie innerösterreichisch an Niederösterreich, die Steiermark und das Land Salzburg. Der Name des Landes leitet sich ab vom Namen des Vorgängerterritoriums, des Erzherzogtums Österreich ob der Enns, einem der habsburgischen Erblande." };
            InfoContent info2_zusatzinfoText1 = new InfoContent { Zusatzinfo = zusatzinfoText1, Heading = "Seen", Info_Content = "Praktisch alle großen oberösterreichischen Seen liegen im Salzkammergut, so der Almsee, Attersee, die Gosauseen, Hallstätter See, Irrsee, Langbathseen, Mondsee, Offensee, Traunsee und der Wolfgangsee." };
            InfoContent info3_zusatzinfoText1 = new InfoContent { Zusatzinfo = zusatzinfoText1, Heading = "Molassezone", Info_Content = "Zwischen diesen beiden sehr unterschiedlichen Gebirgen befindet sich eine Sedimentationszone, die durch die Ablagerungen der Erosion in den Alpen entstanden ist, die sogenannte Titenzone." };
            InfoContent info4_zusatzinfoText1 = new InfoContent { Zusatzinfo = zusatzinfoText1, Heading = "Granit- und Gneishochland", Info_Content = "Nördlich des Donautales befindet sich die Böhmische Masse (auch Böhmisches Massiv), die geologisch älteste Landschaft Österreichs. Sie ist ein altes Faltengebirge und besteht im westlichen Teil aus dem Moldanubikum, im östlichen Teil (außerhalb von Oberösterreich) aus dem Moravikum. Die Böhmische Masse stellt den Sockel eines abgetragenen, einstigen Hochgebirges (Grundgebirge genannt) dar, das im Zuge der Variszischen Orogenese (Gebirgsbildung) im Paläozoikum entstand. Weitere Reste dieser Gebirgsbildung in Mitteleuropa sind die deutschen Mittelgebirge. " };
            InfoContent info5_zusatzinfoText1 = new InfoContent { Zusatzinfo = zusatzinfoText1, Info_Content = "Südlich der variszischen Gebirgskette erstreckte sich damals die Tethys, die beim Auseinanderdriften der Kontinentalplatten gegen Ende des Paläozoikums immer größer wurde. Unter tropischen bzw. subtropischen Bedingungen wurden hier während des Mesozoikums jene Sedimente abgelagert, die dann später bei der alpidischen Gebirgsbildung, die gegen Ende der Kreide einsetzte, überschoben und nach Norden transportiert wurden." };

            InfoContent info1_zusatzinfoText2 = new InfoContent { Zusatzinfo = zusatzinfoText2, Heading = "Dienstgrade", Info_Content = "Je nach Dienstverwendung oder Dienstalter hat jedes Feuerwehrmitglied in Österreich einen Dienstgrad." };
            InfoContent info2_zusatzinfoText2 = new InfoContent { Zusatzinfo = zusatzinfoText2, Heading = "Dienstgradabzeichen", Info_Content = "Die Kennzeichnung auf den Uniformen erfolgt entweder mit Aufschiebeschlaufen auf der Einsatzbekleidung und auf der Dienstbekleidung marineblau, grün bzw. sandbraun. Auch die Uniformhemden haben Aufschiebeschlaufen. Die Dienstbekleidung, die eigentlich nur bei festlichen Anlässen getragen wird, hat Kragenspiegel. Auch auf dem Feuerwehrhelm gibt es teilweise Kennzeichnungen, die aber nicht einheitlich geregelt sind. Meist werden rote, reflektierende Streifen für die eingeteilten Feuerwehrmitglieder, silberne für Chargen sowie goldene für die Funktionäre aufgeklebt." };
            InfoContent info3_zusatzinfoText2 = new InfoContent { Zusatzinfo = zusatzinfoText2, Info_Content = "Dienstgrade, welche gewählt werden, haben eine Funktionsperiode von fünf Jahren. Gewählt wird jeweils in den Monaten Jänner bis März im ersten und sechsten Jahr eines Jahrzehnts. Die letzte Wahl fand 2016 statt. Scheidet jemand vorzeitig aus einer Funktion aus, wird zwar diese Funktion neu gewählt, endet aber mit der nächsten Wahl. Der Leiter des Verwaltungsdienstes sowie die Chargen und Warte der Feuerwehr werden jeweils für die laufende Funktionsperiode vom Feuerwehrkommandanten ernannt." };

            InfoContent info1_zusatzinfoText3 = new InfoContent { Zusatzinfo = zusatzinfoText3, Heading = "Allgemeines", Info_Content = "Ein Feuerwehrschlauch ist ein wesentlicher Ausrüstungsgegenstand der Feuerwehr und hat die Aufgabe, das Löschmittel Wasser oder Wasser/Schaum-Gemische über Wegstrecken zu fördern. Die Schläuche lassen sich grob in zwei Typen unterscheiden: Schläuche, durch die Wasser oder eine andere nicht aggressive Flüssigkeit gesaugt werden kann (Saugschläuche) und Schläuche, die unter Druck Wasser, Wasser-Schaumgemisch, CAFS-Schaum oder andere, nicht aggressive Flüssigkeiten weiterleiten (Druckschläuche). " };
            InfoContent info2_zusatzinfoText3 = new InfoContent { Zusatzinfo = zusatzinfoText3, Heading = "Druckschläuche", Info_Content = "Druckschläuche haben die primäre Aufgabe der Wasserweiterleitung. Sie werden vorwiegend bei der Brandbekämpfung verwendet, bei der mit entsprechend gewählten Strahlrohren Löschmittel abgegeben werden kann. Sie können aber auch zum Ableiten etwa von Schmutzwasser im Hochwassereinsatz o.ä. genutzt werden. Daher gelten sie grundsätzlich als nicht für die Trinkwasser-Förderung – etwa zur Versorgung von Feldküchen, Behandlungsplätzen oder ganzen Orten, etc. – zulässig. Für die Trinkwasserförderung kommen besonders gekennzeichnete, ansonsten baugleiche Druckschläuche zum Einsatz, die auch rechtlich ganz anderen Vorschriften unterliegen." };

            InfoContent info1_zusatzinfoText4 = new InfoContent { Zusatzinfo = zusatzinfoText4, Heading = "Feuerwehrfahrzeuge", Info_Content = "Ein Feuerwehrfahrzeug ist ein Kraftfahrzeug, das die Feuerwehr im Rahmen ihrer Einsatztätigkeit verwendet. Auch Anhänger, die für Feuerwehrzwecke verwendet werden, fallen in diese Kategorie. Um im Straßenverkehr besondere Rechte in Anspruch nehmen zu können, sind die Feuerwehrfahrzeuge speziell gekennzeichnet und mit Sondersignalen ausgestattet. In Kontinentaleuropa sind die Fahrzeuge meist rot (zum Beispiel RAL 3000) bzw. leuchtrot (zum Beispiel RAL 3024, RAL 3026) lackiert." };
            #endregion---------------------------------------------




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

            #region---------------------------------------------Stattionen in DB einfügen
            context.Stationen.AddOrUpdate(Allgemeinwissen);
            context.Stationen.AddOrUpdate(Dienstgrade);
            context.Stationen.AddOrUpdate(Geräte);
            context.Stationen.AddOrUpdate(VorbeugenderBrandschutz);
            context.Stationen.AddOrUpdate(Seilknoten);
            context.Stationen.AddOrUpdate(Nachrichtenübermittlung);
            context.Stationen.AddOrUpdate(Verkehrserziehung);
            context.Stationen.AddOrUpdate(ErsteHilfe);
            context.Stationen.AddOrUpdate(Taktik);
            context.Stationen.AddOrUpdate(GefährlicheStoffe);
            context.Stationen.AddOrUpdate(Atemschutz);
            #endregion--------------------------------------------

            #region------------------------------------Fragen in DB einfügen
            context.Fragen.AddOrUpdate(fText1);
            context.Fragen.AddOrUpdate(fText2);
            context.Fragen.AddOrUpdate(fSlider1);
            context.Fragen.AddOrUpdate(fSlider2);
            context.Fragen.AddOrUpdate(fCheckBox1);
            context.Fragen.AddOrUpdate(fCheckBox2);
            context.Fragen.AddOrUpdate(fRadioButtons1);
            context.Fragen.AddOrUpdate(fRadioButtons2);
            context.Fragen.AddOrUpdate(fDatePicker1);
            context.Fragen.AddOrUpdate(fDatePicker2);
            context.Fragen.AddOrUpdate(fVerbinden1);
            context.Fragen.AddOrUpdate(fVerbinden2);
            #endregion----------------------------------------------------

            #region----------------------------------------------Zusatzinfo in DB einfügen
            context.Zusatzinfos.AddOrUpdate(zusatzinfoText1);
            context.Zusatzinfos.AddOrUpdate(zusatzinfoText2);
            context.Zusatzinfos.AddOrUpdate(zusatzinfoText3);
            context.Zusatzinfos.AddOrUpdate(zusatzinfoText4);
            context.Zusatzinfos.AddOrUpdate(zusatzinfoText5);
            #endregion

            #region----------------------------------------------InfoContent in DB einfügen
            context.InfoContentM.AddOrUpdate(info1_zusatzinfoText1);
            context.InfoContentM.AddOrUpdate(info2_zusatzinfoText1);
            context.InfoContentM.AddOrUpdate(info3_zusatzinfoText1);
            context.InfoContentM.AddOrUpdate(info4_zusatzinfoText1);
            context.InfoContentM.AddOrUpdate(info5_zusatzinfoText1);

            context.InfoContentM.AddOrUpdate(info1_zusatzinfoText2);
            context.InfoContentM.AddOrUpdate(info2_zusatzinfoText2);
            context.InfoContentM.AddOrUpdate(info3_zusatzinfoText2);

            context.InfoContentM.AddOrUpdate(info1_zusatzinfoText3);
            context.InfoContentM.AddOrUpdate(info2_zusatzinfoText3);

            context.InfoContentM.AddOrUpdate(info1_zusatzinfoText4);
            #endregion


            context.SaveChanges();



        }
    }
}
