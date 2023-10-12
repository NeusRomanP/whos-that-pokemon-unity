using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PokemonGuesser : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI pokemonText;
    public TMP_InputField input;
    public APIConnectionController apiConnectionController;

    private Pokemon pokemon = null;

    private string pokemonName = null;

    // Start is called before the first frame update
    void Start()
    {
        CallNewPokemon();
    }

    // Update is called once per frame
    void Update()
    {
        pokemonName = pokemon.GetPokemonName();
    }

    public void GuessPokemon(){
        string inputName = input.text.ToString().Replace("_", " ").Replace("-", " ").Replace("'", "").ToUpper();
        string parsedPokemonName = pokemonName.Replace("_", " ").Replace("-", " ").Replace("'", "").ToUpper();

        if(String.Equals(inputName, parsedPokemonName)){
            pokemonText.text = parsedPokemonName;
            UnshadowPokemon();
        }
    }

    public void CallNewPokemon(){
        pokemon = EventsManager.onPokemonSpawned(); 
        pokemonText.text = "Who's that Pokemon?";
        input.text = "";
    }

    private void UnshadowPokemon(){
        pokemon.GetPokemonImg().color = new Color32(255,255,225,255);
    }
    
}
