namespace DB_lib.Migrations
{
    using DB_lib.Tables;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<DB_lib.TestDB_Context>
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

            #region------------------------------Antwort_Text erzeugen
            Antwort_Text a_text1 = new Antwort_Text {Text = "Schärding" };
            Antwort_Text a_text2 = new Antwort_Text { Text = "Verwalter" };
            #endregion--------------------------------------------------

            #region------------------------------Antwort_DatePicker erzeugen
            Antwort_DatePicker a_DP1 = new Antwort_DatePicker{ Date = new DateTime(1998, 11, 06) };
            Antwort_DatePicker a_DP2 = new Antwort_DatePicker { Date = new DateTime(2017, 12, 24) };
            #endregion--------------------------------------------------

            #region------------------------------Antwort_Slider erzeugen
            Antwort_Slider a_s1 = new Antwort_Slider { Min_val = 5, Max_val = 40, Sprungweite = 5, RightVal = 15, Slider_text = "C-Druckschlauch" };
            Antwort_Slider a_s2 = new Antwort_Slider { Min_val = 5, Max_val = 40, Sprungweite = 5, RightVal = 20 };
            #endregion--------------------------------------------------

            #region---------------------------------------Antwort_RB erzeugen
            Antwort_RadioButton a_rb1 = new Antwort_RadioButton { Anzahl = 4 };
            Antwort_RadioButton a_rb2 = new Antwort_RadioButton { Anzahl = 5 };
            Antwort_RadioButton a_rb3 = new Antwort_RadioButton { Anzahl = 3 };
            Antwort_RadioButton a_rb4 = new Antwort_RadioButton { Anzahl = 6 };
            Antwort_RadioButton a_rb5 = new Antwort_RadioButton { Anzahl = 2 };
            #endregion-----------------------------------------------

            #region---------------------------------------RadioButtons erzeugen
            RadioButton radioB1_Arb1 = new RadioButton {Content = "Andreas", IsTrue = false, Antwort_RadioButton = a_rb1};
            RadioButton radioB2_Arb1 = new RadioButton { Content = "Florian", IsTrue = true, Antwort_RadioButton = a_rb1 };
            RadioButton radioB3_Arb1 = new RadioButton { Content = "Maximilian", IsTrue = false, Antwort_RadioButton = a_rb1 };
            RadioButton radioB4_Arb1 = new RadioButton { Content = "Johannes", IsTrue = false, Antwort_RadioButton = a_rb1 };

            RadioButton radioB1_Arb2 = new RadioButton { Content = "21", IsTrue = false, Antwort_RadioButton = a_rb2 };
            RadioButton radioB2_Arb2 = new RadioButton { Content = "18", IsTrue = true, Antwort_RadioButton = a_rb2 };
            RadioButton radioB3_Arb2 = new RadioButton { Content = "13", IsTrue = false, Antwort_RadioButton = a_rb2 };
            RadioButton radioB4_Arb2 = new RadioButton { Content = "17", IsTrue = false, Antwort_RadioButton = a_rb2 };
            RadioButton radioB5_Arb2 = new RadioButton { Content = "16", IsTrue = false, Antwort_RadioButton = a_rb2 };
            #endregion-----------------------------------------------

            #region--------------------------------------Antwort_CheckBox erzeugen
            Antwort_CheckBox a_cb1 = new Antwort_CheckBox {Anzahl = 4 };
            Antwort_CheckBox a_cb2 = new Antwort_CheckBox { Anzahl = 5 };
            Antwort_CheckBox a_cb3 = new Antwort_CheckBox { Anzahl = 3 };
            Antwort_CheckBox a_cb4 = new Antwort_CheckBox { Anzahl = 6 };
            Antwort_CheckBox a_cb5 = new Antwort_CheckBox { Anzahl = 2 };
            #endregion-------------------------------------------

            #region--------------------------------------------CheckBoxes erzeugen
            CheckBox checkB1_aCb1 = new CheckBox { Content = "TLF", CheckBoxVal = true, Antwort_CheckBox = a_cb1};
            CheckBox checkB2_aCb1 = new CheckBox { Content = "RLF", CheckBoxVal = false, Antwort_CheckBox = a_cb1 };
            CheckBox checkB3_aCb1 = new CheckBox { Content = "KLF", CheckBoxVal = true, Antwort_CheckBox = a_cb1 };
            CheckBox checkB4_aCb1 = new CheckBox { Content = "KDO", CheckBoxVal = false, Antwort_CheckBox = a_cb1 };

