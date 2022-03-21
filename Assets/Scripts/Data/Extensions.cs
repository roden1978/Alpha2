using UnityEngine;

namespace Data
{
    public static class Extensions
    {
        public static Vector3Data AsVector3Data(this Vector3 position)
        {
            return new Vector3Data(position.x, position.y, position.z);
        }

        public static Vector3 AsVector3(this Vector3Data position)
        {
            return new Vector3(position.X, position.Y, position.Z);
        }

        public static string ToJSON(this object obj) => 
            JsonUtility.ToJson(obj);
        public static T Deserialize<T>(this string json) => 
            JsonUtility.FromJson<T>(json);
    }
}