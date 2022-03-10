using System.Collections;
using System.Collections.Generic;
using MyMath;
using UnityEngine;

public class PhysicsGravitationalAtraction : MonoBehaviour
{
    [Header("Mass")] [SerializeField] public float mass = 1;
    
    [SerializeField] [Range(0, 1)] private float _G;

    [SerializeField] private PhysicsGravitationalAtraction otherStar;

    private Vector3 displacement;
    private  Vector3 gravitationalForce;

    [Header("Vectors")] 
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 acceleration;

    void Update()
    {
        Vector3 _R = otherStar.transform.position - transform.position ;
        
        acceleration = Vector3.zero;
        
        ApplyForce(((mass * otherStar.mass * _G) / (_R.magnitude * _R.magnitude)) * _R.normalized);
        
        Move();
        
        acceleration.Draw(transform.position,Color.yellow);
        
    }
    
    void Gravity()
    {
        if (gravitationalForce.magnitude > 5)
        {
            gravitationalForce = gravitationalForce.normalized * 5;
        }

    }

    private void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }
    
    public void Move()
    {
        velocity = velocity + acceleration * Time.deltaTime;
        displacement = velocity * Time.deltaTime;
        transform.position += displacement;
    }
}
