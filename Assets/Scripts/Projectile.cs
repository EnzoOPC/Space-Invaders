using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 30;

    public System.Action destroyed;

    public GameObject explosionPrefab;

    private PointManager pointManager;

    void Start() {

        pointManager = GameObject.Find("Point Manager").GetComponent<PointManager>();
    }

    private void Update() {
        this.transform.position += this.direction * Time.deltaTime * this.speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (this.destroyed != null) {
            if (other.gameObject.tag != "Limit") {

                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 1);
            }
            
            this.destroyed.Invoke();
            if (other.gameObject.tag != "Bunker" && other.gameObject.tag != "Limit") {

                pointManager.UpdateScore(50);
            }
        }

        Destroy(this.gameObject);
    }
}
