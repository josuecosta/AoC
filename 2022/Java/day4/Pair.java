package day4;

public class Pair {
    private final Assignment LPair;
    private final Assignment RPair;

    public Pair(String LAssignment, String RAssignment) {
        var assignmentSplitted = LAssignment.split("-");
        LPair = new Assignment(assignmentSplitted[0], assignmentSplitted[1]);

        assignmentSplitted = RAssignment.split("-");
        RPair = new Assignment(assignmentSplitted[0], assignmentSplitted[1]);
    }

    public Assignment getLPair() {
        return LPair;
    }
    public Assignment getRPair() {
        return RPair;
    }
}
