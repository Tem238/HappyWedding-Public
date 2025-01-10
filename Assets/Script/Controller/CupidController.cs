using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CupidController : BasePrefab
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject arrowPosition;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(6.5f, 2.3f, 0);
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<SpriteRenderer>().DOFade(1, 1.0f)
            .OnComplete(() => StartCoroutine(attack()));
    }

    private IEnumerator attack()
    {
        yield return new WaitForSeconds(1);
        var obj = Instantiate(prefab, arrowPosition.transform.position, Quaternion.identity);
        obj.GetComponent<BasePrefab>().Init(GameManager);
        gameObject.GetComponent<SpriteRenderer>().DOFade(0, 1.0f)
            .OnComplete(() => Destroy(gameObject));
    }

    void Update()
    {
        Vector3 diff = (player.transform.position - gameObject.transform.position);
        this.transform.rotation = Quaternion.FromToRotation(Vector3.left, diff);
    }
}
