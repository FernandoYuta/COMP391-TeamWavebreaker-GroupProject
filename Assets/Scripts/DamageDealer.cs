using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage = 50;

    


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void DealDamage(GameObject obj)
    {
        obj.GetComponent<Health>().DecreaseHealth(damage);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision detected");

        if (other.gameObject.CompareTag("Player"))
        {

            DealDamage(other.gameObject);
            Debug.Log("Deal Damage has been called");
        }
    }

}
