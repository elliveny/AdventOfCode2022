<Query Kind="Program">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day06\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day06\testinput</Reference>
  <NuGetReference>System.Interactive</NuGetReference>
</Query>

static bool useTestInput = false;

void Main() {
	var line = File.ReadAllLines(useTestInput ? @".\testinput" : @".\input")[0];
	
	var answer=GetMarker(line, 4);
	Console.WriteLine($"Part 1: {answer}");
	Debug.Assert(useTestInput ? answer == 7 : answer == 1702);

	answer=GetMarker(line, 14);
	Console.WriteLine($"Part 2: {answer}");
	Debug.Assert(useTestInput ? answer == 19 : answer == 3559);
}

int GetMarker(string line, int numChars) {
	var last = line.Select((c, idx) => new { sub = idx < numChars ? "" : line.Substring(idx - numChars, numChars), idx})
		.TakeWhile(c => c.sub.Distinct().Count() < numChars)
		.Last();
	return last.idx + 1;
}