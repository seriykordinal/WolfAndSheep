using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashSkill : MonoBehaviour, ISkill
{
    [SerializeField] private float DashForce = 2500f;
    [SerializeField] private float DashDelaySec = 1f;
    public Image DashBar;
    public TextMeshProUGUI DashText;


    WaitForSeconds _waitForSecondsDashDelay;
    private bool _canDash;
    private float _timer;

    private Rigidbody2D _rb;
    Vector2 _moveDirection;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Start()
    {
        _waitForSecondsDashDelay = new WaitForSeconds(DashDelaySec);
        _canDash = true;
        DashBar.fillAmount = 0f;
        DashText.enabled = false;


    }
    public void UseSkill()
    {
        Dash();

    }

    void Dash()
    {
        if (_canDash)
        {
            
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            _moveDirection = new Vector2(moveX, moveY).normalized;

            if (_moveDirection.y == 0 && _moveDirection.x == 0)
            {
                Vector2 vec = GetComponentInParent<AimRotation>().MousePostion - new Vector2(transform.position.x, transform.position.y);
                //Debug.Log("000 " + vec.normalized);
                _rb.velocity = vec.normalized * -DashForce * Time.deltaTime;
            }
            else
            {
                _rb.velocity = _moveDirection.normalized * DashForce * Time.deltaTime;

                //Debug.Log("111 " + _moveDirection.normalized);

            }
            StartCoroutine(DashDelayCor());

            VisualDash();
        }
    }
    private void VisualDash()
    {
        _timer = DashDelaySec;
        DashBar.fillAmount = 1f;

        StartCoroutine(VisualDashDelayCor());
    }
    IEnumerator DashDelayCor()
    {
        _canDash = false;

        //Debug.Log("cant");
        yield return _waitForSecondsDashDelay;
        _canDash = true;
        //Debug.Log("can");


    }

    IEnumerator VisualDashDelayCor()
    {
        DashText.enabled = true;

        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            DashBar.fillAmount = _timer / DashDelaySec;
            //Debug.Log("fill: " + DelayBar.fillAmount + "timer: " + _timer);

            yield return null;

        }
        DashText.enabled = false;

    }
}
