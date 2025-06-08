using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Victim : Flyweight, IDamageable, IVictim
{
    private VictimDefinition _victimDefinition => (VictimDefinition)Definition;
    public VictimDefinition VictimDefinition { get { return _victimDefinition; } }

    private bool _canMove;


    private VictimSpawner _victimSpawner;
    
    private Rigidbody2D _rb;
    public Rigidbody2D Rb { get { return _rb; } set { _rb = value; } }
    
    //private SpriteRenderer _stunRenderer;
    //public SpriteRenderer StunRender { get { return _stunRenderer; } set { _stunRenderer = value; } }



    private static int _counter = 0;
    private int _id;
    public int Id { get { return _id; } }

    private int _health;
    public int Health { get { return _health; } }

    private bool _isStunned;

    private int _hungerForKill;
    public int HungerForKill { get { return _hungerForKill; } set { _hungerForKill = value; } }
    
    private int _scoreForKill;
    public int ScoreForKill { get { return _scoreForKill; } set { _scoreForKill = value; } }

    private int _healthForKill;
    public int HealthForKill { get { return _healthForKill; } set { _healthForKill = value; } }



    private void Start()
    {
        //_stunRenderer.color = new Color(0, 0, 0, 0);
        _canMove = true;

    }

    private void Update()
    {
        if (_isStunned) return;

        GoToGrases();
    }

    private void GoToGrases()
    {
        if (!_canMove) return;
        if (_victimSpawner.SpawndeGrases.Count <= 0) return;

        StartCoroutine(MoveToGrassCor());

    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("def dam" + Definition + " vi dam " + VictimDefinition);
        if (_health > damage)
        {
            _health -= damage;
        }
        else
        {
            _health = 0;

            DestroyVictim();
        }
    }
    public void Stun(float sec)
    {
        //_stunRenderer.color = new Color(0, 0, 0, 0);

        StartCoroutine(DelayStunCor(sec));
    }

    private void DestroyVictim()
    {
        _victimSpawner.RemoveVictimById(_id);
        Destroy(gameObject);
    }

    public void TakeHeal(int heal)
    {
        //со временем хил
    }

    public void Initialize(VictimSpawner victimSpawner, GameObject visual, int maxHealth, float scale)
    {
        _victimSpawner = victimSpawner;
        gameObject.transform.localScale *= scale;
        Instantiate(visual, gameObject.transform);
        _health = maxHealth;
        _id = _counter++;





        //Debug.Log("def ini " + Definition + " vi ini " + VictimDefinition);

    }

    IEnumerator MoveToGrassCor()
    {

        System.Random random = new System.Random();
        int curIndex = random.Next(0, _victimSpawner.SpawndeGrases.Count);

        //Debug.Log(!_victimSpawner.SpawndeGrases[curIndex].GetComponent<Grass>().IsBusy);
        if (!_victimSpawner.SpawndeGrases[curIndex].GetComponent<Grass>().IsBusy)
        {
            _canMove = false;
            _victimSpawner.SpawndeGrases[curIndex].GetComponent<Grass>().IsBusy = true;

            while (Mathf.Abs(_victimSpawner.SpawndeGrases[curIndex].transform.position.x - transform.position.x) > 1f || Mathf.Abs(_victimSpawner.SpawndeGrases[curIndex].transform.position.y - transform.position.y) > 1f)
            {
                //Debug.Log("move");


                Vector3 grassPosition = _victimSpawner.SpawndeGrases[curIndex].transform.position - transform.position;
                float rotateZ = Mathf.Atan2(grassPosition.y, grassPosition.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotateZ - 90f);
                _rb.AddForce((_victimSpawner.SpawndeGrases[curIndex].transform.position - transform.position).normalized * _victimDefinition.VictimSpeed * Time.deltaTime);

                yield return null;
                //Debug.Log("cur" + curIndex);

            }

            StartCoroutine(DelayMoveToGrassCor(curIndex));
            //_canMove = true;
        }





    }
    IEnumerator DelayMoveToGrassCor(int curIndex)
    {
        //Debug.Log("start");

        yield return _victimDefinition.WaitForSecondsMoveToGrassDelay;
        //Debug.Log("end");

        _canMove = true;
        _victimSpawner.SpawndeGrases[curIndex].GetComponent<Grass>().IsBusy = false;



    }

    IEnumerator DelayStunCor(float sec)
    {
        Debug.Log("Stun");
        _isStunned = true;
        yield return new WaitForSeconds(sec);
        _isStunned = false;
        //_stunRenderer.color = new Color(0,0, 0, 1);
        Debug.Log("not Stun");

    }

}
