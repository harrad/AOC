
void Main()
{
	var text = File.ReadAllText(@"2020_12_04.txt");
	var data = text.Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
		.Select(x => x.Replace(Environment.NewLine, " "))
		.Select(x => x.Split(' ').ToDictionary(y => y.Split(':')[0], y => y.Split(':')[1]));

	// QUESTION 1 
	data.Count(x => HasRequiredFields(x)).Dump();

	// QUESTION 2 
	data.Count(x => IsValid(x)).Dump();
}

bool HasRequiredFields(Dictionary<string, string> input)
{	
	var required = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" }; // NOT "cid"	
	return !required.Any(x => !input.ContainsKey(x));
}

bool StringMinMax(string input, int min, int max)
{
	var v = int.Parse(input);
	return v >= min && v <= max;
}

bool IsYearValid(Dictionary<string, string> input, string key, int min, int max)
{
	if (!Regex.IsMatch(input[key], "^\\d{4}$")) return false;
	return StringMinMax(input[key], min, max);
}

bool IsHeightValid(Dictionary<string, string> input)
{
	var h = Regex.Match(input["hgt"], "^(\\d+)(cm|in)$");
	if (!h.Success) return false;
	var min = h.Groups[2].Value == "cm" ? 150 : 59;
	var max = h.Groups[2].Value == "cm" ? 193 : 76;
	return StringMinMax(h.Groups[1].Value, min, max);
}

bool IsValid(Dictionary<string, string> dict)
{
	if (!HasRequiredFields(dict)) return false;

	var years = new[] { ("byr", 1920, 2002), ("iyr", 2010, 2020), ("eyr", 2020, 2030) };
	if (years.Any(x => !IsYearValid(dict, x.Item1, x.Item2, x.Item3))) return false;

	var re = new[] { ("hcl", "^#[0-9a-f]{6}$"), ("ecl", "^(amb|blu|brn|gry|grn|hzl|oth)$"), ("pid", "^\\d{9}$") };
	if (re.Any(x => !Regex.IsMatch(dict[x.Item1], x.Item2))) return false;

	return IsHeightValid(dict);
}

