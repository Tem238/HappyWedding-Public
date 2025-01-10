using UnityEngine;

public class HandSpinnerSpawnerController : BasePrefab
{
    [SerializeField] GameObject prefab;
    private float timer = 0;
    private int count = 0;
    [SerializeField] private int span = 2;
    [SerializeField] private int loop = 5;
    // Start is called before the first frame update
    void Start()
    {
        attack();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > span)
        {
            timer = 0;
            attack();
        }
    }

    private void attack()
    {
        var obj = Instantiate(prefab, new Vector3(0, 10, 0), Quaternion.identity);
        obj.GetComponent<BasePrefab>().Init(GameManager);
        if (++count > loop)
        {
            Destroy(gameObject);
        }
    }
}
