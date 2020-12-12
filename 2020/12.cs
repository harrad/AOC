void Main()
{
	var text = File.ReadAllLines(@"2020_12_12.txt");
	var data = text.Select(x => (x.Substring(0, 1), int.Parse(x.Substring(1))));

	Question1(data, 1).Dump();
	Question2(data, 10, 1).Dump();
}

public static double DegToRad(int angle) => (Math.PI / 180) * angle;

public int Question1(IEnumerable<(string letter, int value)> text, int dir)
{
	var v = 0;
	var h = 0;

	foreach (var i in text)
	{
		if ((i.letter == "F" && dir == 0) || i.letter == "N") v += i.value;
		if ((i.letter == "F" && dir == 1) || i.letter == "E") h -= i.value;
		if ((i.letter == "F" && dir == 2) || i.letter == "S") v -= i.value;
		if ((i.letter == "F" && dir == 3) || i.letter == "W") h += i.value;

		if (i.letter == "R" || i.letter == "L")
		{
			var n = i.letter == "L" ? -i.value : i.value;
			dir = (dir + (n / 90) + 4) % 4;
		}
	}

	return Math.Abs(h) + Math.Abs(v);
}

public int Question2(IEnumerable<(string letter, int value)> text, int wpx, int wpy)
{
	var v = 0;
	var h = 0;

	foreach (var i in text)
	{
		if (i.letter == "N") wpy += i.value;
		if (i.letter == "E") wpx += i.value;
		if (i.letter == "S") wpy -= i.value;
		if (i.letter == "W") wpx -= i.value;

		if (i.letter == "R" || i.letter == "L")
		{
			var n = i.letter == "R" ? -i.value : i.value;
			var c = (int) Math.Cos(DegToRad(n));
			var s = (int) Math.Sin(DegToRad(n));
			var x = wpx * c - wpy * s;
			var y = wpx * s + wpy * c;

			wpx = x;
			wpy = y;
		}

		if (i.letter == "F")
		{
			h += wpx * i.value;
			v += wpy * i.value;
		}
	}

	return (Math.Abs(h) + Math.Abs(v));
}

