<Query Kind="Program">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day07\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2022\Day07\testinput</Reference>
  <NuGetReference>System.Interactive</NuGetReference>
</Query>

static bool useTestInput = false;

void Main() {
	var input = System.IO.File.OpenText(useTestInput ? @".\testinput" : @".\input");

	string currentPath = "/";
	string currentLine = null;

	var directorySizes = new Dictionary<string, int>();

	var cmdParser = new Dictionary<string, Action<string>>();
	cmdParser.Add("cd", parameter => {
		switch (parameter) {
			case "/":
				currentPath = "/";
				break;
			case "..":
				currentPath = GetParent(currentPath);
				break;
			default:
				currentPath += parameter + "/";
				break;
		}
		currentLine = input.ReadLine();
	});

	cmdParser.Add("ls", parameter => {
		currentLine = input.ReadLine();
		while (currentLine != null && !currentLine.StartsWith("$")) {
			var parts = currentLine.Split(' ');
			switch (parts[0]) {
				case "dir":
					var dirPath = currentPath + parts[1] + "/";
					if (!directorySizes.ContainsKey(dirPath)) directorySizes.Add(dirPath, 0);
					break;
				default:
					var fileSize = int.Parse(parts[0]);
					if (!directorySizes.ContainsKey(currentPath)) directorySizes.Add(currentPath, 0);
					directorySizes[currentPath] += fileSize;
					var path = currentPath;
					while (path != "/") {
						path = GetParent(path);
						directorySizes[path] += fileSize;
					}
					break;
			}
			currentLine = input.ReadLine();
		}
	});

	currentLine = input.ReadLine();
	while (currentLine != null) {
		var cmdParts = currentLine.Split(' ');
		cmdParser[cmdParts[1]](cmdParts.Length > 2 ? cmdParts[2] : null);
	}

	var answer = directorySizes.Where(s => s.Value <= 100000).Sum(s => s.Value);
	Console.WriteLine($"Part 1: {answer}");
	Debug.Assert(useTestInput ? answer == 95437 : answer == 1443806);

	var unusedSpace = 70000000 - directorySizes["/"];
	var spaceRequired = 30000000 - unusedSpace;
	answer = directorySizes.OrderBy(s => s.Value).Where(s => s.Value >= spaceRequired).First().Value;
	Console.WriteLine($"Part 2: {answer}");
	Debug.Assert(useTestInput ? answer == 24933642 : answer == 942298);
}

string GetParent(string path) {
	var pos = path.Substring(0, path.Length - 1).LastIndexOf("/") + 1;
	return path.Substring(0, pos);
}