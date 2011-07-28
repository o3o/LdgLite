namespace Talaran.Ldg {
   public enum Action {
      Interactive,
      Insert,
      CreateList,
      CreateResult,
      Module
   }
   public class Options {
      [CommandLine.Option("a", "action", Required = true,
                          HelpText = "Azione da eseguire (Interactive, Insert, CreateList, CreateResult)")]
      public Action Action = Action.CreateResult;

      [CommandLine.Option("y", "year", Required = false, HelpText = "Anno. Solo per Create*")]
      public int YearEdition = 2011;
      
      [CommandLine.Option("d", "db", Required = true, HelpText = "Database.")]
      public string Database  = "../../db/ldg2011.sqlite";

      [CommandLine.Option("i", "input", Required = false, HelpText = "File cvs di input (atleti). Solo in caso di Insert")]
      public string Input  = "../../doc/athletes2011.csv";

      [CommandLine.Option("c", "config", Required = false, HelpText = "File di configurazione. Solo in caso di Interactive, Create*")]
      public string Categories = string.Empty;

      
      [CommandLine.Option("o", "output", Required = false, HelpText = "File pdf di output. Solo in caso di Create*")]
      public string Output = string.Empty;

      
      [CommandLine.HelpOption(HelpText = "Display this help screen.")]
      public string GetUsage() {
         var help = new CommandLine.Text.HelpText(new CommandLine.Text.HeadingInfo("Ldg", "1.1"));
         help.Copyright = new CommandLine.Text.CopyrightInfo("Gruppo Ragni", 2009, 2014);
         help.AddPreOptionsLine("by ODV");
         help.AddPreOptionsLine("====================");
         help.AddPreOptionsLine("Usage: Ldg -a createresult -y 2011 -d ../../db/ldg.sqlite -c ../../doc/cat.cvs -");
         help.AddOptions(this);
         return help;
      }

   } 
}
