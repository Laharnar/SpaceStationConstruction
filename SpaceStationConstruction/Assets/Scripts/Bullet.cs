using UnityEngine;

public class Bullet:MonoBehaviour {

    public Rigidbody2D rig;
    public Rigidbody rig3d;
    public ProjectileStats stats;

    const float bulletPlane = -1;

    private void Awake() {
        Destroy(gameObject, 5f);
    }

    private void Update() {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, bulletPlane) 
            + transform.up * Time.deltaTime * stats.speed;
        if (rig) {
            rig.MovePosition(newPos);
        } else if (rig3d){
            rig3d.MovePosition(newPos);
        } else {
            Debug.Log("Error");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        OnCollision(other.transform);
    }

    private void OnTriggerEnter(Collider other) {
        OnCollision(other.transform);
    }
    private void OnCollision(Transform other) {
        if (other.transform.GetComponent<Fighter>()) {
            Fighter f = other.transform.GetComponent<Fighter>();
            CombatMessageManager.AddMessage("Bullet collision with fighter." + transform.name + " - " + f.name, 0);
            if (f.data.stats.alliance != stats.alliance) {
                CombatMessageManager.AddMessage("Bullet collision with enemy fighter." + transform.name+" - " + f.name, 1);
                GameManager.Instance.fighterBehaviour.ApplyDmg(stats.damage, f);
                Destroy(gameObject);
            }
        }
        if (other.transform.GetComponent<StationModule>()) {
            StationModule f = other.transform.GetComponent<StationModule>();
            CombatMessageManager.AddMessage("Bullet collision with station."+transform.name+" - " + f.name, 0);
            if (f.stats.alliance != stats.alliance) {
                CombatMessageManager.AddMessage("Bullet collision with enemy station."+transform.name+" - " + f.name, 1);
                GameManager.Instance.fighterBehaviour.ApplyDmg(stats.damage, f);
                Destroy(gameObject);
            }
        }
    }
}
