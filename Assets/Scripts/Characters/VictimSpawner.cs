using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictimSpawner : MonoBehaviour
{

    [SerializeField] private List<VictimDefinition> ListIntrinsics;
    [SerializeField] private List<GameObject> ListSheepsVisual;

    [SerializeField] int SpawnMaxVictim;
    [SerializeField] int SpawnVictimOnStart;
    [SerializeField] private float SpawnDelaySec;
    [SerializeField] float SpawnMinScale;
    [SerializeField] float SpawnMaxScale;
    [SerializeField] int SpawnOffset;
    




    private List<GameObject> _spawnedVictim;
    private List<GameObject> _spawndeGrases;

    WaitForSeconds _waitForSecondsSpawnDelay;
    private bool _canSpawn;

    public List<GameObject> SpawndeGrases { get { return _spawndeGrases; } }

    private void Awake()
    {
        _spawnedVictim = new List<GameObject>();
        _spawndeGrases = new List<GameObject>();
        
        Grass[] grases = GetComponentsInChildren<Grass>();
        foreach (Grass g in grases)
        {
            _spawndeGrases.Add(g.gameObject);
        }

        _waitForSecondsSpawnDelay = new WaitForSeconds(SpawnDelaySec);
        _canSpawn = true;
    }

    private void Start()
    {
        for (int i = 0; i < SpawnVictimOnStart; i++)
        {
            SpawnWithExtrinsic();
        }
    }

    private void Update()
    {
        //MySpawn();
        SpawnVictim();
    }

    

    void SpawnWithExtrinsic()
    {
        System.Random random = new System.Random();
        
        int curIndex = random.Next(0, ListIntrinsics.Count);
        Vector3 vectorOffset = new Vector3(random.Next(-SpawnOffset, SpawnOffset), random.Next(-SpawnOffset, SpawnOffset), 0);
        //Debug.Log(vectorOffset);

        var spawned = FactoryFlyweight.Instance.Spawn(ListIntrinsics[curIndex], transform.position + vectorOffset, Quaternion.identity);
        spawned.GetComponent<Victim>().Initialize(this, ListSheepsVisual[random.Next(0, ListSheepsVisual.Count)], ListIntrinsics[curIndex].MaxHealth, random.Next((int)(SpawnMinScale * 100), (int)(SpawnMaxScale * 100)) / 100f);
        _spawnedVictim.Add(spawned.gameObject);
    }

    public void RemoveVictimById(int id)
    {
        for (int i = 0; i < _spawnedVictim.Count;i++)
        {
            if (_spawnedVictim[i].GetComponent<Victim>().Id == id)
            {
                _spawnedVictim.Remove(_spawnedVictim[i]);
            }
        }
    }
    private void MySpawn()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SpawnWithExtrinsic();
        }
    }


    private void SpawnVictim()
    {
        if (_spawnedVictim.Count >= SpawnMaxVictim) return;


        if (_canSpawn)
        {
            StartCoroutine(DelaySpawnCor());

            //SpawnWithExtrinsic();
        }
    }

    IEnumerator DelaySpawnCor()
    {
        _canSpawn = false;

        yield return _waitForSecondsSpawnDelay;
        SpawnWithExtrinsic();


        _canSpawn = true;
    }
}
