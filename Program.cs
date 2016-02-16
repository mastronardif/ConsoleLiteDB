using LiteDB;
using my.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLiteDB
{
    class Program
    {
        static string dbConn = @"..\..\..\..\MyData.db";   // get from config

        static void Main(string[] args)
        {
            string cmdResults = string.Empty;
            //List<BsonDocument> results; // string results;


            //string jsonQry = "{Name:'Frank Mastronardi'}"; // " { type: 'food', price: { $lt: 9.95 } }";
            

            string runCmd = "db.categorys.insert { Name: \"Farm trades\" }";
            //string cmdResults = DALLiteDB.RunCommand(dbConn, @"db.fuck.bulk ..\..\..\..\test.dmp");
            //string cmdResults = DALLiteDB.RunCommand(dbConn, "db.fuck.find  glossary.GlossDiv.GlossList.GlossEntry.ID like \"SGML\"");
            //cmdResults = DALLiteDB.RunCommand(dbConn, "db.customer.insert { Name: \"John Doe\" }");
            cmdResults = DALLiteDB.RunCommand(dbConn, runCmd);

            runCmd = "db.categorys.find Name like \"Farm trades\" "; //glossary.GlossDiv.GlossList.GlossEntry.ID like \"SGML\"";
            cmdResults = DALLiteDB.RunCommand(dbConn, runCmd);
            Console.WriteLine("\tDALLiteDB.RunCommand ==>\n{0}\n", cmdResults);

            //runCmd = "db.categorys.find Name like \"Blue prints\"";
            //cmdResults = DALLiteDB.RunCommand(dbConn, runCmd);
            //Console.WriteLine("\tDALLiteDB.RunCommand ==>\n{0}\n", cmdResults);

            //results = DALLiteDB.Find(dbConn, collection, jsonQry);
            //Console.WriteLine("results = " + results);
            //DALLiteDB.hlp(results);

            //string results22 = DALLiteDB.FirstExample();
            //Console.WriteLine("results22 = " + results22);
        }
    }
}
