using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HunterLevel
{
    Average
    

}
[CreateAssetMenu(fileName = "HunterDef", menuName = "Definitions/HunterDefinition")]

public class HunterDefinition : FlyweightDefinition
{

    [SerializeField] public HunterLevel Level;
    [SerializeField] public int MaxHealth;
    [SerializeField] public int HungerForKill;
    [SerializeField] public int ScoreForKill;
    [SerializeField] public int HealthForKill;
    [SerializeField] public float HunterSpeed = 500f;
    [SerializeField] private float MoveToPointDelaySec = 2f;
    public WaitForSeconds WaitForSecondsMoveToPointDelay;
    [SerializeField] private float ShootDelaySec = 2f;
    public WaitForSeconds WaitForSecondsShootDelay;
    
    
    [SerializeField] public float BulletSpeed;
    [SerializeField] public int BulletDamage;


    [SerializeField] public GameObject BasePrefab;
    [SerializeField] public GameObject GunPrefab;
    [SerializeField] public GameObject UIPrefab;


    public override Flyweight Initialize()
    {
        GameObject go = Instantiate(BasePrefab);

        SpriteRenderer[] renderers = UIPrefab.GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer levelRender = null;
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].name == "Level")
            {
                levelRender = renderers[i];
            }
        }



        switch (Level)
        {
            
            case HunterLevel.Average:
                levelRender.color = Color.yellow;
                break;
            
            default:
                break;
        }
        Instantiate(UIPrefab, go.transform);


        Instantiate(GunPrefab, go.transform);

        Hunter flyweight = go.AddComponent<Hunter>();
        flyweight.Definition = this;
        flyweight.Rb = go.GetComponent<Rigidbody2D>();
        WaitForSecondsMoveToPointDelay = new WaitForSeconds(MoveToPointDelaySec);
        flyweight.ScoreForKill = ScoreForKill;
        flyweight.HungerForKill = HungerForKill;
        flyweight.HealthForKill = HealthForKill;
        flyweight.HunterVision = go.GetComponent<HunterVision>();
        flyweight.OnDetectingPlayer += flyweight.PlayerDetected;
        flyweight.OnUndetectingPlayer += flyweight.PlayerUndetected;
        flyweight.Gun = go.GetComponentInChildren<Gun>();
        WaitForSecondsShootDelay = new WaitForSeconds(ShootDelaySec);
        //Debug.Log("def: " + flyweight.VictimDefinition + " this: " + this);


        return flyweight;
    }



}
