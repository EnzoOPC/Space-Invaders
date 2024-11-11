using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5;

    public Projectile laserPrefab;

    public bool laserActive;

    private void Update() {
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)) {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }
         else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)) {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }
    private void Shoot() {

        if (!laserActive) {
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            laserActive = true;
        }
        
    }

    private void LaserDestroyed() {
        laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") ||
            other.gameObject.layer == LayerMask.NameToLayer("Invader")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
