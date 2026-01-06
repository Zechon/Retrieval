using UnityEngine;

public class Bottle_Break : MonoBehaviour
{
    [SerializeField] private GameObject[] breaks;
    [SerializeField] private float breakForce = 3f;

    private MeshCollider col;
    private MeshRenderer rend;
    private GameObject bottle;
    private bool broken = false;

    private void Awake()
    {
        bottle = this.gameObject;
        col = bottle.GetComponent<MeshCollider>();
        rend = bottle.GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (broken) return;

        float impact = collision.relativeVelocity.magnitude;

        if (impact < breakForce)
            return;

        BreakBottle();
    }

    private void BreakBottle()
    {
        broken = true;

        col.enabled = false;
        rend.enabled = false;

        GameObject prefab = breaks[Random.Range(0, breaks.Length)];
        GameObject brokenBottle = Instantiate(prefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }

}
