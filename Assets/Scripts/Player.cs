using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5;

    private Vector2 movement;

    public Projectile laserPrefab;

    public bool laserActive;

    public Vector3 laserSpawnPosition;

    public int Lives = 3;

    public Image[] LivesUI;

    public GameObject explosionPrefab;

    private Vector2 screenLimit;

    private float playerHalfWidth;

    private void Start() {

        screenLimit = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        playerHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    private void Update() {

        float input = Input.GetAxisRaw("Horizontal");
        movement.x = input * speed * Time.deltaTime;
        transform.Translate(Vector2.right * input * speed * Time.deltaTime);

        float clampX = Mathf.Clamp(transform.position.x, -screenLimit.x + playerHalfWidth, screenLimit.x - playerHalfWidth);
        Vector2 pos = transform.position;
        pos.x = clampX;
        transform.position = pos;

        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
        
    }
    private void Shoot() {

        if (!laserActive) {
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position + laserSpawnPosition, Quaternion.identity);
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
            if (other.gameObject.tag == "Enemy") {
            Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);

            Lives -= 1;

            for(int i = 0; i < LivesUI.Length; i++) {
                if (i < Lives) {
                    LivesUI[i].enabled = true;
                }
                else {
                    LivesUI[i].enabled = false;
                }
            }
            Destroy(other.gameObject);
            if (Lives <= 0) {

                Destroy(gameObject);
            }
        }
        }
    }
}
