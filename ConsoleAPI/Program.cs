using ConsoleAppForSportsBook.Classes;
using ConsoleAppForSportsBook.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using Nancy.Json;

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
            
            Console.ReadLine();


        
            var serializer = new JavaScriptSerializer();
            Result results = serializer.Deserialize<Result>(sLine);

            foreach (var item in results)
            {
               
                if (SaveToDatabase(conn, item))
                {
                    Console.WriteLine("Success : " + item.Description + " Saved into database");
                }
                else
                {
                    Console.WriteLine("Error : " + item.Description + " unable to Saved into database");
                }
            }
            static bool SaveToDatabase(SqlConnection conn, Result results)
            {
               
                    string insertQuery = @"Insert into Result(Id, SportTypeId, Name, EventCode, EventDate, Status, ExtId, EventType,Node, IsLiveEvent, IsParlay, IsBetpalEvent, IsVirtual)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", results.Id));
                        cmd.Parameters.Add(new SqlParameter("@SportTypeId", results.SportTypeId));
                        cmd.Parameters.Add(new SqlParameter("@Name", results.Name));
                        cmd.Parameters.Add(new SqlParameter("@EventCode", results.EventCode));
                        cmd.Parameters.Add(new SqlParameter("@EventDate", results.EventDate));
                        cmd.Parameters.Add(new SqlParameter("@Status", results.Status));
                        cmd.Parameters.Add(new SqlParameter("@ExtId", results.ExtId));
                        cmd.Parameters.Add(new SqlParameter("@EventType", results.EventType));
                        cmd.Parameters.Add(new SqlParameter("@Node", results.Node));

                        cmd.Parameters.Add(new SqlParameter("@IsLiveEvent", results.IsLiveEvent));
                        cmd.Parameters.Add(new SqlParameter("@IsParlay", results.IsParlay));
                        cmd.Parameters.Add(new SqlParameter("@IsBetpalEvent", results.IsBetpalEvent));
                        cmd.Parameters.Add(new SqlParameter("@IsVirtual", results.IsVirtual));

                        cmd.ExecuteNonQuery();
                    }
                    return true;

                }

                }
        }
    
    }


