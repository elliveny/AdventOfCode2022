<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day01\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day01\testinput</Reference>
  <Namespace>System.Net</Namespace>
</Query>

var inputFile = File.OpenText(@".\input");
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
Console.WriteLine(maxCalories);
