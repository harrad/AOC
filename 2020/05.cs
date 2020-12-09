void Main()
{
	var text = File.ReadAllLines(@"2020_12_05.txt");

	// QUESTION 1
	var highest = text.Max(x => GetSeatId(x));
	($"Q1: { highest }").Dump();

	// QUESTION 2 
	var tickets = text.Select(x => GetSeatId(x));
	var match = tickets.First(t => !tickets.Contains(t + 1) && tickets.Contains(t + 2));
	($"Q2: { match + 1 }").Dump();
}

int GetSeatId(string input)
{
	input = Regex.Replace(input, "[FL]", "0");
	input = Regex.Replace(input, "[BR]", "1");
	return Convert.ToInt32(input, 2);
}
