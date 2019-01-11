using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class Monetizer : MonoBehaviour
{

    // Variables for monetizing
    static string gameID = "2999979";
    static bool testMode = false;
    string plcementID = "rewardedVideo";

    //GUI Variables
    public GameObject monetizePanel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting monetization");
        try
        {
         Monetization.Initialize(gameID, testMode);
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void ShowAD()
    {
        StartCoroutine(ShowAdWhenReady());
    }
    private IEnumerator ShowAdWhenReady()
    {
        yield return null;
    }
    void AdFinished (ShowResult res)
    {
        if (res == ShowResult.Finished) //reward player;
            monetizePanel.SetActive(false);
    }
    public void CloseMonetizePanel()
    {
        monetizePanel.SetActive(false);
    }

    public void ShowMonetizePanel()
    {
        monetizePanel.SetActive(true);
    }

}
