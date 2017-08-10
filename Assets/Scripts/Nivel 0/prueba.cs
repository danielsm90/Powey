using UnityEngine;
using System.Collections;

public class prueba : MonoBehaviour {

    float lerpTime = 1f;
    float currentLerpTime;
    bool bandOcultar;

    float moveDistance = 1f;

    GameObject objMover;
    Component posFinal;

    Vector3 startPos, endPos, startPosM, endPosM;

    void Start()
    {
        posFinal = GameObject.FindGameObjectWithTag("finalAnimacion").GetComponent<RectTransform>();
        bandOcultar = false;
    }

    void FixedUpdate(){
        if (bandOcultar)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
                bandOcultar = false;
            }

            //lerp!
            float perc = currentLerpTime / lerpTime;
            objMover.gameObject.GetComponent<RectTransform>().position = Vector3.Lerp(startPos, endPos, perc);
            transform.position = Vector3.Lerp(startPosM, endPosM, perc);
        }
    }

    public void ocultar(GameObject gObj)
    {
        currentLerpTime = 0f;
        objMover = gObj;
        startPosM = transform.position;
        startPos = objMover.gameObject.GetComponent<RectTransform>().position;
        endPosM = posFinal.transform.position;
        endPos = startPosM;
        bandOcultar = true;
    }
}
