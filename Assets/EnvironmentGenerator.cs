using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    public GameObject EnvironmentPrefab;
    public float PrefabSize;
    public float Rows;
    public float Columns;

    void Start()
    {
        GenerateEnvironments();
    }


    void GenerateEnvironments()
    {
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                var posX = i * PrefabSize;
                var posZ = j * PrefabSize;

                var envClone = GameObject.Instantiate(
                    EnvironmentPrefab, 
                    new Vector3(posX, 0, posZ),
                    Quaternion.identity
                );
            }
        }
    }
}
