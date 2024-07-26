using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private CharacterController controller;

    private Vector3 point;
    
    private void Awake()
    {
        controller = gameObject.AddComponent<CharacterController>();
        point = GameMath.GetRandomPointOnEarth(GameManager.planetRadius);
        
        GameObject a = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        a.transform.position = point;
    }

    private void Update()
    {
        Vector3 direction = (transform.position - point).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
        
        controller.Move( speed * Time.deltaTime * transform.up);
        // GameMath.LookAtTransform(transform, GameManager.planet);
    }
}
