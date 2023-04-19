package day11;

import lombok.Getter;
import lombok.Setter;
import resources.IAOC;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.LinkedList;
import java.util.List;
import java.util.function.Function;
import java.util.stream.Collectors;
import java.util.stream.Stream;

@Getter
@Setter
public class MonkeyGame
        implements IAOC {

    private List<Monkey> monkeys;

    public MonkeyGame(String[] data) {
        this.monkeys = ParseMonkeysInformation(data);
    }

    private List<Monkey> ParseMonkeysInformation(String[] data) {
        var monkeysList = new ArrayList<Monkey>();
/*
        // TEST
        monkeysList.add(new Monkey(new LinkedList<>(Stream.of(79, 98).mapToLong(Long::valueOf).boxed().collect(Collectors.toList())),
                o -> o * 19,
                23));
        monkeysList.add(new Monkey(new LinkedList<>(Stream.of(54, 65, 75, 74).mapToLong(Long::valueOf).boxed().collect(Collectors.toList())),
                o -> o + 6,
                19));
        monkeysList.add(new Monkey(new LinkedList<>(Stream.of(79, 60, 97).mapToLong(Long::valueOf).boxed().collect(Collectors.toList())),
                o -> Math.multiplyExact(o, o),
                13));
        monkeysList.add(new Monkey(new LinkedList<>(Stream.of(74).mapToLong(Long::valueOf).boxed().collect(Collectors.toList())),
                o -> o + 3,
                17));

        monkeysList.get(0).setMonkeyTrue(monkeysList.get(2));
        monkeysList.get(0).setMonkeyFalse(monkeysList.get(3));

        monkeysList.get(1).setMonkeyTrue(monkeysList.get(2));
        monkeysList.get(1).setMonkeyFalse(monkeysList.get(0));

        monkeysList.get(2).setMonkeyTrue(monkeysList.get(1));
        monkeysList.get(2).setMonkeyFalse(monkeysList.get(3));

        monkeysList.get(3).setMonkeyTrue(monkeysList.get(0));
        monkeysList.get(3).setMonkeyFalse(monkeysList.get(1));
*/
        // REAL
        monkeysList.add(new Monkey(new LinkedList<>(Stream.of(54, 82, 90, 88, 86, 54).mapToLong(Long::valueOf).boxed().collect(Collectors.toList())),
                o -> o * 7,
                11));
        monkeysList.add(new Monkey(new LinkedList<>(Stream.of(91, 65).mapToLong(Long::valueOf).boxed().collect(Collectors.toList())),
                o -> o * 13,
                5));
        monkeysList.add(new Monkey(new LinkedList<>(Stream.of(62, 54, 57, 92, 83, 63, 63).mapToLong(Long::valueOf).boxed().collect(Collectors.toList())),
                o -> o + 1,
                7));
        monkeysList.add(new Monkey(new LinkedList<>(Stream.of(67, 72, 68).mapToLong(Long::valueOf).boxed().collect(Collectors.toList())),
                o -> o * o,
                2));
        monkeysList.add(new Monkey(new LinkedList<>(Stream.of(68, 89, 90, 86, 84, 57, 72, 84).mapToLong(Long::valueOf).boxed().collect(Collectors.toList())),
                o -> o + 7,
                17));
        monkeysList.add(new Monkey(new LinkedList<>(Stream.of(79, 83, 64, 58).mapToLong(Long::valueOf).boxed().collect(Collectors.toList())),
                o -> o + 6,
                13));
        monkeysList.add(new Monkey(new LinkedList<>(Stream.of(96, 72, 89, 70, 88).mapToLong(Long::valueOf).boxed().collect(Collectors.toList())),
                o -> o + 4,
                3));
        monkeysList.add(new Monkey(new LinkedList<>(Stream.of(79).mapToLong(Long::valueOf).boxed().collect(Collectors.toList())),
                o -> o + 8,
                19));

        monkeysList.get(0).setMonkeyTrue(monkeysList.get(2));
        monkeysList.get(0).setMonkeyFalse(monkeysList.get(6));

        monkeysList.get(1).setMonkeyTrue(monkeysList.get(7));
        monkeysList.get(1).setMonkeyFalse(monkeysList.get(4));

        monkeysList.get(2).setMonkeyTrue(monkeysList.get(1));
        monkeysList.get(2).setMonkeyFalse(monkeysList.get(7));

        monkeysList.get(3).setMonkeyTrue(monkeysList.get(0));
        monkeysList.get(3).setMonkeyFalse(monkeysList.get(6));

        monkeysList.get(4).setMonkeyTrue(monkeysList.get(3));
        monkeysList.get(4).setMonkeyFalse(monkeysList.get(5));

        monkeysList.get(5).setMonkeyTrue(monkeysList.get(3));
        monkeysList.get(5).setMonkeyFalse(monkeysList.get(0));

        monkeysList.get(6).setMonkeyTrue(monkeysList.get(1));
        monkeysList.get(6).setMonkeyFalse(monkeysList.get(2));

        monkeysList.get(7).setMonkeyTrue(monkeysList.get(4));
        monkeysList.get(7).setMonkeyFalse(monkeysList.get(5));

        return monkeysList;
    }

    @Override
    public Object Part1() {
        for (int i = 0; i < 20; i++) {
            for (var monkey : monkeys) {
                // Get items
                var items = monkey.getItems();
                var initialSize = items.size();
                for (int j = 0; j < initialSize; j++) {
                    var item = items.pollFirst();
                    monkey.addInspectedItems();
                    // Process item
                    var newValue = monkey.inspectItem(item) / 3;
                    // Decide and send to other monkey
                    monkey.decideToThrow(newValue);
                }
            }
        }
        return monkeys
                .stream()
                .sorted(Comparator.comparing(Monkey::getInspectedItems).reversed())
                .limit(2)
                .mapToInt(Monkey::getInspectedItems)
                .reduce(1, Math::multiplyExact);
    }

    @Override
    public Object Part2() {
        var modMMC = monkeys
                .stream()
                .mapToInt(Monkey::getDivider)
                .reduce(1, Math::multiplyExact);

        for (int i = 0; i < 10000; i++) {
            for (var monkey : monkeys) {
                // Get items
                var items = monkey.getItems();
                var initialSize = items.size();
                for (int j = 0; j < initialSize; j++) {
                    var item = items.pollFirst();
                    monkey.addInspectedItems();
                    monkey.inspectAndDecideToThrow(item, modMMC);
                }
            }
        }
        return monkeys
                .stream()
                .sorted(Comparator.comparing(Monkey::getInspectedItems).reversed())
                .limit(2)
                .mapToLong(Monkey::getInspectedItems)
                .reduce(1, Math::multiplyExact);
    }

    @Getter
    @Setter
    public class Monkey {
        public static int currId = 0;
        private int id;
        private int inspectedItems;
        private LinkedList<Long> items;
        Function<Long, Long> operation;
        Function<Long, Boolean> decision;
        private int divider;
        private Monkey monkeyTrue;
        private Monkey monkeyFalse;

        public Monkey(LinkedList<Long> items,
                      Function<Long, Long> operation,
                      Integer divider) {
            this.id = currId++;
            this.items = items;
            this.operation = operation;
            this.divider = divider;
            this.decision = o -> o % this.divider == 0;
        }

        public void addInspectedItems() {
            this.inspectedItems++;
        }

        public void addNewItem(Long value) {
            this.items.add(value);
        }

        public Long inspectItem(Long item) {
            return this.operation.apply(item);
        }

        public boolean isTestTrue(Long value) {
            return this.decision.apply(value);
        }

        public void decideToThrow(Long value) {
            if (this.isTestTrue(value)) {
                this.monkeyTrue.addNewItem(value);
            } else {
                this.monkeyFalse.addNewItem(value);
            }
        }

        public void inspectAndDecideToThrow(Long value, int mod) {
            var newValue = value % mod;
            value = this.inspectItem(newValue);
            decideToThrow(value);
        }
    }
}
