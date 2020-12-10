void Main()
{
	var text = File.ReadAllLines(@"2020_12_10.txt");
	var data = text.Select(x => int.Parse(x)).OrderBy(x => x).ToList();
	data.Insert(0, 0);
	data.Add(data.Last() + 3);
	var am = new AdapterManager(data);
	
	//QUESTION 1 
	am.GetDiffs().Dump();

	//QUESTION 2	
	am.Count(data.Count() - 1).Dump();
}

class AdapterManager
{
	private int[] _data;
	private Dictionary<int, long> _counts = new Dictionary<int, long> { { 0, 1 } };
	
	public AdapterManager(List<int> data)
	{
		_data = data.ToArray();
	}
	
	public long Count(int idx)
	{
		return Enumerable.Range(0, idx)
				.Reverse()
				.Where(x => _data[idx] - _data[x] <= 3)
				.Sum(x => GetValue(x));
	}

	private long GetValue(int idx)
	{
		if (!_counts.ContainsKey(idx)) _counts[idx] = Count(idx);
		return _counts[idx];
	}	
	
	public long GetDiffs()
	{
		var diffs = _data.Take(_data.Count() - 1).Select((v, i) => _data[i + 1] - _data[i]);	
		return (diffs.Count(x => x == 1) * diffs.Count(x => x == 3));
	}
}
