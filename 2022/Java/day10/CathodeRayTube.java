package day10;

import resources.IAOC;

import java.util.*;

public class CathodeRayTube
        implements IAOC {
    private final List<Map.Entry<String, Integer>> instructions;

    public CathodeRayTube(String[] data) {
        this.instructions = GetInstructions(data);
    }

    private List<Map.Entry<String, Integer>> GetInstructions(String[] data) {
        var dic = new ArrayList<Map.Entry<String, Integer>>();
        for (String instruction : data) {
            var instructionSplit = instruction.split(" ");
            var x = 0;
            if (instructionSplit.length > 1) {
                x = Integer.parseInt(instructionSplit[1]);
            }
            dic.add(new AbstractMap.SimpleEntry<>(
                    instructionSplit[0],
                    x));
        }
        return dic;
    }

    private int GetSignalStrength(int maxCycles, boolean print) {
        var signalStrength = 0;
        var x = 1;
        var cycles = 1;
        var listOfCycles = Arrays.asList(20, 60, 100, 140, 180, 220);
        var currentRow = new StringBuilder();
        for (int i = 0; i < instructions.size() && cycles <= maxCycles; i++) {
            var isAddx = instructions.get(i).getKey().equals("addx");

            // Cycles
            for (int j = 0; j < (isAddx ? 2 : 1); j++) {
                if (listOfCycles.contains(cycles)) {
                    signalStrength += (cycles * x);
                }
                // Part 2: Update currentRow
                if (isToWritePixel(x, (cycles % 40))) {
                    currentRow.append("#");
                } else {
                    currentRow.append(".");
                }

                cycles++;
            }

            // Update X after cycles
            if (isAddx) {
                x += instructions.get(i).getValue();
            }
        }

        if (print) {
            for (int i = 0; i < 6; i++) {
                System.out.println(currentRow.substring(i * 40, (i + 1) * 40));
            }
        }
        return signalStrength;
    }

    private boolean isToWritePixel(int x, int cycles) {
        return (x - 1) <= (cycles - 1) && (cycles - 1) <= (x + 1);
    }

    @Override
    public Object Part1() {
        return GetSignalStrength(220, false);
    }

    @Override
    public Object Part2() {
        return GetSignalStrength(6 * 40, true);
    }
}
