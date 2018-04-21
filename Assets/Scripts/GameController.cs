using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public enum TurnStates { Player, Enemies };

public class GameController : MonoBehaviour {

    [Header("Timer")]
    public TurnStates state = TurnStates.Player;
    public float TurnTimer;
    public float PauseTime;
    private double timer;
    private bool gameOver = false;

    [Header("UI")]
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI CountDownText;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    timer = 0;
                }

                TimerText.SetText("Time: " + Math.Round(timer, 4));
            }
            else
            {
                StartCoroutine(Pause());
                state = (state == TurnStates.Player) ? TurnStates.Enemies : TurnStates.Player;
                timer = TurnTimer;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            CountDownText.SetText("GameOver");
        }
    }

    IEnumerator Pause()
    {
        Time.timeScale = 0;

        if (state == TurnStates.Enemies)
        {
            for (float i = PauseTime; i > 0; i--)
            {
                CountDownText.SetText(i.ToString());
                yield return new WaitForSecondsRealtime(1);
            }
            CountDownText.SetText("GO");
        }
        else
        {
            CountDownText.SetText("Enemy Turn");
            yield return new WaitForSecondsRealtime(1);
        }

        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(1);
        CountDownText.SetText("");
    }

    public TurnStates GetTurnState()
    {
        return state;
    }

    public void GameOver()
    {
        gameOver = true;
    }

}
