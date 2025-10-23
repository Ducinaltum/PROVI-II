using UnityEngine;

public class FireballParticles : MonoBehaviour, IPoolable
{
    [SerializeField] private ParticleSystem m_particles;
    public ParticleSystem Particles => m_particles;

    void OnParticleSystemStopped ()
    {
        Dispose();
    }

    public void Dispose()
    {
        m_particles.Stop();
        gameObject.SetActive(false);
        ObjectPool<FireballParticles>.Instance.DisposeObject(this);
    }
}
