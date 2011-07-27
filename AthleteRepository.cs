using SCG = System.Collections.Generic;
namespace Talaran.Ldg {
   public class AthleteRepository: IAthleteRepository {
      private readonly System.Data.IDbCommand command;
      public AthleteRepository(System.Data.IDbCommand command) {
         if (command == null) {
            throw new System.ArgumentNullException("command", "command cannot be null"); 
         } else {
            this.command = command;
         }
      }
      public bool IsNew(Athlete athlete) {
         command.CommandText = "SELECT id FROM at";
         return command.ExecuteScalar() != null;
      }
      public void  DeleteAll() {
         command.CommandText = "DELETE FROM at";
         command.ExecuteNonQuery();
      }
      public void Insert(Athlete athlete) {
           command.CommandText = string.Format("INSERT INTO at VALUES({0},\"{1}\",\"{2}\",{3},'{4}', '{5}');",
                                               athlete.Id,
                                               athlete.Name,
                                               athlete.Surname,
                                               athlete.Year,
                                               athlete.Gender.ToString(),
                                               athlete.Time
                                             
                                             );
           command.ExecuteNonQuery();
           
         
      }
      public SCG.IEnumerable<Athlete> Query(string sql) {
         command.CommandText = sql;
         var list = new SCG.List<Athlete>();
         using (var reader = command.ExecuteReader()) {
            while (reader.Read()) {
               var at = new Athlete();
               
               at.Id = reader.GetInt32(0);
               at.Name = reader.GetString(1);
               at.Surname = reader.GetString(2);
               at.Year = reader.GetInt32(3);
               at.Gender  = reader.GetString(4);
               at.Time  = reader.GetString(5);
               list.Add(at);
            }
         }
         return list;
      }

      public void Save(Athlete athlete) {
         // if (IsNew(athlete)) {
         //    Insert(athlete);
         // } else {
         //    Update(athlete);
         // }
      }

   }

}