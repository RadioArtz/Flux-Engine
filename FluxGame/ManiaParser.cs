using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;

namespace FluxGame.OsuManiaParser
{
    public enum ManiaKey
    {
        Key1,
        Key2,
        Key3,
        Key4
    }

    public class ManiaHitObject
    {
        public ManiaKey Key { get; }
        public int TimeMs { get; }
        /// <summary>
        /// -1 if not a hold note
        /// </summary>
        public int EndTimeMs { get; }

        public ManiaHitObject(ManiaKey key, int timeMs, int endTimeMs = -1)
        {
            Key = key;
            TimeMs = timeMs;
            EndTimeMs = endTimeMs;
        }

        public bool IsLongNote => EndTimeMs > TimeMs;
    }

    public class ManiaBeatmapParser
    {
        public List<ManiaHitObject> HitObjects { get; private set; }

        // A set to keep track of notes that have already been hit (by time).
        public HashSet<ManiaHitObject> hitNotes;

        public ManiaBeatmapParser()
        {
            HitObjects = new List<ManiaHitObject>();
            hitNotes = new HashSet<ManiaHitObject>(); // To track notes that have already been hit
        }

        public void Parse(string filePath)
        {
            bool inHitObjectsSection = false;

            foreach (var line in File.ReadLines(filePath))
            {
                if (line.StartsWith("[HitObjects]"))
                {
                    inHitObjectsSection = true;
                    continue;
                }

                if (inHitObjectsSection)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var values = line.Split(',');

                    if (values.Length < 5) continue;

                    int x = int.Parse(values[0]);
                    int timeMs = int.Parse(values[2]);

                    int endTimeMs = -1;
                    if (values.Length > 5 && int.TryParse(values[5], out var end))
                    {
                        endTimeMs = end;
                    }

                    ManiaKey key = ParseKeyFromX(x);
                    HitObjects.Add(new ManiaHitObject(key, timeMs, endTimeMs));
                }
            }

            // Sort the hit objects by their TimeMs to ensure correct order.
            HitObjects = HitObjects.OrderBy(hitObject => hitObject.TimeMs).ToList();
        }

        private ManiaKey ParseKeyFromX(int x)
        {
            if (x >= 0 && x < 128)
                return ManiaKey.Key1;
            else if (x >= 128 && x < 256)
                return ManiaKey.Key2;
            else if (x >= 256 && x < 384)
                return ManiaKey.Key3;
            else if (x >= 384 && x <= 512)
                return ManiaKey.Key4;

            throw new ArgumentOutOfRangeException(nameof(x), "Invalid X-coordinate for a 4-key osu!mania map.");
        }

        public static int ParseXFromkey(ManiaKey key)
        {

            if(key == ManiaKey.Key1)
                return 0;
            if (key == ManiaKey.Key2)
                return 128;
            if (key == ManiaKey.Key3)
                return 256;
            if (key == ManiaKey.Key4)
                return 384;
            return -1;
        }
    }
}