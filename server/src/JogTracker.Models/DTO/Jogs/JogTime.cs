namespace JogTracker.Models.DTO.Jogs
{
    public class JogTime
    {
        public JogTime() { }

        public JogTime(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }
}
