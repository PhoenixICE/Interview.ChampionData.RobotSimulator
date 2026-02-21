using Spectre.Console;

namespace Interview.ChampionData.RobotSimulator
{
    public class Program
    {
        public static void Main()
        {
            var table = new Tabletop(5);
            var robot = new Robot(table);
            var renderer = new SimulatorRenderer(robot, table);
            var processor = new CommandProcessor(robot, renderer);

            if (File.Exists("commands.txt"))
            {
                foreach (var line in File.ReadAllLines("commands.txt"))
                {
                    processor.Process(line.Trim());
                    renderer.Render();
                }
            }

            // Interactive mode
            renderer.Log("Interactive mode started");
            renderer.Render();

            while (true)
            {
                AnsiConsole.Markup("[yellow]> [/]");
                var input = Console.ReadLine();
                processor.Process(input);
                renderer.Render();
            }
        }
    }
}