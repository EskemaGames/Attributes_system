using System;
using UnityEngine;

using example.attributes;

public class EntityController : MonoBehaviour {


    [SerializeField] private BoxCollider BoxCollider = null;
    [SerializeField] private Rigidbody Rigidbody = null;
    [SerializeField] private Transform tr = null;
    private EntityData entityData = new EntityData();

    private void Awake()
    {
        tr = this.transform;
    }

    public void Init(EntityData data)
    {
        entityData = data;
    }


    public void Update()
    {
        Movement();
        Rotate();
    }


    public void StartAttack()
    {
        Attack(null);
    }


    private void Movement()
    {
        BaseAttribute attr = entityData.GetAttribute<MovementSpeedAttr>();

        if (attr == null) return;
        
        tr.Translate(Vector3.forward * attr.Value * Time.deltaTime);
    }
    
    
    
    
    
    
    private void Rotate()
    {
        BaseAttribute attr = entityData.GetAttribute<RotationAttr>();

        if (attr == null) return;
        
        tr.Rotate(Vector3.forward * attr.Value * Time.deltaTime);
    }

    
    
    
    
    
    
    public void Attack(GameObject target)
    {
        Debug.Log("atk");
        BaseAttribute attack = entityData.GetAttribute<AttackAttr>();

        if (attack == null) {Debug.Log("null");return;}
        
        //do something to attack
        //target.GetComponent<ITarget>().GotHit(attack.Value);

        //initial value was 100 points
        // attack of 30
        //so we add -30 points to our defense
        DefenseAttr attr = new DefenseAttr(
            -attack.Value,
            0,
            100,
            0,
            GameEnums.Modifier.ADDITION);

        Modifier m = new Modifier(1, GameEnums.Modifier.ADDITION, attr);

        entityData.AddModifier(m);
        
    }



    public void GotHit(int damageValue)
    {
        BaseAttribute health = entityData.GetAttribute<HealthAttr>();

        //health exists???, if so, then remove health from our character
        if (health == null) return;

        HealthAttr attr = new HealthAttr(
            -damageValue, 
            health.MinValue,
            health.MaxValue,
            0,
            GameEnums.Modifier.ADDITION);

        Modifier m = new Modifier(
            1,
            GameEnums.Modifier.ADDITION,
            attr);

        entityData.AddModifier(m);




        //as we've modified the health, let's check our current health
        float currentHealth = entityData.GetAttributeValue<HealthAttr>();

        if (currentHealth <= 0f)
        {
            //player is dead, do something about it
        }
        else
        {
            //player is still alive, so maybe put a sound here or just do nothing...
        }


        
    }





    public void EquipSword(int valueSword)
    {
        BaseAttribute atk = entityData.GetAttribute<AttackAttr>();

        if (atk == null) return;
        
        AttackAttr attr = new AttackAttr(valueSword, atk.MinValue, atk.MaxValue, atk.IncreaseStep, GameEnums.Modifier.ADDITION);
    
        Modifier m = new Modifier(1, GameEnums.Modifier.ADDITION, attr);

        entityData.AddModifier(m);
    }


    private void GetAttackValue()
    {
        float currentAttack = entityData.GetAttributeValue<AttackAttr>();
        //current attack attribute value could be something like this..
        //
        // 5 initial points of the attribute, +5 points sword, -2 points ring = 8 total attack
        //
    }
}
