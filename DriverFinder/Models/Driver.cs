namespace DriverFinder.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Driver(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"Водитель(Id={Id}, X={X}, Y={Y})";
        }
    }
}