namespace Navigation.Models
{
    public class CarouselViewData
    {
        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => _imageSource = value;
        }

        private string _labelText;
        public string LabelText
        {
            get => _labelText;
            set => _labelText = value;
        }

        public CarouselViewData(string source, string description)
        {
            ImageSource = source;
            LabelText = description;
        }
    }
}
