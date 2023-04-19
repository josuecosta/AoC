package y2015.day20;

import resources.IAOC;

import java.util.LinkedHashMap;
import java.util.Map;

public class InfinitePresents implements IAOC {
    private Map<Integer, Long> houses;

    public InfinitePresents(String[] data) {
        this.houses = new LinkedHashMap<>();
    }

    @Override
    public Object Part1() {
        return CreatePresents(36000000);
    }

    private int CreatePresents(int n) {
        for (int i = 1; i < n / 10; i++) {
            var sum = 0;
            for (int j = 1; j <= i; j++) {
                if (i % j == 0) {
                    sum += j * 10;
                }
            }
            if (sum >= n) {
                return i;
            }
        }
        return 0;
    }

    private Long CalculatePresents(int house) {
        var presents = 0L;
        for (int i = house; i > 0; i--) {
            if (house % i == 0) {
                presents += (i * 10L);
            }
        }
        return presents;
    }

    @Override
    public Object Part2() {
        return CreatePresentsPart2();
    }

    private int CreatePresentsPart2() {
        var i = 831600;
        var temp = 0L;
        var tempMax = 0L;
        do {
//            this.houses.put(++i, CalculatePresents(i));
            temp = CalculatePresents2(++i, 50);
            if (temp > tempMax) {
                tempMax = temp;
            }
//        } while (this.houses.get(i) < 36000000);
        } while (temp < 36000000);
        return i;
    }

    private Long CalculatePresents2(int house, int limit) {
        var presents = 0L;
        for (int i = house; i > 0 && i > (limit * house); i--) {
            if (house % i == 0) {
                presents += (i * 11L);
            }
        }
        return presents;
    }
}
