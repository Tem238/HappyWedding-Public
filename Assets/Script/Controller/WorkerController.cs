using System.Collections;
using UnityEngine;

public class WorkerController : BasePrefab
{
    int point = 100;
    [SerializeField] GameObject effect;

    public void Go(Sprite sprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        StartCoroutine(destroyThis());
    }

    private IEnumerator destroyThis()
    {
        yield return new WaitForSeconds(1.5f);
        SoundManagerController.soundManager.PlaySE((int)SEType.Work, 2.0f);
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
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
