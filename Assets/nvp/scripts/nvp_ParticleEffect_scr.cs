using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.zong.interfaces;

namespace newvisionsproject.zong.effects
{
  public class nvp_ParticleEffect_scr : MonoBehaviour, IEffect
  {
    public ParticleSystem particleSystemToPlay;
    public void Play()
    {
      particleSystemToPlay.Play();
    }
  }
}