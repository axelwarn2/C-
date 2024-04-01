using NUnit.Framework;

namespace Chess
{
    [TestFixture]
    public class ChessProblemTest
    {
        [Test]
        public void RepeatedMethodCallDoNotChangeBehaviour()
        {
            var boardLines = new[]
            {
                "        ",
                "        ",
                "        ",
                "   q    ",
                "    K   ",
                " Q      ",
                "        ",
                "        ",
            };
            var board = new BoardParser().ParseBoard(boardLines);
            var chess = new ChessProblem(board);
            Assert.AreEqual(ChessStatus.Check, chess.CalculateChessStatus());

            // Now check that internal board modifications during the first call do not change answer
            Assert.AreEqual(ChessStatus.Check, chess.CalculateChessStatus());
        }

        [Test]
        public void AllTests()
        {
            var dir = TestContext.CurrentContext.TestDirectory;
            var testsCount = 0;
            foreach (var filename in Directory.GetFiles(Path.Combine(dir, "ChessTests"), "*.in"))
            {
                TestOnFile(filename);
                testsCount++;
            }
            Console.WriteLine("Tests passed: " + testsCount);
        }

        private static void TestOnFile(string filename)
        {
            var boardLines = File.ReadAllLines(filename);
            var board = new BoardParser().ParseBoard(boardLines);
            var chess = new ChessProblem(board);
            var expectedAnswer = File.ReadAllText(Path.ChangeExtension(filename, ".ans")).Trim();
            chess.CalculateChessStatus();
            Assert.AreEqual(expectedAnswer, chess.CalculateChessStatus().ToString().ToLower(), "Failed test " + filename);
        }
    }
}