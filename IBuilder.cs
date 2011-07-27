namespace Talaran.Ldg {
   public interface IBuilder {
      void BeginDoc(int year);
      void BeginReport(string title);
      void EndReport();
      void Add(Athlete athete);
   }
}