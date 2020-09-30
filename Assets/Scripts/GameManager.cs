using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ScoreBoard, OrignMask, NameBoard;
    GameObject ScoreSave;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenScoreBoard()
    {
        OrignMask.SetActive(false);
        ScoreBoard.SetActive(true);
    }
    public void CloseScoreBoard()
    {
        OrignMask.SetActive(true);
        ScoreBoard.SetActive(false);
        NameBoard.SetActive(false);
    }
    public void EnterName()
    {
        NameBoard.SetActive(true);
        OrignMask.SetActive(false);
    }
    public void SetName()
    {
        GameObject.FindGameObjectWithTag("NameStore").GetComponent<Name_Store>().Name = GameObject.FindGameObjectWithTag("Finish").GetComponent<Text>().text;
        StartGame();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
