

namespace example.attributes
{
    [System.Serializable]
    public class DefenseAttr : BaseAttribute
    {
        #region constructor
        public DefenseAttr() { }

        public override BaseAttribute Clone()
        {
            return new DefenseAttr(this);
        }

        public DefenseAttr(DefenseAttr attribute):base(attribute)
        {
        }
        
        public DefenseAttr(float value, float minvalue, float maxvalue, float increasestep, GameEnums.Modifier modifier) 
            : base(value, minvalue, maxvalue, increasestep, modifier)
        {
        }
        #endregion

    }

}