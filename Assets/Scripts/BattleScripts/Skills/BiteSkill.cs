using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BiteSkill : MonoBehaviour, ISkill
{
    [SerializeField] private int BiteDamage;
    [SerializeField] private float BiteDelaySec;
    public Image BiteBar;
    public TextMeshProUGUI BiteText;


    WaitForSeconds _waitForSecondsBiteDelay;
    private bool _canBite;
    private float _timer;

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

    private void Start()
    {
        _waitForSecondsBiteDelay = new WaitForSeconds(BiteDelaySec);
        _canBite = true;
        BiteBar.fillAmount = 0f;
        BiteText.enabled = false;


    }
    public void UseSkill()
    {
        Bite();
    }

    private void Bite()
    {
        if (_damageableCharacter == null) return;

        if (_canBite)
        {
            
            if (_damageableCharacter.Health <= BiteDamage)
            {
                //Debug.Log("vi ski" + _damageablesCharacter.VictimDefinition);
                ManagerScore.Instance.OnAddScore(_damageableCharacter.ScoreForKill);
                GetComponentInParent<Wolf>().IncreaseHunger(_damageableCharacter.HungerForKill);
                GetComponentInParent<Wolf>().TakeHeal(_damageableCharacter.HungerForKill);
            }

            _damageableCharacter.TakeDamage(BiteDamage);
            
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Collider2D>().enabled = true;
            StartCoroutine(BiteDelayCor());

            VisualBite();




        }



    }
    private void VisualBite()
    {
        _timer = BiteDelaySec;
        BiteBar.fillAmount = 1f;
        
        StartCoroutine(VisualBiteDelayCor());
    }


    IEnumerator BiteDelayCor()
    {
        _canBite = false;
        
        yield return _waitForSecondsBiteDelay;

        _canBite = true;
    }

    IEnumerator VisualBiteDelayCor()
    {
        BiteText.enabled = true;

        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            BiteBar.fillAmount = _timer / BiteDelaySec;
            //Debug.Log("fill: " + DelayBar.fillAmount + "timer: " + _timer);

            yield return null;

        }

        BiteText.enabled = false;

    }
}
