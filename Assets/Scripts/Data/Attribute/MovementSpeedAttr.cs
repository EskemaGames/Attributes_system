namespace example.attributes
{
    [System.Serializable]
    public class MovementSpeedAttr : BaseAttribute
    {
        
        #region constructor
        public MovementSpeedAttr() { }

        public override BaseAttribute Clone()
        {
            return new MovementSpeedAttr(this);
        }

        public MovementSpeedAttr(MovementSpeedAttr attribute) : base(attribute)
        {
        }
        
        public MovementSpeedAttr(float value, float minvalue, float maxvalue, float increasestep, GameEnums.Modifier modifier) 
            : base(value, minvalue, maxvalue, increasestep, modifier)
        {
        }
        #endregion

    }

}