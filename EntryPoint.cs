[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension="log4net", Watch=true)]
namespace Talaran.Ldg {
   public class EntryPoint {
      private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
      public static void Main(string[] args) {
         var options = new Options();
         CommandLine.ICommandLineParser cmdParser =
           new CommandLine.CommandLineParser(new CommandLine.CommandLineParserSettings(System.Console.Error));
         
         if (cmdParser.ParseArguments(args, options)) {
            string connectionString = string.Format("URI=file:{0}", options.Database); //, ../../db/ldg2011.sqlite";
            var dbcon = new Mono.Data.Sqlite.SqliteConnection(connectionString);
            dbcon.Open();
            var command = dbcon.CreateCommand();
              command.CommandText = "CREATE TABLE IF NOT EXISTS at (id INTEGER PRIMARY KEY  NOT NULL,name VARCHAR,surname VARCHAR,year INTEGER,gender CHAR,time VARCHAR)";
            command.ExecuteNonQuery();
            var repo = new AthleteRepository(command);
            if (options.Insert) {
               System.Console.WriteLine("Drop all results?[y/N]?"); 
               string yes  = System.Console.ReadLine(); 
               if (yes == "y") {
                  FileHelpers.FileHelperEngine<Athlete> engine = new FileHelpers.FileHelperEngine<Athlete>();
                  Athlete[] athletes = engine.ReadFile(options.Input);
                  
                  repo.DeleteAll();
                  foreach (var a in  athletes) {
                     System.Console.WriteLine(a.Name);
                     repo.Insert(a);
                  } 
               }
               dbcon.Close();
            } else {
            
               var document = new iTextSharp.text.Document();
               iTextSharp.text.pdf.PdfWriter.GetInstance(document,
                                                         new System.IO.FileStream(options.Output, System.IO.FileMode.Create));
               document.Open();

               IBuilder builder = null;
               if (options.List) {
                  builder = new ListBuilder(document);
               } else {
                  builder = new PdfBuilder(document);
               }


               FileHelpers.FileHelperEngine<Category> engineCat = new FileHelpers.FileHelperEngine<Category>();
               Category[] cats = engineCat.ReadFile(options.Categories);
               
               foreach (Category cat in cats) {
                  if (log.IsDebugEnabled) log.Debug("parse" + cat.Id);
                  builder.BeginReport(cat.Title, options.YearEdition);
                  var athletes = repo.Query(string.Format (cat.Sql, options.YearEdition));
                  foreach (Athlete athlete in athletes) {
                     builder.Add(athlete);
                  } 
                  builder.EndReport();
               } 
               document.Close();
               dbcon.Close();
            }
         }
      }
   }
}

