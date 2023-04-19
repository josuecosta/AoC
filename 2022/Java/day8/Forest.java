package day8;

import common.Coordinate;
import resources.IAOC;

import java.util.ArrayList;
import java.util.List;

public class Forest
        implements IAOC {

    private final String[] rawData;
    private final List<ForestTree> treesList;

    public Forest(String[] rawData) {
        this.rawData = rawData;
        this.treesList = new ArrayList<>();
        BuildForest();
    }

    private void BuildForest() {
        var rows = this.rawData.length;
        var columns = this.rawData[0].length();

        for (var row = 0; row < rows; row++) {
            for (int col = 0; col < columns; col++) {
                var height = this.rawData[row].charAt(col);
                var tree = new ForestTree(col, row, Integer.parseInt(String.valueOf(height)));
                tree.setVisible(this.rawData);
                tree.setScenicScore(this.rawData);
                treesList.add(tree);
            }
        }
    }

    @Override
    public Object Part1() {
        return this.treesList.stream().filter(ForestTree::isVisible).count();
    }

    @Override
    public Object Part2() {
        return this.treesList.stream().mapToDouble(ForestTree::getScenicScore).max().getAsDouble();
    }

    public class ForestTree {
        private Coordinate position;
        private int height;
        private boolean isVisible;
        private double scenicScore;

        public ForestTree(int x, int y, int height) {
            this.position = new Coordinate(x, y);
            this.height = height;
        }

        public int getX() {
            return position.getX();
        }
        public int getY() {
            return position.getY();
        }
        public boolean isVisible() {
            return isVisible;
        }
        public void setVisible(String[] forest) {
            var rows = forest.length;
            var columns = forest[0].length();

            this.isVisible = true;

            // edge
            if (this.getX() == 0
             || this.getY() == 0
             || this.getX() == columns - 1
             || this.getY() == rows - 1) {
                return;
            }

            // Check Vertically Up
            var isVisibleFromTop = true;
            for (var row = 0; row < this.getY(); row++) {
                var col = this.getX();
                var height = Integer.parseInt(String.valueOf(forest[row].charAt(col)));
                if (height >= this.height) {
                    isVisibleFromTop = false;
                }
            }
            if (isVisibleFromTop) return;

            // Check Vertically Down
            var isVisibleFromBottom = true;
            for (var row = this.getY() + 1; row < rows; row++) {
                var col = this.getX();
                var height = Integer.parseInt(String.valueOf(forest[row].charAt(col)));
                if (height >= this.height) {
                    isVisibleFromBottom = false;
                }
            }
            if (isVisibleFromBottom) return;

            // Check Horizontally Left
            var isVisibleFromLeft = true;
            for (var col = 0; col < this.getX(); col++) {
                var row = this.getY();
                var height = Integer.parseInt(String.valueOf(forest[row].charAt(col)));
                if (height >= this.height) {
                    isVisibleFromLeft = false;
                }
            }
            if (isVisibleFromLeft) return;

            // Check Horizontally Right
            var isVisibleFromRight = true;
            for (var col = this.getX() + 1; col < columns; col++) {
                var row = this.getY();
                var height = Integer.parseInt(String.valueOf(forest[row].charAt(col)));
                if (height >= this.height) {
                    isVisibleFromRight = false;
                }
            }
            this.isVisible = isVisibleFromBottom
                          || isVisibleFromTop
                          || isVisibleFromLeft
                          || isVisibleFromRight;
        }
        public double getScenicScore() {
            return scenicScore;
        }
        public void setScenicScore(String[] forest) {
            var rows = forest.length;
            var columns = forest[0].length();

            // edge
            if (this.getX() == 0
             || this.getY() == 0
             || this.getX() == columns - 1
             || this.getY() == rows - 1) {
                this.scenicScore = 0;
                return;
            }

            // Check Vertically Up
            var topScore = 0;
            for (var row = this.getY() - 1; row >= 0; row--) {
                topScore++;
                var col = this.getX();
                var height = Integer.parseInt(String.valueOf(forest[row].charAt(col)));
                if (height >= this.height) {
                    break;
                }
            }

            // Check Vertically Down
            var downScore = 0;
            for (var row = this.getY() + 1; row < rows; row++) {
                downScore++;
                var col = this.getX();
                var height = Integer.parseInt(String.valueOf(forest[row].charAt(col)));
                if (height >= this.height) {
                    break;
                }
            }

            // Check Horizontally Left
            var leftScore = 0;
            for (var col = this.getX() - 1; col >= 0; col--) {
                leftScore++;
                var row = this.getY();
                var height = Integer.parseInt(String.valueOf(forest[row].charAt(col)));
                if (height >= this.height) {
                    break;
                }
            }

            // Check Horizontally Right
            var rightScore = 0;
            for (var col = this.getX() + 1; col < columns; col++) {
                rightScore++;
                var row = this.getY();
                var height = Integer.parseInt(String.valueOf(forest[row].charAt(col)));
                if (height >= this.height) {
                    break;
                }
            }
            this.scenicScore = topScore * downScore * leftScore * rightScore;
        }
    }
}