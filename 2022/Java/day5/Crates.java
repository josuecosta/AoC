package day5;

import resources.IAOC;

import java.util.*;

public class Crates implements IAOC {
    protected class Instruction {
        private int quantity;
        private int from;
        private int to;

        public Instruction(int quantity, int from, int to) {
            this.quantity = quantity;
            this.from = from;
            this.to = to;
        }

        public int getQuantity() {
            return quantity;
        }

        public int getFrom() {
            return from;
        }

        public int getTo() {
            return to;
        }
    }

    private Map<Integer, Stack<String>> stacks;
    private List<Instruction> instructions;
    private final String[] rawData;

    public Crates(String[] data) {
        rawData = data;
        this.stacks = new LinkedHashMap<>();
        this.instructions = new LinkedList<>();
        ParseData();
    }

    private void ParseData() {
        var middleRow = 0;
        for (int i = 0; i < rawData.length; i++) {
            if (rawData[i].isEmpty()) {
                middleRow = i;
                break;
            }
        }

        var numberOfStacks = (int) Arrays.stream(rawData[middleRow - 1].split("\\D"))
                .filter(s -> s.trim().length() > 0)
                .count();

        var stacksList = new LinkedList<ArrayList<String>>();
        var stackNumberLine = rawData[middleRow - 1];
        for (int i = 1; i <= numberOfStacks; i++) {
            var stackIndex = stackNumberLine.indexOf(String.valueOf(i));
            var currentLine = middleRow - 2;
            var tempList = new ArrayList<String>();
            while ((currentLine >= 0)) {
                var line = rawData[currentLine];
                try {
                    var tempCrate = line.charAt(stackIndex);
                    if (Character.isWhitespace(line.charAt(stackIndex))) {
                        break;
                    }
                    tempList.add(String.valueOf(tempCrate));
                } catch (StringIndexOutOfBoundsException e) {
                    break;
                }
                currentLine--;
            }
            stacksList.add(tempList);
        }

        for (int i = 0; i < stacksList.size(); i++) {
            var l = stacksList.get(i);
            var stack = new Stack<String>();
            stack.addAll(l);
            stacks.put(i, stack);
        }

        for (int row = middleRow + 1; row < rawData.length; row++) {
            var instructionsArr = Arrays.stream(rawData[row].split("\\D"))
                    .filter(s -> s.trim().length() > 0)
                    .mapToInt(Integer::parseInt)
                    .toArray();
            instructions.add(new Instruction(instructionsArr[0], instructionsArr[1], instructionsArr[2]));
        }
    }

    @Override
    public Object Part1() {
        for (var move : instructions) {
            Move(move);
        }

        return String.join("", stacks.values().stream().map(Stack::peek).toList());
    }

    private void Move(Instruction move) {
        var stackFrom = stacks.get(move.getFrom() - 1);
        var stackTo = stacks.get(move.getTo() - 1);

        for (int i = 0; i < move.getQuantity(); i++) {
            var crateToBeMoved = stackFrom.pop();
            stackTo.push(crateToBeMoved);
        }
    }

    @Override
    public Object Part2() {
        for (var move : instructions) {
            Move9001(move);
        }
        return String.join("", stacks.values().stream().map(Stack::peek).toList());
    }

    private void Move9001(Instruction move) {
        var tempStack = new Stack<String>();
        var stackFrom = stacks.get(move.getFrom() - 1);
        var stackTo = stacks.get(move.getTo() - 1);

        for (int i = 0; i < move.getQuantity(); i++) {
            var crateToBeMoved = stackFrom.pop();
            tempStack.push(crateToBeMoved);
        }

        var tempStackSize = tempStack.size();
        for (int i = 0; i < tempStackSize; i++) {
            var crateToBeMoved = tempStack.pop();
            stackTo.push(crateToBeMoved);
        }
    }
}
