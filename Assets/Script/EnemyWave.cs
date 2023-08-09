using UnityEngine;

public class EnemyWave : MonoBehaviour, IActorTemplate
{
    int health;
    int travelSpeed;
    int fireSpeed;
    int hitPower;

    //wave enemy
    [SerializeField]
    float verticalSpeed = 2;

    [SerializeField]
    float verticalAmplitude = 1;

    Vector3 sineVer;
    float time;

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        time += Time.deltaTime;
        sineVer.y = Mathf.Sin(time * verticalSpeed) * verticalAmplitude;
        transform.position = new Vector3(transform.position.x + travelSpeed * Time.deltaTime, transform.position.y + sineVer.y, transform.position.z);
    }

    public void ActorStats(SOActorModel actorModel)
    {
        health = actorModel.health;
        travelSpeed = actorModel.speed;
        hitPower = actorModel.hitPower;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public int SendDamage()
    {
        return hitPower;
    }

    public void TakeDamage(int incomingDamage)
    {
        health -= incomingDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("collided with player or bullet");

            Debug.Log(health);

            if (health >= 1)
            {
                Debug.Log(other.name);
                health -= other.GetComponent<IActorTemplate>().SendDamage();
            }

            if (health < 0)
            {
                Die();
            }
        }
    }
}
