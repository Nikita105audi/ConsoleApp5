namespace RunTracker
{
    public class RunLog
    {
        private RunEntry[] _entries;
        private int _count;

        public RunLog(int capacity = 4)
        {
            _entries = new RunEntry[capacity];
            _count = 0;
        }

        public int Count => _count;

        public RunEntry this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException("Неверный индекс записи.");
                return _entries[index];
            }
        }

        public void Add(RunEntry entry)
        {
            if (_count == _entries.Length)
            {
                // Расширяем массив в 2 раза
                var newArray = new RunEntry[_entries.Length * 2];
                Array.Copy(_entries, newArray, _entries.Length);
                _entries = newArray;
            }
            _entries[_count] = entry;
            _count++;
        }

        // Перегрузка оператора + для объединения двух хранилищ
        public static RunLog operator +(RunLog a, RunLog b)
        {
            var result = new RunLog(a._count + b._count);
            for (int i = 0; i < a._count; i++) result.Add(a._entries[i]);
            for (int i = 0; i < b._count; i++) result.Add(b._entries[i]);
            return result;
        }

        // Дополнительная перегрузка: сравнение двух записей
        public static bool operator ==(RunEntry x, RunEntry y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            return x.RouteName == y.RouteName &&
                   x.DistanceKm == y.DistanceKm &&
                   x.Date.Date == y.Date.Date &&
                   x.WasHard == y.WasHard;
        }

        public static bool operator !=(RunEntry x, RunEntry y) => !(x == y);

        public override bool Equals(object? obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
    }
}