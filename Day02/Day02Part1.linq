<Query Kind="Program">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day02\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day02\testinput</Reference>
  <Namespace>System.Net</Namespace>
</Query>

void Main() {
	var inputFile = File.OpenText(@".\input");
	int totalScore = 0;
	var line = inputFile.ReadLine();
	while (line != null) {
		var lineParts = line.Split(" ");
		var opponentPlay = Decode(lineParts[0][0]);
		var myPlay = Decode(lineParts[1][0]);
		totalScore += GetScore(myPlay, opponentPlay);
		line = inputFile.ReadLine();
	}
	Console.WriteLine(totalScore);
}


enum Play {
	Rock = 1,
	Paper = 2,
	Scissors = 3
};

Play Decode(char letter) {
	if ((byte)letter >= (byte)'X') {
		return (Play)(byte)letter - (byte)'X' + 1;
	}
	return (Play)(byte)letter - (byte)'A' + 1;
}

int GetScore(Play myPlay, Play opponentPlay) {
	int score;
	if (myPlay == opponentPlay) {
		score = 3 + (int)myPlay;
	} else if ((myPlay == Play.Paper && opponentPlay == Play.Rock)
		|| (myPlay == Play.Rock && opponentPlay == Play.Scissors)
		|| (myPlay == Play.Scissors && opponentPlay == Play.Paper)) {
		// Win
		score = 6 + (int)myPlay;
	} else {
		// Win
		score = 0 + (int)myPlay;
	}
	//Console.WriteLine($"Opponent plays {opponentPlay}. I play {myPlay}: {score}");
	return score;
}