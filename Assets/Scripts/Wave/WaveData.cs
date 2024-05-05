
using System.Collections.Generic;
using TrapLight.Wave.Light;

namespace TrapLight.Wave
{
    [System.Serializable]
    public struct WaveData
    {
        public int WaveID;
        public List<LightParticleType> ListOfLights;
    }
}
