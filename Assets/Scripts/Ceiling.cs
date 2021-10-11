using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    public Renderer ceiling;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = gameObject.GetComponent<GameObject>().GetComponent<Renderer>().GetComponent<Material>();
        material.color = Color.grey;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
