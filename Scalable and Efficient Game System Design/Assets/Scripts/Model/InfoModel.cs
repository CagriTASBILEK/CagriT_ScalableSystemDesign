namespace Model
{
    public class InfoModel
    {
        public int CurrentValue { get; private set; }
        public int MaxValue { get; }

        public InfoModel(int maxValue)
        {
            MaxValue = maxValue;
            CurrentValue = maxValue;
        }

        public void UpdateValue(int newValue)
        {
            CurrentValue = newValue;
        }
    }
}