using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSceneMgr : MonoBehaviour
{
    public UnityEngine.UI.Image splash;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame()
    {
        if (splash != null)
        {
            Color c = new Color(1.0f, 1.0f, 1.0f,0.0f);
            splash.color = c;
            while (c.a < 1.0f)
            {
                c.a += 0.01f;
                splash.color = c;
                yield return new WaitForEndOfFrame();
            }
        }
        
        SceneManager.LoadScene(1);
    }
}
