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
    public class Particles : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    particlesRandom(GetLayer("part"), 50, 145854, 165054, Color4.Pink, "sb/particle.png");
            particlesRandom(GetLayer("part"), 50, 11454, 33397, Color4.Pink, "sb/particle.png");
            particlesRandom(GetLayer("part"), 10, 22425, 33397, Color4.Pink, "sb/pl.png");
            sqare();
            hit();
            puddle();
        }

        void puddle(){
             foreach(var hitObject in Beatmap.HitObjects){

                if(!(hitObject.StartTime >= 143111 && hitObject.StartTime < 145854)) continue;

                
                    
                if((Math.Abs(hitObject.StartTime - 143454) < 100) ||
                    (Math.Abs(hitObject.StartTime - 143968) < 100) ||
                    (Math.Abs(hitObject.StartTime - 144483) < 100) ||
                    (Math.Abs(hitObject.StartTime - 144825) < 100)
                ){

                        var p3 = GetLayer("part a").CreateSprite("sb/e.png", OsbOrigin.Centre, hitObject.Position);
                
                        p3.Fade(hitObject.StartTime, hitObject.StartTime + 5000, 0.7, 0);
                        p3.Scale(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + 5000, 0, Random( 0.2, 0.5));
                        p3.Color(hitObject.StartTime, hitObject.Color);
                        p3.Additive(hitObject.StartTime, hitObject.StartTime + 5000);

                }

                

                

            }
        }

        void sqare(){
            var diffLayer = GetLayer("part a");

            foreach(var hitObject in Beatmap.HitObjects){

                if(!(
                    (hitObject.StartTime >= 52597 && hitObject.StartTime < 55340) ||
                    (hitObject.StartTime >= 118425 && hitObject.StartTime < 121168) 
                    )) continue;
                

                for(int i = 0; i < 7; i++){

                    var angle = Random(-Math.PI * 2, Math.PI * 2);
                    var rad = Random(20, 100);

                    var Pos = hitObject.Position + new Vector2((float)(Math.Cos(angle) * (rad)), (float)(Math.Sin(angle) * (rad)));

                    var p3 = diffLayer.CreateSprite("sb/p.png");
                    p3.Move(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + 2000, hitObject.Position, Pos);
                    p3.Scale(hitObject.StartTime, Random( 2, 8));
                    p3.Fade(hitObject.StartTime, hitObject.StartTime + 100, 0 , 0.7);
                    p3.Fade(hitObject.StartTime + 500, hitObject.StartTime + 2000, 0.7, 0);
                    p3.Color(hitObject.StartTime, hitObject.Color);
                    p3.Rotate(hitObject.StartTime, angle);
                    p3.Additive(hitObject.StartTime, hitObject.StartTime + 2000);

                }
            }


               

                var pattern = new List<int>(){
                    33397, 34083, 34597, 35111, 35454, 35625, 35968, 36140,36826, 37340, 37854, 38197, 38368, 38711, 38883, 39569, 
40083, 40597, 40940, 41111, 41454, 41625, 42311, 42825,43339, 43682, 43853, 44196, 44368, 45054, 45568, 46082, 
46425, 46596, 46939, 47111, 47797, 48311, 48825, 49168,49339, 49682, 49854, 50540, 51054, 51568, 51911, 52082, 
52425, 99225, 99911, 100425,100939,101282,101453,101796,101968,102654,
103168,103682,104025,104196,104539,104711,105397,105911,
106425,106768,106939,107282,107454,108140,108654,109168,
109511,109682,110025
                    };
                

            foreach(var hitObject in Beatmap.HitObjects){

                if(!(
                    (hitObject.StartTime >= 33397 && hitObject.StartTime < 55340) ||
                    (hitObject.StartTime >= 99225 && hitObject.StartTime < 110197)
                    )) continue;

                foreach(var p in pattern){
                    if(Math.Abs(hitObject.StartTime - p) < 100){
                        for(int i = 0; i < 7; i++){

                            var angle = Random(-Math.PI * 2, Math.PI * 2);
                            var rad = Random(20, 100);

                            var Pos = hitObject.Position + new Vector2((float)(Math.Cos(angle) * (rad)), (float)(Math.Sin(angle) * (rad)));

                            var p3 = diffLayer.CreateSprite("sb/p.png");
                            p3.Move(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + 2000, hitObject.Position, Pos);
                            p3.Scale(hitObject.StartTime, Random( 2, 8));
                            p3.Fade(hitObject.StartTime, hitObject.StartTime + 100, 0 , 0.7);
                            p3.Fade(hitObject.StartTime + 500, hitObject.StartTime + 2000, 0.7, 0);
                            p3.Color(hitObject.StartTime, hitObject.Color);
                            p3.Rotate(hitObject.StartTime, angle);
                            p3.Additive(hitObject.StartTime, hitObject.StartTime + 2000);

                        }
                    }
                }                
            }
        }

        void hit(){
            var diffLayer = GetLayer("part a");

            foreach(var hitObject in Beatmap.HitObjects){

                if(!(
                    (hitObject.StartTime >= 110197 && hitObject.StartTime < 118425) 
                    )) continue;

                var p = diffLayer.CreateSprite("sb/p.png", OsbOrigin.Centre, hitObject.Position);

                p.ScaleVec(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + 2000, 10, 0, 10, 1080);
                p.Fade(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + 2000, 1, 0);
                p.Color(hitObject.StartTime, hitObject.Color);
                p.Rotate(hitObject.StartTime, Random(-Math.PI/100, Math.PI/100));


            }
        }

        void particlesRandom(StoryboardLayer layer, int numOfParticles, int pStartTime, int pEndTime, Color4 particleColor, string path){
           
            for(int i = 0; i < numOfParticles; i ++){


                int randomX = Random(-100, 740);
                int randomY = Random(0, 480);
                int distance = Random(10, 100);
                double angle = Random(-Math.PI, Math.PI);

                Vector2 Newpos = new Vector2(randomX, randomY) + new Vector2((float)(Math.Cos(angle) * distance), (float) (Math.Sin(angle) * distance));

                var p = layer.CreateSprite(path); 

                int durationStart = Random(pStartTime, pEndTime - 5000);
                int durationEnd = durationStart + Random(5000, 10000);
                double Scale = 0;
                if(i % 10 == 0){
                    Scale = Random(0.25, 0.5);
                }else{
                    Scale = Random(0.07, 0.12);
                }
                
                p.Move(durationStart, durationEnd, new Vector2(randomX, randomY), Newpos);
                p.Scale(durationStart, Scale);
                p.Fade(durationStart, durationStart + 1000, 0, 0.6);

                if(durationEnd >= pEndTime){
                    p.Fade(pEndTime - 1000, pEndTime, 0.6, 0);
                }else{
                    p.Fade(durationEnd - 1000, durationEnd, 0.6, 0);
                }
                
                p.Color(durationStart, particleColor);
                p.Additive(durationStart, durationEnd);

            }
        }
    }
}
