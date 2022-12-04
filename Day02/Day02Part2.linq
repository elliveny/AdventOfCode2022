<Query Kind="Program">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day02\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day02\testinput</Reference>
  <Namespace>System.Net</Namespace>
</Query>

void Main() {
	var useTestInput = false;
	var inputFile = File.OpenText(useTestInput ? @".\testinput" : @".\input");
	int totalScore = 0;
	var line = inputFile.ReadLine();
	while (line != null) {
		var opponentPlay = DecodePlay(line[0]);
		var desiredResult = DecodeResult(line[2]);
		var myPlay = DetermineMyPlay(opponentPlay, desiredResult);
		totalScore += GetScore(myPlay, opponentPlay);
		line = inputFile.ReadLine();
	}
	inputFile.Close();
	Console.WriteLine(totalScore);
	Debug.Assert(useTestInput ? totalScore == 12 : totalScore == 12091);
}

enum Play {
	Rock = 1,
	Paper = 2,
	Scissors = 3
};

enum Result {
	Lose,
	Draw,
	Win
}

Play DetermineMyPlay(Play opponentPlay, Result desiredResult) {
	if (desiredResult == Result.Draw) return opponentPlay;
	if (desiredResult == Result.Win) {
		if (opponentPlay == Play.Rock) return Play.Paper;
		if (opponentPlay == Play.Paper) return Play.Scissors;
		return Play.Rock;
	} else {
		if (opponentPlay == Play.Rock) return Play.Scissors;
		if (opponentPlay == Play.Paper) return Play.Rock;
		return Play.Paper;
	}
}

Play DecodePlay(char letter) {
	return (Play)(byte)letter - (byte)'A' + 1;
}

Result DecodeResult(char letter) {
	return (Result)(byte)letter - (byte)'X';
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