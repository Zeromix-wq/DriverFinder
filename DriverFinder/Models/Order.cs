namespace DriverFinder.Models
{
    public class Order
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Order(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"Заказ(X={X}, Y={Y})";
        }
    }
}