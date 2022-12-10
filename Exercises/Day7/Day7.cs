public class Day7 : IDay
{
    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        var root = inputToFolders(inputLines);
        List<AoCFolder> smallFolders = new();
        getSmallFolders(root, ref smallFolders);
        return Task.FromResult(smallFolders.Sum(folder => folder.Size).ToString());
    }

    private void getSmallFolders(AoCFolder folder, ref List<AoCFolder> smallFolders)
    {
        if (folder.Size <= 100000)
        {
            smallFolders.Add(folder);
        }
        foreach (var f in folder.Children.Values)
        {
            getSmallFolders(f, ref smallFolders);
        }
    }

    private AoCFolder inputToFolders(IEnumerable<string> inputLines)
    {

        AoCFolder root = new AoCFolder(@"/", null);
        AoCFolder currFolder = root;
        var enu = inputLines.GetEnumerator();
        while (enu.MoveNext())
        {
            if (enu.Current.StartsWith("$ cd "))
            {
                // cd command
                var targ = enu.Current.Substring(5);
                if (targ == @"/")
                {
                    currFolder = root;
                }
                else if (targ == "..")
                {
                    currFolder = currFolder.Parent ?? currFolder;
                }
                else
                {
                    currFolder = currFolder.Children[targ];
                }
            }
            else if (enu.Current.StartsWith("dir "))
            {
                // must be a dir from the output of the ls command
                currFolder.AddFolder(enu.Current.Substring(4));
            }
            else if (!enu.Current.StartsWith("$"))
            {
                // must be a file from the output of the ls command
                var split = enu.Current.Split(" ");
                currFolder.AddFile(split[1], long.Parse(split[0]));
            }
        }
        return root;
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        var root = inputToFolders(inputLines);
        var sizeToDelete = root.Size - 40000000;
        List<AoCFolder> folders = new();
        getFolderList(root, ref folders);
        var folderToDelete = folders.OrderBy(f => f.Size).First(f => f.Size > sizeToDelete);
        return Task.FromResult(folderToDelete.Size.ToString());
    }
    private void getFolderList(AoCFolder folder, ref List<AoCFolder> folders) {
        folders.Add(folder);
        foreach (var f in folder.Children.Values) {
            getFolderList(f, ref folders);
        }
    }
    public class AoCFolder
    {
        public string Name { get; }
        public AoCFolder? Parent { get; }
        public Dictionary<string, AoCFolder> Children { get; } = new Dictionary<string, AoCFolder>();
        public Dictionary<string, long> Files { get; } = new Dictionary<string, long>();

        private long? _size;
        public long Size
        {
            get
            {
                if (_size != null)
                {
                    return _size.Value;
                }
                long size = Files.Values.Sum();
                foreach (AoCFolder folder in Children.Values)
                {
                    size += folder.Size;
                }
                return size;
            }
        }
        public AoCFolder(string name, AoCFolder? parent)
        {
            Name = name;
            Parent = parent;
        }

        public void AddFolder(string name)
        {
            if (_size != null)
            {
                throw new InvalidOperationException("Can't add folder after checking size");
            }
            if (!Children.ContainsKey(name))
            {
                Children.Add(name, new AoCFolder(name, this));
            }
        }
        public void AddFile(string name, long size)
        {
            if (_size != null)
            {
                throw new InvalidOperationException("Can't add file after checking size");
            }
            if (!Files.ContainsKey(name))
            {
                Files.Add(name, size);
            }
        }
    }
}