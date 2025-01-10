using UnityEngine;

public class EmptyPrefabController : BasePrefab
{
    public override void Init(GameManager gameManager)
    {
        Destroy(gameObject);
    }
}
