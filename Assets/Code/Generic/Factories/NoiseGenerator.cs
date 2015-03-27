using UnityEngine;

namespace Assets.Code.Generic.Factories
{
    public class NoiseGenerator
    {
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }

        public NoiseGenerator(int mapWidth, int mapHeight)
        {
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;
        }

        public float GetNoise(int x, int y)
        {
            float noise = 0.0f;
            return noise;
        }
    }
}