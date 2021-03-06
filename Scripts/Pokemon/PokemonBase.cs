using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
public class PokemonBase : ScriptableObject
{
    
    [SerializeField]  new string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;
    [SerializeField] PokemonType type;
   

    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defence;
    [SerializeField] int spAttack;
    [SerializeField] int spDefence;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves;
    

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public PokemonType Type
    {
        get { return type; }
    }
    public PokemonType Type1
    {
        get { return type1; }
    }

    public PokemonType Type2
    {
        get { return type2; }
    }
    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }
    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }
    public int Attack
    {
        get { return attack; }
    }
    public int Defence
    {
        get { return defence; }
    }

    public int SPAttack
    {
        get { return spAttack; }
    }
    public int SpDefence
    {
        get { return spDefence; }
    }
    public int Speed
    {
        get { return speed; }
    }
   
    
    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }
    [System.Serializable]

    public class LearnableMove
    {
        [SerializeField] MoveBase moveBase;
        [SerializeField] int level;

        public MoveBase Base
        {
            get { return moveBase; }
        }

        public int Level
        {
            get
            {
                return level;
            }
        }
    }

    public enum PokemonType
    {
        None,
        normal,
        Fire,
        Water,
        Electric,
        Grass,
        Ice,
        Fighting,
        Poison,
        Ground,
        Flying,
        Psychic,
        Bug,
        Rock,
        Ghost,
        Dragon

    }



    public enum Stat
    {
        Attack,
        Defense,
        SPAttack,
        SpDefence,
        Speed
    }

    public class TypeChart
    {
       static float[][] chart =
        {
            //                 
            /*Nor*/new float[] {1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f},
            /*Fire*/ new float[]{1f, 0,5f,0,5f, 1f,  2f,  2f,  1f,  1f},
            /*WAT*/new float[] {1f,  2f,  0.5f, 2f, 0.5f, 1f,  1f,  1f,},
            /*ELE*/new float[] {1f,  1f,  2f,  0.5f,0.5f, 2f,   1f,  1f},
            /*GRS*/new float[] {1f,  0.5f,2f,  2f,  0.5f, 1f,   1f, 0.5f},
            /*POI*/new float[] {1f,  1f,  1f,  1f,  1f,   1f,   1f, 1f}
        };


       public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
       {
           if (attackType == PokemonType.None || defenseType == PokemonType.None)
               return 1;

           int row = (int) attackType - 1;
           int col = (int) defenseType - 1;

           return chart[row][col];
       }
    }
}