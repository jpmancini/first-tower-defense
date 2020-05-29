using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //Is temp will be replaced later
    [SerializeField]
    private GameObject towerPrefab;

    //this is how vs auto encapsulates
    public GameObject TowerPrefab 
    { 
        get => towerPrefab; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
