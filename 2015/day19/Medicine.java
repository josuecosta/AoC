package y2015.day19;

import resources.IAOC;

import java.util.*;
import java.util.regex.Pattern;

public class Medicine implements IAOC {
    private final HashSet<String> molecules;
    private List<InstructionMolecule> replacements;
    private String medicineMolecule;

    public Medicine(String[] data) {
        ParseData(data);
        this.molecules = new HashSet<>();
    }

    private void ParseData(String[] data) {
        this.replacements = new ArrayList<>();
        var i = 0;
        for (; i < data.length; i++) {
            if (data[i].isBlank()) {
                break;
            }
            var instructions = data[i].split("=>");
            this.replacements.add(new InstructionMolecule(instructions[0], instructions[1]));
        }
        this.medicineMolecule = data[i + 1];
    }

    @Override
    public Object Part1() {
        Calibration();
        return this.molecules.size();
    }

    private void Calibration() {
        for (var replacement : replacements) {
            var matcher = Pattern.compile(replacement.key).matcher(this.medicineMolecule);
            while (matcher.find()) {
                var buff = new StringBuffer(this.medicineMolecule);
                buff.replace(matcher.start(), matcher.end(), replacement.value);
                this.molecules.add(buff.toString());
            }
        }
    }

    @Override
    public Object Part2() {
        return CreateMolecule();
    }

    private int CreateMolecule() {
        var buff = new StringBuffer(this.medicineMolecule).reverse();
        var steps = 0;
        while (!buff.toString().equals("e")) {
            // Add all matches into a temp Map
            var temp = new HashMap<InstructionMolecule, Integer>();
            for (var match : replacements.stream().filter(x -> !x.key.equals("e")).toList()) {
                var pattern = new StringBuffer(match.value).reverse().toString();
                var matcher = Pattern.compile(pattern).matcher(buff);
                if (matcher.find()) {
                    temp.put(match, matcher.start());
                }
            }

            // Replace one step using the most right (left reversed) match
            if (temp.size() > 0) {
                var match = temp.entrySet().stream().min(Map.Entry.comparingByValue()).get().getKey();
                var pattern = new StringBuffer(match.value).reverse().toString();
                var matcher = Pattern.compile(pattern).matcher(buff);
                if (matcher.find()) {
                    buff.replace(matcher.start(), matcher.end(), new StringBuffer(match.key).reverse().toString());
                    steps++;
                }
            } else {
                // When there is no more matches, replace using one of the 'e' replacements
                for (var match : replacements.stream().filter(x -> x.key.equals("e")).toList()) {
                    var pattern = new StringBuffer(match.value).reverse().toString();
                    var matcher = Pattern.compile(pattern).matcher(buff);
                    if (matcher.find()) {
                        buff.replace(matcher.start(), matcher.end(), new StringBuffer(match.key).reverse().toString());
                        steps++;
                        break;
                    }
                }
            }
        }
        return steps;
    }

    public class InstructionMolecule {
        private final String key;
        private final String value;

        public InstructionMolecule(String key, String newValue) {
            this.key = key.trim();
            this.value = newValue.trim();
        }

        public String getKey() {
            return key;
        }

        public String getValue() {
            return value;
        }
    }
}
