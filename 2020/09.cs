void Main()
{
	var text = File.ReadAllLines(@"2020_12_09.txt");
	var data = text.Select(x => long.Parse(x)).ToArray();	

	// QUESTION 1 
	var target =  Question1(25, data).Dump();
	
	//QUESTION 2 
	Question2(2, target, data).Dump();
}

long Question1(int range, long[] data)
{
	for (var i = range; i < data.Count(); i++)
	{
		if (!IsSumOfPrevious(data.Skip(i - range).Take(range), data[i])) return data[i];
	}
	return -1;
}

long Question2(int minRange, long target, long[] data)
{
	for (var range = minRange; range < data.Count() - minRange; range++)
	{
		for (var idx = 0; idx < data.Count() - range; idx++)
		{
			var group = data.Skip(idx).Take(range);
			if (group.Sum() == target) return group.Min() + group.Max();			
		}
	}	
	return -1;
}

bool IsSumOfPrevious(IEnumerable<long> prev, long target)
{
	return prev.Any(i => prev.Where(j => j != i).Any(j => i + j == target));
}
