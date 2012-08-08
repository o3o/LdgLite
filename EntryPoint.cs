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
            string connectionString = string.Format("URI=file:{0}", options.Database); 
                                                                                       
#if (NET)
            var connection = new System.Data.SQLite.SQLiteConnection(connectionString);
#else
            var connection = new Mono.Data.Sqlite.SqliteConnection(connectionString);
#endif

            connection.Open();
            var command = connection.CreateCommand();
            var repo = new AthleteRepository(command);
            repo.CreateTableIfNotExists();
            switch (options.Action) {
               case Action.Module: {
                  CreateModule(options);
                  break;
               }
               case Action.Insert: {
                  System.Console.WriteLine("Drop all results?[y/N]?"); 
                  string yes = System.Console.ReadLine(); 
                  if (yes == "y") {
                     FileHelpers.FileHelperEngine<Athlete> engine = new FileHelpers.FileHelperEngine<Athlete>();
                     Athlete[] athletes = engine.ReadFile(options.Input);
                  
                     repo.DeleteAll();
                     foreach (var a in athletes) {
                        System.Console.WriteLine(a.Name);
                        repo.Insert(a);
                     } 
                  }
                  break;
               }
               case Action.CreateList:
               case Action.CreateResult: {
                  string catFileName = GetCatFileName(options);
                  string reportFileName = GetReportFileName(options);
                  var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 36, 36);
                  iTextSharp.text.pdf.PdfWriter.GetInstance(document,
                                                            new System.IO.FileStream(reportFileName, System.IO.FileMode.Create));
                  document.Open();
                  IBuilder builder = null;
                  
                  if (options.Action == Action.CreateList) {
                     builder = new ListBuilder(document);
                  } else {
                     builder = new PdfBuilder(document);
                  }
                  
                  Category[] cats = GetCategories(catFileName);
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
                  break;
               }
               case Action.Interactive: {
                  Category[] cats = GetCategories(GetCatFileName(options));
                  var parser = new TimeParser();
                  foreach (Category cat in cats) {
                     System.Console.WriteLine("========{0}=========", cat.Id);
                     var athletes = repo.Query(string.Format (cat.Sql, options.YearEdition));
                     foreach (Athlete athlete in athletes) {
                        System.Console.Write("{0:00} {1}\t{2}({3}):", athlete.Id, athlete.Surname, athlete.Name, athlete.Gender);
                        string time = string.Empty;
                        string fmt = string.Empty;
                        do {
                           time = System.Console.ReadLine();
                           fmt = parser.Parse(time);
                           if (!string.IsNullOrEmpty(fmt) ) {
                              System.Console.WriteLine(fmt);
                              repo.UpdateTime(athlete.Id, fmt);
                           } else {
                              if (time != "s") {
                                 System.Console.WriteLine("invalid..");
                              }
                           }
                        } while (string.IsNullOrEmpty(fmt) && time != "s");
                     } 
                  }
                  break;
               }
            }
            connection.Close();
         }
      }
      
      private static void CreateModule(Options options) {
         // 10mm d=> 28pt
         // 15mm => 42pt
         //float marginLeft, float marginRight, float marginTop, float marginBottom
         var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 36, 36);
         iTextSharp.text.pdf.PdfWriter.GetInstance(document,
                                                   new System.IO.FileStream("./moduloDiIscrizione.pdf", System.IO.FileMode.Create));
         document.Open();
         var builder = new ModuleBuilder(document, options.YearEdition, 10);
         for (int page = 0; page < 10; page++) {
            builder.AddPage();
         } 
         document.Close();
      }

      private static Category[] GetCategories(string filename) {
         FileHelpers.FileHelperEngine<Category> engineCat = new FileHelpers.FileHelperEngine<Category>();
         return engineCat.ReadFile(filename);
      }

      private static string GetCatFileName(Options options) {
         string catFileName = string.Empty;
         switch (options.Action) {
            case Action.Interactive:
            case Action.CreateList: {
               catFileName = "../../doc/list.csv";
               break;
            }
            case Action.CreateResult: {
               catFileName = "../../doc/cat.csv";
               break;
            }
            default: {
               throw new System.ArgumentException("invalid"); 
            }

         } 
         if (!string.IsNullOrEmpty(options.Categories)) {
            catFileName = options.Categories;
         } 
         if (log.IsDebugEnabled) log.Debug("use category: " + catFileName);
         return catFileName;
      }

      private static string GetReportFileName(Options options) {
         string reportFileName = string.Empty;
         if (options.Action == Action.CreateList) {
            reportFileName = string.Format("../../doc/list{0}.pdf", options.YearEdition);
         } else {
            reportFileName = string.Format("../../doc/class{0}.pdf", options.YearEdition);
         }
         if (!string.IsNullOrEmpty(options.Output)) {
            reportFileName = options.Output;
         }
         if (log.IsDebugEnabled) log.Debug("report: " + reportFileName);
         return reportFileName;
      }
   }
}

