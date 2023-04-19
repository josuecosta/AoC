package day9;

import common.Coordinate;
import resources.IAOC;

import java.util.*;

public class RopeBridge
        implements IAOC {

    private final List<Map.Entry<String, Integer>> instructions;
    private HashMap<Coordinate, Boolean> visitedPlaces;
    private Rope rope;

    public RopeBridge(String[] data) {
        this.instructions = GetInstructions(data);
        this.visitedPlaces = new HashMap<>();
        this.visitedPlaces.put(new Coordinate(0, 0), true);
    }

    private List<Map.Entry<String, Integer>> GetInstructions(String[] data) {
        var dic = new ArrayList<Map.Entry<String, Integer>>();
        for (String instruction : data) {
            var instructionSplit = instruction.split(" ");
            dic.add(new AbstractMap.SimpleEntry<>(
                    instructionSplit[0],
                    Integer.parseInt(instructionSplit[1])));

        }
        return dic;
    }

    private void ProcessInstructions() {
        for (var instruction : instructions) {
            UpdateHead(instruction);
        }
    }

    private void UpdateHead(Map.Entry<String, Integer> instruction) {
        var value = instruction.getValue();
        var head = rope.getHead();
        for (int i = 0; i < value; i++) {
            switch (instruction.getKey()) {
                case "R":
                    head.setX(head.getX() + 1);
                    UpdateNode(0);
                    break;
                case "L":
                    head.setX(head.getX() - 1);
                    UpdateNode(0);
                    break;
                case "U":
                    head.setY(head.getY() + 1);
                    UpdateNode(0);
                    break;
                case "D":
                    head.setY(head.getY() - 1);
                    UpdateNode(0);
                    break;
            }
        }
    }

    private void UpdateNode(int index) {
        if (index + 1 == rope.getNodesNumber()) {
            var newPosition = rope.getTail().clone();
            if (!this.visitedPlaces.containsKey(newPosition)) {
                this.visitedPlaces.put(newPosition, true);
            }
            return;
        }

        var headPosition = rope.getIndex(index);
        var tailPosition = rope.getIndex(index + 1);

        var xDiff = Math.abs(tailPosition.getX() - headPosition.getX());
        var yDiff = Math.abs(tailPosition.getY() - headPosition.getY());

        if (xDiff > 1 && yDiff > 1) {
            if (headPosition.getX() - tailPosition.getX() > 1
                    && headPosition.getY() - tailPosition.getY() > 1) {
                // R U
                tailPosition.setX(tailPosition.getX() + 1);
                tailPosition.setY(tailPosition.getY() + 1);
            } else if (headPosition.getX() - tailPosition.getX() < -1
                    && headPosition.getY() - tailPosition.getY() > 1) {
                // L U
                tailPosition.setX(tailPosition.getX() - 1);
                tailPosition.setY(tailPosition.getY() + 1);
            } else if (headPosition.getX() - tailPosition.getX() < -1
                    && headPosition.getY() - tailPosition.getY() < -1) {
                // L D
                tailPosition.setX(tailPosition.getX() - 1);
                tailPosition.setY(tailPosition.getY() - 1);
            } else if (headPosition.getX() - tailPosition.getX() > 1
                    && headPosition.getY() - tailPosition.getY() < -1) {
                // R D
                tailPosition.setX(tailPosition.getX() + 1);
                tailPosition.setY(tailPosition.getY() - 1);
            }
            UpdateNode(index + 1);
        } else if (xDiff > 1 || yDiff > 1) {
            if (headPosition.getX() - tailPosition.getX() > 1) {
                // R
                tailPosition.setX(tailPosition.getX() + 1);
                tailPosition.setY(headPosition.getY());
            } else if (headPosition.getX() - tailPosition.getX() < -1) {
                // L
                tailPosition.setX(tailPosition.getX() - 1);
                tailPosition.setY(headPosition.getY());
            } else if (headPosition.getY() - tailPosition.getY() > 1) {
                // U
                tailPosition.setY(tailPosition.getY() + 1);
                tailPosition.setX(headPosition.getX());
            } else if (headPosition.getY() - tailPosition.getY() < -1) {
                // D
                tailPosition.setY(tailPosition.getY() - 1);
                tailPosition.setX(headPosition.getX());
            }
            UpdateNode(index + 1);
        }
    }

    @Override
    public Object Part1() {
        this.rope = new Rope(2);
        ProcessInstructions();
        return visitedPlaces.values().size();
    }

    @Override
    public Object Part2() {
        this.rope = new Rope(10);
        ProcessInstructions();
        return visitedPlaces.values().size();
    }

    class Rope {
        private final LinkedList<Coordinate> nodesList;

        public Rope(int nodes) {
            this.nodesList = new LinkedList<>();
            for (int i = 0; i < nodes; i++) {
                this.nodesList.add(new Coordinate(0, 0));
            }
        }

        public Coordinate getHead() {
            return nodesList.getFirst();
        }

        public Coordinate getTail() {
            return nodesList.getLast();
        }

        public Coordinate getIndex(int index) {
            return nodesList.get(index);
        }

        public int getNodesNumber() {
            return nodesList.size();
        }

        public boolean hasPosition(int x, int y) {
            return rope.nodesList.stream().anyMatch(c -> c.getX() == x && c.getY() == y);
        }
    }
}
