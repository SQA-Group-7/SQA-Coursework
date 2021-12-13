using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CraneController : MonoBehaviour
{

    public enum CraneStatus
    {
        STILL, ROTATE_CRANE, ROTATE_LOAD, MOVE_LOAD_VERTICAL, MOVE_LOAD_HORIZONTAL
    }

    Transform craneBase;
    public CraneStatus cranestatus = CraneStatus.ROTATE_CRANE;
    public int speed = 50;
    public int upperBound = 0;
    public int lowerBound = -10;
    Boolean movingUp = false;

    /*
        public void SetToStill()
        {
            cranestatus = CraneStatus.STILL;
        }

        public void SetToRotateCrane()
        {
            cranestatus = CraneStatus.ROTATE_CRANE;
        }
        public void SetToRotateLoad()
        {
            cranestatus = CraneStatus.ROTATE_LOAD;
        }
        public void SetToMoveLoad()
        {
            cranestatus = CraneStatus.MOVE_LOAD_HORIZONTAL;
        }
        */


    public void IncreaseHeight()
    {
        GameObject baseFrame = transform.Find("Prefabs/CraneBaseFrame").gameObject;
        GameObject duplicate = Instantiate(baseFrame);
        float height = transform.Find("CraneBaseGroup/CraneBaseFrame0").position.y - transform.Find("CraneBaseGroup/CraneBaseFrame1").position.y;

        for (int i = 1; i < 100; i++)
        {
            if (!transform.Find("CraneBaseGroup/CraneBaseFrame" + i))
            {
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

    public void DecreaseHeight()
    {
        Debug.Log("Test");
        float height = transform.Find("CraneBaseGroup/CraneBaseFrame0").position.y - transform.Find("CraneBaseGroup/CraneBaseFrame1").position.y;

        for (int i = 1; i < 100; i++)
        {
            if (!transform.Find("CraneBaseGroup/CraneBaseFrame" + i))
            {
                DestroyImmediate(transform.Find("CraneBaseGroup/CraneBaseFrame" + (i - 1)).gameObject);
                transform.Translate(new Vector3(0, height * -1, 0));
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            foreach (Transform grandChild in child.transform)
            {
                if (grandChild.name.Contains("BaseFrame"))
                {
                    craneBase = grandChild;
                    break;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (cranestatus == CraneStatus.ROTATE_CRANE)
        {
            foreach (Transform child in transform)
            {
                if (!child.name.Contains("CraneBaseGroup"))
                {
                    child.transform.RotateAround(craneBase.position, Vector3.up, speed * Time.deltaTime);
                }
            }
        }
        else if (cranestatus == CraneStatus.ROTATE_LOAD)
        {
            Transform platformTransform = transform.Find("CraneTopGroup/LoadGroup/Platform");
            transform.Find("CraneTopGroup/LoadGroup").RotateAround(platformTransform.position, Vector3.up, speed * Time.deltaTime);
        }
        else if (cranestatus == CraneStatus.MOVE_LOAD_VERTICAL) {
            Transform loadGroupTransform = transform.Find("CraneTopGroup/LoadGroup");
            Transform cableGroupTransform = transform.Find("CraneTopGroup/HookGroup/CableGroup");
            if (loadGroupTransform.localPosition.y < upperBound && movingUp) {
                loadGroupTransform.Translate(new Vector3(0,speed / 300f, 0));
                cableGroupTransform.localScale += new Vector3(0,speed / -1300f, 0);
            } else if (loadGroupTransform.localPosition.y > lowerBound && !movingUp){
                loadGroupTransform.Translate(new Vector3(0,speed / -300f, 0));
                cableGroupTransform.localScale += new Vector3(0,speed / 1300f, 0);
            } else if (loadGroupTransform.localPosition.y >= upperBound && movingUp) {
                movingUp = false;
            } else if (loadGroupTransform.localPosition.y <= lowerBound && !movingUp) {
                movingUp = true;
            }
        } else if (cranestatus == CraneStatus.MOVE_LOAD_HORIZONTAL) {
            Transform loadGroupTransform = transform.Find("CraneTopGroup/LoadGroup");
            Transform hookGroupTransform = transform.Find("CraneTopGroup/HookGroup");
            if (loadGroupTransform.localPosition.x < upperBound && movingUp) {
                loadGroupTransform.localPosition += new Vector3(speed / 300f,0, 0);
                hookGroupTransform.localPosition += new Vector3(speed / 300f,0, 0);
            } else if (loadGroupTransform.localPosition.x > lowerBound && !movingUp){
                loadGroupTransform.localPosition += new Vector3(speed / -300f,0, 0);
                hookGroupTransform.localPosition += new Vector3(speed / -300f,0, 0);
            } else if (loadGroupTransform.localPosition.x >= upperBound && movingUp) {
                movingUp = false;
            } else if (loadGroupTransform.localPosition.x <= lowerBound && !movingUp) {
                movingUp = true;
            }
        }
    }
}
