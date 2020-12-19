namespace example.attributes
{
    [System.Serializable]
    public abstract class BaseAttribute
    {
        public float Value { get; private set; }
        public float MinValue { get; private set; }
        public float MaxValue { get; private set; }
        public float IncreaseStep { get; private set; }
        public GameEnums.Modifier Modifier { get; private set; }

        public string Name { get { return GetType().Name; } }


        #region constructor

        public BaseAttribute() { }

        public BaseAttribute(
            float value,
            float minvalue,
            float maxvalue,
            float increasestep, 
            GameEnums.Modifier modifier)
        {
            Value = value;
            MinValue = minvalue;
            MaxValue = maxvalue;
            IncreaseStep = increasestep;
            Modifier = modifier;
        }
        
        public BaseAttribute(BaseAttribute attr)
        {
            Value = attr.Value;
            MinValue = attr.MinValue;
            MaxValue = attr.MaxValue;
            IncreaseStep = attr.IncreaseStep;
            Modifier = attr.Modifier;
        }
        
        public virtual BaseAttribute Clone()
        {
            return null;
        }
        
        #endregion

    }

}