using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int shootInterval = 1;
    [SerializeField] private int shootDamage = 70;

    [SerializeField] private Transform gunHolder;
    [SerializeField] private Transform firePos;
    [SerializeField] private LineRenderer line;

    private bool isReady = false;
    private void Awake()
    {
        line.enabled = false;
    }

    IEnumerator Start()
    {
        WaitForSeconds wait = new WaitForSeconds(shootInterval);
        while (true)
        {
            yield return wait;
            Shoot();
        }
    }


    void Update()
    {
        if (showingEffect) return;
        var zombie = Spawner.Instance.GetMinXZombie();
        if (zombie != null)
        {
            isReady = true;
            Vector3 dir = zombie.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            gunHolder.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            isReady = false;
            gunHolder.rotation = Quaternion.identity;
        }
    }
  

    private void Shoot()
    {
        if (isReady == false) return;

        RaycastHit2D hit = Physics2D.Raycast(firePos.position, firePos.right, 20f, LayerMask.GetMask("Zombie"));
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<Zombie>(out var zombie))
            {
                Vector3 localHitPoint = line.transform.InverseTransformPoint(hit.point);
                ShowEffect(localHitPoint, 0.2f);
                zombie.TakeDamage(shootDamage);
            }
        }
    }
    void ShowEffect(Vector3 end, float time)
    {
        showingEffect = true;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, end);
        line.enabled = true;
        StartCoroutine(LineCo(time));
    }

    private bool showingEffect = false;
    IEnumerator LineCo(float time)
    {
        yield return new WaitForSeconds(time);
        line.enabled = false;
        showingEffect = false;
    }
}