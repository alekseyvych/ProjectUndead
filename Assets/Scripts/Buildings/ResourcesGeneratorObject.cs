using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesGeneratorObject : MonoBehaviour
{
    public int[] productionPerSecond = new int[] { 1, 5, 20, 100 };
    public int[] storageCapacity = new int[] { 1000, 5000, 20000, 100000 };
    public int currentCapacity = 0;
    private int level = 0;
    // Start is called before the first frame update
    void Start()
    {
        TimeTickSystem.OnTick += delegate (object sender, System.EventArgs empty)
        {
            if (currentCapacity <= storageCapacity[level])
            {
                currentCapacity += productionPerSecond[level];

                if (currentCapacity > storageCapacity[level])
                {
                    currentCapacity = storageCapacity[level];
                }
            }

        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
