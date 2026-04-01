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
}