using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TMP_Text healthDisplay;
    public TMP_Text scoreDisplay, gameOverScoreDisplay;
    public TMP_Text weaponNameDisplay;
    public TMP_Text ammoDisplay;

    public GameObject gameOverPanel;


    public void SetDisplays(string playerHealth, string playerScore, string weaponName, string weaponAmmo)
    {
        healthDisplay.text = playerHealth;
        scoreDisplay.text = playerScore;
        weaponNameDisplay.text = weaponName;
        ammoDisplay.text = weaponAmmo ==""?"":"x" + weaponAmmo;
    }

    public void ShowGameOver(string playerScore)
    {
        gameOverScoreDisplay.text = playerScore;
        gameOverPanel.SetActive(true);
    }

    public void ResetScene()
    {
        GameManager.Instance.ResetScene();
    }
}
