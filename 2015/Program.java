import resources.IAOC;
import y2015.day20.InfinitePresents;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;

public class Program {
    public static void main(String[] args) throws IOException {
        System.out.println(Solutions.Play());
    }

    public static class Solutions {
        private static String input = "src/resources/Input.txt";

        public static Object Play() throws IOException {
            var data = Files.readAllLines(Paths.get(input)).toArray(new String[0]);
            IAOC entity = new InfinitePresents(data);
            return entity.Part1();
//            return entity.Part2();
        }
    }
}

