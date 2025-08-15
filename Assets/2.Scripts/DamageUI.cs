using UnityEngine;

public class DamageUI : MonoBehaviour
{
    public static DamageUI Instance = null;
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

    public DamageUIItem prefab;

    public void ShowDamage(Vector3 pos, int damage)
    {
        DamageUIItem item = Instantiate(prefab, transform);
        item.SetInfo(pos, damage);
        item.Show();
    }
}