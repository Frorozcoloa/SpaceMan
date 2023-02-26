using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameState currentGameState = GameState.Menu;
    public static GameManager sharedInstance;
    private PlayerController cotroller;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        cotroller = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && currentGameState != GameState.Playing) {
            StartGame();
        }
    }
    public void StartGame()
    {
        cotroller.StartGame();
        currentGameState = GameState.Playing;

    }
    public void GameOver()
    {
        currentGameState = GameState.GameOver;
    }
    public void BackToMenu()
    {
        currentGameState= GameState.Menu;
    }
    
}
