using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerGame : ManagerSingleton<ManagerGame> 
{
    [SerializeField] private GameObject _playerPrefab;
    private GameObject _actualPlayer;

    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _hungerBar;
    [SerializeField] private GameObject _mainCamera;

    [SerializeField] private Image _biteBar;
    [SerializeField] private TextMeshProUGUI _biteText;
    [SerializeField] private Image _dashBar;
    [SerializeField] private TextMeshProUGUI _dashText;
    [SerializeField] private Image _preasureBar;
    [SerializeField] private TextMeshProUGUI _preasureText;

    [SerializeField] private GameObject _inGamePanel;
    [SerializeField] private GameObject _inPausePanel;
    [SerializeField] private GameObject _inLosePanel;
    [SerializeField] private TextMeshProUGUI _textReasonOfLose;
    [SerializeField] private TextMeshProUGUI _textTotalScore;




    private void Start()
    {
        _inGamePanel.SetActive(true);
        _inPausePanel.SetActive(false);
        _inLosePanel.SetActive(false);
        CreatePlayer();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void CreatePlayer()
    {
        GameObject go = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        _actualPlayer = go;
        go.GetComponent<Wolf>().HealthBar = _healthBar;
        go.GetComponent<Wolf>().HungerBar = _hungerBar;
        _mainCamera.GetComponent<CameraMovement>().PlayerTransform = go.transform;
        
        go.GetComponentInChildren<BiteSkill>().BiteBar = _biteBar;
        go.GetComponentInChildren<BiteSkill>().BiteText = _biteText;

        go.GetComponentInChildren<DashSkill>().DashBar = _dashBar;
        go.GetComponentInChildren<DashSkill>().DashText = _dashText;

        go.GetComponentInChildren<PreasureSkill>().PreasureBar = _preasureBar;
        go.GetComponentInChildren<PreasureSkill>().PreasureText = _preasureText;

    }
    public void EndGame(string reason, int totalScore)
    {
        Destroy(_actualPlayer);
        _inGamePanel.SetActive(false);
        _inPausePanel.SetActive(false);
        _inLosePanel.SetActive(true);

        _textReasonOfLose.text = "Reason of lose: " + reason;
        _textTotalScore.text = "Total score: " + totalScore;


    }

    public void PauseGame()
    {

        if (_inGamePanel.activeSelf)
        {
            Time.timeScale = 0;
            _actualPlayer.SetActive(false);
            _inGamePanel.SetActive(false);
            _inPausePanel.SetActive(true);
        }
        else
        {
            buttonResume();
        }
        
    }

    public void buttonResume()
    {
        Time.timeScale = 1;
        _actualPlayer.SetActive(true);
        _inGamePanel.SetActive(true);
        _inPausePanel.SetActive(false);
    }

    public void buttonRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        _inGamePanel.SetActive(true);
        _inPausePanel.SetActive(false);
    }

    public void buttonBack()
    {
        SceneManager.LoadScene(0);

    }
}