            CheckBox checkB1_aCb2 = new CheckBox { Content = "TestR", CheckBoxVal = true, Antwort_CheckBox = a_cb2 };
            CheckBox checkB2_aCb2 = new CheckBox { Content = "TestF", CheckBoxVal = false, Antwort_CheckBox = a_cb2 };
            CheckBox checkB3_aCb2 = new CheckBox { Content = "TestF", CheckBoxVal = false, Antwort_CheckBox = a_cb2 };
            CheckBox checkB4_aCb2 = new CheckBox { Content = "TestR", CheckBoxVal = true, Antwort_CheckBox = a_cb2 };
            CheckBox checkB5_aCb2 = new CheckBox { Content = "TestR", CheckBoxVal = true, Antwort_CheckBox = a_cb2 };
            #endregion----------------------------------------------------

            #region--------------------------------------------Antwort_Verbinden erzeugen
            Antwort_Verbinden a_V1 = new Antwort_Verbinden { Anzahl = 4 };
            Antwort_Verbinden a_V2 = new Antwort_Verbinden { Anzahl = 5 };
            Antwort_Verbinden a_V3 = new Antwort_Verbinden { Anzahl = 3 };
            Antwort_Verbinden a_V4 = new Antwort_Verbinden { Anzahl = 6 };
            Antwort_Verbinden a_V5 = new Antwort_Verbinden { Anzahl = 2 };
            #endregion----------------------------------------------------

            #region--------------------------------------------Paare erzeugen
            Paar paar1_aV1 = new Paar { Teil1 = "aaaaaaaa", Teil2 = "aaaaaaa", Antwort_Verbinden = a_V1 };
            Paar paar2_aV1 = new Paar { Teil1 = "bbbbbb", Teil2 = "bbbbbbbbbbb", Antwort_Verbinden = a_V1 };
            Paar paar3_aV1 = new Paar { Teil1 = "ccccc", Teil2 = "ccccccccccccccc", Antwort_Verbinden = a_V1 };
            Paar paar4_aV1 = new Paar { Teil1 = "dddd", Teil2 = "ddddddddd", Antwort_Verbinden = a_V1 };

            Paar paar1_aV3 = new Paar { Teil1 = "111111", Teil2 = "11111111111111", Antwort_Verbinden = a_V3 };
            Paar paar2_aV3 = new Paar { Teil1 = "222222222222222", Teil2 = "22222222", Antwort_Verbinden = a_V3 };
            Paar paar3_aV3 = new Paar { Teil1 = "333333333", Teil2 = "33333333", Antwort_Verbinden = a_V3 };
            #endregion----------------------------------------------------

            #region-------------------------------Antworten erzeugen
            Antwort Antwort_Text1 = new Antwort {Typ = A_T, Inhalt_Id = 1 };
            Antwort Antwort_Text2 = new Antwort { Typ = A_T, Inhalt_Id = 2 };

            Antwort Antwort_Slider1 = new Antwort { Typ = A_S, Inhalt_Id = 1 };
            Antwort Antwort_Slider2 = new Antwort { Typ = A_S, Inhalt_Id = 2 };

            Antwort Antwort_DatePicker1 = new Antwort { Typ = A_DP, Inhalt_Id = 1 };
            Antwort Antwort_DatePicker2 = new Antwort { Typ = A_DP, Inhalt_Id = 2 };

            Antwort Antwort_CheckBox1 = new Antwort { Typ = A_CBxT, Inhalt_Id = 1 };
            Antwort Antwort_CheckBox2 = new Antwort { Typ = A_CBxT, Inhalt_Id = 2 };

            Antwort Antwort_RadioButtons1 = new Antwort { Typ = A_RBxT, Inhalt_Id = 1 };
            Antwort Antwort_RadioButtons2 = new Antwort { Typ = A_RBxT, Inhalt_Id = 2 };

            Antwort Antwort_Verbinden1 = new Antwort { Typ = A_VxBBuM, Inhalt_Id = 1 };
            Antwort Antwort_Verbinden2 = new Antwort { Typ = A_VxBBuV, Inhalt_Id = 2 };
            #endregion----------------------------------------------------------

            #region------------------------------------------AUFGABEN ERSTELLEN
            Aufgabe AufgabeText1 = new Aufgabe {Frage = fText1, Antwort = Antwort_Text1, Zusatzinfo = zusatzinfoText1, Pflichtaufgabe = true, Station = Allgemeinwissen, Stufe = Bronze, AufgabeBezirk = "Schärding"};
            Aufgabe AufgabeText2 = new Aufgabe { Frage = fText2, Antwort = Antwort_Text2, Zusatzinfo = zusatzinfoText2, Pflichtaufgabe = false, Station = Dienstgrade, Stufe = Silver };

