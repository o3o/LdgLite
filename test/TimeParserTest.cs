using NUF = NUnit.Framework;
namespace Talaran.Ldg.Test {
   // rake uc test TF='-run:Talaran.Ldg.Test.TimeParserTest'
   [NUF.TestFixture]
   public class TimeParserTest {
      [NUF.TestCase("2+5+36", "02:05.36")]
      [NUF.TestCase("+5+36", "00:05.36")]
      [NUF.TestCase("+5", "00:05.00")]
      [NUF.TestCase("2+5", "02:05.00")]
      [NUF.TestCase("2+0", "02:00.00")]

      [NUF.TestCase("2/5/36", "02:05.36")]
      [NUF.TestCase("/5/36", "00:05.36")]
      [NUF.TestCase("/5", "00:05.00")]
      [NUF.TestCase("2/5", "02:05.00")]
      [NUF.TestCase("2/0", "02:00.00")]

      [NUF.TestCase("2.5.36", "02:05.36")]
      [NUF.TestCase(".5.36", "00:05.36")]
      [NUF.TestCase(".5", "00:05.00")]
      [NUF.TestCase("2.5", "02:05.00")]
      [NUF.TestCase("2.0", "02:00.00")]
      [NUF.TestCase("rit", "99:99.99")]
      [NUF.TestCase("R", "99:99.99")]
      public void Parse(string input, string output) {
         var parser = new TimeParser();
         NUF.Assert.That(parser.Parse(input), NUF.Is.EqualTo(output));
      }
   }
}