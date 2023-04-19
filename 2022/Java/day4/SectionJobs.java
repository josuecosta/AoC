package day4;

import java.util.HashSet;
import java.util.LinkedList;
import java.util.stream.IntStream;

public class SectionJobs {
    private final LinkedList<Pair> pairs;
    private final String[] data;

    public SectionJobs(String[] data) {
        this.data = data;
        this.pairs = new LinkedList<>();
        ParseData();
    }

    private void ParseData() {
        for (String datum : this.data) {
            var pairsSplitted = datum.split(",");
            pairs.add(new Pair(pairsSplitted[0], pairsSplitted[1]));
        }
    }

    public double GetFullyContainedPairs() {
        var count = 0;
        for (var pair : pairs) {
            if (IsFullyContainedPair(pair))
                count++;
        }
        return count;
    }

    public double GetOverlapedPairs() {
        var count = 0;
        for (var pair : pairs) {
            if (IsOverlapedPair(pair))
                count++;
        }
        return count;
    }

    private boolean IsFullyContainedPair(Pair pair) {

        var LRange = IntStream.range(pair.getLPair().getMin(), pair.getLPair().getMax() + 1).boxed().toList();
        var RRange = IntStream.range(pair.getRPair().getMin(), pair.getRPair().getMax() + 1).boxed().toList();

        return new HashSet<>(RRange).containsAll(LRange)
            || new HashSet<>(LRange).containsAll(RRange);
    }

    private boolean IsOverlapedPair(Pair pair) {
        var LRange = IntStream.range(pair.getLPair().getMin(), pair.getLPair().getMax() + 1).boxed().toList();
        var RRange = IntStream.range(pair.getRPair().getMin(), pair.getRPair().getMax() + 1).boxed().toList();

        return LRange.stream().anyMatch(RRange::contains); // ==> .anyMatch(l -> RRange.contains(l));
    }
}
