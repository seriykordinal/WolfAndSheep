using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum VictimLevel
{
    Small, 
    Average, 
    Big

}
[CreateAssetMenu(fileName = "VictimDef", menuName = "Definitions/VictimDefinition")]

public class VictimDefinition : FlyweightDefinition
{

    [SerializeField] public VictimLevel Level;
    [SerializeField] public int MaxHealth;
    [SerializeField] public int HungerForKill;
    [SerializeField] public int ScoreForKill;
    [SerializeField] public int HealthForKill;
    [SerializeField] public float VictimSpeed = 500f;
    [SerializeField] private float MoveToGrassDelaySec = 2f;
    public WaitForSeconds WaitForSecondsMoveToGrassDelay;


    [SerializeField] public GameObject BasePrefab;
    [SerializeField] public GameObject UIPrefab;


    public override Flyweight Initialize()
    {
        GameObject go = Instantiate(BasePrefab);
        
        SpriteRenderer[] renderers = UIPrefab.GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer levelRender = null/*, stunRender = null*/;
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].name == "Level")
            {
                levelRender = renderers[i];
            }
            
            //if (renderers[i].name == "Stun")
            //{
            //    Debug.Log("ok");
            //    stunRender = renderers[i];

            //}
        }
        
        
        
        switch (Level)
        {
            case VictimLevel.Small:
                levelRender.color = Color.green;
                break;
            case VictimLevel.Average:
                levelRender.color = Color.yellow;
                break;
            case VictimLevel.Big:
                levelRender.color = Color.red;
                break;
            default:
                break;
        }
        Instantiate(UIPrefab, go.transform);

        Victim flyweight = go.AddComponent<Victim>();
        flyweight.Definition = this;
        flyweight.Rb = go.GetComponent<Rigidbody2D>();
        WaitForSecondsMoveToGrassDelay = new WaitForSeconds(MoveToGrassDelaySec);
        flyweight.ScoreForKill = ScoreForKill;
        flyweight.HungerForKill = HungerForKill;
        flyweight.HealthForKill = HealthForKill;
        //flyweight.StunRender = stunRender;

        //Debug.Log("def: " + flyweight.VictimDefinition + " this: " + this);


        return flyweight;   
    }

}
