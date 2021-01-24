using UnityEngine;

public class PlayerImprovedController : MonoBehaviour {

    [SerializeField] private BoxCollider BoxCollider;
    [SerializeField] private Rigidbody Rigidbody;
    
    public enum CharacterTypes
    {
        MAGE,
        WARRIOR,
        ARCHER,
        CLERIC,
        ROGUE
    }

    [SerializeField] private CharacterTypes characterType = CharacterTypes.ARCHER;

    [SerializeField] private int defense;
    [SerializeField] private int attack;
    [SerializeField] private int magic;
    [SerializeField] private int agility;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int health;

    private int _initialDefenseValue = 0;
    private int _initialAttackValue = 0;
    private int _initialMagicValue = 0;
    //more variables to keep track of all initial parameters



    

    void Start()
    {
        _initialDefenseValue = defense;
        _initialAttackValue = attack;
        _initialMagicValue = magic;
    }




    public void Attack(int someValue)
    {
        attack = _initialAttackValue;
        magic = _initialMagicValue;
        
        switch (characterType)
        {
            case CharacterTypes.MAGE:
                magic += 2; //because it's a mage we give 2 extra points for attack, plus the attack itself
                magic += someValue;
                break;
            
            case CharacterTypes.ROGUE:
                break;
            
            case CharacterTypes.ARCHER:
                break;
            
            case CharacterTypes.CLERIC:
                break;
            
            case CharacterTypes.WARRIOR:
                break;
                
            default:
                attack += 2;
                attack += someValue;
                break;
        }
    }

    public void Defense(int someValue)
    {
        switch (characterType)
        {
            case CharacterTypes.MAGE:
                magic += 2;
                break;
            
            case CharacterTypes.ROGUE:
                break;
            
            case CharacterTypes.ARCHER:
                break;
            
            case CharacterTypes.CLERIC:
                break;
            
            case CharacterTypes.WARRIOR:
                break;
                
            default:
                attack += 2;
                break;
        }
        
        defense += someValue;
    }

    public void GotHit(int damageValue)
    {
        float defenseForAttack = defense;

        switch (characterType)
        {
            case CharacterTypes.MAGE:
                defenseForAttack *= 0.5f;
                break;
            
            case CharacterTypes.ROGUE:
                break;
            
            case CharacterTypes.ARCHER:
                break;
            
            case CharacterTypes.CLERIC:
                break;
            
            case CharacterTypes.WARRIOR:
                break;
        }
        
        damageValue -= (int)defenseForAttack;

        ApplyDamage(damageValue);
    }

    private void ApplyDamage(int value)
    {
        //do something with your health, animations, sounds, etc, etc
    }
    
}
