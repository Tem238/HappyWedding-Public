using UnityEngine;

public class RolyPolyToyBodyController : MonoBehaviour
{
    [SerializeField] GameObject parent;
    private bool onGround = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        parent.GetComponent<RolyPolyToyController>().CountUp(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!onGround)
        {
            onGround = true;
            SoundManagerController.soundManager.PlaySE((int)SEType.Toy);
        }
    }
}
