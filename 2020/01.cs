void Main()
{
	var text = File.ReadAllLines(@"2020_12_01.txt");
	var data = text.Select(x => int.Parse(x)).OrderBy(x=> x);	
	
	Question1(data).Dump();
	Question2(data).Dump();
}

long Question1(IEnumerable<int> data)
{
	var q1 = data.First(x => data.Contains(2020 - x));
	return q1 * (2020 - q1);
}

long Question2(IEnumerable<int> data)
{
	foreach (var d in data)
	{
		var q2 = data.Where(x => x != d).FirstOrDefault(x => data.Contains(2020 - x - d));
		if (q2 == 0) continue;
		return d * q2 * (2020 - q2 - d);
	}
	return -1;
}
