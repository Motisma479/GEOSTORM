﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Geostorm.Core.Entities
{
    //------------------------PLAYER_RELATED_GEOMETRY------------------------
    static class PlayerTexture
    {
        private static readonly float preScale = 20.0f;
        public static float thickness = 2.5f;
        public static readonly Vector2[] Points =
        {
            new Vector2(-0.9f, 0.0f)    * preScale,     //outerCenter 
            new Vector2(-0.3f,-0.8f)    * preScale,     //topOuterWing 
            new Vector2(0.7f,-0.3f)     * preScale,     //topGun
            new Vector2(-0.1f, -0.55f)  * preScale,     //topInnerWing
            new Vector2(-0.4f, 0.0f)    * preScale,     //innerCenter 
            new Vector2(-0.1f,0.55f)    * preScale,     //bottomInnerWing
            new Vector2(0.7f,0.3f)      * preScale,     //bottomGun
            new Vector2(-0.3f,0.8f)     * preScale      //bottomOuterWing
        };
    }
    static class TuretTexture
    {
        private static readonly float preScale = 10.0f;
        public static float thickness = 2.5f;
        public static readonly Vector2[] Points =
        {
            new Vector2(-0.9f, 0.0f)    * preScale,     //outerCenter 
            new Vector2(-0.3f,-1f)    * preScale,     //topOuterWing 
            new Vector2(0.7f,0f)     * preScale,     //topGun
            new Vector2(-0.1f, -0.55f)  * preScale,     //topInnerWing
            new Vector2(-0.4f, 0.0f)    * preScale,     //innerCenter 
            new Vector2(-0.1f,0.55f)    * preScale,     //bottomInnerWing
            new Vector2(0.7f,0f)      * preScale,     //bottomGun
            new Vector2(-0.3f,1f)     * preScale      //bottomOuterWing
        };
    }
    static class BulletTexture
    {
        private static readonly float preScale = 15.0f;
        public static float thickness = 2.0f;
        public static readonly Vector2[] Points =
        {
            new Vector2(-0.3f, 0.0f)    * preScale,     //left  
            new Vector2(-0.1f, 0.2f)    * preScale,     //top  
            new Vector2(0.8f, 0.0f)     * preScale,     //right 
            new Vector2(-0.1f,-0.2f)    * preScale      //bottom
        };
    }
    static class PointerTexture
    {
        private static readonly float preScale = 5.0f;
        public static float thickness = 2.5f;
        public static readonly Vector2[] Points =
        {
            new Vector2(-1.0f, 0.0f)    * preScale,     //left  
            new Vector2(-0.0f,-1.0f)    * preScale,     //top  
            new Vector2( 1.0f, 0.0f)    * preScale,     //right
        };
    }
    static class CursorTexture
    {
        public static readonly float preScale = 25.0f;
        public static float thickness = 2.5f;
        public static readonly Vector2[] Points =
        {
            new Vector2(0.0f, 0.0f)    * preScale,     //p1  
            new Vector2(0.5f,0.0f)    * preScale,     //p2  
            new Vector2( 1.0f, 0.5f)     * preScale,     //p3
            new Vector2( 1.0f, 1.0f)     * preScale,     //p4
            new Vector2( 0.5f, 1.5f)     * preScale,     //p5
            new Vector2( 0.0f, 1.5f)     * preScale,     //p6
            new Vector2( -0.5f, 1.0f)     * preScale,     //p7
            new Vector2( -0.5f, 0.5f)     * preScale,     //p8
        };
        public static readonly Vector2[] Cross =
        {
            new Vector2( 0.25f, 0.0f)     * preScale,     //c1
            new Vector2( 0.25f, 1.5f)     * preScale,     //c2
            new Vector2( -0.5f, 0.75f)    * preScale,     //c3
            new Vector2( 1.0f, 0.75f)     * preScale,     //c4
        };
    }
    //------------------------ENEMYS_RELATED_GEOMETRY------------------------
    static class GruntTexture
    {
        private static readonly float preScale = 18.0f;
        public static float thickness = 5f;
        public static readonly Vector2[] Points =
        {
            new Vector2(-1.0f, 0.0f)    * preScale,     //left  
            new Vector2(-0.0f,-1.0f)    * preScale,     //top  
            new Vector2( 1.0f, 0.0f)    * preScale,     //right 
            new Vector2(-0.0f, 1.0f)    * preScale      //bottom
        };
    }
    static class MillTexture
    {
        private static readonly float preScale = 20.0f;
        public static float thickness = 4f;
        public static readonly Vector2[] Pal1 =
        {
            new Vector2( 0.0f, 1.0f)    * preScale,     //p1  
            new Vector2( 1.0f, 1.0f)    * preScale,     //p2  
            new Vector2(-1.0f,-1.0f)    * preScale,     //p3 
            new Vector2( 0.0f,-1.0f)    * preScale      //p4
        };
        public static readonly Vector2[] Pal2 =
        {
            new Vector2(-1.0f, 0.0f)    * preScale,     //p5  
            new Vector2(-1.0f, 1.0f)    * preScale,     //p6  
            new Vector2( 1.0f,-1.0f)    * preScale,     //p7 
            new Vector2( 1.0f, 0.0f)    * preScale      //p8
        };
    }
    static class BlazeTexture
    {
        private static readonly float preScale = 20.0f;
        public static float thickness = 4f;
        public static readonly Vector2[] Points =
        {
            new Vector2(-1.0f, 0.0f)    * preScale,     //p1  
            new Vector2(-0.2f,-0.2f)    * preScale,     //p2  
            new Vector2( 0.0f, -1.0f)   * preScale,     //p3
            new Vector2(0.2f,-0.2f)     * preScale,     //p4
            new Vector2( 1.0f, 0.0f)    * preScale,     //p5
            new Vector2(0.2f,0.2f)      * preScale,     //p6
            new Vector2( 0.0f, 1.0f)    * preScale,     //p7
            new Vector2( -0.2f, 0.2f)   * preScale,     //p8
        };
    }
    static class TankerTexture
    {
        private static readonly float preScale = 35.0f;
        public static float thickness = 4f;
        public static readonly Vector2[] Points =
        {
            new Vector2(0.0f, 1.0f)     * preScale,     //p1
            new Vector2(-0.4f, 0.8f)    * preScale,     //p2
            new Vector2(-0.4f, 1.0f)    * preScale,     //p3
            new Vector2(-0.6f, 1.2f)    * preScale,     //p4
            new Vector2(-1.0f, 0.6f)    * preScale,     //p5
            new Vector2(-0.6f, -0.2f)   * preScale,     //p6
            new Vector2(-0.4f, 0.2f)    * preScale,     //p7
            new Vector2(-0.2f, -0.6f)   * preScale,     //p8
            new Vector2(-0.2f, 0.0f)    * preScale,     //p9
            new Vector2(0.0f, -1.0f)    * preScale,     //p10
            new Vector2(0.2f, 0.0f)     * preScale,     //p11
            new Vector2(0.2f, -0.6f)    * preScale,     //p12
            new Vector2(0.4f, 0.2f)     * preScale,     //p13
            new Vector2(0.6f, -0.2f)    * preScale,     //p14
            new Vector2(1.0f, 0.6f)     * preScale,     //p15
            new Vector2(0.6f, 1.2f)     * preScale,     //p16
            new Vector2(0.4f, 1.0f)     * preScale,     //p17
            new Vector2(0.4f, 0.8f)     * preScale,     //p18
        };
    }
    //-----------------------------------------------------------------------
}
