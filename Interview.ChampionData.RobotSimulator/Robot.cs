namespace Interview.ChampionData.RobotSimulator
{
    public class Robot(Tabletop table)
    {
        private readonly Tabletop _table = table;

        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction Facing { get; private set; }
        public bool IsPlaced { get; private set; }

        public void Place(int x, int y, Direction direction)
        {
            if (_table.IsValid(x, y))
            {
                X = x;
                Y = y;
                Facing = direction;
                IsPlaced = true;
            }
        }

        public void Move()
        {
            if (!IsPlaced) return;

            int newX = X;
            int newY = Y;

            switch (Facing)
            {
                case Direction.NORTH:
                    newY++;
                    break;
                case Direction.SOUTH:
                    newY--;
                    break;
                case Direction.EAST:
                    newX++;
                    break;
                case Direction.WEST:
                    newX--;
                    break;
            }

            if (_table.IsValid(newX, newY))
            {
                X = newX;
                Y = newY;
            }
        }

        public void Left()
        {
            if (!IsPlaced) return;
            Facing = (Direction)(((int)Facing + 3) % 4);
        }

        public void Right()
        {
            if (!IsPlaced) return;
            Facing = (Direction)(((int)Facing + 1) % 4);
        }

        public string Report()
        {
            return IsPlaced ? $"{X},{Y},{Facing}" : string.Empty;
        }
    }
}