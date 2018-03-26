using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

enum DBColumns { Admintabelle, Antwort, Antwort_checkbox, Antwort_datepicker, Antwort_radiobutton, Antwort_slider, Antwort_text, Antwort_verbinden, Aufgabe, Bezirk, Checkbox, Frage, Hintergrund, Infocontent, Paar, Radiobutton, Standort, Station, Stufe, Typendefinition, Zusatzinfo }


namespace WissenstestOnlineWebseite.Controllers
{
    public class GetDBController : Controller
    {
        public object Get(string table)
        {
            int count = 0;
            string jsondata = null;
            string dbtable = null;
            bool check = false;

            if (table == null)
            {
                return "";
            }
            #region ~~~~~check_Parameter~~~~~
            if (table.Equals(DBColumns.Admintabelle.ToString()))
            {
                check = false;
                return "Failure";
            }
            else if (table.Equals(DBColumns.Antwort.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Antwort_checkbox.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Antwort_datepicker.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Antwort_radiobutton.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Antwort_slider.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Antwort_text.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Antwort_verbinden.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Aufgabe.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Bezirk.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Checkbox.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Frage.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Hintergrund.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Infocontent.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Paar.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Radiobutton.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Standort.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Station.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Stufe.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Typendefinition.ToString()))
            {
                check = true;
            }
            else if (table.Equals(DBColumns.Zusatzinfo.ToString()))
            {
                check = true;
            }

            #endregion

            if (check == true)
            {
                SqlConnection conn = new SqlConnection("Data Source=SRV-WITE01;Initial Catalog=WissenstestDB;Persist Security Info=True;User ID=NETWORK SERVICE;Password=56Network731;");
                //SqlConnection conn = new SqlConnection("Server=localhost;Database=WissenstestDB;Trusted_Connection=true");
                conn.Open();

                dbtable = SecurityElement.Escape(table);
                SqlCommand cmd = new SqlCommand("SELECT * FROM " + dbtable + " FOR JSON PATH;", conn);
                //SqlCommand cmd = new SqlCommand("SELECT * FROM " + dbtable + " FOR JSON PATH, ROOT('"+dbtable+"')", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    jsondata += reader.GetString(0);
                    Console.WriteLine(reader.GetString(0));
                }
                reader.Close();
                conn.Close();
                //ViewBag.jsondata = jsondata;
                return jsondata;
            }
            return "Failure";
        }
    }
}

public class GetPicture : Controller
{
    public object GetView(string picture)
    {
        if (picture == null)
        {
            ViewBag.path = "Jugend.png";
            ViewBag.text = "Bild";
            return View();
        }
        ViewBag.path = picture + ".png";
        return View();
    }
}
