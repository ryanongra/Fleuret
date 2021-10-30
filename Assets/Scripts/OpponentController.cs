using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{

    public bool isParrying;
    public float disabledTime = 0.5f;
    float disabledRemaining = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsDisabled())
        {
            disabledRemaining -= Time.deltaTime;
        }
    }

    public bool IsDisabled()
    {
        return disabledRemaining > 0;
    }

    public void GetDisabled()
    {
        print("disabled");
        disabledRemaining = disabledTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Weapon"))
        {
            print("player touche");
        }
    }
}
