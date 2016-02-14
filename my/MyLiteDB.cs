using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using LiteDB.Shell;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

/*
 * https://github.com/mbdavid/LiteDB/wiki/Shell
 * ***/

namespace my.DAL
{
    public class DALLiteDB
    {

        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string[] Phones { get; set; }
            public bool IsActive { get; set; }
        };


        public static string RunCommand(string conn, string command)
        {
            List<BsonDocument> results = new List<BsonDocument>();
            BsonValue bv = new BsonValue();
            string retval_bvv = string.Empty; // bv.ToString();
            try
            {
                using (var db = new LiteDatabase(conn))
                {
                    Console.WriteLine( bv.ToString());           
                    //BsonValue bv = db.RunCommand("db.customer.insert { Name: \"John Doe\" }");
                    //BsonValue bv = db.RunCommand("db.fuck.find  glossary.GlossDiv.GlossList.GlossEntry.ID like \"SGML\"");
                    //BsonValue bv = db.RunCommand(@"db.fuck.bulk C:\FxM\Dev\vs12\LiteDB\LiteDB-master\LiteDB.Shell\bin\Debug\test.dmp");
                    //BsonValue bv = db.RunCommand(@"db.fuck.bulk ..\..\..\..\test.dmp");
                    
                    bv = db.RunCommand(command);
                    retval_bvv = bv.ToString();                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("wtf: " + ex.Message);
                retval_bvv = ex.Message;
            }

            return retval_bvv;
        }

        public static List<BsonDocument> Find(string conn, string collection, string jsonQry)
        {
            List<BsonDocument> results = new List<BsonDocument>();
            try
            {
               
                using (var db = new LiteDatabase(conn))
                {


                    var coll = db.GetCollection(collection);
                    var results1 = coll.Find(Query.EQ("Name", "Frank Mastronardi"));
                    //hlp(results);
                    return results1.ToList(); //.Last();  
                    //return new BsonDocument(results);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("wtf: " + ex.Message);

            }
            return results;           
        }

        public static string hlp(IEnumerable<BsonDocument> wtfResults)
        {
            Console.WriteLine("_________b____________");
            
            if (wtfResults != null) {
                foreach (BsonDocument r2 in wtfResults)
                {
                    Console.WriteLine(r2.ToString());
                }
            }
            
            Console.WriteLine("_________e____________");

            return "";
        }


        public static string FirstExample()
        {
            // Open database (or create if doesn't exist)
            //using (var db = new LiteDatabase(@".\MyData.db"))
            using (var db = new LiteDatabase(@"..\..\..\..\MyData.db"))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Customer>("customers");

                var nnn = col.Find(Query.EQ("Name", "Frank Mastronardi"));

                var wtf = db.GetCollection("customers");
                var wtfResults = wtf.Find(Query.EQ("Name", "Frank Mastronardi"));

                Console.WriteLine(wtfResults.ToString() );

                //wtfResults.ToList()[0].AsDocument;
                Console.WriteLine("_________b____________");
                foreach (BsonDocument r2 in wtfResults) {
                    Console.WriteLine(r2.ToString());                
                }
                Console.WriteLine("_________e____________");

                BsonDocument r1 = wtfResults.ToList()[0].AsDocument;
                //Console.WriteLine(r1);
                Console.WriteLine(r1.ToString());

                //LiteDB.JsonSerializer.Serialize(wtfResults[0].Name, true, true);

                return nnn.Last().Name;



                // Create your new customer instance
                var customer = new Customer
                {
                    //Name = "John Doe",
                    Name = "Frank Mastronardi",
                    Phones = new string[] { "908-858-0954", "9000-0000" },
                    IsActive = true
                };

                // Insert new customer document (Id will be auto-incremented)
                col.Insert(customer);

                // Update a document inside a collection
                customer.Name = "Joana Doe";

                //col.Update(customer);

                // Index document using document Name property
                col.EnsureIndex(x => x.Name);

                // Use LINQ to query documents
                var results = col.Find(x => x.Name.StartsWith("Jo"));
                Console.WriteLine(string.Format("Jo count = {0}", results.Count()));
                Console.WriteLine(results);
                Console.WriteLine(results.ToList()[0].Name);

                //results = collection.Find(Query.EQ("Name", "John Doe"));
                var collection = db.GetCollection<Customer>("customers");
           

                results = collection.Find(Query.EQ("Name", "Joana Doe"));
                Console.WriteLine(string.Format("count = {0}", results.Count()));

                StringBuilder csv = new StringBuilder();
                int iii = 0;
                foreach (var item in results)
                {
                    iii++;
                    Console.WriteLine(string.Format("{0:D3}: {1}", iii, item.Name));
                }


                var cnt = collection.Find(Query.All()).Count();
                Console.WriteLine(string.Format("count = {0}", cnt));

                string lastNames = string.Join(",", results.Select(x => x.Name));
                string theNames = String.Join(", ", results.Select(p => p.Name).ToArray());

                string delimiter = "\n";
                String.Join(delimiter, results.ToList());
                //Console.WriteLine(results.Select(i => i.Name).Aggregate((i, j) => i + "\n" + j));

                //results = collection.Find(Query.EQ("Name", "John Doe"));

                Console.WriteLine(results.ToList());
                //Console.WriteLine("wait a second.");
                //Console.ReadLine();
                return "asdfasdf";
            }
        }
    }
}