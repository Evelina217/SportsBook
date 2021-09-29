using ConsoleAppForSportsBook.Classes;
using ConsoleAppForSportsBook.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;

namespace ConsoleAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");
            SqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                Console.WriteLine("Openning Connection ...");
                conn.Open();
                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            Console.Read();

            // string url = "http://api.openweathermap.org/data/2.5/weather?id=2172797&appid=32167ecf128ba03e3e3d6791b709b309";
            string url = "https://sb1-geteventdetailsapi-altenar.biahosted.com/Sportsbook/GetEventDetails?timezoneOffset=-180&langId=1&skinName=default&configId=1&culture=en-GB&deviceType=Desktop&numformat=en&eventId=200002379052&sportId=1";
            WebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            Stream objStream;
            objStream = httpWebRequest.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    Console.WriteLine("{0}:{1}", i, sLine);
            }
            /* string jsonString;
             {
                 StreamReader reader = new StreamReader(objStream, System.Text.Encoding.UTF8);
                 jsonString = reader.ReadToEnd();
             }*/
            Console.ReadLine();

            Result Results = JsonConvert.DeserializeObject<Result>(sLine);
            string sprocname = "ProcResult";
            string paramName = "@json";
            // Sample JSON string 
            string paramValue = sLine;

            using (SqlCommand cmd = new SqlCommand(sprocname, conn))
            {
                // Set command object as a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter that will be passed to stored procedure
                cmd.Parameters.Add(new SqlParameter(paramName, paramValue));

                cmd.ExecuteReader();
            }

            

           





        }
    }
}

