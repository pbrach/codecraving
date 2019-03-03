using System.Threading;
using Unit.UnitCommands;

namespace Unit.Units
{
    public abstract class CoreUnit
    {
        public UnitData UnitData { get; set; }

        public UnitCommand UnitCommand { get; set; }
        
        public CoreUnit()
        {
            var lifeThread = new Thread(new ThreadStart(MainLoop));
            lifeThread.Start();
        }

        private void MainLoop()
        {
            var innerState = new UnitState
            {
                CurrentData = UnitData.Clone(), 
                CurrentCommand = UnitCommand.Clone()
            };
            
            while (true)
            {
                UnitData = innerState.CurrentData.Clone();
                innerState.CurrentCommand = UnitCommand.Clone();

                if (innerState.CurrentCommand is StopUnitCommand)
                {
                    break;
                }
                
                PerformCommand(innerState.CurrentCommand);
                Thread.Sleep(300); // We hold this thread open infinitely anyways, thus sleep may be used here
            }
        }


        protected abstract void PerformCommand(UnitCommand cmd);
    }
}