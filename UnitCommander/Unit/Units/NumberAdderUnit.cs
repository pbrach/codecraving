namespace Unit.Units
{
    public class AdderData : UnitData
    {
        public double Number { get; set; }
    }

    public class AddCommand : UnitCommand
    {
        public double ToBeAdded { get; set; }
    }

    public class NumberAdderUnit : CoreUnit
    {
        private readonly AdderData _adderData;
        private AddCommand _adderCommand;

        public NumberAdderUnit()
        {
            _adderData = new AdderData {Number = 0};
            _adderCommand = new AddCommand {ToBeAdded = 0};
        }

        public override UnitData UnitData
        {
            get => _adderData;
            set { }
        }

        public override UnitCommand UnitCommand
        {
            get => _adderCommand;
            set
            {
                if (value is AddCommand addCmd)
                {
                    _adderCommand = addCmd;
                }
            }
        }

        protected override void PerformCommand<T>(T cmd)
        {
            var addCmd = cmd as AddCommand;
            if (addCmd != null)
            {
                ProcessAddCommand(addCmd);
            }
        }

        private void ProcessAddCommand(AddCommand addCmd)
        {
            _adderData.Number += addCmd.ToBeAdded;
            _adderCommand = null;
        }
    }
}