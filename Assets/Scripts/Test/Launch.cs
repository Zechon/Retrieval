using UnityEngine;

public class Launch : MonoBehaviour
{
    public float forceAmount = 5f;
    public float wait = 2f;
    private float time;
    private bool launched = false;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (launched) return;
        time += Time.deltaTime;

        if (time >= wait)
        {
            rb.AddForce(Vector3.right * forceAmount, ForceMode.Impulse);
            launched = true;
        }
    }
}
