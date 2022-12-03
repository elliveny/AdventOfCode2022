<Query Kind="Program">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day03\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day03\testinput</Reference>
</Query>

void Main() {
	// Part 1
	var inputFile = File.OpenText(@".\input");
	int totalPriorities = 0;
	var line = inputFile.ReadLine();
	while (line != null) {
		var c1 = line.Substring(0, line.Length / 2);
		var c2 = line.Substring(line.Length / 2);
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
	Console.WriteLine(totalPriorities);
	inputFile.Close();

	// Part 2
	inputFile = File.OpenText(@".\input");
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
	Console.WriteLine(totalPriorities);
	inputFile.Close();

}

int ItemPriority(char item) {
	if ((int)item > 96) return (int)item - 96;
	return (int)item - 65 + 27;
}

// Part 2
//var oxygenGeneratorValues = inputValues;
//var co2ScrubberValues = inputValues;
//var mask = 0;
//for (int bitNum = numBits; bitNum > 0; bitNum--) {
//	mask = (1 << bitNum - 1);
//	if (oxygenGeneratorValues.Length > 1) {
//		var mostCommonOxygenGeneratorMask = GetMostCommonBits(oxygenGeneratorValues, numBits) & mask;
//		oxygenGeneratorValues = oxygenGeneratorValues.Where(v => (v & mask) == (mostCommonOxygenGeneratorMask)).ToArray();
//	}
//	if (co2ScrubberValues.Length > 1) {
//		var mostCo2ScrubberMask = (GetMostCommonBits(co2ScrubberValues, numBits) ^ ((topBitValue << 1) - 1)) & mask;
//		co2ScrubberValues = co2ScrubberValues.Where(v => (v & mask) == (mostCo2ScrubberMask)).ToArray();
//	}
//}
//Console.WriteLine($"Part 2: life support rating = oxygenGenerator x co2Scrubber = {oxygenGeneratorValues[0]} x {co2ScrubberValues[0]} = {oxygenGeneratorValues[0] * co2ScrubberValues[0]}");
//
//// Functions
//int GetMostCommonBits(int[] inputValues, int numBits) {
//	var bitTotals = new int[numBits];
//	for (var i = 0; i < inputValues.Length; i++) {
//		for (int bitNum = 0; bitNum < numBits; bitNum++) {
//			bitTotals[bitNum] += (inputValues[i] & (1 << bitNum)) > 0 ? 1 : -1;
//		}
//	}
//
//	var mostCommonBits = 0;
//	for (int bitNum = 0; bitNum < numBits; bitNum++) {
//		if (bitTotals[bitNum] > 0 || (bitTotals[bitNum] == 0)) {
//			mostCommonBits |= (1 << bitNum);
//		}
//	}
//	return mostCommonBits;
//}
