using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 30;

    public System.Action destroyed;

    public GameObject explosionPrefab;

    private void Update() {
        this.transform.position += this.direction * Time.deltaTime * this.speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (this.destroyed != null) {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            this.destroyed.Invoke();
        }

        Destroy(this.gameObject);
    }
}
