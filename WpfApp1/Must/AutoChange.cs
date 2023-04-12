using System.Windows;

namespace WpfApp1
{
    class AutoChange
    {
        /// <summary>
        /// Margin实现百分比制，可隐式转化为int
        /// </summary>
        public AutoChange() { }
        public AutoChange(double percent,double height) { Percent = percent; Height = height; }

        private double Percent { get; set; }
        private double Height { get; set; }

        protected int MarginPercent {
            get
            {
                double result = Height * Percent;
                return (int)result;
            }
        }

        public static implicit operator int (AutoChange autoChange) { return autoChange.MarginPercent; }

    }
}
