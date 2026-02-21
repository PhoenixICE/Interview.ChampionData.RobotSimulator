using Spectre.Console;
using System.Collections.Generic;

namespace Interview.ChampionData.RobotSimulator
{
    public class SimulatorRenderer
    {
        private readonly Robot _robot;
        private readonly Tabletop _table;
        private readonly List<string> _log = new();

        public SimulatorRenderer(Robot robot, Tabletop table)
        {
            _robot = robot;
            _table = table;
        }

        public void Log(string message)
        {
            _log.Add(message);
            if (_log.Count > 100)
                _log.RemoveAt(0);
        }

        public void Render()
        {
            var layout = new Layout()
                .SplitColumns(
                    new Layout("Log"),
                    new Layout("Simulator")
                );

            layout["Log"].Update(BuildLogPanel());
            layout["Simulator"].Update(BuildTablePanel());

            AnsiConsole.Clear();
            AnsiConsole.Write(layout);
        }

        private Panel BuildLogPanel()
        {
            var logText = string.Join("\n", _log);
            return new Panel(logText)
                .Header("Logs", Justify.Center)
                .Border(BoxBorder.Rounded);
        }

        private Panel BuildTablePanel()
        {
            var grid = new Grid();
            grid.AddColumn();

            for (int y = _table.Size - 1; y >= 0; y--)
            {
                var row = "";
                for (int x = 0; x < _table.Size; x++)
                {
                    if (_robot.IsPlaced && _robot.X == x && _robot.Y == y)
                    {
                        row += $"[green]{GetRobotChar()}[/] ";
                    }
                    else
                    {
                        row += "[grey].[/] ";
                    }
                }
                grid.AddRow(row);
            }

            return new Panel(grid)
                .Header("Tabletop", Justify.Center)
                .Border(BoxBorder.Rounded);
        }

        private string GetRobotChar()
        {
            return _robot.Facing switch
            {
                Direction.NORTH => "^",
                Direction.SOUTH => "v",
                Direction.EAST => ">",
                Direction.WEST => "<",
                _ => "?"
            };
        }
    }
}