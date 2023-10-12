using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class APIConnectionController : MonoBehaviour
{

    public delegate void OnPokemonSpawned();
    public static OnPokemonSpawned onPokemonSpawned;

    private string URL = "https://pokeapi.co/api/v2/";
    private string pokemonName;
    private Pokemon pokemon;

    public Image img = null;
    private Texture2D downloadedTexture = null;

    
    private void OnEnable()
    {
        EventsManager.onPokemonSpawned += NewPokemon;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        EventsManager.onPokemonSpawned -= NewPokemon;
    }

    public Pokemon NewPokemon(){
        ShadowPokemon();
        int number = GetRandomNumber();
        StartCoroutine(GetPokemon(number));
        StartCoroutine(GetPokemonImage(number));
        pokemon = new Pokemon(pokemonName, img);
        return pokemon;
    }

    private int GetRandomNumber(){
        return UnityEngine.Random.Range(1, 152);
    }

    IEnumerator GetPokemon(int number)
    {
        using(UnityWebRequest request = UnityWebRequest.Get(URL+"pokemon-species/"+ number.ToString())){
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError){
                Debug.Log(request.error);
            }else{
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode data = SimpleJSON.JSON.Parse(json);
                //Pokemon.SetPokemonName(data["name"]);
                pokemon.SetPokemonName(data["name"]);
            }
        }
    }

    IEnumerator GetPokemonImage(int number)
    {
        using(UnityWebRequest request = UnityWebRequest.Get(URL+"pokemon/"+ number.ToString())){
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError){
                Debug.Log(request.error);
            }else{
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode data = SimpleJSON.JSON.Parse(json);

                string imgUrl = data["sprites"]["front_default"];

                StartCoroutine(GetPokemonTexture(imgUrl));

            }
        }
    }

    IEnumerator GetPokemonTexture(string imgUrl)
    {
        using(UnityWebRequest request = UnityWebRequestTexture.GetTexture(imgUrl)){
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError){
                Debug.Log(request.error);
            }else{
                if(downloadedTexture != null){
                    Destroy(downloadedTexture);
                }

                downloadedTexture = DownloadHandlerTexture.GetContent(request) as Texture2D;

                request.Dispose();

                img.GetComponent<Image>().sprite = Sprite.Create(downloadedTexture, new Rect(0, 0, downloadedTexture.width, downloadedTexture.height), new Vector2(0, 0));
            }
        }
    }

    private void ShadowPokemon(){
        img.color = new Color32(0,0,0,255);
    }

}
