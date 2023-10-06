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

    private string pokemonName = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pokemonName = Pokemon.GetPokemonName();
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
        apiConnectionController.NewPokemon();
        pokemonText.text = "Who's that Pokemon?";
        input.text = "";
    }

    private void UnshadowPokemon(){
        Pokemon.GetPokemonImg().color = new Color32(255,255,225,255);
    }
    
}
