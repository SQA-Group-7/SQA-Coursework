using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Enums for the status of the crane
//Still: The crane will not move.
//RotateCrane: The upper part of the crane will rotate.
//RotateLoad: Only the load part will be rotating.
//MoveLoadVertically: The load part will move up and down.
//MoveLoadHorizontally: The load part will move along the arm of the crane.
public enum CraneStatus
{
    Still, RotateCrane, RotateLoad, MoveLoadVertically, MoveLoadHorizontally
}
public class CraneController : MonoBehaviour
{
    public CraneStatus CraneStatus { get; set; } = CraneStatus.RotateCrane;

    //Defines the speed of rotating/moving
    public int Speed = 50;

    //Defines the range in which the crane will move up/down or along the crane arm.
    public int UpperBound = 0;
    public int LowerBound = -10;

    //Tells whether the crane is currently move up/outwards.
    private Boolean _movingUp = false;

    private Transform _craneBase;

    //Increases the height of the whole crane by adding one base frame object at the bottom.
    public void IncreaseHeight()
    {

        //Get the relavent objects
        GameObject baseFrame = transform.Find("Prefabs/CraneBaseFrame").gameObject;
        GameObject duplicate = Instantiate(baseFrame);

        //Obtain the heigh of one frame
        float height = transform.Find("CraneBaseGroup/CraneBaseFrame0").position.y - transform.Find("CraneBaseGroup/CraneBaseFrame1").position.y;

        for (int i = 1; i < 100; i++)
        {
            //Find the bottomest base frame of the whole crane.
            if (!transform.Find("CraneBaseGroup/CraneBaseFrame" + i))
            {
                //Move the whole crane upwards, and then add a base frame at the bottom
                duplicate.name = "CraneBaseFrame" + i;
                duplicate.transform.parent = transform.Find("CraneBaseGroup");
                Transform above = transform.Find("CraneBaseGroup/CraneBaseFrame" + (i - 1));
                duplicate.transform.position = above.position;
                transform.Translate(new Vector3(0, height, 0));
                duplicate.transform.Translate(new Vector3(0, height * -1, 0));
                break;
            }
        }


    }

    //Decrease the height of the whole frame by removing the bottom base frame.
    public void DecreaseHeight()
    {

        //Get the height of the base frame.
        float height = transform.Find("CraneBaseGroup/CraneBaseFrame0").position.y - transform.Find("CraneBaseGroup/CraneBaseFrame1").position.y;

        for (int i = 1; i < 100; i++)
        {
            //Find the bottom base frame.
            if (!transform.Find("CraneBaseGroup/CraneBaseFrame" + i))
            {
                //Destroy the base frame and move the whole crane downwards.
                DestroyImmediate(transform.Find("CraneBaseGroup/CraneBaseFrame" + (i - 1)).gameObject);
                transform.Translate(new Vector3(0, height * -1, 0));
                break;
            }
        }
    }

    //Start is called before the first frame update
    void Start()
    {
        //Find the crane base group.
        foreach (Transform child in transform)
        {
            foreach (Transform grandChild in child.transform)
            {
                if (grandChild.name.Contains("BaseFrame"))
                {
                    _craneBase = grandChild;
                    break;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        //Rotate the top part of the crane
        if (CraneStatus == CraneStatus.RotateCrane)
        {
            //Check whether it is a top group.
            foreach (Transform child in transform)
            {
                if (!child.name.Contains("CraneBaseGroup"))
                {
                    child.transform.RotateAround(_craneBase.position, Vector3.up, Speed * Time.deltaTime);
                }
            }
        }
        //Rotate the load group
        else if (CraneStatus == CraneStatus.RotateLoad)
        {
            Transform platformTransform = transform.Find("CraneTopGroup/LoadGroup/Platform");
            transform.Find("CraneTopGroup/LoadGroup").RotateAround(platformTransform.position, Vector3.up, Speed * Time.deltaTime);
        }
        //Move the load group up and down
        else if (CraneStatus == CraneStatus.MoveLoadVertically)
        {
            Transform loadGroupTransform = transform.Find("CraneTopGroup/LoadGroup");
            Transform cableGroupTransform = transform.Find("CraneTopGroup/HookGroup/CableGroup");
            //If it's moving up and it hasn't reached the highest point yet
            if (loadGroupTransform.localPosition.y < UpperBound && _movingUp)
            {
                //Move up and stretch the cables.
                loadGroupTransform.Translate(new Vector3(0, Speed / 300f, 0));
                cableGroupTransform.localScale += new Vector3(0, Speed / -1300f, 0);
            }
            //If it's moving down and it has't reached the lowest point yet.
            else if (loadGroupTransform.localPosition.y > LowerBound && !_movingUp)
            {
                //Move down and shrink the cables.
                loadGroupTransform.Translate(new Vector3(0, Speed / -300f, 0));
                cableGroupTransform.localScale += new Vector3(0, Speed / 1300f, 0);
            }
            //If it's moving up but it has reached the highest point.
            else if (loadGroupTransform.localPosition.y >= UpperBound && _movingUp)
            {
                //Change the status to moving down.
                _movingUp = false;
            }
            //If it's moving down but it has reached the lowest point.
            else if (loadGroupTransform.localPosition.y <= LowerBound && !_movingUp)
            {
                //Change the status to move up.
                _movingUp = true;
            }
        }
        //Move the load along the arm of the crane.
        else if (CraneStatus == CraneStatus.MoveLoadHorizontally)
        {
            Transform loadGroupTransform = transform.Find("CraneTopGroup/LoadGroup");
            Transform hookGroupTransform = transform.Find("CraneTopGroup/HookGroup");
            //If it's moving outwards but it hasn't reached the farthest point yet.
            if (loadGroupTransform.localPosition.x < UpperBound && _movingUp)
            {
                //Move outwards.
                loadGroupTransform.localPosition += new Vector3(Speed / 300f, 0, 0);
                hookGroupTransform.localPosition += new Vector3(Speed / 300f, 0, 0);
            }
            //If it's moving inwards but it hasn't reached the closest point yet.
            else if (loadGroupTransform.localPosition.x > LowerBound && !_movingUp)
            {
                //Move inwards
                loadGroupTransform.localPosition += new Vector3(Speed / -300f, 0, 0);
                hookGroupTransform.localPosition += new Vector3(Speed / -300f, 0, 0);
            }
            //If it's moving outwards but it has reached the farthest point
            else if (loadGroupTransform.localPosition.x >= UpperBound && _movingUp)
            {
                //Change status to moving inwards.
                _movingUp = false;
            }
            //If it's moving inwards but it has reached the closest point
            else if (loadGroupTransform.localPosition.x <= LowerBound && !_movingUp)
            {
                //Change status to moving outwards.
                _movingUp = true;
            }
        }
    }
}
