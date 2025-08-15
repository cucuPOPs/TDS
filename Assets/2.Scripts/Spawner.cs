using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Zombie zombiePrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private Transform spawnPoint;

    private List<Zombie> zombies = new List<Zombie>();
    public static Spawner Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnCo());
    }

    IEnumerator SpawnCo()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnInterval);
        while (true)
        {
            yield return wait;
            var zombie = GameObject.Instantiate(zombiePrefab) as Zombie;
            zombie.transform.position = spawnPoint.position;
            zombies.Add(zombie);
        }
    }

    public Zombie GetMinXZombie()
    {
        if (zombies.Count == 0)
            return null;

        return zombies.OrderBy(x => x.transform.position.x).First();
    }

    public void OnDead(Zombie zombie)
    {
        zombies.Remove(zombie);
    }
}