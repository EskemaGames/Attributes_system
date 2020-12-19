using System.Collections.Generic;
using UnityEngine;

using example.attributes;

public class GameController : MonoBehaviour {

    public EntityController playerController;


    // Use this for initialization
    void Start () {

        List<BaseAttribute> attributes = new List<BaseAttribute>();

        EntityData pData = new EntityData();

        AttackAttr attk = new AttackAttr(4, 1, 100, 1, GameEnums.Modifier.ADDITION);

        HealthAttr health = new HealthAttr(100, 0, 100, 0, GameEnums.Modifier.ADDITION);

        DefenseAttr defense = new DefenseAttr(10, 1, 100, 0, GameEnums.Modifier.ADDITION);
        
        attributes.Add(attk);
        attributes.Add(health);
        attributes.Add(defense);

        pData.Init(1, attributes);
        
        playerController.Init(pData);	
	}
	
    


}
