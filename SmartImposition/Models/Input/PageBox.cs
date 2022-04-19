namespace SmartImposition.Models.Input
{
    public class PageBox
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public override string ToString()
        {
            return $"{Width:N1} x {Height:N1}";
        }
    }
}
