using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Numerics;
namespace Geostorm.Core.Entities
{
    static class PlayerTexture
    {
        private static readonly float preScale = 20.0f;
        public static readonly Vector2[] Points =
        {
            new Vector2(-0.5f, 0.0f)    * preScale,     //innerCenter 
            new Vector2(-1.0f, 0.0f)    * preScale,     //outerCenter 
            new Vector2(-0.2f, -0.55f)  * preScale,     //topInnerWing
            new Vector2(-0.4f,-0.8f)    * preScale,     //topOuterWing 
            new Vector2(0.6f,-0.3f)     * preScale,     //topGun
            new Vector2(-0.2f,0.55f)    * preScale,     //bottomInnerWing
            new Vector2(-0.4f,0.8f)     * preScale,     //bottomOuterWing
            new Vector2(0.6f,0.3f)      * preScale      //bottomGun
        };
    }
    static class BulletTexture
    {
        private static readonly float preScale = 15.0f;
        public static readonly Vector2[] Points =
        {
            new Vector2(-0.3f, 0.0f)    * preScale,     //left  
            new Vector2(-0.1f, 0.2f)    * preScale,     //top  
            new Vector2(0.8f, 0.0f)     * preScale,     //right 
            new Vector2(-0.1f,-0.2f)    * preScale      //bottom
        };
    }
    static class GruntTexture
    {
        private static readonly float preScale = 18.0f;
        public static readonly Vector2[] Points =
        {
            new Vector2(-1.0f, 0.0f)    * preScale,     //left  
            new Vector2(-0.0f,-1.0f)    * preScale,     //top  
            new Vector2( 1.0f, 0.0f)     * preScale,     //right 
            new Vector2(-0.0f, 1.0f)    * preScale      //bottom
        };
    }
}
