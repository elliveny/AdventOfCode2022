<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day01\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day01\testinput</Reference>
  <Namespace>System.Net</Namespace>
</Query>

var useTestInput = false;
var inputFile = File.OpenText(useTestInput ? @".\testinput" : @".\input");
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
inputFile.Close();
var top3 = caloriesByElf.OrderByDescending(calories => calories).Take(3).Sum();
Console.WriteLine(top3);
Debug.Assert(useTestInput ? top3 == 45000 : top3 == 205381);