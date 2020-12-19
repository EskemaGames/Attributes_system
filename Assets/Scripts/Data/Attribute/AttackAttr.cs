namespace example.attributes
{
    [System.Serializable]
    public class AttackAttr : BaseAttribute
    {
        
        #region constructor
        
        public AttackAttr() { }

        public override BaseAttribute Clone()
        {
            return new AttackAttr(this);
        }

        public AttackAttr(AttackAttr attribute) : base(attribute)
        {
        }
        
        public AttackAttr(float value, float minvalue, float maxvalue, float increasestep, GameEnums.Modifier modifier) 
            : base(value, minvalue, maxvalue, increasestep, modifier)
        {
        }
        #endregion

    }

}