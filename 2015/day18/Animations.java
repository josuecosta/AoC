package y2015.day18;

import common.Coordinate;
import resources.IAOC;

import java.util.ArrayList;
import java.util.stream.Collectors;

public class Animations implements IAOC {
    private ArrayList<Light> lights;

    public Animations(String[] data) {
        lights = ParseData(data);
    }

    private ArrayList<Light> ParseData(String[] data) {
        var lights = new ArrayList<Light>();
        for (int row = 0; row < data.length; row++) {
            for (int col = 0; col < data[0].length(); col++) {
                lights.add(new Light(
                        col,
                        row,
                        String.valueOf(data[row].charAt(col)),
                        data.length));
            }
        }
        return lights;
    }

    @Override
    public Object Part1() {
        for (int i = 0; i < 5; i++) {
            ExecuteStep();
        }
        return lights
                .stream()
                .filter(Light::isOn)
                .count();
    }

    private void ExecuteStep() {
        lights.forEach(l -> l.setNextStatus(GetNeighbors(l)));
        lights.forEach(Light::switchStatus);
    }

    private ArrayList<Light> GetNeighbors(Light light) {
        return (ArrayList<Light>) this.lights
                .stream()
                .filter(l ->
                        (light.getX() - 1 <= l.getX() && l.getX() <= light.getX() + 1
                                && light.getY() - 1 <= l.getY() && l.getY() <= light.getY() + 1)
                                && !(l.getX() == light.getX() && l.getY() == light.getY()))
                .collect(Collectors.toList());
    }

    @Override
    public Object Part2() {
        for (int i = 0; i < 100; i++) {
            ExecuteStep();
        }
        return lights
                .stream()
                .filter(Light::isOn)
                .count();
    }

    public class Light {
        private Coordinate coordinates;
        private boolean isOn;
        private boolean nextStatus;
        private int gridSize;

        public Light(int x, int y, String value, int gridSize) {
            this.coordinates = new Coordinate(x, y);
            this.isOn = value.equals("#");
            this.gridSize = gridSize;
        }

        public void switchStatus() {
            var size = this.isOn = nextStatus;
        }

        public int getX() {
            return coordinates.getX();
        }

        public int getY() {
            return coordinates.getY();
        }

        public boolean isOn() {
            if ((this.getX() == 0 && this.getY() == 0)
                    || (this.getX() == 0 && this.getY() == this.gridSize - 1)
                    || (this.getX() == this.gridSize - 1 && this.getY() == 0)
                    || (this.getX() == this.gridSize - 1 && this.getY() == this.gridSize - 1)) {
                return true;
            }
            return this.isOn;
        }

        public void setNextStatus(ArrayList<Light> neighbors) {
            var neighborsOn = neighbors
                    .stream()
                    .filter(Light::isOn)
                    .count();

            if (isOn()) {
                this.nextStatus = neighborsOn >= 2 && neighborsOn <= 3;
            } else {
                this.nextStatus = neighborsOn == 3;
            }
        }
    }
}
