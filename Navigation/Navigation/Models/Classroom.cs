using Xamarin.Forms;

namespace Navigation.Models
{
    public class Classroom
    {
        public string Name { get; set; }
        public Rectangle Region { get; set; }

        public Classroom(string name, Rectangle rectangle)
        {
            Name = name;
            Region = rectangle;
        }

        public override string ToString()
        {
            return Name; 
        }
    }
}
