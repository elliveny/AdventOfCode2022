<Query Kind="Program">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day11\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day11\testinput</Reference>
  <NuGetReference>System.Interactive</NuGetReference>
  <RuntimeVersion>7.0</RuntimeVersion>
</Query>

static bool useTestInput = false;

static long DIVISORS = 2 * 3 * 5 * 7 * 11 * 13 * 17 * 19 * 23;

void Main() {
	var answer = Run(useTestInput ? @".\testinput" : @".\input", 20, true);
	Console.WriteLine($"Part 1: {answer.LevelOfMonkeyBusiness}");
	Debug.Assert(useTestInput ? answer.LevelOfMonkeyBusiness == 10605 : answer.LevelOfMonkeyBusiness == 55216);

	answer = Run(useTestInput ? @".\testinput" : @".\input", 10000, false);
	Console.WriteLine($"Part 2: {answer.LevelOfMonkeyBusiness}");
	Debug.Assert(useTestInput ? answer.LevelOfMonkeyBusiness == 2713310158 : answer.LevelOfMonkeyBusiness == 12848882750);
}

Answer Run(string fileName, int numRounds, bool divideByThree) {
	Answer a = new Answer();
	var monkeys = ReadMonkeys(fileName);
	for (int round = 1; round <= numRounds; round++) {
		for (int monkeyNum = 0; monkeyNum < monkeys.Count; monkeyNum++) {
			var m = monkeys[monkeyNum];
			while (m.Items.Count > 0) {
				var item = (long)m.Items.Dequeue();
				m.NumInspections++;
				var oldItem = item;
				item = m.Operation.Apply(item);
				if (divideByThree) item = (long)Math.Floor(item / 3m);
				var throwTo = item % m.TestDivisor == 0 ? m.True : m.False;
				monkeys[throwTo].Items.Enqueue(item);
			}
		}
	}

	var top2 = monkeys.OrderByDescending(m => m.NumInspections).Select(m => m.NumInspections).Take(2).ToArray();
	a.LevelOfMonkeyBusiness = top2[0] * top2[1];
	return a;
}

struct Answer {
	public long LevelOfMonkeyBusiness { get; set; }
}

List<Monkey> ReadMonkeys(string fileName) {
	var monkeys = new List<Monkey>();
	var input = System.IO.File.OpenText(fileName);
	var line = input.ReadLine(); // e.g. Monkey 0:
	while (line != null) {
		var m = new Monkey();
		line = input.ReadLine(); // e.g. Starting items: 64

		line.Substring(17).Split(',').ForEach(item => m.Items.Enqueue(long.Parse(item)));
		line = input.ReadLine(); // e.g. Operation: new = old * 7
		var opParts = line.Substring(19).Split(' ');
		m.Operation = new Operation() { Left = opParts[0], Operator = opParts[1], Right = opParts[2] };
		line = input.ReadLine(); // e.g. Test: divisible by 13
		m.TestDivisor = long.Parse(line.Substring(21));
		line = input.ReadLine(); // e.g. If true: throw to monkey 1
		m.True = int.Parse(line.Split(' ').Last());
		line = input.ReadLine(); // e.g. If false: throw to monkey 3
		m.False = int.Parse(line.Split(' ').Last());
		line = input.ReadLine(); // (Blank)
		line = input.ReadLine(); // e.g. Monkey 1:
		monkeys.Add(m);
	}
	input.Close();
	return monkeys;
}


class Monkey {
	public Monkey() {
		this.Items = new Queue();
		this.NumInspections = 0;
	}
	public long NumInspections { get; set; }
	public Queue Items { get; }
	public Operation Operation { get; set; }
	public long TestDivisor { get; set; }
	public int True { get; set; }
	public int False { get; set; }
}

class Operation {
	public string Left { get; set; }
	public string Operator { get; set; }
	public string Right { get; set; }

	public long Apply(long item) {
		long left = this.Left == "old" ? item : long.Parse(this.Left);
		long right = this.Right == "old" ? item : long.Parse(this.Right);
		switch (Operator) {
			case "*":
				return (left * right) % DIVISORS;
			case "+":
				return (left + right) % DIVISORS;
			default:
				throw new ArgumentException($"{Operator} unknown");
		}
	}
}