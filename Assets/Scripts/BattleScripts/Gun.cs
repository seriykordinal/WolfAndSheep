using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    public void Shoot(Vector3 target, float speed, int damage)
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().DamageForHit = damage;
        Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = (target - transform.position).normalized * speed * Time.deltaTime;
        //Debug.Log("I shoot to taerget " + target);


    }

}
