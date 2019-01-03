//Json Serialization Test Script


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Prueba : MonoBehaviour {

	// Use this for initialization
	void Start () {

        TestJson json = new TestJson();
        json._a = 1;
        json._b = 2;
        json._c = 3;
        json._z.X = 4;
        json._l = new List<TestJson.z>();
        for (int i = 0; i < 5; i++) {
            var z = new TestJson.z();
            z.X = i;
            z.mae = "WOLO" + i.ToString();
            json._l.Add(z);
         }

        string tuvi = TestJson.toJson(json);

        TestJson test2 = TestJson.fromJson(tuvi);

        Debug.Log(tuvi);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
[System.Serializable]
public class TestJson
{
    public int _a;
    public int _b;
    public int _c;
    public z _z;
    public List<z> _l;
    [System.Serializable]
    public struct z
    {
       public int X;
        public string mae;
    }

    public static string toJson (TestJson toconvert)
    {
        return UnityEngine.JsonUtility.ToJson(toconvert);
    }
    public static TestJson fromJson(string json)
    {
        return UnityEngine.JsonUtility.FromJson<TestJson>(json);
    }
}
