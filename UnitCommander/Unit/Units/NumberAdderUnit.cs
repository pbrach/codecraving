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
        private UnitCommand _adderCommand;

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
            set => _adderCommand = value;
        }

        protected override void PerformCommand(UnitCommand cmd)
        {
            if (cmd is AddCommand addCmd)
            {
                _adderData.Number += addCmd.ToBeAdded;
            }
            
            _adderCommand = null;
        }
    }
}