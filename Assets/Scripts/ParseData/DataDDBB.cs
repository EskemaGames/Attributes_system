using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using example;
using example.attributes;
using UnityEngine;




public class DataDDBB : MonoBehaviour
{
    [SerializeField] TextAsset[] charactersTextList = null;

    
    private static DataDDBB instance = null;
    private List<EntityData> charactersData = new List<EntityData>();

    public static DataDDBB Instance
    {
        get { return instance; }
    }
    




    
    #region init and destroy

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Init()
    {
        uint characterIdCounterTmp = 1;

        //create the characters for the data parsed
        charactersData = new List<EntityData>(30);

        ParseCharactersData(charactersTextList, ref characterIdCounterTmp);
    }
    
    public void OnDestroy()
    {
        charactersData.Clear();
    }

    #endregion



    
    

    #region parse data from json

    private void ParseCharactersData(TextAsset[] files, ref uint characterIdCounter)
    {
        for (var i = 0; i < files.Length; ++i)
        {
            var charsData = files[i];
            PARSERootCharactersJsonData allData = null;

            try
            {
                allData = JsonUtility.FromJson<PARSERootCharactersJsonData>(charsData.text);
            }
            catch (Exception)
            {
                Debug.LogError("error parsing characters json number in list= " + charsData.name);
                return;
            }


            for (var cnt = 0; cnt < allData.Characters.Count; ++cnt)
            {
                //parse the character attributes
                var attributesParsed = ParseAttributes(allData.Characters[cnt].Attributes);

                var character = new EntityData(
                    characterIdCounter,
                    allData.Characters[cnt].NameId,
                    allData.Characters[cnt].PortraitName,
                    allData.Characters[cnt].ClassName,
                    attributesParsed);

                charactersData.Add(character);
                
                characterIdCounter++;
            }
        }
    }

    #endregion


    #region internal parse of elements

    private List<BaseAttribute> ParseAttributes(List<AttributesJsonData> attributes)
    {
        //parse the character attributes
        var attributesParsed = new List<BaseAttribute>();

        for (var counter = 0; counter < attributes.Count; ++counter)
        {
            var formula = (GameEnums.Modifier) System.Enum.Parse(typeof(GameEnums.Modifier),
                attributes[counter].FormulaType);

            var typeClass = System.Type.GetType("example.attributes." + attributes[counter].AttributeName);

            object[] argTypes = new object[]
            {
                attributes[counter].Value,
                attributes[counter].MinValue,
                attributes[counter].MaxValue,
                1f,
                formula
            };

            var attr = Activator.CreateInstance(typeClass, argTypes) as BaseAttribute;

            attributesParsed.Add(attr);
        }

        return attributesParsed;
    }

    #endregion
    

    #region getters
    
    public ReadOnlyCollection<EntityData> GetCharactersData
    {
        get { return charactersData.AsReadOnly(); }
    }

    public EntityData GetCharacterData(string name)
    {
        //look in both players and npcs, enemies, etc
        for (var i = 0; i < charactersData.Count; ++i)
        {
            if (charactersData[i].ClassName.Equals(name))
            {
                return charactersData[i];
            }
        }

        return null;
    }

    #endregion

    

}

