public interface IStat
{
    float CurrentValue { get; set; }
    float MaxValue { get; }

    void ChangeValue(float value);
}
