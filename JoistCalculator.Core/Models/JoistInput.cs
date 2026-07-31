namespace JoistCalculator.Core.Models
{
    public class JoistInput
    {
        // Material Properties
        public double Fc { get; set; } = 20.0; // MPa
        public double Fy { get; set; } = 400.0; // MPa
        public double Fyt { get; set; } = 300.0; // MPa

        // Geometry
        public double SpanLength { get; set; } = 7.5; // m
        public double TotalHeight { get; set; } = 300.0; // mm
        public double SlabThickness { get; set; } = 50.0; // mm
        public double JoistSpacing { get; set; } = 500.0; // mm
        public double RibWidth { get; set; } = 100.0; // mm
        public string JoistType { get; set; } = "D"; // D: Double, S: Single

        // Loads
        public double LiveLoad { get; set; } = 2.5; // kPa
        public double PartitionLoad { get; set; } = 1.0; // kPa
        public double SuperDeadLoad { get; set; } = 3.0; // kPa
        public double PointLoad { get; set; } = 1.3; // kN
        public double PointLoadSize { get; set; } = 120.0; // mm
        public double EarthquakeFactor { get; set; } = 0.21; // 0.6*(A*I)

        // Load Timing & Fractions
        public double FractionSD_Before { get; set; } = 0.7; // چه کسری از بار SD و P قبل از اتصال قطعات غیرسازه ای
        public double FractionLive_Perm { get; set; } = 0.25; // چه کسری از بار Live دائمی محسوب می شود

        // Reinforcement
        public string BottomRebarDia1 { get; set; } = "f16";
        public string BottomRebarDia2 { get; set; } = "f16";
        public double BottomRebarCount1 { get; set; } = 2.0;
        public double BottomRebarCount2 { get; set; } = 1.0;

        public string ShearRebarDia { get; set; } = "f10";
        public double ShearRebarSpacing { get; set; } = 150.0; // mm

        public string TopRebarSize { get; set; } = "f8";

        // Vibration & Creep
        public double VibrationFrequencyLimit { get; set; } = 5.0; // Hz
        public double Shrinkage3Month { get; set; } = 0.000562;
        public double CreepCoeff3Month { get; set; } = 1.4;
        public double ShrinkageLongTerm { get; set; } = 0.00078;
        public double CreepCoeffLongTerm { get; set; } = 2.35;
        public double AgingCoefficient { get; set; } = 0.8;
    }
}
