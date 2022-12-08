<Query Kind="Program">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day08\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day08\testinput</Reference>
  <NuGetReference>System.Interactive</NuGetReference>
</Query>

static bool useTestInput = false;

void Main() {
	var input = System.IO.File.OpenText(useTestInput ? @".\testinput" : @".\input");

	List<char> forest = new List<char>();

	var line = input.ReadLine();
	var xSize = line.Length;
	while (line != null) {
		forest.AddRange(line.ToArray());
		line = input.ReadLine();
	}
	var ySize = forest.Count / xSize;

	var answer = Enumerable.Range(0, forest.Count).Where(tree => IsVisible(tree, xSize, ySize, forest)).Count(); ;
	Console.WriteLine($"Part 1: {answer}");
	Debug.Assert(useTestInput ? answer == 21 : answer == 1763);

	answer = Enumerable.Range(0, forest.Count).Max(tree => GetScenicScore(tree, xSize, ySize, forest));
	Console.WriteLine($"Part 2: {answer}");
	Debug.Assert(useTestInput ? answer == 8 : answer == 671160);
}

bool IsVisible(int tree, int xSize, int ySize, List<char> forest) {
	return CheckVisible(forest, tree, tree % ySize, -xSize) // up
	|| CheckVisible(forest, tree, xSize * (ySize - 1) + tree % ySize, +xSize) // down
	|| CheckVisible(forest, tree, ((tree / ySize) * xSize), -1) // left
	|| CheckVisible(forest, tree, (((tree / ySize) + 1) * xSize) - 1, +1); // right
}

bool CheckVisible(List<char> forest, int startTree, int stopTree, int increment) {
	var tree = startTree + increment;
	while ((increment > 0 && tree <= stopTree) || (increment < 0 && tree >= stopTree)) {
		if (forest[tree] >= forest[startTree]) return false;
		tree += increment;
	};
	return true;
}

int GetScenicScore(int tree, int xSize, int ySize, List<char> forest) {
	return CalcScenicScore(forest, tree, tree % ySize, -xSize) // up
	* CalcScenicScore(forest, tree, xSize * (ySize - 1) + tree % ySize, +xSize) // down
	* CalcScenicScore(forest, tree, ((tree / ySize) * xSize), -1) // left
	* CalcScenicScore(forest, tree, (((tree / ySize) + 1) * xSize) - 1, +1); // right
}

int CalcScenicScore(List<char> forest, int startTree, int stopTree, int increment) {
	var tree = startTree + increment;
	var count = 0;
	while ((increment > 0 && tree <= stopTree) || (increment < 0 && tree >= stopTree)) {
		count++;
		if (forest[tree] >= forest[startTree]) {
			return count;
		}
		tree += increment;
	};
	return count;
}

