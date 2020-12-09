void Main()
{
	var text = File.ReadAllLines(@"2020_12_03.txt");
	var slopes = new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };

	// QUESTION 1 
	var total = CountTrees(text, 3, 1);
	total.Dump();

	// QUESTION 2 
	total = slopes.Aggregate(1L, (agg, input) => agg * CountTrees(text, input.Item1, input.Item2));
	total.Dump();
}

public static class Extensions
{
	public static IEnumerable<T> TakeEvery<T>(this  IEnumerable<T> input, int n)
	{
		return input.Where((i, j) => j % n == 0);
	}
}

public long CountTrees(string[] map,  int x, int y)
{
	int cX = 0;
	long trees = 0;
	
	foreach (var i in map.TakeEvery(y))
	{
		cX = cX % i.Length;
		if (i[cX] == '#') trees++;
		cX += x;
	}
	return trees;
}

