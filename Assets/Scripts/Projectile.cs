using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 30;

    public System.Action destroyed;

    private void Update() {
        this.transform.position += this.direction * Time.deltaTime * this.speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (this.destroyed != null) {
            this.destroyed.Invoke();
        }
        Destroy(this.gameObject);
    }
}
