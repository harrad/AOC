void Main()
{
	var sm = new SeatCounter(@"2020_12_11.txt");
	sm.GetStableAnswer(4, false).Dump();
	sm.GetStableAnswer(5, true).Dump();
}

class SeatCounter
{
	private int _width;
	private int _height;
	private char[] _data;
	private string[] _text;
	private bool _isStable;

	public SeatCounter(string path)
	{
		_text = File.ReadAllLines(path);
		ResetData();
	}

	public void ResetData()
	{
		_width = _text.First().Count();
		_height = _text.Count();
		_data = _text.SelectMany(x => x).ToArray();
		_isStable = false;
	}

	private int GetIndex(int x, int y) => x + (_width * y);
	private bool IsValid(int x, int y) => (x >= 0 && x < _width && y >= 0 && y < _height);
	public char GetValue(int x, int y) => IsValid(x, y) ? _data[GetIndex(x, y)] : '?';

	private char GetNextSeat(int currentX, int currentY, int dirX, int dirY, bool skipFloor)
	{
		while (IsValid(currentX, currentY))
		{
			currentX += dirX;
			currentY += dirY;
			var v = GetValue(currentX, currentY);
			if (!skipFloor || v != '.') return v;
		}
		return '?';
	}

	public void Update(int maxSeat, bool skipFloor)
	{
		var adjacent = new[] { (0, -1), (0, 1), (-1, 0), (1, 0), (-1, -1), (1, -1), (-1, 1), (1, 1) };
		var hasChanged = false;
		var buffer = new char[_data.Length];

		for (int x = 0; x < _width; x++)
		{
			for (int y = 0; y < _height; y++)
			{
				var adjResults = adjacent.Select(a => GetNextSeat(x, y, a.Item1, a.Item2, skipFloor));

				var value = GetValue(x, y);
				var newValue = value;

				if (value == 'L' && !adjResults.Any(c => c == '#')) newValue = '#';
				if (value == '#' && adjResults.Count(c => c == '#') >= maxSeat) newValue = 'L';

				if (value != newValue) hasChanged = true;
				buffer[GetIndex(x, y)] = newValue;
			}
		}

		if (!hasChanged) _isStable = true;
		_data = buffer;
	}

	public int GetStableAnswer(int maxSeat, bool skipFloor)
	{
		ResetData();
		while (!_isStable) Update(maxSeat, skipFloor);
		return _data.Count(x => x == '#');
	}
}
