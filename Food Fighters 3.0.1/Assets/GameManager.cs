using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int _TakeDowns;
    public TMP_Text _TakeDownsText;
    public int WinNumber;
    void Start()
    {
        _TakeDowns = 20;
        
    }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
        // Update is called once per frame
        void Update()
    {
        _TakeDownsText.text = "Enemies Defeated: " + _TakeDowns;

        if (_TakeDowns == WinNumber)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("WinScreen");

        }
    }
}
