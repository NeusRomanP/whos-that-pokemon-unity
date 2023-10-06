using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public static class Pokemon
{
    private static string pokemonName = null;
    private static Image pokemonImg = null;

    public static string GetPokemonName(){
        return pokemonName;
    }

    public static void SetPokemonName(string name){
        pokemonName = name;
    }

    public static Image GetPokemonImg(){
        return pokemonImg;
    }

    public static void SetPokemonImg(Image img){
        pokemonImg = img;
    }
}
