namespace Talaran.Ldg {
   public static class Assumes {
      public static void NotNull<T>(T value, string paramName) where T : class {
         if (value == null) {
            throw new System.ArgumentNullException(paramName, paramName + " cannot be null"); 
         }
      }

      public static void NotNullOrEmpty(string value, string paramName) {
         if (string.IsNullOrEmpty(value)) {
            throw new System.ArgumentNullException(paramName);
         }
      }

      public static void NotZeroLength<T>(T[] array, string paramName) {
         if (array.Length == 0) {
            throw new System.ArgumentOutOfRangeException(paramName);
         }
      }
      public static void Positive(int value, string paramName) {
         if (value <= 0) {
            throw new System.ArgumentOutOfRangeException(paramName);
         }
      }
      public static void NotNegative(int value, string paramName) {
         if (value < 0) {
            throw new System.ArgumentOutOfRangeException(paramName);
         }
      }

      public static void NotZero(double value, string paramName) {
         if (value == 0) {
            throw new System.ArgumentOutOfRangeException(paramName);
         }
      }
   }

}