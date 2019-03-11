using System.Threading;
using Unit.UnitCommands;

namespace Unit.Units
{
    public class UnitInterface
    {
        private readonly CoreUnit _coreUnit;
        private UnitCommand _unitCommand;
        private UnitData _unitData;

        public UnitData UnitData => _unitData;

        public UnitCommand UnitCommand
        {
            set => _unitCommand = value;
        }

        public UnitInterface(CoreUnit unit)
        {
            _coreUnit = unit;
            UpdateInterface();
            var lifeThread = new Thread(MainLoop);
            lifeThread.Start();
        }

        private void MainLoop()
        {
            while (true)
            {
                UpdateInterface();
                if (_coreUnit.UnitCommand is StopUnitCommand)
                {
                    break;
                }
                
                _coreUnit.PerformCommand();
                Thread.Sleep(300); // We hold this thread open infinitely anyways,
                                   // thus sleep may be used here: we do not intend to make this thread available for some other processing
            }
        }

        private void UpdateInterface()
        {
            _unitData = _coreUnit.UnitData.Clone();
            
            if(_unitCommand != null)
            {
                _coreUnit.UnitCommand = _unitCommand.Clone();
            }
        }
    }
}