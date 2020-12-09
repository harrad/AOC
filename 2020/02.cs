void Main()
{
	var text = File.ReadAllLines(@"2020_12_02.txt");
	var rExtract = new Regex("([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)");

	var count1 = 0;
	var count2 = 0;
	
	foreach (var i in text)
	{
		var m = rExtract.Match(i);
		var letter = m.Groups[3].Value.First();
		var pass = m.Groups[4].Value;
		var n1 = int.Parse(m.Groups[1].Value);
		var n2 = int.Parse(m.Groups[2].Value);

		// QUESTION 1
		var c = pass.Count(x => x == letter);
		if (c >= n1 && c <= n2) count1++;

		// QUESTION 2 
		if (pass[n1 - 1] == letter ^ pass[n2 - 1] == letter) count2++;
	}

	($"Q1 : { count1 }\nQ2 : { count2 }").Dump();
}
