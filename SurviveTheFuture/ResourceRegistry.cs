using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheFuture
{
    public static class ResourceRegistry
    {
        public static Dictionary<string, Texture2D> Registry = new Dictionary<string, Texture2D>();

        public static ContentManager CM;  
    }
}
