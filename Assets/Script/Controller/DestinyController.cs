using UnityEngine;

public class DestinyController : BasePrefab
{
    int point = 1000;
    float speed = 1.5f;
    private GameObject player;
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(0, 7);
    }

    void Update()
    {
        Vector3 diff = (player.transform.position - gameObject.transform.position);
        diff.Normalize();
        transform.position = transform.position + diff * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && point != 0)
        {
            GameManager.countUp(point, collision.transform.position);
            GameManager.BigHeartApply();
            DisableEffect(effect, ref point);
            ApplyAndDestroyUtil.FadeDestroyWithEffect(gameObject, effect, 0f);
        }
    }
}
