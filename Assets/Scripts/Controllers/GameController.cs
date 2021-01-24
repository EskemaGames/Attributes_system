using System.Collections.Generic;
using UnityEngine;

using example.attributes;

public class GameController : MonoBehaviour {

    public EntityController playerController;


    // Use this for initialization
    void Start () {
	    
	    DataDDBB.Instance.Init();

	    
	    //instantiate via hard coded variables
        // List<BaseAttribute> attributes = new List<BaseAttribute>();
        //
        // AttackAttr attk = new AttackAttr(4, 1, 100, 1, GameEnums.Modifier.ADDITION);
        //
        // HealthAttr health = new HealthAttr(100, 0, 100, 0, GameEnums.Modifier.ADDITION);
        //
        // DefenseAttr defense = new DefenseAttr(10, 1, 100, 0, GameEnums.Modifier.ADDITION);
        //
        // attributes.Add(attk);
        // attributes.Add(health);
        // attributes.Add(defense);
        //
        // var pData = new EntityData(1, "id", "name", "classname", attributes);
        // playerController.Init(pData);



        //instantiate via our database from the json or scriptables or whatever type of file was in there
        var plData = DataDDBB.Instance.GetCharacterData("EnemyType1");
        playerController.Init(plData);


    }
	
    


}
