namespace Interview.ChampionData.RobotSimulator
{
    public class Tabletop(int size)
    {
        public int Size { get; } = size;

        public bool IsValid(int x, int y)
        {
            return x >= 0 && x < Size &&
                   y >= 0 && y < Size;
        }
    }
}