<Query Kind="Program">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day03\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day03\testinput</Reference>
</Query>

void Main() {
	var useTestInput = false;

	// Part 1
	var inputFile = File.OpenText(useTestInput ? @".\testinput" : @".\input");
	int totalPriorities = 0;
	var line = inputFile.ReadLine();
	while (line != null) {
		var c1 = line.Substring(0, line.Length >> 1);
		var c2 = line.Substring(line.Length >> 1);
		char sharedItem = '-';
		foreach (char item in c1) {
			if (c2.Contains(item)) {
				sharedItem = item;
				break;
			}
		}
		//Console.WriteLine($"{c1} {c2} {sharedItem} {ItemPriority(sharedItem)}");
		totalPriorities += ItemPriority(sharedItem);
		line = inputFile.ReadLine();
	}
	inputFile.Close();
	Console.WriteLine($"Part 1: {totalPriorities}");
	Debug.Assert(useTestInput ? totalPriorities == 157 : totalPriorities == 8394);

	// Part 2
	inputFile = File.OpenText(useTestInput ? @".\testinput" : @".\input");
	totalPriorities = 0;
	var line1 = inputFile.ReadLine();
	var line2 = inputFile.ReadLine();
	var line3 = inputFile.ReadLine();
	while (line1 != null) {
		char sharedItem = '-';
		foreach (char item in line1) {
			if (line2.Contains(item) && line3.Contains(item)) {
				sharedItem = item;
				break;
			}
		}
		//Console.WriteLine($"{c1} {c2} {sharedItem} {ItemPriority(sharedItem)}");
		totalPriorities += ItemPriority(sharedItem);
		line1 = inputFile.ReadLine();
		line2 = inputFile.ReadLine();
		line3 = inputFile.ReadLine();
	}
	inputFile.Close();
	Console.WriteLine($"Part 2: {totalPriorities}");
	Debug.Assert(useTestInput ? totalPriorities == 70 : totalPriorities == 2413);
}

int ItemPriority(char item) {
	if ((int)item > 96) return (int)item - 96;
	return (int)item - 65 + 27;
}