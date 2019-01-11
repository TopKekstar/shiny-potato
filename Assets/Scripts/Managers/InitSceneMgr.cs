using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// For managing the load scene
/// </summary>
public class InitSceneMgr : MonoBehaviour
{
    /// <summary>
    /// the image that show up in the start
    /// </summary>
    public UnityEngine.UI.Image splash;
    /// <summary>
    /// the awesome audio that plays on the start
    /// </summary>
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(LoadGame());
    }

    /// <summary>
    /// coroutine  for the splash screens
    /// </summary>
    /// <returns></returns>
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
        while (audio.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(1);
    }
}
