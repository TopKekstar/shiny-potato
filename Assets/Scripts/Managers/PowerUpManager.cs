using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    BoardManager boardManager;
    public void Init(BoardManager bm)
    {
        boardManager = bm;
    }

    private void DoEarthquake()
    {
        boardManager.Earthquake();
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