            Aufgabe AufgabeSlider1 = new Aufgabe { Frage = fSlider1, Antwort = Antwort_Slider1, Zusatzinfo = zusatzinfoText3, Pflichtaufgabe = false, Station = Allgemeinwissen, Stufe = Bronze };
            Aufgabe AufgabeSlider2 = new Aufgabe { Frage = fSlider2, Antwort = Antwort_Slider2, Zusatzinfo = zusatzinfoText3, Pflichtaufgabe = false, Station = Allgemeinwissen, Stufe = Bronze, TeilaufgabeVon = AufgabeSlider1};

            Aufgabe AufgabeDatePicker1 = new Aufgabe { Frage = fDatePicker1, Antwort = Antwort_DatePicker1, Zusatzinfo = zusatzinfoText1, Pflichtaufgabe = true, Station = Dienstgrade, Stufe = Gold };
            Aufgabe AufgabeDatePicker2 = new Aufgabe { Frage = fDatePicker2, Antwort = Antwort_DatePicker2, Zusatzinfo = zusatzinfoText1, Pflichtaufgabe = true, Station = Dienstgrade, Stufe = Bronze, TeilaufgabeVon = AufgabeDatePicker1 };

            Aufgabe AufgabeCheckBox1 = new Aufgabe { Frage = fCheckBox1, Antwort = Antwort_CheckBox1, Zusatzinfo = zusatzinfoText2, Pflichtaufgabe = true, Station = Dienstgrade, Stufe = Bronze, AufgabeBezirk = "Schärding", AufgabeOrt = "Eggerding" };
            Aufgabe AufgabeCheckBox2 = new Aufgabe { Frage = fCheckBox2, Antwort = Antwort_CheckBox2, Zusatzinfo = zusatzinfoText3, Pflichtaufgabe = true, Station = Dienstgrade, Stufe = Bronze };

            Aufgabe AufgabeRadioButtons1 = new Aufgabe { Frage = fRadioButtons1, Antwort = Antwort_RadioButtons1, Zusatzinfo = zusatzinfoText1, Pflichtaufgabe = true, Station = Allgemeinwissen, Stufe = Silver };
            Aufgabe AufgabeRadioButtons2 = new Aufgabe { Frage = fRadioButtons2, Antwort = Antwort_RadioButtons2, Zusatzinfo = zusatzinfoText3, Pflichtaufgabe = false, Station = Allgemeinwissen, Stufe = Silver };

            Aufgabe AufgabeVerbinden1 = new Aufgabe { Frage = fVerbinden1, Antwort = Antwort_Verbinden1, Zusatzinfo = zusatzinfoText3, Pflichtaufgabe = false, Station = Allgemeinwissen, Stufe = Bronze };
            Aufgabe AufgabeVerbinden2 = new Aufgabe { Frage = fVerbinden2, Antwort = Antwort_Verbinden2, Zusatzinfo = zusatzinfoText2, Pflichtaufgabe = false, Station = Allgemeinwissen, Stufe = Bronze };
            #endregion------------------------------------------------------



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

            #region------------------------------Antwort_Text in DB einfügen
            context.Antwort_Texte.AddOrUpdate(a_text1);
            context.Antwort_Texte.AddOrUpdate(a_text2);
            #endregion--------------------------------------------------

            #region------------------------------Antwort_DatePicker in DB einfügen
            context.Antwort_DatePickerM.AddOrUpdate(a_DP1);
            context.Antwort_DatePickerM.AddOrUpdate(a_DP2);
            #endregion--------------------------------------------------

            #region------------------------------Antwort_Slider in DB einfügen
            context.Antwort_Sliders.AddOrUpdate(a_s1);
            context.Antwort_Sliders.AddOrUpdate(a_s2);
            #endregion--------------------------------------------------

            #region---------------------------------------Antwort_RB in DB einfügen
            context.Antwort_RadioButtons.AddOrUpdate(a_rb1);
            context.Antwort_RadioButtons.AddOrUpdate(a_rb2);
            context.Antwort_RadioButtons.AddOrUpdate(a_rb3);
            context.Antwort_RadioButtons.AddOrUpdate(a_rb4);
            context.Antwort_RadioButtons.AddOrUpdate(a_rb5);
            #endregion-----------------------------------------------

            #region---------------------------------------RadioButtons in DB einfügen
            context.RadioButtons.AddOrUpdate(radioB1_Arb1);
            context.RadioButtons.AddOrUpdate(radioB2_Arb1);
            context.RadioButtons.AddOrUpdate(radioB3_Arb1);
            context.RadioButtons.AddOrUpdate(radioB4_Arb1);

