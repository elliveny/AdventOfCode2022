<Query Kind="Program">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day10\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day10\testinput</Reference>
  <NuGetReference>System.Interactive</NuGetReference>
</Query>

static bool useTestInput = false;

void Main() {
	var answer = Run(useTestInput ? @".\testinput" : @".\input");
	Console.WriteLine($"Part 1: {answer.TotalSignalStrength}");
	Debug.Assert(useTestInput ? answer.TotalSignalStrength == 13140 : answer.TotalSignalStrength == 15220);

	Console.WriteLine($"Part 2:");
	Console.WriteLine(answer.CRT); //RFZEKBFA
}

Answer Run(string fileName) {
	var input = System.IO.File.OpenText(fileName);
	Answer a = new Answer();

	int cycleNum = 0;
	int execTime = 0;
	int x = 1;
	string line = null;
	string[] parts = null;
	var crtChars = new List<char>();
	do {
		if (execTime==0) {
			if (line !=null) {
				x += parts[0] switch {
					"addx" => int.Parse(parts[1]),
					_ => 0
				};
			}
			line = input.ReadLine();
			if (line != null) {
				parts = line.Split(' ');
				execTime = parts[0] switch {
					"addx" => 1,
					_ => 0
				};
			}
		} else {
			execTime--;
		}
		cycleNum++;
		if ((cycleNum - 20) % 40 == 0) {
			a.TotalSignalStrength+=x * cycleNum;
		}
		var pixelCheck = x - (cycleNum % 40);		
		crtChars.Add(pixelCheck>=-2 && pixelCheck<=0 ? '#':'.');
	} while (line != null);
	
	// Render CRT
	StringBuilder sb = new StringBuilder();
	for (int idx = 0; idx < crtChars.Count; idx++) {
		if (idx > 0 && (idx % 40) == 0) sb.AppendLine();
		sb.Append(crtChars[idx]);
	}
	a.CRT = sb.ToString();
	return a;
}

struct Answer {
	public int TotalSignalStrength {get; set;}
	public string CRT {get; set;}
}