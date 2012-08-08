using SCG = System.Collections.Generic;
namespace Talaran.Ldg {
   public interface IAthleteRepository {
      void CreateTableIfNotExists();
      void Update(Athlete athlete);
      void DeleteAll();
      SCG.IEnumerable<Athlete> Query(string sql);
   }

}