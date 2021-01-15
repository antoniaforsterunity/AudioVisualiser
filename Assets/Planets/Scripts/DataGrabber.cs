using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
//This script grabs the audio data amnd turns it into an array of floats
public class DataGrabber : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    float[] _freqCurrentValues = new float[8];


    //The thing we want to produce. This is the value that we're going to make, between zero and one, to apply to gameObjects (glow, rotations, anything)
    public static float[] _usefulValues = new float[8];

    //if _freqBand is higher than buffer, buffer jumps up to match. If _freBand is lower than buffer, buffer gradually drops
    public static float[] _buffer = new float[8];
    float[] _amountBufferDrops = new float[8];

    //To get a value between 0 and 1, we want to divide our value by the current highest frequency band value
    float[] _freqHighestValues = new float[8];

    //This value might fluctuate so we're going to create a buffer
    public static float[] _usefulValueBuffer = new float[8];



    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        Buffer();
        CreateUsefulValue();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    //Make eight buckets instead of 521 frequency bands
    //Put more frequency bands into the lower buckets, because they don't often get activated
    void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            //Add this but to mop up the last two
            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }
            average /= count;
            _freqCurrentValues[i] = average * 10;
        }
    }

    void Buffer()
    {
        for (int g = 0; g < 8; g++)
        {
            //If value in that frequency bucket is higher than our buffer (our way of making it smooth), buffer jumps up to match. 
            if (_freqCurrentValues[g] > _buffer[g])
            {
                _buffer[g] = _freqCurrentValues[g];
                _amountBufferDrops[g] = 0.005f;
            }
            //If value in that frequency bucket is lower than our buffer, buffer gradually drops
            if (_freqCurrentValues[g] < _buffer[g])
            {
                //Buffer decreases to current frequency, but does it gradually (slows down the closer it is)
                _amountBufferDrops[g] = (_buffer[g] - _freqCurrentValues[g]) / 8;
                _buffer[g] -= _amountBufferDrops[g];
            }
        }
    }

    //By "useful value", I mean one between 0-1, that we can apply to gameObjects (their glow, rotation, whatever). This method creates that value.
    void CreateUsefulValue()
    {
        for (int i = 0; i < 8; i++)
        {
            //Grab and store the highest value
            if (_freqCurrentValues[i] > _freqHighestValues[i])
            {
                _freqHighestValues[i] = _freqCurrentValues[i];
            }
            //The "useful value" is the amplitude of that freq. band right now, relative to its highest amplitude
            _usefulValues[i] = _freqCurrentValues[i] / _freqHighestValues[i];

            _usefulValueBuffer[i] = _buffer[i] / _freqHighestValues[i];

            //Debug.Log("Useful value: " + _usefulValues[i]);
            //Debug.Log("Useful value buffer: " + _usefulValueBuffer[i]);

        }
    }
}
