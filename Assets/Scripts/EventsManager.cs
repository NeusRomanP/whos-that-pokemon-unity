using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager
{
    public delegate Pokemon OnPokemonSpawned();
    public static OnPokemonSpawned onPokemonSpawned;
}
