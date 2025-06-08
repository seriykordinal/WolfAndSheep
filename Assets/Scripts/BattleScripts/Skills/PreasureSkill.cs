using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreasureSkill : MonoBehaviour, ISkill
{
    [SerializeField] private float PreasureForce = 4000f;
    [SerializeField] private float PreasureDelaySec = 1f;
    [SerializeField] private float SecToStun = 2.5f;
    public Image PreasureBar;
    public TextMeshProUGUI PreasureText;


    WaitForSeconds _waitForSecondsPreasureDelay;
    private bool _canPreasure;
    private float _timer;

    private Rigidbody2D _rb;
    Vector2 _moveDirection;
    private IDamageable _damageableCharacter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Damageable")
        {
            collision.TryGetComponent(out _damageableCharacter);
            //Debug.Log("add" + collision.gameObject);

        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Damageable")
        {
            _damageableCharacter = null;
            //Debug.Log("remove" + collision.gameObject);

        }


    }

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Start()
    {
        _waitForSecondsPreasureDelay = new WaitForSeconds(PreasureDelaySec);
        _canPreasure = true;
        PreasureBar.fillAmount = 0f;
        PreasureText.enabled = false;


    }
    public void UseSkill()
    {
        Dash();

    }

    void Dash()
    {
        if (_canPreasure)
        {

            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            _moveDirection = new Vector2(moveX, moveY).normalized;

            if (_moveDirection.y == 0 && _moveDirection.x == 0)
            {
                Vector2 vec = GetComponentInParent<AimRotation>().MousePostion - new Vector2(transform.position.x, transform.position.y);
                //Debug.Log("000 " + vec.normalized);
                _rb.velocity = vec.normalized * PreasureForce * Time.deltaTime;
            }
            else
            {
                _rb.velocity = _moveDirection.normalized * PreasureForce * Time.deltaTime;
                //Debug.Log("111 " + _moveDirection.normalized);

            }

            if (_damageableCharacter != null)
            {
                _damageableCharacter.Stun(SecToStun);
            }
            StartCoroutine(DashDelayCor());

            VisualDash();
        }
    }
    private void VisualDash()
    {
        _timer = PreasureDelaySec;
        PreasureBar.fillAmount = 1f;

        StartCoroutine(VisualDashDelayCor());
    }
    IEnumerator DashDelayCor()
    {
        _canPreasure = false;
        //Debug.Log("cant");

        yield return _waitForSecondsPreasureDelay;
        
        _canPreasure = true;
        //Debug.Log("can");


    }

    IEnumerator VisualDashDelayCor()
    {
        PreasureText.enabled = true;

        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            PreasureBar.fillAmount = _timer / PreasureDelaySec;
            //Debug.Log("fill: " + DelayBar.fillAmount + "timer: " + _timer);

            yield return null;

        }
        PreasureText.enabled = false;

    }
    
}
