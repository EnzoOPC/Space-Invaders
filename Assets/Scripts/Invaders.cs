using UnityEngine;
using UnityEngine.SceneManagement;
public class Invaders : MonoBehaviour
{
    public int rows = 5;
    public int cols = 11;

    public Invader[] prefabs;

    public AnimationCurve speed;

    public Projectile missilePrefab; 

    private Vector3 direction = Vector2.right;

    public float missileAtackRate = 5;
    public int ammountKilled {get; private set; }

    public int ammountAlive => this.totalInvaders - this.ammountKilled; 
    public int totalInvaders => this.rows * this.cols;
    public float percentKilled => (float)this.ammountKilled / (float)this.totalInvaders;

    

    private void Awake() {
        for (int row = 0; row < this.rows; row++) {
            
            float width = 2 * (this.cols - 1);
            float height = 2 * (this.rows -1); 
            Vector2 centering = new Vector2(-width / 2, -height /2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2), 0); 
            for (int col = 0; col < this.cols; col++) {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;

                Vector3 position = rowPosition;
                position.x += col * 2;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Start() {
        InvokeRepeating(nameof(MissileAttack), this.missileAtackRate, this.missileAtackRate);
    }

    private void Update() {

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        this.transform.position += direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        foreach (Transform invader in this.transform) {
            
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }
            if (direction == Vector3.right && invader.position.x >= (rightEdge.x - 1)) {
                AdvanceRow();
            }
            else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + 1)) {
                AdvanceRow();
            }

            }
        }

        private void MissileAttack() {
            foreach (Transform invader in this.transform) {
                if (!invader.gameObject.activeInHierarchy) {
                    continue;
                }
                if (Random.value < (1/(float)ammountAlive)) {
                    Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                    break;
                }
            }
        }

        private void AdvanceRow() {

            direction *= -1;

            Vector3 position = this.transform.position;
            position.y -= 1;
            this.transform.position = position; 
        }

        private void InvaderKilled() {
            
            this.ammountKilled++;

            if (this.ammountKilled >= this.totalInvaders) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        
    }

