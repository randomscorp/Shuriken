using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ItemChanger;
using Shuriken.GO;
using UnityEngine;

namespace Shuriken.Rando
{
    public class ItemSprite : ISprite
    {
        [Newtonsoft.Json.JsonIgnore]
        public Sprite Value => LoadSprite("rando.png");
        public ISprite Clone() => (ISprite)MemberwiseClone();

        private static Sprite LoadSprite(string name)
        {
            Texture2D texture;
            using (FileStream s = File.Open((Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"/static/{name}").ToString(), FileMode.Open))
            {

                byte[] buffer = new byte[s.Length];
                s.Read(buffer, 0, buffer.Length);
                s.Dispose();
                texture = new Texture2D(1, 1);
                texture.LoadImage(buffer);

                texture.Apply();

                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }

    }
}
