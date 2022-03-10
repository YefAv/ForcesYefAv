using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsForcesFluids : MonoBehaviour
{
    [Header("Mass")] [SerializeField] private float mass = 1;

    [SerializeField] private float gravity = -9.8f;
    [SerializeField] [Range(0, 1)] private float dragCoefficient;
    [SerializeField] private float area=3;
    

    private Vector3 displacement;

    [Header("Vectors")] 
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 acceleration;

    private Vector3 weight;

    [SerializeField] private float damping=0.9f;

    [Header("World limits")] 
    [SerializeField] private float Xborde;
    [SerializeField] private float Yborde;
    
    void Start()
    {
        weight = mass * new Vector3(0, gravity, 0);
    }
    
    void Update()
    {
        ApplyForce(weight);
        FluidResistance();

        Move();
        CheckCollisions();
        
        acceleration = Vector3.zero;
    }

    void FluidResistance()
    {
        if(transform.position.y <= 0)
        ApplyForce(-0.5f*Mathf.Pow(velocity.magnitude,2)*area*dragCoefficient*velocity.normalized);
    }
    
    private void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }
    
    public void Move()
    {
        velocity = velocity + acceleration * Time.deltaTime;
        displacement = velocity * Time.deltaTime;
        transform.position = transform.position + displacement;
    }

    private void CheckCollisions() {

        if (transform.position.x >= Xborde || transform.position.x <= -Xborde) {

            if (transform.position.x >= Xborde) transform.position = new Vector3(Xborde, transform.position.y, 0);
            else if (transform.position.y <= -Xborde) transform.position = new Vector3(-Xborde, transform.position.y, 0);
            velocity.x = velocity.x * -1 * damping;
        }

        if (transform.position.y >= Yborde || transform.position.y <= -Yborde)
        {
            if (transform.position.y >= Yborde) transform.position = new Vector3(transform.position.x, Yborde, 0);
            else if (transform.position.y <= -Yborde)  transform.position = new Vector3(transform.position.x, -Yborde, 0);
            velocity.y = velocity.y * -1;
            velocity.y = velocity.y * damping;
        }
    }
}
