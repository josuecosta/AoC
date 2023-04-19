package day7;

import lombok.Getter;
import lombok.Setter;
import resources.IAOC;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.List;

@Getter
@Setter
public class FileSystemManager implements IAOC {
    private final String[] rawData;
    private MyDirectory root;

    public FileSystemManager(String[] rawData) {
        this.rawData = rawData;
        this.root = new MyDirectory("/", null);
        BuildDirectory();
    }

    private void BuildDirectory() {
        var currDir = this.root;

        for (int i = 0; i < this.rawData.length; i++) {
            var line = this.rawData[i];
            if (line.startsWith("$ ls")) {
                // ls
                line = this.rawData[++i];
                while (!line.startsWith("$")) {
                    ParseDirectoryItem(line, currDir);
                    if (++i >= this.rawData.length) {
                        return;
                    }
                    line = this.rawData[i];
                }
            }
            // cd
            currDir = UpdatePath(currDir, line);
        }
    }

    private void ParseDirectoryItem(String line, MyDirectory currDir) {
        var properties = line.split(" ");
        if (properties[0].equalsIgnoreCase("dir")) {
            var newDir = new MyDirectory(properties[1].trim(), currDir);
            currDir.addSubDirectory(newDir);
        } else {
            var file = new MyFile(properties[1], Integer.parseInt(properties[0]));
            currDir.addFiles(file);
        }
    }

    private MyDirectory UpdatePath(MyDirectory current, String line) {
        var dir = Arrays.stream(line.split("^(\\$ cd )+"))
                .filter(s -> s.trim().length() > 0)
                .findFirst()
                .get();

        switch (dir) {
            case "/" -> {
                return this.root;
            }
            case ".." -> {
                return current.getParent();
            }
            default -> {
                return current.getSubDirectories()
                        .stream()
                        .filter(d -> d.getName().equals(dir.trim()))
                        .findFirst()
                        .orElseThrow();
            }
        }
    }

    @Override
    public Object Part1() {
        var directories = FindDirectoriesAtMost(100000, this.root);
        return directories.stream().mapToInt(MyDirectory::GetTotalSize).sum();
    }

    private ArrayList<MyDirectory> FindDirectoriesAtMost(int maxSize, MyDirectory directory) {
        var directories = new ArrayList<MyDirectory>();

        var totalSize = directory.GetTotalSize();
        if (totalSize < maxSize) {
            directories.add(directory);
        }

        for (var subDirectory : directory.subDirectories) {
            directories.addAll(FindDirectoriesAtMost(maxSize, subDirectory));
        }

        return directories;
    }

    private ArrayList<MyDirectory> FindDirectoriesAtLeast(int minSize, MyDirectory directory) {
        var directories = new ArrayList<MyDirectory>();

        var totalSize = directory.GetTotalSize();
        if (totalSize >= minSize) {
            directories.add(directory);
        }

        for (var subDirectory : directory.subDirectories) {
            directories.addAll(FindDirectoriesAtLeast(minSize, subDirectory));
        }

        return directories;
    }

    @Override
    public Object Part2() {
        //  updateSize (30000000) - unusedSpace (70000000 - rootSize)
        var targetSize = 30000000 - (70000000 - this.root.GetTotalSize());
        var directories = FindDirectoriesAtLeast(targetSize, this.root);
        return directories
                .stream()
                .filter(d -> d.GetTotalSize() >= targetSize)
                .min(Comparator.comparing(MyDirectory::GetTotalSize))
                .get()
                .GetTotalSize();
    }


    public class MyDirectory {
        private String name;
        private MyDirectory parent;
        List<MyFile> files;

        public void addSubDirectory(MyDirectory subDirectory) {
            this.subDirectories.add(subDirectory);
        }

        public void addFiles(MyFile file) {
            this.files.add(file);
        }

        List<MyDirectory> subDirectories;

        public List<MyDirectory> getSubDirectories() {
            return subDirectories;
        }

        public String getName() {
            return name;
        }

        public MyDirectory getParent() {
            return parent;
        }

        public MyDirectory(String name, MyDirectory parent) {
            this.name = name;
            this.parent = parent;
            this.files = new ArrayList<>();
            this.subDirectories = new ArrayList<>();
        }

        public int GetTotalSize() {
            var filesSize = files.stream().mapToInt(MyFile::getSize).sum();
            var directorySize = subDirectories.stream().mapToInt(MyDirectory::GetTotalSize).sum();
            return directorySize + filesSize;
        }
    }


    public class MyFile {
        private String name;
        private int size;

        public MyFile(String name, int size) {
            this.name = name;
            this.size = size;
        }

        public int getSize() {
            return size;
        }
    }
}
