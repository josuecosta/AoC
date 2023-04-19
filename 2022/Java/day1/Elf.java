package day1;

import lombok.Getter;
import lombok.Setter;
import org.apache.commons.lang3.StringUtils;
import resources.IAOC;

import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;

@Getter
@Setter
//@NoArgsConstructor
//@AllArgsConstructor
public class Elf implements IAOC {
    public Double getMax() {

        // Option 1
//        var optionalDouble = getElfList().values()
//                .stream()
//                .mapToDouble(Double::doubleValue)
//                .max();
//        return optionalDouble.isPresent() ? optionalDouble.getAsDouble() : 0.0;

        // Option 2
//        return getElfList().values().stream()
//                .max(Comparator.naturalOrder())
//                .get();

        // Option 3
        return Collections.max(getElfList().values());
    }

    public Double getTotal() {
        return getElfList().values()
                .stream()
                .sorted(Comparator.reverseOrder())
                .limit(3)
                .mapToDouble(Double::doubleValue)
                .sum();
    }

    public HashMap<Integer, Double> getElfList() {
        var index = 0;
        for (String rawDatum : rawData) {
            if (!elfList.containsKey(index)) {
                elfList.put(index, 0.0);
            }

            if (StringUtils.isEmpty(rawDatum)) {
                index++;
                continue;
            }

            elfList.put(index, elfList.get(index) + Double.parseDouble(rawDatum));
        }
        return elfList;
    }

    private final HashMap<Integer, Double> elfList;
    private final String[] rawData;
    private Double total;
    private Double max;

    public Elf(String[] data) {
        this.elfList = new HashMap<>();
        this.rawData = data;
    }

    @Override
    public Object Part1() {
        return getMax();
    }

    @Override
    public Object Part2() {
        return getTotal();
    }
}
