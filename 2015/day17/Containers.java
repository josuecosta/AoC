package y2015.day17;

import resources.IAOC;

import java.util.Collections;
import java.util.LinkedList;
import java.util.stream.Collectors;

public class Containers
        implements IAOC {
    private LinkedList<Integer> containers;
    private int combinations;
    private int goal = 150;
    private int miniumNumberOfContainers;

    public Containers(String[] data) {
        this.containers = ParseData(data);
    }

    private LinkedList<Integer> ParseData(String[] data) {
        var arr = new LinkedList<Integer>();
        for (var line :
                data) {
            arr.add(Integer.parseInt(line));
        }
        arr.sort(Collections.reverseOrder());
        return arr;
    }

    @Override
    public Object Part1() {
        CalculateCombinations((LinkedList<Integer>) containers.clone(), 0);
        return combinations;
    }

    private void CalculateCombinations(LinkedList<Integer> containers, int accumulator) {
        if (containers.size() < 1) {
            return;
        }
        for (int i = 0; i < containers.size(); i++) {
            var current = containers.stream().skip(i).findFirst().get();
            var liters = accumulator + current;
            if (liters == this.goal) {
                this.combinations++;
            } else if (liters < goal) {
                CalculateCombinations(
                        containers.stream().skip(i + 1).collect(Collectors.toCollection(LinkedList::new)),
                        liters);
            }
        }
    }

    @Override
    public Object Part2() {
        this.miniumNumberOfContainers = containers.size();
        CalculateCombinations((LinkedList<Integer>) containers.clone(), 0, 1);
        return combinations;
    }

    private void CalculateCombinations(LinkedList<Integer> containers, int accumulator, int numberOfContainers) {
        if (containers.size() < 1) {
            return;
        }
        for (int i = 0; i < containers.size(); i++) {
            var current = containers.stream().skip(i).findFirst().get();
            var liters = accumulator + current;
            if (liters == this.goal) {
                if (numberOfContainers < this.miniumNumberOfContainers) {
                    this.miniumNumberOfContainers = numberOfContainers;
                    this.combinations = 1;
                } else if (numberOfContainers == this.miniumNumberOfContainers) {
                    this.combinations++;
                }
            } else if (liters < goal) {
                CalculateCombinations(
                        containers.stream().skip(i + 1).collect(Collectors.toCollection(LinkedList::new)),
                        liters, numberOfContainers+1);
            }
        }
    }
}
