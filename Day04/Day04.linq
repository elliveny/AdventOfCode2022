<Query Kind="Program">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day04\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day04\testinput</Reference>
</Query>

void Main() {
	var useTestInput = false;
	var inputFile = File.OpenText(useTestInput ? @".\testinput" : @".\input");
	int numFullyContained = 0;
	int numOverlaps = 0;
	var line = inputFile.ReadLine();
	while (line != null) {
		var parts = line.Split(',');
		var elf1 = new ElfAssignment(parts[0]);
		var elf2 = new ElfAssignment(parts[1]);
		if (elf1.FullyContains(elf2) || elf2.FullyContains(elf1)) numFullyContained++;
		if (elf1.Overlaps(elf2) || elf2.Overlaps(elf1)) numOverlaps++;
		line = inputFile.ReadLine();
	}
	inputFile.Close();
	Console.WriteLine($"Part 1: {numFullyContained} Part 2: {numOverlaps}");
	Debug.Assert(useTestInput ? numFullyContained == 2 : numFullyContained == 511);
	Debug.Assert(useTestInput ? numOverlaps == 4 : numOverlaps == 821);
}

class ElfAssignment {
	public ElfAssignment(string assignment) {
		var parts = assignment.Split('-');
		this.From = int.Parse(parts[0]);
		this.To = int.Parse(parts[1]);
	}

	public int From { get; set; }
	public int To { get; set; }

	public bool FullyContains(ElfAssignment other) {
		return ((other.From >= this.From && other.From <= this.To) && (other.To <= this.To && other.To >= this.From));
	}

	public bool Overlaps(ElfAssignment other) {
		return ((other.From >= this.From && other.From <= this.To) || (other.To <= this.To && other.To >= this.From));
	}

	public override string ToString() {
		return $"{From}-{To}";
	}
}