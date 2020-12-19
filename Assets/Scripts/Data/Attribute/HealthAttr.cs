

namespace example.attributes
{
    [System.Serializable]
    public class HealthAttr : BaseAttribute
    {
        #region constructor
        public HealthAttr() { }

        public override BaseAttribute Clone()
        {
            return new HealthAttr(this);
        }

        public HealthAttr(HealthAttr attr):base(attr)
        {
        }
        
        public HealthAttr(float value, float minvalue, float maxvalue, float increasestep, GameEnums.Modifier modifier) 
            : base(value, minvalue, maxvalue, increasestep, modifier)
        {
        }
        #endregion

    }
    

}