void Main()
{
	var text = File.ReadAllLines(@"2020_12_07.txt");
	var data = new BagData(text);
	
	data.CanHoldCount("shiny gold").Dump();		
	data.GetBag("shiny gold").Count().Dump();
}

class Bag
{
	public string Name { get; }
	private Dictionary<string, int> _contents { get; } = new Dictionary<string, int>();
	private BagData _parent;
	
	public Bag(BagData parent, string input)
	{
		_parent = parent;
		
		var regex = Regex.Match(input, @"^(.+) bags contain (?:no other bags.|(.+))$");
		Name = regex.Groups[1].Value;
		
		if (!regex.Groups[2].Success) return;
		foreach (var i in regex.Groups[2].Value.Split(','))
		{
			var contents = Regex.Match(i, @" ?(\d+) (.+) bags?");					
			_contents.Add(contents.Groups[2].Value, int.Parse(contents.Groups[1].Value));
		}
	}
	
	public bool HasBag(string name)
	{
		if (_contents.Any(x => x.Key == name)) return true;
		return _contents.Any(b => _parent.GetBag(b.Key).HasBag(name));
	}
	
	public int Count() => _contents.Sum(b => (_parent.GetBag(b.Key).Count() * b.Value) + b.Value);
}

class BagData 
{
	public Bag[] Bags { get; set; }
	public BagData(string[] input)
	{	
		Bags = input.Select(x => new Bag(this, x)).ToArray();	
	}
	
	public int CanHoldCount(string name) => Bags.Count(x => x.HasBag(name));	
	public Bag GetBag(string name) => Bags.First(x => x.Name == name);
}
