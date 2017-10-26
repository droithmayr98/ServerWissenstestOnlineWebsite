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
            Bezirk Sch�rding = new Bezirk { Bezirksname = "Sch�rding" };
            Bezirk Urfahr_Umgebung = new Bezirk { Bezirksname = "Urfahr-Umgebung" };
            Bezirk V�cklabruck = new Bezirk { Bezirksname = "V�cklabruck" };
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
            Ort Eggerding = new Ort { Ortsname = "Eggerding", Bezirk = Sch�rding };
            Ort Hof = new Ort { Ortsname = "Hof", Bezirk = Sch�rding };
            Ort Maasbach = new Ort { Ortsname = "Maasbach", Bezirk = Sch�rding };
            Ort Hartkirchen = new Ort { Ortsname = "Hartkirchen", Bezirk = Eferding };
            #endregion---------------------------------------

            #region--------------------------------------------------------Stationen erzeugen
            Station Allgemeinwissen = new Station { Stationsname = "Allgemeinwissen" };
            Station Dienstgrade = new Station { Stationsname = "Dienstgrade" };
            Station Ger�te = new Station { Stationsname = "Wasserf�hrende Amateuren + technische Ger�te" };
            Station VorbeugenderBrandschutz = new Station { Stationsname = "Vorbeugender Brandschutz" };
            Station Seilknoten = new Station { Stationsname = "Seilknoten" };
            Station Nachrichten�bermittlung = new Station { Stationsname = "Nachrichten�bermittlung" };
            Station Verkehrserziehung = new Station { Stationsname = "Verkehrserziehung und Absichern von Einsatzstellen" };
            Station ErsteHilfe = new Station { Stationsname = "Erste Hilfe" };
            Station Taktik = new Station { Stationsname = "Taktik" };
            Station Gef�hrlicheStoffe = new Station { Stationsname = "Gef�hrliche Stoffe" };
            Station Atemschutz = new Station { Stationsname = "Atem- und K�rperschutz" };
            #endregion-------------------------------------------------------

            #region---------------------------------------------------Fragen erzeugen
            Frage fText1 = new Frage {Typ = F_T, Fragetext = "Zu welchen Bezirk geh�rt deine Gemeinde?"}; //Antwort Text
            Frage fText2 = new Frage { Typ = F_T, Fragetext = "Welchen Dienstgrad hat der Ger�tewart?" }; //Antwort Text
            Frage fSlider1 = new Frage { Typ = F_T, Fragetext = "Wie lange ist ein C-Druckschlauch?" }; //Antwort Slider
            Frage fSlider2 = new Frage { Typ = F_T, Fragetext = "Wie lange ist ein B-Druckschlauch?" }; //Antwort Slider
            Frage fCheckBox1 = new Frage { Typ = F_T, Fragetext = "Welche Feuerwehrfahrzeuge besitzt deine Feuerwehr?" }; //Antwort CheckBox
            Frage fCheckBox2 = new Frage { Typ = F_T, Fragetext = "Welche Mitglieder hat die Feuerwehr?" }; //Antwort CheckBox
            Frage fRadioButtons1 = new Frage { Typ = F_T, Fragetext = "Wer ist der Schutzpatron der Feuerwehr?" }; //Antwort RadioButtons
            Frage fRadioButtons2 = new Frage { Typ = F_T, Fragetext = "Wie viele Bezirke hat O�?" }; //Antwort RadioButtons
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
            InfoContent info1_zusatzinfoText1 = new InfoContent { Zusatzinfo = zusatzinfoText1, Heading = "Allgemeines", Info_Content= "Ober�sterreich ist ein �sterreichisches Bundesland; Landeshauptstadt ist Linz. Ober�sterreich ist mit 11.982 Quadratkilometern fl�chenm��ig das viertgr��te und mit 1,47 Millionen Einwohnern bev�lkerungsm��ig das drittgr��te Bundesland �sterreichs. Es grenzt an Bayern (Deutschland), S�db�hmen (Tschechien) sowie inner�sterreichisch an Nieder�sterreich, die Steiermark und das Land Salzburg. Der Name des Landes leitet sich ab vom Namen des Vorg�ngerterritoriums, des Erzherzogtums �sterreich ob der Enns, einem der habsburgischen Erblande." };
            InfoContent info2_zusatzinfoText1 = new InfoContent { Zusatzinfo = zusatzinfoText1, Heading = "Seen", Info_Content = "Praktisch alle gro�en ober�sterreichischen Seen liegen im Salzkammergut, so der Almsee, Attersee, die Gosauseen, Hallst�tter See, Irrsee, Langbathseen, Mondsee, Offensee, Traunsee und der Wolfgangsee." };
            InfoContent info3_zusatzinfoText1 = new InfoContent { Zusatzinfo = zusatzinfoText1, Heading = "Molassezone", Info_Content = "Zwischen diesen beiden sehr unterschiedlichen Gebirgen befindet sich eine Sedimentationszone, die durch die Ablagerungen der Erosion in den Alpen entstanden ist, die sogenannte Titenzone." };
            InfoContent info4_zusatzinfoText1 = new InfoContent { Zusatzinfo = zusatzinfoText1, Heading = "Granit- und Gneishochland", Info_Content = "N�rdlich des Donautales befindet sich die B�hmische Masse (auch B�hmisches Massiv), die geologisch �lteste Landschaft �sterreichs. Sie ist ein altes Faltengebirge und besteht im westlichen Teil aus dem Moldanubikum, im �stlichen Teil (au�erhalb von Ober�sterreich) aus dem Moravikum. Die B�hmische Masse stellt den Sockel eines abgetragenen, einstigen Hochgebirges (Grundgebirge genannt) dar, das im Zuge der Variszischen Orogenese (Gebirgsbildung) im Pal�ozoikum entstand. Weitere Reste dieser Gebirgsbildung in Mitteleuropa sind die deutschen Mittelgebirge. " };
            InfoContent info5_zusatzinfoText1 = new InfoContent { Zusatzinfo = zusatzinfoText1, Info_Content = "S�dlich der variszischen Gebirgskette erstreckte sich damals die Tethys, die beim Auseinanderdriften der Kontinentalplatten gegen Ende des Pal�ozoikums immer gr��er wurde. Unter tropischen bzw. subtropischen Bedingungen wurden hier w�hrend des Mesozoikums jene Sedimente abgelagert, die dann sp�ter bei der alpidischen Gebirgsbildung, die gegen Ende der Kreide einsetzte, �berschoben und nach Norden transportiert wurden." };

            InfoContent info1_zusatzinfoText2 = new InfoContent { Zusatzinfo = zusatzinfoText2, Heading = "Dienstgrade", Info_Content = "Je nach Dienstverwendung oder Dienstalter hat jedes Feuerwehrmitglied in �sterreich einen Dienstgrad." };
            InfoContent info2_zusatzinfoText2 = new InfoContent { Zusatzinfo = zusatzinfoText2, Heading = "Dienstgradabzeichen", Info_Content = "Die Kennzeichnung auf den Uniformen erfolgt entweder mit Aufschiebeschlaufen auf der Einsatzbekleidung und auf der Dienstbekleidung marineblau, gr�n bzw. sandbraun. Auch die Uniformhemden haben Aufschiebeschlaufen. Die Dienstbekleidung, die eigentlich nur bei festlichen Anl�ssen getragen wird, hat Kragenspiegel. Auch auf dem Feuerwehrhelm gibt es teilweise Kennzeichnungen, die aber nicht einheitlich geregelt sind. Meist werden rote, reflektierende Streifen f�r die eingeteilten Feuerwehrmitglieder, silberne f�r Chargen sowie goldene f�r die Funktion�re aufgeklebt." };
            InfoContent info3_zusatzinfoText2 = new InfoContent { Zusatzinfo = zusatzinfoText2, Info_Content = "Dienstgrade, welche gew�hlt werden, haben eine Funktionsperiode von f�nf Jahren. Gew�hlt wird jeweils in den Monaten J�nner bis M�rz im ersten und sechsten Jahr eines Jahrzehnts. Die letzte Wahl fand 2016 statt. Scheidet jemand vorzeitig aus einer Funktion aus, wird zwar diese Funktion neu gew�hlt, endet aber mit der n�chsten Wahl. Der Leiter des Verwaltungsdienstes sowie die Chargen und Warte der Feuerwehr werden jeweils f�r die laufende Funktionsperiode vom Feuerwehrkommandanten ernannt." };

            InfoContent info1_zusatzinfoText3 = new InfoContent { Zusatzinfo = zusatzinfoText3, Heading = "Allgemeines", Info_Content = "Ein Feuerwehrschlauch ist ein wesentlicher Ausr�stungsgegenstand der Feuerwehr und hat die Aufgabe, das L�schmittel Wasser oder Wasser/Schaum-Gemische �ber Wegstrecken zu f�rdern. Die Schl�uche lassen sich grob in zwei Typen unterscheiden: Schl�uche, durch die Wasser oder eine andere nicht aggressive Fl�ssigkeit gesaugt werden kann (Saugschl�uche) und Schl�uche, die unter Druck Wasser, Wasser-Schaumgemisch, CAFS-Schaum oder andere, nicht aggressive Fl�ssigkeiten weiterleiten (Druckschl�uche). " };
            InfoContent info2_zusatzinfoText3 = new InfoContent { Zusatzinfo = zusatzinfoText3, Heading = "Druckschl�uche", Info_Content = "Druckschl�uche haben die prim�re Aufgabe der Wasserweiterleitung. Sie werden vorwiegend bei der Brandbek�mpfung verwendet, bei der mit entsprechend gew�hlten Strahlrohren L�schmittel abgegeben werden kann. Sie k�nnen aber auch zum Ableiten etwa von Schmutzwasser im Hochwassereinsatz o.�. genutzt werden. Daher gelten sie grunds�tzlich als nicht f�r die Trinkwasser-F�rderung � etwa zur Versorgung von Feldk�chen, Behandlungspl�tzen oder ganzen Orten, etc. � zul�ssig. F�r die Trinkwasserf�rderung kommen besonders gekennzeichnete, ansonsten baugleiche Druckschl�uche zum Einsatz, die auch rechtlich ganz anderen Vorschriften unterliegen." };

            InfoContent info1_zusatzinfoText4 = new InfoContent { Zusatzinfo = zusatzinfoText4, Heading = "Feuerwehrfahrzeuge", Info_Content = "Ein Feuerwehrfahrzeug ist ein Kraftfahrzeug, das die Feuerwehr im Rahmen ihrer Einsatzt�tigkeit verwendet. Auch Anh�nger, die f�r Feuerwehrzwecke verwendet werden, fallen in diese Kategorie. Um im Stra�enverkehr besondere Rechte in Anspruch nehmen zu k�nnen, sind die Feuerwehrfahrzeuge speziell gekennzeichnet und mit Sondersignalen ausgestattet. In Kontinentaleuropa sind die Fahrzeuge meist rot (zum Beispiel RAL 3000) bzw. leuchtrot (zum Beispiel RAL 3024, RAL 3026) lackiert." };
            #endregion---------------------------------------------




            #region-----------------------------------------------------------Bezike in DB einf�gen
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
            context.Bezirke.AddOrUpdate(Sch�rding);
            context.Bezirke.AddOrUpdate(Urfahr_Umgebung);
            context.Bezirke.AddOrUpdate(V�cklabruck);
            context.Bezirke.AddOrUpdate(Wels);
            context.Bezirke.AddOrUpdate(Wels_Land);
            #endregion------------------------------------------------------------------------------

            #region--------------------------------------------------------------------Andmins in DB einf�gen
            context.Admins.AddOrUpdate(a1);
            context.Admins.AddOrUpdate(a2);
            context.Admins.AddOrUpdate(a3);
            context.Admins.AddOrUpdate(a4);
            #endregion----------------------------------------------------------------------

            #region--------------------------------------------Typendefinitionen in DB einf�gen
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

            #region-----------------------------------------------------------Stufen in DB einf�gen
            context.Stufen.AddOrUpdate(Bronze);
            context.Stufen.AddOrUpdate(Silver);
            context.Stufen.AddOrUpdate(Gold);
            #endregion-----------------------------------------------------

            #region---------------------------------------------------Orte in DB einf�gen
            context.Orte.AddOrUpdate(Eggerding);
            context.Orte.AddOrUpdate(Hof);
            context.Orte.AddOrUpdate(Maasbach);
            context.Orte.AddOrUpdate(Hartkirchen);
            #endregion-----------------------------------------------------------

            #region---------------------------------------------Stattionen in DB einf�gen
            context.Stationen.AddOrUpdate(Allgemeinwissen);
            context.Stationen.AddOrUpdate(Dienstgrade);
            context.Stationen.AddOrUpdate(Ger�te);
            context.Stationen.AddOrUpdate(VorbeugenderBrandschutz);
            context.Stationen.AddOrUpdate(Seilknoten);
            context.Stationen.AddOrUpdate(Nachrichten�bermittlung);
            context.Stationen.AddOrUpdate(Verkehrserziehung);
            context.Stationen.AddOrUpdate(ErsteHilfe);
            context.Stationen.AddOrUpdate(Taktik);
            context.Stationen.AddOrUpdate(Gef�hrlicheStoffe);
            context.Stationen.AddOrUpdate(Atemschutz);
            #endregion--------------------------------------------

            #region------------------------------------Fragen in DB einf�gen
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

            #region----------------------------------------------Zusatzinfo in DB einf�gen
            context.Zusatzinfos.AddOrUpdate(zusatzinfoText1);
            context.Zusatzinfos.AddOrUpdate(zusatzinfoText2);
            context.Zusatzinfos.AddOrUpdate(zusatzinfoText3);
            context.Zusatzinfos.AddOrUpdate(zusatzinfoText4);
            context.Zusatzinfos.AddOrUpdate(zusatzinfoText5);
            #endregion

            #region----------------------------------------------InfoContent in DB einf�gen
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
