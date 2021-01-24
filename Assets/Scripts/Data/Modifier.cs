namespace example.attributes
{
    [System.Serializable]
    public class Modifier
    {
        public uint Id { private set; get; } //try to put a unique id, that way same modifier cant be added twice
        public GameEnums.Modifier ModifierType { private set; get; } //3 basic formulas "percent", "addition" and "critics"
        public BaseAttribute Attribute { private set; get; } //what is the attribute for this modifier?

        #region constructors
        public Modifier() { }
        
        public Modifier(uint _id,
            GameEnums.Modifier _formula,
            BaseAttribute _attribute)
        {
            Id = _id;
            ModifierType = _formula;
            Attribute = _attribute;
        }


        public Modifier(Modifier modifier)
        {
            Id = modifier.Id;
            ModifierType = modifier.ModifierType;
            Attribute = modifier.Attribute;
        }

        public Modifier Clone()
        {
            return new Modifier(this);
        }
        #endregion


        //just clear the attribute
        public void Destroy()
        {
            Attribute = null;
        }

    }

}