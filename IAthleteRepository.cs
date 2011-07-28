using SCG = System.Collections.Generic;
namespace Talaran.Ldg {
   public interface IAthleteRepository {
      void Update(Athlete athlete);

      //bool IsNew(Athlete athlete);
      //void Save(Athlete athlete);
      void DeleteAll();
      SCG.IEnumerable<Athlete> Query(string sql);
   }

}