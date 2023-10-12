using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Pokemon
{
    private static string pokemonName = null;
    private static Image pokemonImg = null;

    public Pokemon(string name, Image image){
        pokemonName = name;
        pokemonImg = image;
    }

    public string GetPokemonName(){
        return pokemonName;
    }

    public void SetPokemonName(string name){
        pokemonName = name;
    }

    public Image GetPokemonImg(){
        return pokemonImg;
    }

    public void SetPokemonImg(Image img){
        pokemonImg = img;
    }
}
