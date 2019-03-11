namespace Unit.Units
{
    public abstract class CoreUnit
    {
        public void PerformCommand()
        {
            PerformCommand(UnitCommand);
        }
        
        protected abstract void PerformCommand(UnitCommand cmd);
        
        public abstract UnitData UnitData { get; set; }
        public abstract UnitCommand UnitCommand { get; set; }
    }
}