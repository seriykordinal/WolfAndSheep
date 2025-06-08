using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Flyweight, IDamageable, IHunter
{
    private HunterDefinition _hunterDefinition => (HunterDefinition)Definition;
    public HunterDefinition HunterDefinition { get { return _hunterDefinition; } }


    private HunterSpawner _hunterSpawner;
    
    private HunterVision _hunterVision;
    public HunterVision HunterVision { get { return _hunterVision; } set { _hunterVision = value; }}

    private List<PointForHunter> _pointsForHunter;

    private Rigidbody2D _rb;
    public Rigidbody2D Rb { get { return _rb; } set { _rb = value; } }

    private Gun _gun;
    public Gun Gun { get { return _gun; } set { _gun = value; } }



    public Action<GameObject> OnDetectingPlayer;
    public Action OnUndetectingPlayer;

    private GameObject _player;


    private bool _canMoveToPoint;
    private int _actualIndex = 0;
    private bool _canShoot;
    public bool _isCanSearch;

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

    private bool _isPlayerDetected;


    private void Start()
    {
        _isPlayerDetected = false;
        _canMoveToPoint = true;
        _canShoot = true;
        _isCanSearch = false;

    }

    private void Update()
    {
        if (_isStunned) return;
        if (_isCanSearch) return;


        GuardVictim();
    }

    public void PlayerDetected(GameObject player)
    {
        _isPlayerDetected = true;
        _player = player;



    }
    public void PlayerUndetected()
    {
        _isPlayerDetected = false;
        _player = null;

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

            DestroyHunter();
        }
    }
    public void Stun(float sec)
    {

        StartCoroutine(DelayStunCor(sec));
    }

    private void DestroyHunter()
    {
        OnDetectingPlayer -= PlayerDetected;
        OnUndetectingPlayer -= PlayerUndetected;
        _hunterSpawner.RemoveHunterById(_id);
        Destroy(gameObject);
    }

    public void TakeHeal(int heal)
    {

    }

    public void Initialize(HunterSpawner hunterSpawner, GameObject visual, int maxHealth, float scale)
    {
        _hunterSpawner = hunterSpawner;
        gameObject.transform.localScale *= scale;
        Instantiate(visual, gameObject.transform);
        _health = maxHealth;
        _id = _counter++;





        //Debug.Log("def ini " + Definition + " vi ini " + VictimDefinition);

    }



    private void GuardVictim()
    {

        if (!_isPlayerDetected)
        {
            if (!_canMoveToPoint) return;
            StartCoroutine(MoveToPointCor());
            

        }
        else
        {
            _canMoveToPoint = true;
            _hunterSpawner.PointsForHunter[_actualIndex].IsBusy = false;

            StartCoroutine(HuntPlayerCor());
        }
    }

   

    IEnumerator MoveToPointCor()
    {
        if (!_hunterSpawner.PointsForHunter[_actualIndex].IsBusy)
        {
            _canMoveToPoint = false;
            _hunterSpawner.PointsForHunter[_actualIndex].IsBusy = true;

            while (Mathf.Abs(_hunterSpawner.PointsForHunter[_actualIndex].transform.position.x - transform.position.x) > 1f || Mathf.Abs(_hunterSpawner.PointsForHunter[_actualIndex].transform.position.y - transform.position.y) > 1f)
            {
                //Debug.Log("move");


                Vector3 pointPosition = _hunterSpawner.PointsForHunter[_actualIndex].transform.position - transform.position;
                float rotateZ = Mathf.Atan2(pointPosition.y, pointPosition.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotateZ - 90f);
                _rb.AddForce((_hunterSpawner.PointsForHunter[_actualIndex].transform.position - transform.position).normalized * _hunterDefinition.HunterSpeed * Time.deltaTime);

                yield return null;

            }

            StartCoroutine(DelayMoveToPointCor());
        }

    }

    IEnumerator HuntPlayerCor()
    {
        while(_player != null && !_isStunned)
        {
            Vector3 playerPosition = _player.transform.position - transform.position;
            float rotateZ = Mathf.Atan2(playerPosition.y, playerPosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotateZ - 90f);


            yield return null;

            if(_canShoot && !_isStunned)
            {
                _gun.Shoot(_player.transform.position, _hunterDefinition.BulletSpeed, _hunterDefinition.BulletDamage);

                StartCoroutine(DelayShootCor());
            }
            
            

        }

        

        

    }

    
    IEnumerator DelayShootCor()
    {
        _canShoot = false;
        yield return _hunterDefinition.WaitForSecondsMoveToPointDelay;
        _canShoot = true;

    }

    IEnumerator DelayMoveToPointCor()
    {
        //Debug.Log("start");

        yield return _hunterDefinition.WaitForSecondsMoveToPointDelay;
        //Debug.Log("end");

        _canMoveToPoint = true;
        _hunterSpawner.PointsForHunter[_actualIndex].IsBusy = false;
        
        if (_hunterSpawner.PointsForHunter.Count - 1 == _actualIndex)
        {
            _actualIndex = 0;
        }
        else
        {
            _actualIndex++;
        }



    }
    IEnumerator DelayStunCor(float sec)
    {
        Debug.Log("Stun");
        _isStunned = true;
        yield return new WaitForSeconds(sec);
        _isStunned = false;
        Debug.Log("not Stun");

    }
}
