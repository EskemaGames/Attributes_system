using System;
using System.Collections.Generic;


namespace example.attributes
{
    [System.Serializable]
    public class EntityData
    {
        //assign a number for enemies or players
        public uint CharacterId { get; private set; }
        public string Id{ get; private set; }
        public string PortraitName{ get; private set; }
        public string ClassName{ get; private set; }
        private List<BaseAttribute> _attributes = new List<BaseAttribute>();
        private List<Modifier> _modifiers = new List<Modifier>();

        
        
        #region constructor

        public EntityData() { }

        public EntityData(
            uint characterid,
            string id,
            string name,
            string classname,
            List<BaseAttribute> attributes)
        {
            CharacterId = characterid;
            Id = id;
            PortraitName = name;
            ClassName = classname;
            attributes = new List<BaseAttribute>(attributes);
        }
        
        public EntityData(EntityData data)
        {
            CharacterId = data.CharacterId;
            Id = data.Id;
            PortraitName = data.PortraitName;
            ClassName = data.ClassName;
            _attributes = data._attributes;
            _modifiers = data._modifiers;
        }
        
        public EntityData Clone()
        {
            return new EntityData(this);
        }
        
        #endregion

        
        
        // public void Init(uint characterid,
        //     string id,
        //     string name,
        //     string classname, 
        //     List<BaseAttribute> attributes)
        // {
        //     CharacterId = characterid;
        //     Id = id;
        //     PortraitName = name;
        //     ClassName = classname;
        //     _attributes = new List<BaseAttribute>(attributes);
        // }


        public void Destroy()
        {
            var total = _modifiers.Count - 1;
            for (var i = total; i > -1; --i)
            {
                _modifiers[i].Destroy();
            }

            _modifiers.Clear();
            _modifiers = null;
        }



        #region attributes
        public BaseAttribute GetAttribute<T>()
        {
            for (var i = 0; i < _attributes.Count; ++i)
            {
                if (_attributes[i] is T)
                {
                    return _attributes[i];
                }
            }

            return null;
        }

        public BaseAttribute GetAttribute(string name)
        {
            for (var i = 0; i < _attributes.Count; ++i)
            {
                if (_attributes[i].Name == name)
                {
                    return _attributes[i];
                }
            }

            return null;
        }

        public void RemoveAttribute<T>()
        {
            for (var i = 0; i < _attributes.Count; ++i)
            {
                if (_attributes[i] is T)
                {
                    _attributes.RemoveAt(i);
                    break;
                }
            }
        }

        public float GetAttributeDefaultValue<T>()
        {
            var attr = GetAttribute<T>();
            return attr == null ? 0f : attr.Value;
        }

        public float GetAttributeDefaultValue(string name)
        {
            var attr = GetAttribute(name);
            return attr == null ? 0f : attr.Value;
        }







        public float GetAttributeValue<T>()
        {      
            var existingModifiers = new List<Modifier>();

            //loop and search if a modifier exists
            for (var i = 0; i < _modifiers.Count; ++i)
            {
                if (_modifiers[i].Attribute is T)
                {
                    existingModifiers.Add(_modifiers[i]);
                }
            }


            //no modifiers, return default value
            if (existingModifiers.Count == 0)
            {
                return GetAttributeDefaultValue<T>();
            }


            var finalValue = 0f;
            var multValue = 0f;
            var additionValue = 0f;
            var foundValue = false;

            //something exists
            for (var i = 0; i < existingModifiers.Count; ++i)
            {
                if (existingModifiers[i].ModifierType == GameEnums.Modifier.PERCENTAGE)
                {
                    multValue += existingModifiers[i].Attribute.Value;
                    finalValue = GetAttributeDefaultValue<T>();
                    foundValue = true;
                }            
                else if (existingModifiers[i].ModifierType == GameEnums.Modifier.ADDITION)
                {
                    additionValue = GetAttributeDefaultValue<T>();

                    foundValue = true;
                    //first operation we add the base value
                    //next operations will only increment/decrement the value
                    if (finalValue == 0)
                    {
                        finalValue += additionValue + existingModifiers[i].Attribute.Value;
                    }
                    else
                    {
                        finalValue += existingModifiers[i].Attribute.Value;
                    }
                }
            }


            //we had modifiers, but none of them apply to the attribute
            //we are looking, so just return the default value
            if (finalValue == 0f && !foundValue)
            {
                return finalValue = GetAttributeDefaultValue<T>();
            }

            return finalValue + (finalValue * multValue);
        }
        #endregion



        #region add and get modifiers
        public void AddModifier(Modifier modifier)
        {
            var canAdd = true;
            var positionId = 0;

            for (var i = 0; i < _modifiers.Count; ++i)
            {
                if (modifier.Attribute.Name.Equals(_modifiers[i].Attribute.Name))
                {
                    if (modifier.ModifierType == _modifiers[i].ModifierType)
                    {
                        positionId = i;
                        canAdd = false;
                        break;
                    }
                }
            }
            
            if (canAdd)
            {
                _modifiers.Add(modifier);
            }
            else
            {
                var tmpmodifier = _modifiers[positionId];
                
                var value = tmpmodifier.Attribute.Value; //get our "original" value
                value += modifier.Attribute.Value; //add the new value that came in, as we want to overwrite the existing value
                
                //prepare the constructor parameters
                //BaseAttribute(float value, float minvalue, float maxvalue, float increasestep, GameEnums.Modifier modifier)
                var argTypeInstruction = new object[]
                {
                    value,
                    tmpmodifier.Attribute.MinValue,
                    tmpmodifier.Attribute.MaxValue,
                    tmpmodifier.Attribute.IncreaseStep,
                    tmpmodifier.ModifierType
                };
                
                //create the new attribute
                var newAttr = CreateInstance<BaseAttribute>(
                    "example.attributes.",
                    modifier.Attribute.Name,
                    argTypeInstruction);
                
                //create the new modifier to be added
                var mod = new Modifier(tmpmodifier.Id, tmpmodifier.ModifierType, newAttr);

                //replace the modifier with this new one
                _modifiers[positionId] = mod;
            }
        }

        public Modifier GetModifier<T>()
        {
            for (var i = 0; i < _modifiers.Count; ++i)
            {
                if (_modifiers[i].Attribute is T)
                {
                    return _modifiers[i];
                }
            }

            return null;
        }
        #endregion


        #region remove modifiers
        public void RemoveModifier(Modifier aModifier)
        {
            for (var i = _modifiers.Count - 1; i > -1; --i)
            {
                if (_modifiers[i] == aModifier)
                {
                    _modifiers.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveModifier<T>()
        {
            for (var i = _modifiers.Count - 1; i > -1; --i)
            {
                if (_modifiers[i].Attribute is T)
                {
                    _modifiers.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveAllModifiersWithType<T>()
        {
            for (var i = _modifiers.Count - 1; i > -1; --i)
            {
                if (_modifiers[i].Attribute is T)
                {
                    _modifiers.RemoveAt(i);
                }
            }
        }

        public void RemoveAllModifiers()
        {
            _modifiers.Clear();
        }
        #endregion



        #region helper
        
        private I CreateInstance<I>(string namespaceName, string className, object[] someParams) where I : class
        {
            var typeClass = System.Type.GetType(namespaceName + className);
            return Activator.CreateInstance(typeClass, someParams) as I;
        }
        
        #endregion

    }

}