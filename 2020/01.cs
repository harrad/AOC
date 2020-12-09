void Main()
{
	var text = File.ReadAllLines(@"2020_12_01.txt");
	var data = text.Select(x => int.Parse(x)).OrderBy(x=> x);	
	
	//QUESTION 1
	var q1 = data.First(x => data.Contains(2020 - x));
	($"Q1 : {q1 * (2020 - q1)}").Dump();

	//QUESTION 2
	foreach (var d in data)
	{
		var q2 = data.Where(x => x != d).FirstOrDefault(x => data.Contains(2020 - x - d));
		if (q2 == 0) continue;		
		($"Q2 : {d * q2 * (2020 - q2 - d)}").Dump();
		break;		
	}
}
