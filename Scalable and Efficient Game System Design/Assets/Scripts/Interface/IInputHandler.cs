namespace Interface
{
    public interface IInputHandler
    {
        InputData GetInputData();
    }

    public struct InputData
    {
        public float HorizontalInput;
        public bool JumpRequested;
    }
}