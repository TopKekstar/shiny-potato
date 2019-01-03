using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager manager;
    private int _loadedLevel;

    // Use this for initialization
    private void Awake()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }
    void Start () {
        manager = this;
    }
    public void loadLevel(int id)
    {
        _loadedLevel = id;
        SceneManager.LoadScene(1);
      

    }
    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            _loadedLevel = -1;
           
        }
        else if (scene.buildIndex == 1) // Esto significa que hemos cargado la escena con indice 1, que es la de juego
        {
            var a = scene.GetRootGameObjects();
            GameObject z = null;
            foreach (var gO in a)
                if (gO.GetComponent<LevelManager>() != null)
                    z = gO;

            z.GetComponent<LevelManager>().buildLevel(_loadedLevel);

        }

    }
	// Update is called once per frame
	void Update () {
		
	}
    private void OnApplicationQuit() // aquí es donde deberíamos cerrar y codificar el progreso
    {
        
    }


    [System.Serializable]
    public class gameProgress
    {
        [System.Serializable]
        public struct levelProgress
        {

            public bool complete;
            public bool unlocked;
            public int stars;
            public int score;
            public short levelNumber;
        }

        public List<levelProgress> _progresses;
        public int nLevels;



        public static string toJson(ref gameProgress g)
        {
            return JsonUtility.ToJson(g);
        }
        public static gameProgress fromJson(string json)
        {
            return JsonUtility.FromJson<gameProgress>(json);
        }

        

        public gameProgress(int nLevels)
        {
            this.NLevels = nLevels;
        }
        public List<levelProgress> Progresses
        {
            get
            {
                return _progresses;
            }

            set
            {
                _progresses = value;
            }
        }

        public int NLevels
        {
            get
            {
                return nLevels;
            }

            set
            {
                nLevels = value;
            }
        }

    }
}
