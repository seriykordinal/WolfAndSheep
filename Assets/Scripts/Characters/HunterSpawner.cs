using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HunterSpawner : MonoBehaviour
{
    [SerializeField] private List<HunterDefinition> ListIntrinsics;
    [SerializeField] private List<GameObject> ListHunterVisual;

    [SerializeField] int SpawnMaxHunter = 1;
    [SerializeField] int SpawnHunterOnStart;
    [SerializeField] private float SpawnDelaySec;
    [SerializeField] float SpawnMinScale = 1f;
    [SerializeField] float SpawnMaxScale = 1f;
    [SerializeField] int SpawnOffset = 0;




    private List<PointForHunter> _pointsForHunter;
    public List<PointForHunter> PointsForHunter { get { return _pointsForHunter; } }
    private List<GameObject> _spawnedHunter;


    WaitForSeconds _waitForSecondsSpawnDelay;
    private bool _canSpawn;


    private void Awake()
    {
        _spawnedHunter = new List<GameObject>();
        _pointsForHunter = GetComponentsInChildren<PointForHunter>().ToList();

        _waitForSecondsSpawnDelay = new WaitForSeconds(SpawnDelaySec);
        _canSpawn = true;
    }

    private void Start()
    {
        for (int i = 0; i < SpawnHunterOnStart; i++)
        {
            SpawnWithExtrinsic();
        }
    }

    private void Update()
    {
        //MySpawn();
        SpawnHunter();
    }



    void SpawnWithExtrinsic()
    {
        System.Random random = new System.Random();

        int curIndex = random.Next(0, ListIntrinsics.Count);
        Vector3 vectorOffset = new Vector3(random.Next(-SpawnOffset, SpawnOffset), random.Next(-SpawnOffset, SpawnOffset), 0);
        //Debug.Log(vectorOffset);

        var spawned = FactoryFlyweight.Instance.Spawn(ListIntrinsics[curIndex], transform.position + vectorOffset, Quaternion.identity);
        spawned.GetComponent<Hunter>().Initialize(this, ListHunterVisual[random.Next(0, ListHunterVisual.Count)], ListIntrinsics[curIndex].MaxHealth, random.Next((int)(SpawnMinScale * 100), (int)(SpawnMaxScale * 100)) / 100f);
        _spawnedHunter.Add(spawned.gameObject);
    }

    public void RemoveHunterById(int id)
    {
        for (int i = 0; i < _spawnedHunter.Count; i++)
        {
            if (_spawnedHunter[i].GetComponent<Hunter>().Id == id)
            {
                _spawnedHunter.Remove(_spawnedHunter[i]);
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


    private void SpawnHunter()
    {
        if (_spawnedHunter.Count >= SpawnMaxHunter) return;


        if (_canSpawn)
        {
            StartCoroutine(DelaySpawnCor());

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
