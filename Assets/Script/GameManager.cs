using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    // Start is called before the first frame update
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (_instance == null)
                {
                    Debug.Log("There is no AppManager");
                }
            }
            return _instance;
        }
    }
    public bool gameStart = false;

}
