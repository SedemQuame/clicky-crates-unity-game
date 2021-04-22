using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 8, maxSpeed = 14, maxTorque = 10;
    private float xRange = -4, ySpawnPos = -2;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        // Add force
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        // Add torque
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        // Spawn at random position.
        targetRb.position = RandomSpawnPos();
        // getting the GameManager instance.
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        if(gameManager.isGameActive){
            if(gameObject.CompareTag("Bad")){
                gameManager.UpdateScore(-5);
            }else{
                gameManager.UpdateScore(5);
            }
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    void OnTriggerEnter(Collider other){
        if(!gameObject.CompareTag("Bad")){
            gameManager.GameOver();
        }
        Destroy(gameObject);
    }

    Vector3 RandomForce(){
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque(){
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos(){
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
