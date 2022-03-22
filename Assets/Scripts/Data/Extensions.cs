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
        public static Texture2D TextureFromSprite(this Sprite sprite)
        {
            Texture2D texture = new Texture2D(Mathf.FloorToInt(sprite.rect.width), Mathf.FloorToInt(sprite.rect.height));
            Color[] pixels = sprite.texture.GetPixels(
                Mathf.FloorToInt(sprite.rect.xMin),
                Mathf.FloorToInt(sprite.rect.yMin),
                Mathf.FloorToInt(sprite.rect.width),
                Mathf.FloorToInt(sprite.rect.height));
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }
    }
}