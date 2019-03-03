using Unit.UnitCommands;

namespace Unit.Units
{
    public class DriveUnit : CoreUnit
    {
        protected override void PerformCommand(UnitCommand cmd)
        {
            throw new System.NotImplementedException();
        }
        
        private void PerformGenericCommand<T>(T cmd) where T: UnitCommand
        {
            InnerPerformCommand(cmd);
        }

        private void InnerPerformCommand(StopUnitCommand cmd)
        {
            
        }
        
        private void InnerPerformCommand(UnitCommand cmd)
        {
            
        }
    }
}