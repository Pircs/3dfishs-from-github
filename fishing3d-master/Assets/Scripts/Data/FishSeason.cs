using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class WaveFish
{
    public int fkid;        //fish kind id
    public Vector3 p;      //local position
    public Vector3 s;      //local scale
}

[Serializable]
public class OneWave
{
    public Vector3 o;  //起点origin
    public float speed;
    public Vector3 ea;//eulerangle
    public int pathid;
    public WaveFish[] fishes;
    public Vector3 rootea;

    public OneWave()
    {
        fishes = new WaveFish[0];
    }

    public void AddWaveFish(WaveFish waveFish)
    {
        List<WaveFish> fishlist = new List<WaveFish>(fishes);
        fishlist.Add(waveFish);
        fishes = fishlist.ToArray();
    }
}

[Serializable]
public class FishSeason
{
    public OneWave[] waves;
    public FishSeason()
    {
        waves = new OneWave[0];
    }

    public void AddWave(OneWave wave)
    {
        List<OneWave> waveList = new List<OneWave>(waves);
        waveList.Add(wave);
        waves = waveList.ToArray();
    }
}
