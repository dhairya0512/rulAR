using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using TMPro;
using UnityEngine.XR.ARFoundation;
using System;

public class linemanager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ARPlacementInteractable placementInteractable;
    public TextMeshPro mText;
    private int pointcount = 0;
    public TextMeshProUGUI buttonText;
    LineRenderer line;
    public bool continuous = true;
    // Start is called before the first frame update
    void Start()
    {
        placementInteractable.objectPlaced.AddListener(DrawLine);


    }


    public void togglebetweendiscreteandcontinuous()
    {
        Debug.Log(LayerMask.LayerToName(0));
        continuous = !continuous;
        if (continuous)
        {
            buttonText.text = "Discrete";
        }
        else
        {
            buttonText.text = "Continuous";

        }
    }

    // Update is called once per frame
    void DrawLine(ARObjectPlacementEventArgs args)
    {
        pointcount++;
        if (pointcount < 2)
        {
            line = Instantiate(lineRenderer);
            line.positionCount = 1;
        }
        else
        {
            line.positionCount = pointcount;
            if (!continuous)
            {
                pointcount = 0;
            }
        }
      
        //2. let the points locate in teh line renderer
        line.SetPosition(line.positionCount-1,args.placementObject.transform.position);
        if (line.positionCount > 1)
        {
            Vector3 pointA = line.GetPosition(line.positionCount - 1);
            Vector3 pointB = line.GetPosition(line.positionCount - 2);
            float dist = Vector3.Distance(pointA,pointB) *100f;
          //  mText.maxWidth = (float)dist;
            dist = (float)System.Math.Round(dist,2);
            TextMeshPro distText = Instantiate(mText);
            distText.text = "" + dist+ " cms";

            Vector3 directionVector = (pointB - pointA);
            Vector3 normal = args.placementObject.transform.up;

            Vector3 upd = Vector3.Cross(directionVector,normal).normalized;

            Quaternion rotation = Quaternion.LookRotation(-normal, upd);

            distText.transform.rotation = rotation; 
            distText.transform.position = (pointA  + directionVector * 0.5f) + upd * 0.015f;



        }
    }
}
