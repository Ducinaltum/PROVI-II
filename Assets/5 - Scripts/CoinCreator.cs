using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class CoinCreator : MonoBehaviour
{
    [SerializeField] private SplineContainer  m_splineContainer;
    [SerializeField] private int m_numberOfObjects;
    [SerializeField] private GameObject m_objectToSpawn;
    [SerializeField] private GameObject[] m_spawnedObjects;

    [ContextMenu(nameof(SpreadObjects))]
    public void SpreadObjects()
    {
        Spline spline = m_splineContainer.Spline;
        List<GameObject> spawnedObjects = new();
        ClearExistingObjects();
        float offset = 1.0f / (m_numberOfObjects - 1.0f);
        for (int i = 0; i < m_numberOfObjects; i++)
        {
            float t = offset * i;
            Vector3 pos = spline.EvaluatePosition(t);
            Debug.Log(t);
            Debug.Log(pos);
            GameObject spawnedObject = Instantiate(m_objectToSpawn, transform.position + pos, Quaternion.identity);
            spawnedObject.transform.parent = transform;
            spawnedObjects.Add(spawnedObject);
        }
        m_spawnedObjects = spawnedObjects.ToArray();
    }

    private void ClearExistingObjects()
    {
        if (m_spawnedObjects != null)
        {
            foreach (GameObject item in m_spawnedObjects)
            {
                DestroyImmediate(item);
            }
        }
    }
}
