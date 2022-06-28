using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Pokemon 
{
    


   [SerializeField] PokemonBase _base;
   [SerializeField] int level;

   public PokemonBase Base 
   {
       get { return _base; }
   }

   public int Level
    {

        get
        {
            return level;
        }
    
   }
    public int Hp { get; set; }
    public List<Move> Moves { get; set; }
    public Dictionary<PokemonBase.Stat,int>Stats { get; private set; }
    public Dictionary<PokemonBase.Stat, int> StatBoosts { get; private set; }

    public Queue<string> StatusChanges { get; private set; } = new Queue<string>();
      
    public void Init()
    {



        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves)
        {

            if (move.Level <= Level)
                Moves.Add(new Move(move.Base));

            if (Moves.Count >= 4)
                break;
        }
        CalculateStats();
        Hp = MaxHp;


        ResetStatBoost();
    }
        void CalculateStats()
        {
            Stats = new Dictionary<PokemonBase.Stat, int>();
            Stats.Add(PokemonBase.Stat.Attack, Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5);
            Stats.Add(PokemonBase.Stat.Defense, Mathf.FloorToInt((Base.Defence * Level) / 100f) + 5);
            Stats.Add(PokemonBase.Stat.SPAttack, Mathf.FloorToInt((Base.SPAttack * Level) / 100f) + 5);
            Stats.Add(PokemonBase.Stat.SpDefence, Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5);
            Stats.Add(PokemonBase.Stat.Speed, Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5);

            MaxHp = Mathf.FloorToInt((Base.Speed * Level) / 100f + 10);
        }

    void ResetStatBoost()
    {
        StatBoosts = new Dictionary<PokemonBase.Stat, int>()
        {

            { PokemonBase.Stat.Attack, 0 },
            { PokemonBase.Stat.Defense, 0 },
            { PokemonBase.Stat.SPAttack, 0 },
            { PokemonBase.Stat.SpDefence, 0 },
            { PokemonBase.Stat.Speed, 0 },

        };
    }
        int GetStat(PokemonBase.Stat stat)
        {
            int statVal = Stats[stat];

           
        
            int boost = StatBoosts[stat]; 
            var boostValues = new float[] { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f };

            if (boost >= 0)
                statVal = Mathf.FloorToInt(statVal * boostValues[boost]);

            else
                statVal = Mathf.FloorToInt(statVal / boostValues[-boost]);
            return statVal;

        }
    public void ApplyBoosts(List<StatBoost> statBoosts)
    {
        foreach (var  statBoost in statBoosts)
        {
            var stat = statBoost.stat;
            var boost = statBoost.boost;

            StatBoosts[stat] = Mathf.Clamp(StatBoosts[stat] + boost, -6, 6);

            if (boost > 0)
                StatusChanges.Enqueue($"{Base.Name}'s{stat} rose!");
            else
                StatusChanges.Enqueue($"{Base.Name}'s {stat} fell!"); 
            Debug.Log($"{stat} has been boosted  to {StatBoosts[stat]}");
        }
    }
    
    public int Attack
    {
        get { return GetStat(PokemonBase.Stat.Attack); }
    }

    public int Defence
    {
        get { return GetStat(PokemonBase.Stat.Defense); } 
    }

    public int SpAttack
    {
        get { return GetStat(PokemonBase.Stat.SPAttack); }
    }

    public int SpDefence
    {
        get { return GetStat(PokemonBase.Stat.SpDefence); }
    }
    public int Speed
    {
        get { return GetStat(PokemonBase.Stat.Speed); }
    }
    public int MaxHp { get; private set; }
   

    public DamageDetails TakeDamage(Move move, Pokemon attacker)
    {

       
        float critical = 1f;
        if (Random.value * 100f <= 40f)
            critical = 2f;
        float type = PokemonBase.TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) *
                     PokemonBase.TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2);
        var damageDetails = new DamageDetails()
        {
            typeEffectinees = type,
            Critical = critical,
            Fainted = false
        };
        float attack = (move.Base.Category == MoveCategory.Special) ? attacker.SpAttack : attacker.Attack;                                                                
                                                                                                                                                                                       
        float defense = (move.Base.Category == MoveCategory.Special) ? SpDefence : Defence;
        float modifiers = Random.Range(0.85f, 1f) * type * critical;
        float a = (2 * attacker.Level + 10 / 250f);
        float d = a * move.Base.Power * ((float) attack / defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            damageDetails.Fainted = true;
        }


        return damageDetails;
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
    public void OnBattleOver()
    {
        ResetStatBoost();
    }
}

public class DamageDetails
{
    public bool Fainted { get; set; }

    public float Critical { get; set; }

    public float typeEffectinees { get; set; }
}
