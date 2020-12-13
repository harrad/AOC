
    class Program
    {
        static (int id, int offset, int remaining) GetOffset(int target, int id)
        {
            var fraction = ((double) target / (double) id) % 1;
            var offset = id * fraction;
            var mins = (int) Math.Round(offset);
            var remaining = id - mins;

            return (id, mins, remaining);

        }

        static void Main()
        {
            var text = File.ReadAllLines(@"2020_12_13.txt");
            var target = int.Parse(text[0]);
            var ids = text[1].Split(',')
                             .Where(x => x != "x")
                             .Select(x => int.Parse(x));


            var answer = ids.Select(x => GetOffset(target, x))
                            .OrderBy(x => x.Item2)
                            .Last();

            Console.WriteLine(answer);
            Console.WriteLine(answer.id * answer.remaining);




            Console.ReadKey();
        }
    }


