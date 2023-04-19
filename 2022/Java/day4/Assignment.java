package day4;

public class Assignment {
    public Assignment(String v1, String v2)
    {
        this.Min = Integer.parseInt(v1);
        this.Max = Integer.parseInt(v2);
    }
    private final int Min;
    private final int Max;

    public int getMin() {
        return Min;
    }

    public int getMax() {
        return Max;
    }
}
