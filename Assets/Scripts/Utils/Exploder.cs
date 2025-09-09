using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _explosionForce = 300f;

    public void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.attachedRigidbody != null && hit.attachedRigidbody != GetComponent<Rigidbody>())
                hit.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}
