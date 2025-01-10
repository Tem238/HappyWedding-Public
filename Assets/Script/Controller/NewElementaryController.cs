using UnityEngine;

public class NewElementaryController : BasePrefab
{
    int point = 500;
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        ApplyAndDestroyUtil.FadeApplyWithEffect(
            gameObject,
            effect,
            effect.transform.localScale.x,
            () => ApplyAndDestroyUtil.FadeDestroyWithEffect(gameObject, effect, 5.0f)
        );
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(0f, -2f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && point != 0)
        {
            GameManager.countUp(point, collision.transform.position);
            DisableEffect(effect, ref point);
        }
    }
}
