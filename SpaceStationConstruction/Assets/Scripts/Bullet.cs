using UnityEngine;

public class Bullet:MonoBehaviour {
    public Rigidbody2D rig;
    public ProjectileStats stats;

    private void Awake() {
        Destroy(gameObject, 5f);
    }

    private void Update() {
        rig.MovePosition(transform.position+transform.up);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.GetComponent<Fighter>()) {
            Fighter f = other.transform.GetComponent<Fighter>();
            if (f.data.stats.alliance != stats.alliance) {
                GameManager.Instance.fighterBehaviour.ApplyDmg(stats.damage, f);
                Destroy(gameObject);
            }
        }
        if (other.transform.GetComponent<StationModule>()) {
            StationModule f = other.transform.GetComponent<StationModule>();
            if (f.stats.alliance != stats.alliance) {
                GameManager.Instance.fighterBehaviour.ApplyDmg(stats.damage, f);
                Destroy(gameObject);
            }
        }
    }
}
