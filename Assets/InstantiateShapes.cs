using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateShapes : MonoBehaviour
{
//    public GameObject _shapePrefab;
//    GameObject[] _shapeArray = new GameObject[512];
//    public float _maxScale;

//    // Start is called before the first frame update
//    void Start()
//    {
//        for(int i=0; i<512; i++)
//        {
//            GameObject _instanceOfShape = (GameObject)Instantiate(_shapePrefab);
//            //Centre cube in world, parent, rename
//            _instanceOfShape.transform.position = this.transform.position;
//            _instanceOfShape.transform.parent = this.transform;
//            _instanceOfShape.name = "SampleShape " + i;

//            //Position shapes in a circle
//            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
//            _instanceOfShape.transform.position = Vector3.forward * 100;

//            //Add shape to the array (so we can communicate to it)
//            _shapeArray[i] = _instanceOfShape;
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        for (int i = 0; i < 512; i++)
//        {
//            if(_shapeArray != null)
//            {
//                //Set shape's y (height) to the amplitude value. Multiple by maxScale because raw value is really small. 2 is starting size (so if no ampltidue at that freq, the shape isn't invisible)
//                _shapeArray[i].transform.localScale = new Vector3(10, (DataGrabber._samples[i] * _maxScale) +2 , 10);
//            }

//        }
//    }
}
