using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager manager;
    private int _loadedLevel;
    
	// Use this for initialization
	void Start () {
        manager = this;
    }
    public void loadLevel(int id)
    {
        _loadedLevel = id;
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += onSceneLoaded;

    }
    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 1)
        {
            _loadedLevel = -1;
        }
        else // Esto significa que hemos cargado la escena con indice 1, que es la de juego
        {
            var a = scene.GetRootGameObjects();
            GameObject z = null;
            foreach (var gO in a)
                if (gO.GetComponent<LevelManager>() != null)
                    z = gO;

            if (z != null)
                z.GetComponent<LevelManager>().buildLevel(_loadedLevel);
            else
                Debug.Log("cago en cristo");

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
