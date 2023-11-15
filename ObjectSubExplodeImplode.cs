using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObjectSubExplodeImplode : MonoBehaviour
{
    public Vector3[] toolHouseChildPosition = new Vector3[79];
    public Vector3 parentCenterPosition;
    public int childCounter;
    public Vector3 parentTranslate = new Vector3(0f, -10.0f, 0f);
    public float explodeDist = 10;
    public float explodeDiffParam = 4.0f;
    public float implodeSpeed = 14.0f;
    public float implodeSmooth = 0.05f;
    public float implodeSmoothDist = 0.1f;

    void Awake()
    {
        childCounter = transform.childCount;
        toolHouseChildPosition = new Vector3[childCounter];
        parentCenterPosition = transform.parent.transform.position + parentTranslate;

        for (int i = 0; i < childCounter; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            toolHouseChildPosition[i] = child.transform.position;
            var explodeDiff = explodeDist + (explodeDiffParam*i); 
            child.transform.position = Vector3.MoveTowards(child.transform.position, parentCenterPosition, -explodeDiff);
        }
    }

    void Update()
    {      
        for (int i = 0; i < childCounter; i++)
        {
            float smooth = 1.0f;
            GameObject child = transform.GetChild(i).gameObject; 
            float dist = Vector3.Distance(child.transform.position, toolHouseChildPosition[i]);
            if (dist < implodeSmoothDist) smooth = implodeSmooth;
            child.transform.position = Vector3.MoveTowards(child.transform.position, toolHouseChildPosition[i], implodeSpeed * Time.deltaTime * smooth);
        }
    }
}
