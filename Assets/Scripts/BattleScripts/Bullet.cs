using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damageForHit;
    public int DamageForHit {  get { return _damageForHit; } set { _damageForHit = value; } }

    [SerializeField] private float _selfDestroyDelaySec;
    public float SelfDestroyDelaySec { get { return _selfDestroyDelaySec; } set { _selfDestroyDelaySec = value; } }
    private WaitForSeconds _waitForSecondsSelfDestroyDelay;

    private void Start()
    {
        _waitForSecondsSelfDestroyDelay = new WaitForSeconds(SelfDestroyDelaySec);
        StartCoroutine(SelfDestroyCor());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Wolf>().TakeDamage(_damageForHit);
        }
        Destroy(gameObject);

    }

    IEnumerator SelfDestroyCor()
    {
        yield return _waitForSecondsSelfDestroyDelay;
        Destroy(gameObject);
    }
}
