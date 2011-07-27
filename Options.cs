namespace Talaran.Ldg {
   public class Options {

      [CommandLine.Option("", "insert", Required = false, HelpText = "Inserisce i file cvs nel db")]
      public bool Insert = false;

      [CommandLine.Option("l", "list", Required = false, HelpText = "Crea solo una lista ")]
      public bool List = false;

      [CommandLine.Option("y", "year", Required = true, HelpText = "Anno")]
      public int YearEdition = 2011;
      
      [CommandLine.Option("d", "db", Required = true, HelpText = "Database")]
      public string Database  = "../../doc/ldg.sqlite";

      [CommandLine.Option("i", "input", Required = false, HelpText = "Files cvs di input (atleti)")]
      public string Input  = "../../doc/athletes.csv";


      [CommandLine.Option("c", "create", Required = false, HelpText = "Crea la classifica in base al file di configurazione")]
      public string Categories = "../../doc/cat.csv";


      
      [CommandLine.Option("o", "output", Required = false, HelpText = "File pdf di output.")]
      public string Output = "../../doc/report.pdf";

      
      [CommandLine.HelpOption(HelpText = "Display this help screen.")]
      public string GetUsage() {
         var help = new CommandLine.Text.HelpText(new CommandLine.Text.HeadingInfo("Ldg", "1.1"));
         help.Copyright = new CommandLine.Text.CopyrightInfo("Gruppo Ragni", 2009, 2014);
         help.AddPreOptionsLine("by ODV");
         help.AddPreOptionsLine("====================");
         help.AddPreOptionsLine("Usage: Ldg -y 2011");
         help.AddOptions(this);
         return help;
      }

   } 
}
