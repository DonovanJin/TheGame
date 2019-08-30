using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTerrain : MonoBehaviour
{
    [Range(0,100)]
    public int StructuralHealth = 100;

    //0 Full health, 1 below 75%, 2 below 50%, 3, below 25%
    public Sprite[] TerrainSprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TestStructuralHealth();
    }

    private void TestStructuralHealth()
    {
        if (StructuralHealth > 75)
        {
            GetComponent<SpriteRenderer>().sprite = TerrainSprites[0];
        }
        else if ((StructuralHealth <= 75) && (StructuralHealth > 50))
        {
            GetComponent<SpriteRenderer>().sprite = TerrainSprites[1];
        }
        else if ((StructuralHealth <= 50) && (StructuralHealth > 25))
        {
            GetComponent<SpriteRenderer>().sprite = TerrainSprites[2];
        }
        else if ((StructuralHealth <= 25) && (StructuralHealth > 0))
        {
            GetComponent<SpriteRenderer>().sprite = TerrainSprites[3];
        }
        else if (StructuralHealth == 0)
        {
            GetComponent<SpriteRenderer>().sprite = TerrainSprites[4];
        }
    }

    public void SustainDamage(int DamageReceived)
    {        
        StructuralHealth -= DamageReceived;

        if (StructuralHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
