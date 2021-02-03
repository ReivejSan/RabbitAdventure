using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Souls : MonoBehaviour
{
    public int souls;

    public playerCombat player;
    Text soulsText;
    
    // Start is called before the first frame update
    void Start()
    {
        soulsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        souls = PlayerPrefs.GetInt("Souls");
        soulsText.text = "" + souls;
    }
}
