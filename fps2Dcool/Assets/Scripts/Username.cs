using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Username : MonoBehaviour
{
    public TMP_InputField username;
    [SerializeField] Button Join;
    [SerializeField] Button Create;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(string.IsNullOrEmpty(username.text))
        {
            Join.interactable = false;
            Create.interactable = false;
        }else
        {
            Join.interactable = true;
            Create.interactable = true; 
        }
    }
}