            context.RadioButtons.AddOrUpdate(radioB1_Arb2);
            context.RadioButtons.AddOrUpdate(radioB2_Arb2);
            context.RadioButtons.AddOrUpdate(radioB3_Arb2);
            context.RadioButtons.AddOrUpdate(radioB4_Arb2);
            context.RadioButtons.AddOrUpdate(radioB5_Arb2);
            #endregion-----------------------------------------------

            #region--------------------------------------------Antwort_CheckBox in DB einfügen
            context.Antwort_CheckBoxes.AddOrUpdate(a_cb1);
            context.Antwort_CheckBoxes.AddOrUpdate(a_cb2);
            context.Antwort_CheckBoxes.AddOrUpdate(a_cb3);
            context.Antwort_CheckBoxes.AddOrUpdate(a_cb4);
            context.Antwort_CheckBoxes.AddOrUpdate(a_cb5);
            #endregion----------------------------------------------------

            #region--------------------------------------------CheckBoxes in DB einfügen
            context.CheckBoxes.AddOrUpdate(checkB1_aCb1);
            context.CheckBoxes.AddOrUpdate(checkB2_aCb1);
            context.CheckBoxes.AddOrUpdate(checkB3_aCb1);
            context.CheckBoxes.AddOrUpdate(checkB4_aCb1);

            context.CheckBoxes.AddOrUpdate(checkB1_aCb2);
            context.CheckBoxes.AddOrUpdate(checkB2_aCb2);
            context.CheckBoxes.AddOrUpdate(checkB3_aCb2);
            context.CheckBoxes.AddOrUpdate(checkB4_aCb2);
            context.CheckBoxes.AddOrUpdate(checkB5_aCb2);
            #endregion----------------------------------------------------

            #region--------------------------------------------Antwort_Verbinden in DB einfügen
            context.Antwort_VerbindenM.AddOrUpdate(a_V1);
            context.Antwort_VerbindenM.AddOrUpdate(a_V2);
            context.Antwort_VerbindenM.AddOrUpdate(a_V3);
            context.Antwort_VerbindenM.AddOrUpdate(a_V4);
            context.Antwort_VerbindenM.AddOrUpdate(a_V5);
            #endregion----------------------------------------------------

            #region--------------------------------------------Paare in DB einfügen
            context.Paare.AddOrUpdate(paar1_aV1);
            context.Paare.AddOrUpdate(paar2_aV1);
            context.Paare.AddOrUpdate(paar3_aV1);
            context.Paare.AddOrUpdate(paar4_aV1);

            context.Paare.AddOrUpdate(paar1_aV3);
            context.Paare.AddOrUpdate(paar2_aV3);
            context.Paare.AddOrUpdate(paar3_aV3);
            #endregion----------------------------------------------------

            #region---------------------------------------------Antworten in DB einfügen
            context.Antworten.AddOrUpdate(Antwort_Text1);
            context.Antworten.AddOrUpdate(Antwort_Text2);

            context.Antworten.AddOrUpdate(Antwort_Slider1);
            context.Antworten.AddOrUpdate(Antwort_Slider2);

            context.Antworten.AddOrUpdate(Antwort_DatePicker1);
            context.Antworten.AddOrUpdate(Antwort_DatePicker2);

            context.Antworten.AddOrUpdate(Antwort_CheckBox1);
            context.Antworten.AddOrUpdate(Antwort_CheckBox2);

            context.Antworten.AddOrUpdate(Antwort_RadioButtons1);
            context.Antworten.AddOrUpdate(Antwort_RadioButtons2);

            context.Antworten.AddOrUpdate(Antwort_Verbinden1);
            context.Antworten.AddOrUpdate(Antwort_Verbinden2);
            #endregion--------------------------------------------

            #region------------------------------------------AUFGABEN IN DB EINFÜGEN
            context.Aufgaben.AddOrUpdate(AufgabeText1);
            context.Aufgaben.AddOrUpdate(AufgabeText2);

            context.Aufgaben.AddOrUpdate(AufgabeSlider1);
            context.Aufgaben.AddOrUpdate(AufgabeSlider2);

            context.Aufgaben.AddOrUpdate(AufgabeDatePicker1);
            context.Aufgaben.AddOrUpdate(AufgabeDatePicker2);

            context.Aufgaben.AddOrUpdate(AufgabeCheckBox1);
            context.Aufgaben.AddOrUpdate(AufgabeCheckBox2);

            context.Aufgaben.AddOrUpdate(AufgabeRadioButtons1);
            context.Aufgaben.AddOrUpdate(AufgabeRadioButtons2);

            context.Aufgaben.AddOrUpdate(AufgabeVerbinden1);
            context.Aufgaben.AddOrUpdate(AufgabeVerbinden2);
            #endregion------------------------------------------------------


            context.SaveChanges();



        }
    }
}
