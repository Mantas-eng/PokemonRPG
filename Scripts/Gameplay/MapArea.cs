using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    [SerializeField]  List<Pokemon> wildPokemons;

    public Pokemon GetRandomWildPokemon()
    {
         var wildpokemon = wildPokemons[Random.Range(0, wildPokemons.Count)];
         wildpokemon.Init();
         return wildpokemon;
    }
}
