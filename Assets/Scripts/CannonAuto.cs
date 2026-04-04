using UnityEngine;

public class CannonAuto : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Vector3 direction = Vector3.forward;
    public float force = 10f;

    public float fireRate = 1f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = direction.normalized * force;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 dir = direction.normalized * force * 0.2f;
        Gizmos.DrawLine(firePoint.position, firePoint.position + dir);

        Gizmos.color = Color.blue;

        Vector3 start = firePoint.position;
        Vector3 velocity = direction.normalized * force;

        float step = 0.2f;
        Vector3 prev = start;

        for (float t = 0; t < 2f; t += step)
        {
            Vector3 next = start + velocity * t;
            Gizmos.DrawLine(prev, next);
            prev = next;
        }
    
    }
}