using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wolf : MonoBehaviour, IDamageable
{
    [SerializeField] private int MaxHealth;
    [SerializeField] private int MaxHunger;
    [SerializeField] private float HungerDelaySec;
    [SerializeField] private int HungerDecreaseValue;

    public Image HealthBar;
    public Image HungerBar;

    private bool _isDecreasing;
    WaitForSeconds _waitForSecondsHungerDelay;


    private int _health;
    public int Health { get { return _health; } }
    
    private int _hunger;
    public int Hunger {  get { return _hunger; } }

    private bool _isAlive;
    public bool IsAlive {  get { return _isAlive; } }

    private int _hungerForKill;
    public int HungerForKill { get { return _hungerForKill; } }

    private int _scoreForKill;
    public int ScoreForKill { get { return _scoreForKill; } }

   

    private void Start()
    {
        _health = MaxHealth;
        _hunger = MaxHunger;

        _waitForSecondsHungerDelay = new WaitForSeconds(HungerDelaySec);
        _isDecreasing = true;
        _isAlive = true;
    }
    private void Update()
    {
        DecreaseHunger();

        
    }


    public void TakeDamage(int deltaDamage)
    {
        if (deltaDamage < _health)
        {
            _health -= deltaDamage;
        }
        else
        {
            _health = 0;
            ManagerGame.Instance.EndGame("Hunter shoot you!", ManagerScore.Instance.Score);
        }
        HealthBar.fillAmount =  (float)_health / MaxHealth;

    }

    public void TakeHeal(int heal)
    {
        if (heal < _health)
        {
            _health += heal;
        }
        else
        {
            _health = MaxHealth;
        }
        HealthBar.fillAmount = (float)_health / MaxHealth;
    }

    public void Stun(float sec)
    {
        throw new System.NotImplementedException();
    }

    public void IncreaseHunger(int deltaHunger)
    {

        if (MaxHunger > deltaHunger + _hunger)
        {
            _hunger += deltaHunger;
        }
        else
        {
            _hunger = MaxHunger;
        }
        HungerBar.fillAmount = (float)_hunger / MaxHunger;

        //Debug.Log("hunger: " + _hunger);


    }

    void DecreaseHunger()
    {
        if (_isDecreasing && _isAlive)
        {
            StartCoroutine(HungerCor());
            if (_hunger < HungerDecreaseValue)
            {
                _isAlive = false;
                ManagerGame.Instance.EndGame("You died of hunger!", ManagerScore.Instance.Score);
            }
            _hunger -= HungerDecreaseValue;
            //Debug.Log("hunger: " + _hunger);
            HungerBar.fillAmount = (float)_hunger / MaxHunger;


        }
    }
    IEnumerator HungerCor()
    {
        _isDecreasing = false;

        yield return _waitForSecondsHungerDelay;

        _isDecreasing = true;

    }
}
