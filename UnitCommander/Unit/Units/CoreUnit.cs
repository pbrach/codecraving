using System.Threading;
using Unit.UnitCommands;

namespace Unit.Units
{
    public abstract class CoreUnit
    {
        public abstract UnitData UnitData { get; set; }

        public abstract UnitCommand UnitCommand { get; set; }
        
        public CoreUnit()
        {
            var lifeThread = new Thread(new ThreadStart(MainLoop));
            lifeThread.Start();
        }

        private void MainLoop()
        {
            var innerCurrentData = UnitData.Clone();
            var innerCurrentCommand = UnitCommand.Clone();
            
            while (true)
            {
                UnitData = innerCurrentData.Clone();
                innerCurrentCommand = UnitCommand.Clone();

                if (innerCurrentCommand is StopUnitCommand)
                {
                    break;
                }
                
                PerformCommand(innerCurrentCommand);
                Thread.Sleep(300); // We hold this thread open infinitely anyways,
                                   // thus sleep may be used here: we do not intend to make this thread available for some other processing
            }
        }

        protected abstract void PerformCommand<T>(T cmd) where T : UnitCommand;
    }
}