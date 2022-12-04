<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day01\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day01\testinput</Reference>
  <Namespace>System.Net</Namespace>
</Query>

var useTestInput = false;
var inputFile = File.OpenText(useTestInput ? @".\testinput" : @".\input");
var line = "";
int maxCalories = 0;
int totalCalories = 0;
while (line != null) {
	line = inputFile.ReadLine();
	if (String.IsNullOrWhiteSpace(line)) {
		if (totalCalories > maxCalories) maxCalories = totalCalories;
		totalCalories=0;
	} else {
		totalCalories += int.Parse(line);	
	}
}
inputFile.Close();
Console.WriteLine(maxCalories);
Debug.Assert(useTestInput ? maxCalories == 24000 : maxCalories == 70296);