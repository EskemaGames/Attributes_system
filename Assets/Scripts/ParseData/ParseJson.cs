using System.Collections.Generic;



namespace example
{


    #region attributes base

    [System.Serializable]
    public class ParseAttributesData
    {
        public List<AttributesJsonData> Attributes = new List<AttributesJsonData>(20);
    }

    [System.Serializable]
    public class AttributesJsonData
    {
        public uint Id = 0;
        public float Value = 0f;
        public float MinValue = 0f;
        public float MaxValue = 0f;
        public string AttributeName = System.String.Empty;
        public string FormulaType = System.String.Empty;
        
    }

    #endregion


    #region character

    [System.Serializable]
    public class CharacterParseJsonData
    {
        
        public string ClassName = System.String.Empty;
        public string PortraitName = System.String.Empty;
        public string PrefabName = System.String.Empty;
        public string NameId = System.String.Empty;
        public List<AttributesJsonData> Attributes = new List<AttributesJsonData>();
    }

    [System.Serializable]
    public class PARSERootCharactersJsonData
    {
        public List<CharacterParseJsonData> Characters = new List<CharacterParseJsonData>();
    }

    #endregion

}
