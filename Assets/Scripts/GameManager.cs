using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager manager;
    private int _loadedLevel;
    private gameProgress gProgress;

    // Use this for initialization
    private void Awake()
    {
        manager = this;
        SceneManager.sceneLoaded += onSceneLoaded;
    }
    void Start () {
        gProgress = ProgressManager.LoadGameProgress();
    }
    public void loadLevel(int id)
    {
        ProgressManager.SaveProgress(gProgress);
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
    public GameManager.gameProgress getGameProgress()
    {
        if (gProgress == null) gProgress = ProgressManager.LoadGameProgress();
        return gProgress;
    }

    public void modifyLevelProgress(gameProgress.levelProgress lProgress)
    {
        getGameProgress().Progresses[lProgress.levelNumber] = lProgress;
    }

	void Update () {

	}


    private void OnApplicationQuit() // aquí es donde deberíamos cerrar y codificar el progreso
    {
        ProgressManager.SaveProgress(gProgress);
    }

    /// <summary>
    /// Clase Utilizada para guardar el progreso del juego.
    /// Dentro de sí tiene una lista de levelProgress, con el estado de cada nivel.
    /// </summary>
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


        /// <summary>
        /// Serialzes a gameProgress instance
        /// </summary>
        /// <param name="toconvert">object to serialize</param>
        /// <returns>A string containing the Json serialized class</returns>
        public static string ToJson(ref gameProgress toconvert)
        {
            return JsonUtility.ToJson(toconvert);
        }
        /// <summary>
        /// Constructs a gameProgress object from a string formatted in json
        /// </summary>
        /// <param name="json">String containing the json-serialized object</param>
        /// <returns>gameProgress instance with the data in json string</returns>
        public static gameProgress FromJson(string json)
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
