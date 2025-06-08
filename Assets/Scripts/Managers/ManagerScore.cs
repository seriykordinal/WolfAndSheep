using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ManagerScore : ManagerSingleton<ManagerScore>
{
    private int _score;
    public int Score {  get { return _score; } }
    private int _combo;
    public Action<int> OnAddScore;

    //[SerializeField] private int _maxCombo;
    //[SerializeField] private float _comboDelaySec;
    //private WaitForSeconds _waitForSecComboDelay;

    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI ComboText;


    private void Start()
    {
        _combo = 1;
        _score = 0;
        //_waitForSecComboDelay = new WaitForSeconds(_comboDelaySec);
    }
    private void OnEnable()
    {
        OnAddScore += IncreaseScore;
    }
    private void OnDisable()
    {
        OnAddScore -= IncreaseScore;
    }

    public void IncreaseScore(int deltaScore)
    {
        //if (_combo < _maxCombo)
        //{
        //    _combo++;
        //}

        _score += (deltaScore * _combo);
        ScoreText.text = ("Score: " + _score);
        ComboText.text = ("Combo: " + _combo + "x");


        //StartCoroutine(DelayToResetCombo());
    }

    //IEnumerator DelayToResetCombo()
    //{
    //    yield return _waitForSecComboDelay;
    //    _combo = 0;
    //}
}
