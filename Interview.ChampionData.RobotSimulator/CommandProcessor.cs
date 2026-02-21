namespace Interview.ChampionData.RobotSimulator
{
    public class CommandProcessor(Robot robot, SimulatorRenderer renderer = null)
    {
        private readonly Robot _robot = robot;
        private readonly SimulatorRenderer _renderer = renderer;

        public string Process(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            input = input.Trim().ToUpperInvariant();

            _renderer?.Log($"> {input}");

            if (input.StartsWith("PLACE"))
            {
                HandlePlace(input);
                return null;
            }

            if (!_robot.IsPlaced)
            {
                _renderer?.Log("Ignored (robot not placed)");
                return null;
            }

            switch (input)
            {
                case "MOVE":
                    _robot.Move();
                    return null;

                case "LEFT":
                    _robot.Left();
                    return null;

                case "RIGHT":
                    _robot.Right();
                    return null;

                case "REPORT":
                    var report = _robot.Report();
                    _renderer?.Log(report);
                    return report;

                default:
                    _renderer?.Log("Unknown command");
                    return null;
            }
        }

        private void HandlePlace(string input)
        {
            var parts = input.Substring(5).Trim().Split(',');
            if (parts.Length != 3)
                return;

            if (int.TryParse(parts[0], out int x) &&
                int.TryParse(parts[1], out int y) &&
                Enum.TryParse(parts[2], out Direction direction))
            {
                _robot.Place(x, y, direction);
            }
        }
    }
}