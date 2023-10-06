using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class APIConnectionController : MonoBehaviour
{

    private string URL = "https://pokeapi.co/api/v2/";

    public Image img = null;
    private Texture2D downloadedTexture = null;

    // Start is called before the first frame update
    void Start()
    {
        Pokemon.SetPokemonImg(img);
        NewPokemon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewPokemon(){
        ShadowPokemon();
        int number = GetRandomNumber();
        StartCoroutine(GetPokemon(number));
        StartCoroutine(GetPokemonImage(number));
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
                Pokemon.SetPokemonName(data["name"]);
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

                Pokemon.GetPokemonImg().GetComponent<Image>().sprite = Sprite.Create(downloadedTexture, new Rect(0, 0, downloadedTexture.width, downloadedTexture.height), new Vector2(0, 0));
            }
        }
    }

    private void ShadowPokemon(){
        Pokemon.GetPokemonImg().color = new Color32(0,0,0,255);
    }

}
