namespace RunTracker
{
    public class RunEntry
    {
        private string _routeName;
        private decimal _distanceKm;

        public string RouteName
        {
            get => _routeName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidRunDataException("Название маршрута не может быть пустым.");
                _routeName = value;
            }
        }

        public decimal DistanceKm
        {
            get => _distanceKm;
            set
            {
                if (value <= 0)
                    throw new InvalidRunDataException("Дистанция должна быть больше нуля.");
                _distanceKm = value;
            }
        }

        public DateTime Date { get; set; }
        public bool WasHard { get; set; }

        public RunEntry(string routeName, decimal distanceKm, DateTime date, bool wasHard)
        {
            RouteName = routeName;
            DistanceKm = distanceKm;
            Date = date;
            WasHard = wasHard;
        }

        public override string ToString()
        {
            return $"{RouteName} | {DistanceKm} км | {Date:dd.MM.yyyy} | {(WasHard ? "Тяжело" : "Легко")}";
        }
    }
}