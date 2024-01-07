using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Foundation : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var mono = GetLayer("mono");
            
            var p = mono.CreateSprite("sb/p.png");

            p.Fade(483, 1);
            p.Fade(11454, 0);
            p.Scale(OsbEasing.OutExpo, 483, 1168, 0, 50);
            p.Rotate(OsbEasing.OutExpo, 483, 1168, Math.PI/2, 0);
            
            var pb = mono.CreateSprite("sb/p.png");

            pb.Fade(483, 1);
            pb.Fade(11454, 0);
            pb.Scale(OsbEasing.OutExpo, 483, 1168, 0, 48);
            pb.Rotate(OsbEasing.OutExpo, 483, 1168, Math.PI/2, 0);
            pb.Color(483, Color4.Black);

            var list = new List<OsbSprite>(){p, pb};

            foreach(var s in list){
                s.Scale(OsbEasing.InExpo, 8711, 11454, s.ScaleAt(8711).X, s.ScaleAt(8711).X + 1000);
            }

            

            var bgLayer = GetLayer("BG");
            var bg = bgLayer.CreateSprite(Beatmap.BackgroundPath);
            var bit = GetMapsetBitmap(Beatmap.BackgroundPath);
            bg.Scale(55340, 480.0f / bit.Height);
            bg.Fade(55340, 1);
            bg.Fade(77282, 0);


            for(int i = 0; i < 12; i ++){
                var posY = i * (480/12);
                var bar = mono.CreateSprite("sb/bar2.png", OsbOrigin.Centre, new Vector2(320, posY));
                bar.Fade(55340, 1);
                bar.Fade(77282, 0);
                bar.Rotate(55340, Math.PI/2);
                
                var duration = 77282 - 55340;
                bar.StartLoopGroup(55340 + (i * 500), 4);
                bar.MoveX(OsbEasing.InOutSine, 0, (duration/4) / 2, 320, 250);
                bar.MoveX(OsbEasing.InOutSine, (duration/4) / 2, (duration/4), 250, 320);
                bar.EndGroup();
                 
            }


		    
            
        }
    }
}
