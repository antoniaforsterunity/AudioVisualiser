using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public int _bandNumber;
    public float _startScale, _scaleMultiplier;
    //public bool _useBuffer;
    Material _glowingMaterial;

    void Start()
    {
        //_glowingMaterial = GetComponent<MeshRenderer>().materials[0];
    }

    
    void Update()
    {
        transform.localScale = new Vector3((DataGrabber._buffer[_bandNumber] * _scaleMultiplier) + _startScale, (DataGrabber._buffer[_bandNumber] * _scaleMultiplier) + _startScale, (DataGrabber._buffer[_bandNumber] * _scaleMultiplier) +_startScale);
       
        
        //If you want them to glow in time with the music (more freq = more white glow), give standard material and uncomment these lines
       // Color _color = new Color(DataGrabber._buffer[_bandNumber], DataGrabber._buffer[_bandNumber], DataGrabber._buffer[_bandNumber]);
       // _glowingMaterial.SetColor("_EmissionColor", _color);
    }
}
