<Query Kind="Program">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day09\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day09\testinput</Reference>
  <Reference Relative="testinput2">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day09\testinput2</Reference>
  <NuGetReference>System.Interactive</NuGetReference>
</Query>

static bool useTestInput = false;

void Main() {
	var tailVisits = DoRope(2, useTestInput ? @".\testinput" : @".\input");
	var answer1 = tailVisits.Count();
	Console.WriteLine($"Part 1: {answer1}");
	Debug.Assert(useTestInput ? answer1 == 13 : answer1 == 6181);

	var tailVisits2 = DoRope(10, useTestInput ? @".\testinput2" : @".\input");
	var answer2 = tailVisits2.Count();
	Console.WriteLine($"Part 2: {answer2}");
	Debug.Assert(useTestInput ? answer2 == 36 : answer2 == 2386);
}

List<Coord> DoRope(int length, string fileName) {
	var input = System.IO.File.OpenText(fileName);

	Coord[] rope = new Coord[length];

	var tailVisits = new List<Coord>();
	var line = input.ReadLine();
	while (line != null) {
		var parts = line.Split(' ');
		Enumerable.Repeat(0, int.Parse(parts[1])).ForEach((x) => {
			rope[0] = parts[0] switch {
				"U" => new Coord() { X = rope[0].X, Y = rope[0].Y - 1 },
				"D" => new Coord() { X = rope[0].X, Y = rope[0].Y + 1 },
				"L" => new Coord() { X = rope[0].X - 1, Y = rope[0].Y },
				"R" => new Coord() { X = rope[0].X + 1, Y = rope[0].Y },
				_ => rope[0]
			};
			for(int idx=1; idx < rope.Length; idx++) {
				rope[idx] = NewTailPos(rope[idx], rope[idx - 1]);
			};
			tailVisits.Add(rope[rope.Length - 1]);
		});
		line = input.ReadLine();
	}
	return tailVisits.Distinct().ToList();
}

Coord NewTailPos(Coord tailPos, Coord headPos) {
	var tailDelta = new Coord() { X = tailPos.X - headPos.X, Y = tailPos.Y - headPos.Y };
	if (Math.Abs(tailDelta.X)<=1 && Math.Abs(tailDelta.Y)<=1) return tailPos;
	return tailDelta switch { 
	{ X: 0, Y: 0 } => tailPos,
	{ X: -1, Y: 0 } => tailPos, 
	{ X: 1, Y: 0 } => tailPos, 
	{ X: 0, Y: -1 } => tailPos, 
	{ X: 0, Y: 1 } => tailPos, 
	{ X: 0, Y: -2 } => new Coord() { X = tailPos.X, Y = tailPos.Y + 1 }, 
	{ X: +1, Y: -2 } => new Coord() { X = tailPos.X - 1, Y = tailPos.Y + 1 }, 
	{ X: -1, Y: -2 } => new Coord() { X = tailPos.X + 1, Y = tailPos.Y + 1 }, 
	{ X: 0, Y: +2 } => new Coord() { X = tailPos.X, Y = tailPos.Y - 1 }, 
	{ X: +1, Y: +2 } => new Coord() { X = tailPos.X - 1, Y = tailPos.Y - 1 }, 
	{ X: -1, Y: +2 } => new Coord() { X = tailPos.X + 1, Y = tailPos.Y - 1 }, 
	{ X: -2, Y: 0 } => new Coord() { X = tailPos.X + 1, Y = tailPos.Y }, 
	{ X: -2, Y: -1 } => new Coord() { X = tailPos.X + 1, Y = tailPos.Y + 1 }, 
	{ X: -2, Y: +1 } => new Coord() { X = tailPos.X + 1, Y = tailPos.Y - 1 }, 
	{ X: +2, Y: 0 } => new Coord() { X = tailPos.X - 1, Y = tailPos.Y }, 
	{ X: +2, Y: -1 } => new Coord() { X = tailPos.X - 1, Y = tailPos.Y + 1 }, 
	{ X: +2, Y: +1 } => new Coord() { X = tailPos.X - 1, Y = tailPos.Y - 1 },		
	{ X: -2, Y: -2 } => new Coord() { X = tailPos.X + 1, Y = tailPos.Y + 1 },	
	{ X: -2, Y: +2 } => new Coord() { X = tailPos.X + 1, Y = tailPos.Y - 1 },	
	{ X: +2, Y: -2 } => new Coord() { X = tailPos.X - 1, Y = tailPos.Y + 1 },	
	{ X: +2, Y: +2 } => new Coord() { X = tailPos.X - 1, Y = tailPos.Y - 1 },	
	_ => throw new Exception(tailDelta.ToString())
};

}
struct Coord {
	public int X;
	public int Y;

	public override string ToString() {
		return $"[{X},{Y}]";
	}
}

//void DrawRope(Coord[] rope) {	
//	var minX=rope.Min(r=>r.X);
//	var maxX=rope.Max(r=>r.X);
//	var minY=rope.Min(r=>r.Y);
//	var maxY=rope.Max(r=>r.Y);
//	for (int y = minY; y <= maxY; y++) {
//		for (int x = minX; x <= maxX; x++) {
//			var idx = rope.Select((r, idx) => new { c=r, idx}).Where(r =>r.c.X==x && r.c.Y==y).Select(r => r.idx).FirstOrDefault(-1);
//			Console.Write(idx == -1 ? "." : idx.ToString());
//		}
//		Console.WriteLine();
//	}
//}