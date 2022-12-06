<Query Kind="Program">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day05\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day05\testinput</Reference>
  <NuGetReference>System.Interactive</NuGetReference>
</Query>

static bool useTestInput = false;

void Main() {
	Action<Stack[], Move> part1Move = (stacks, move) => Enumerable.Range(0, move.Num).ForEach(moveNum => stacks[move.To - 1].Push(stacks[move.From - 1].Pop()));
	Action<Stack[], Move> part2Move = (stacks, move) => {
		stacks[move.From - 1].ToArray().Take(move.Num).Reverse().ForEach(crate => {
			stacks[move.To - 1].Push(crate);
			stacks[move.From - 1].Pop();
		});
	};

	var answer = DoPart(part1Move);
	Console.WriteLine($"Part 1: {answer}");
	Debug.Assert(useTestInput ? answer == "CMZ" : answer == "QMBMJDFTD");

	answer = DoPart(part2Move);
	Console.WriteLine($"Part 2: {answer}");
	Debug.Assert(useTestInput ? answer == "MCD" : answer == "NBTVTJNFJ");
}

string DoPart(Action<Stack[], Move> moveOperation) {
	// Get the stacks
	var lines = File.ReadLines(useTestInput ? @".\testinput" : @".\input").Publish();
	var crateLines = lines.TakeWhile(line => !line.Equals(""))
		.Select(line => line.Chunk(4).Select(chunk => chunk[1]).ToArray())
		.ToArray();

	Stack[] stacks = new Stack[crateLines[0].Length];
	Enumerable.Range(0, crateLines[0].Length).ForEach(idx => stacks[idx] = new Stack());
	crateLines
		.Take(crateLines.Length - 1)
		.Reverse()
		.ForEach(crateLine => crateLine.Select((crate, idx) => new { crate, idx })
					.Where(crateAndIdx => crateAndIdx.crate != ' ')
					.ForEach(crateAndIdx => stacks[crateAndIdx.idx].Push(crateAndIdx.crate.ToString()))
		);

	// Do the moves
	var moveRegex = new Regex("move\\s(?<num>\\d+)\\sfrom\\s(?<from>\\d+)\\sto\\s(?<to>\\d+)");
	var moves = lines.Select(line => moveRegex.Match(line))
		.Select(move => new Move() { Num = int.Parse(move.Groups["num"].Value), From = int.Parse(move.Groups["from"].Value), To = int.Parse(move.Groups["to"].Value) });
	moves.ForEach(move => moveOperation(stacks, move));
	return String.Concat(stacks.Select((stack, idx) => (string)stack.Peek()));
}

class Move {
	public int Num { get; set; }
	public int From { get; set; }
	public int To { get; set; }
}