namespace FOE.Saving
{
    public interface ISavable
    {
        public void RestoreState(object state);
        public object CaptureState();
    }
}