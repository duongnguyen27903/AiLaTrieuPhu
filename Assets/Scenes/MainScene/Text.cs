using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Text : MonoBehaviour
{
    public TextMeshProUGUI text ;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Hello World";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
