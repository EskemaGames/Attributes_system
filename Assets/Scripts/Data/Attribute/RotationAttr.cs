namespace example.attributes
{
    [System.Serializable]
    public class RotationAttr : BaseAttribute
    {
        
        #region constructor
        public RotationAttr() { }

        public override BaseAttribute Clone()
        {
            return new RotationAttr(this);
        }

        public RotationAttr(RotationAttr attribute) : base(attribute)
        {
        }
        
        public RotationAttr(float value, float minvalue, float maxvalue, float increasestep, GameEnums.Modifier modifier) 
            : base(value, minvalue, maxvalue, increasestep, modifier)
        {
        }
        #endregion

    }

}