using System.Collections.Generic;

namespace JoistCalculator.Core.Models
{
    public class JoistResult
    {
        public Dictionary<string, double> NumericResults { get; set; } = new Dictionary<string, double>();
        public Dictionary<string, string> StatusResults { get; set; } = new Dictionary<string, string>();
        
        // Summary properties for easy access or display
        public double Ec { get; set; }
        public double ModularRatio { get; set; }
        public double Mcr { get; set; }
        public double MD { get; set; }
        public double MSD_P { get; set; }
        public double ML { get; set; }
        public double Qu { get; set; }
        public double DeflectionTotal { get; set; }
        public string PunchingShearStatus { get; set; } = "OK";
        public string FlexureStatus { get; set; } = "OK";
        public string VibrationStatus { get; set; } = "N.G.";
    }
}
