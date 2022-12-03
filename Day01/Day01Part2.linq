<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day01\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day01\testinput</Reference>
  <Namespace>System.Net</Namespace>
</Query>

var inputFile = File.OpenText(@".\input");
var line = "";
int totalCalories = 0;
var caloriesByElf = new List<int>();
while (line != null) {
	line = inputFile.ReadLine();
	if (String.IsNullOrWhiteSpace(line)) {
		caloriesByElf.Add(totalCalories);
		totalCalories=0;
	} else {
		totalCalories += int.Parse(line);	
	}
}
Console.WriteLine(caloriesByElf.OrderByDescending(calories => calories).Take(3).Sum());
