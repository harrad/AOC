string[] StringSplit(string i, string s) => i.Split(new[] { s }, StringSplitOptions.RemoveEmptyEntries);
IEnumerable<char> GetLetters(string input) => input.Replace("\r\n", "").Distinct();

void Main()
{
	var text = File.ReadAllText(@"2020_12_06.txt");
	var data = StringSplit(text, "\r\n\r\n");

	var q1 = data.Sum(x => GetLetters(x).Count());
	($"Q1: { q1 }").Dump();

	var q2 = data.Sum(g => GetLetters(g).Count(l => StringSplit(g, "\r\n").All(p => p.Contains(l))));
	($"Q2: { q2 }").Dump();
}
