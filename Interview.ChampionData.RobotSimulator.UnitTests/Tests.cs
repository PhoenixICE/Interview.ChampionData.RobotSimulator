namespace Interview.ChampionData.RobotSimulator.UnitTests
{
    [TestClass]
    public class RobotSimulatorTests
    {
        private string Execute(params string[] commands)
        {
            var table = new Tabletop(5);
            var robot = new Robot(table);
            var processor = new CommandProcessor(robot);

            string lastReport = null;

            foreach (var cmd in commands)
            {
                var result = processor.Process(cmd);
                if (result != null)
                {
                    lastReport = result;
                }
            }

            return lastReport;
        }

        [TestMethod]
        public void Execute_PlaceCommand_ReportPlacePosition()
        {
            var result = Execute(
                "PLACE 0,0,NORTH",
                "REPORT"
            );

            Assert.AreEqual("0,0,NORTH", result);
        }

        [TestMethod]
        public void Execute_NoPlaceCommand_ReportReturnsNull()
        {
            var result = Execute(
                "MOVE",
                "LEFT",
                "RIGHT",
                "REPORT"
            );

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Execute_IgnoreCommandsBeforePlace_ReportCorrectPosition()
        {
            var result = Execute(
                "MOVE",
                "LEFT",
                "REPORT",
                "PLACE 2,2,EAST",
                "MOVE",
                "REPORT"
            );

            Assert.AreEqual("3,2,EAST", result);
        }

        [TestMethod]
        public void Execute_MultiplePlaceCommands_ReportLastPlaceCommand()
        {
            var result = Execute(
                "PLACE 1,1,NORTH",
                "PLACE 4,4,WEST",
                "REPORT"
            );

            Assert.AreEqual("4,4,WEST", result);
        }

        [TestMethod]
        public void Execute_MoveBeyondBoardEdge_ReturnToInitialPosition()
        {
            var result = Execute(
                "PLACE 0,0,NORTH",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "RIGHT",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "RIGHT",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "RIGHT",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "RIGHT",
                "REPORT"
            );

            Assert.AreEqual("0,0,NORTH", result);
        }

        [TestMethod]
        public void Execute_PlaceOutOfBounds_ReportLastValidPlacement()
        {
            var result = Execute(
                "PLACE 5,5,NORTH",
                "PLACE -1,0,EAST",
                "PLACE 2,2,UP",
                "PLACE 2,2,SOUTH",
                "PLACE -1,-1,NORTH",
                "REPORT"
            );

            Assert.AreEqual("2,2,SOUTH", result);
        }

        [TestMethod]
        public void Execute_UnknownCommandsAreIgnored_ReportCorrectPosition()
        {
            var result = Execute(
                "PLACE 1,1,NORTH",
                "JUMP",
                "MOVE",
                "FLY",
                "LEFT",
                "MOVE",
                "REPORT"
            );

            Assert.AreEqual("0,2,WEST", result);
        }
    }
}