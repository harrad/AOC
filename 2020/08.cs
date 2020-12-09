void Main()
{
	var text = File.ReadAllLines(@"2020_12_08.txt");
	
	// QUESTION 1
	var res = Process(text);
	res.Item2.Dump();

	// QUESTION 2 
	for (int i = 0; i < text.Length; i++)
	{
		foreach (var cmds in new[] { ("nop", "jmp"), ("jmp", "nop") })
		{
			res = Process(text, i, cmds.Item1, cmds.Item2);
			if (res.Item1) continue;			
			res.Item2.Dump();
			break;			
		}	
	}
}

(bool, int) Process(string[] input, int indexToFlip = -1, string from = "", string to = "")
{
	var hasRun = new List<int>();
	var total = 0;
	var current = 0;
	var hasError = false;
	
	while (!hasError)
	{
		// TOO LARGE INDEX IS WANTED
		if (current >= input.Length) break;

		hasRun.Add(current);
		
		var match = Regex.Match(input[current], @"(\w{3}) (\-|\+)(\d+)");
		var cmd = match.Groups[1].Value;
		var opp = match.Groups[2].Value;
		var val = int.Parse(match.Groups[3].Value);

		if (current == indexToFlip && cmd == from) cmd = to;		
		
		switch (cmd)
		{
			case "nop":
				current++;
				break;
			case "acc":
				current++;
				total = opp == "+" ? total + val : total - val;
				break;
			case "jmp":
				current = opp == "+" ? current + val : current - val;
				break;
		}
		
		if (hasRun.Contains(current)) hasError = true;	
	}
	
	return (hasError, total);
}
