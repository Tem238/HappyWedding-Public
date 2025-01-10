using System.Collections.Generic;
using UnityEngine;

public class WorkerSpawnerController : BasePrefab
{
    [SerializeField] GameObject prefab;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    private float timer = 0;
    private int count = 0;
    private int span = 2;
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
        var obj = Instantiate(prefab, new Vector3(Random.Range(-5f, 5f), 10, 0), Quaternion.identity);
        obj.GetComponent<BasePrefab>().Init(GameManager);
        obj.GetComponent<WorkerController>().Go(sprites[count]);
        if (++count >= sprites.Count)
        {
            Destroy(gameObject);
        }
    }
}
