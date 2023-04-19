package day6;

import lombok.Getter;
import lombok.Setter;
import resources.IAOC;

import java.util.stream.Collectors;

@Getter
@Setter
public class TuningTrouble
        implements IAOC {

    private final String rawData;

    public TuningTrouble(String[] rawData) {
        this.rawData = rawData[0];
    }

    @Override
    public Object Part1() {
        return GetStartOfMessage(4);
    }

    @Override
    public Object Part2() {
        return GetStartOfMessage(14);
    }

    private Integer GetStartOfMessage(int distinctChars) {
        for (var index = 0; index < this.rawData.length() - 1; index++) {
            var maker = this.rawData.substring(index, index + distinctChars);
            var charSet = maker
                    .chars()
                    .mapToObj(c -> (char) c)
                    .collect(Collectors.toSet());

            // 'Set' contains only unique elements,
            // if the set doesn't have the same size as the distinct chars must be
            // it's because there are duplicated values
            if (charSet.size() == distinctChars) {
                return index + distinctChars;
            }
        }
        return null;
    }
}
