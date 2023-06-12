using System;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
    public static ResourceManager instance;

    // TODO: Better Access
    public TMP_Text WoodCounter;
    public TMP_Text RockCounter;

    public int Wood { get; private set; }
    public int Rock { get; private set; }

    private void Start() {
        instance = this;
    }

    public void AddWood(int amount) {
        Wood += amount;
        WoodCounter.text = Wood.ToString();
    }

    public void AddRock(int amount) {
        Rock += amount;
        RockCounter.text = Rock.ToString();
    }
}
