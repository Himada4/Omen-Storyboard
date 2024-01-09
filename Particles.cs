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
            chorus();
            particleBottomToUp(GetLayer("part"), 100, 119968, 143111, Color4.Pink);
            secondKiai(GetLayer("part"), 100, 66311, 77282, Color4.Pink);
            secondKiai(GetLayer("part"), 100, 132140, 143111, Color4.Pink);
        }

        void secondKiai(StoryboardLayer layer, int numOfParticles, int pStartTime, int pEndTime, Color4 particleColor){

            for(int i = 0; i < numOfParticles; i ++){


                int randomX = Random(-100, 740);
                int randomY = Random(0, 480);
                int distance = Random(10, 100);
                double angle = Random(-Math.PI, Math.PI);

                var p = layer.CreateSprite("sb/particle.png");

                int durationStart = Random(pStartTime, pEndTime - 5000 / 2);
                int durationEnd = durationStart + Random(5000 / 2, 10000 / 2);
                double Scale = 0;
                if(i % 10 == 0){
                    Scale = Random(0.5 / 3, 1 / 3);
                }else{
                    Scale = Random(0.15 / 3, 0.35 / 3);
                }

                var scales = new List<double>();
                for(int k = 0; k < 4; k ++){
                    double temp = 0;
                    if(i % 10 == 0){
                        temp = Random(0.5 / 1.5, 1 / 1.5);
                    }else{
                        temp = Random(0.15 / 1.5, 0.35 / 1.5);
                    }
                    scales.Add(temp);
                }

                var j = 1;
                var perDuration = 2000;
                
                p.MoveY(durationStart, durationEnd, 500, Random(200, 240));
                p.StartLoopGroup(durationStart, (durationEnd - durationStart) / perDuration);
                var dir = Random(10, 20) * j;
                p.MoveX(OsbEasing.InOutSine, 0, perDuration/2, randomX, randomX + dir);
                p.MoveX(OsbEasing.InOutSine, perDuration/2, perDuration, randomX + dir, randomX);
                p.EndGroup();

                var beat = 66483 - 66311;

                p.StartLoopGroup(pStartTime, ((pEndTime - pStartTime) / beat) / 4);
                p.Scale(0, scales[0]);
                p.Scale(beat, scales[1]);
                p.Scale(beat * 2, scales[2]);
                p.Scale(beat * 3, beat * 4, scales[3], scales[3]);
                p.EndGroup();

                
                p.Fade(durationStart, durationStart + 1000, 0, 0.6);

                if(durationEnd >= pEndTime){
                    p.Fade(pEndTime - 1000, pEndTime, 0.8, 0);
                }else{
                    p.Fade(durationEnd - 1000, durationEnd, 0.8, 0);
                }
                
                p.Color(durationStart, particleColor);
                p.Additive(durationStart, durationEnd);
                j = j * -1;

            }

        }

        void chorus(){
            
            /*
            string offsets = "";
            foreach(var h in Beatmap.HitObjects){
                offsets += h.StartTime + ", ";
            }

            Log(offsets);
            */

            var layer = GetLayer("part a");

            var pattern = new List<int>(){
                55340, 55854, 56025, 56540, 56883, 57054, 57397, 58083, 58597, 58768, 59283, 59626, 59797, 60140, 60311, 60825, 61339, 61510, 62025, 62539, 62882, 63568, 64082, 64253, 64768, 65111, 65282, 65625, 65797, 66311, 66825, 66996, 67511, 67854, 68025, 68368, 69054, 69568, 69739, 70254, 70597, 70768, 71111, 71282, 71796, 72310, 72481, 72996, 73340, 73510, 73853, 74539, 75053, 75224, 75739, 76082, 76253, 76596, 76768, 121168, 121511, 121682, 122025, 122197, 122540, 122882, 123140, 123397, 123568, 123740, 123911, 124254, 124425, 124768, 124940, 125283, 125625, 125883, 126139, 126311, 126483, 126653, 126997, 127167, 127511, 127683, 128025, 128367, 128625, 128883, 129054, 129225, 129396, 129740, 129910, 130254, 130425, 130768, 131111, 131368, 131625, 131797, 131968, 132139, 132140, 132483, 132654, 132997, 133169, 133512, 133854, 134112, 134369, 134540, 134712, 134883, 135226, 135397, 135740, 135912, 136255, 136597, 136855, 137111, 137283, 137455, 137625, 137969, 138139, 138483, 138655, 138997, 139339, 139597, 139855, 140026, 140197, 140368, 140712, 140882, 141226, 141397, 141740, 142083, 142340, 142597, 142769, 142940, 143111, 145854, 146197, 146368, 146711, 146883, 147226, 147568, 147826, 148083, 148254, 148426, 148597, 148940, 149111, 149454, 149626, 149969, 150311, 150569, 150825, 150997, 151169, 151339, 151683, 151853, 152197, 152369, 152711, 153053, 153311, 153569, 153740, 153911, 154082, 154426, 154596, 154940, 155111, 155454, 155797, 156054, 156311, 156483, 156654, 156825, 157168, 157339, 157682, 157854, 158197, 158539, 158797, 159054, 159225, 159397, 159568, 159911, 160082, 160425, 160597, 160940, 161282, 161540, 161796, 161968, 162140, 162310, 162654, 162824, 163168, 163340, 163511, 163682, 164024, 164197
            };

            foreach(var hitObject in Beatmap.HitObjects){

                if(!(
                    (hitObject.StartTime >= 55340 && hitObject.StartTime < 77282) ||
                    (hitObject.StartTime >= 121168 && hitObject.StartTime < 143111) ||
                    (hitObject.StartTime >= 145854 && hitObject.StartTime < 165054)
                    )) continue;

                foreach(var pa in pattern){
                    if(Math.Abs(hitObject.StartTime - pa) < 10){
                        for(int i = 0; i < 8; i++){

                            OsbSprite p = null;
                            OsbSprite pb = null;
                            var angle = Random(-Math.PI * 2, Math.PI * 2);

                            switch(i % 4){
                                case 0:
                                    p = layer.CreateSprite("sb/t.png");
                                    p.Scale(hitObject.StartTime, 0.07 / 1.5);
                                    p.Rotate(hitObject.StartTime, angle);
                                    

                                    pb = layer.CreateSprite("sb/t.png");
                                    pb.Color(hitObject.StartTime, Color4.Black);
                                    pb.Scale(hitObject.StartTime, 0.065 / 1.5);
                                    pb.Rotate(hitObject.StartTime, angle);
                                    pb.Fade(hitObject.StartTime, 1);
                                    pb.Fade(OsbEasing.InExpo, hitObject.StartTime + 500, hitObject.StartTime + 2000, 1, 0);
                                    

                                break;
                                case 1:
                                    p = layer.CreateSprite("sb/c.png");
                                    p.Scale(hitObject.StartTime, 0.025 / 1.5);
                                    p.Rotate(hitObject.StartTime, angle);
                                    

                                    pb = layer.CreateSprite("sb/c.png");
                                    pb.Color(hitObject.StartTime, Color4.Black);
                                    pb.Scale(hitObject.StartTime, 0.020 / 1.5);
                                    pb.Rotate(hitObject.StartTime, angle);
                                    pb.Fade(hitObject.StartTime, 1);
                                    pb.Fade(OsbEasing.InExpo, hitObject.StartTime + 500, hitObject.StartTime + 2000, 1, 0);

                                    

                                break;
                                case 2:
                                    p = layer.CreateSprite("sb/p.png");
                                    p.Scale(hitObject.StartTime, 7.5 / 1.5);
                                    p.Rotate(hitObject.StartTime, angle);
                                    

                                    pb = layer.CreateSprite("sb/p.png");
                                    pb.Color(hitObject.StartTime, Color4.Black);
                                    pb.Scale(hitObject.StartTime, 6.5 / 1.5);
                                    pb.Rotate(hitObject.StartTime, angle);
                                    pb.Fade(hitObject.StartTime, 1);
                                    pb.Fade(OsbEasing.InExpo, hitObject.StartTime + 500, hitObject.StartTime + 2000, 1, 0);
                                    

                                break;
                                case 3:
                                    p = layer.CreateSprite("sb/p.png");
                                    p.ScaleVec(hitObject.StartTime, 0.5, 9 / 1.5);
                                    p.Rotate(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + 2000, Math.PI/4, Math.PI/4 + Math.PI);
                                    

                                    pb = layer.CreateSprite("sb/p.png");
                                    pb.ScaleVec(hitObject.StartTime, 0.5, 9 / 1.5);
                                    pb.Rotate(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + 2000, -Math.PI/4, -Math.PI/4 + Math.PI);
                                    pb.Color(hitObject.StartTime, hitObject.Color);
                                    pb.Fade(hitObject.StartTime, hitObject.StartTime + 100, 0 , 0.7);
                                    pb.Fade(hitObject.StartTime + 500, hitObject.StartTime + 2000, 0.7, 0);

                                break;
                    
                            }
                            
                            var rad = Random(20, 60);
                            var Pos = hitObject.Position + new Vector2((float)(Math.Cos(angle) * rad), (float)(Math.Sin(angle) * rad));
                            p.Move(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + 2000, hitObject.Position, Pos);
                            p.Fade(hitObject.StartTime, hitObject.StartTime + 100, 0 , 0.7);
                            p.Fade(hitObject.StartTime + 500, hitObject.StartTime + 2000, 0.7, 0);
                            p.Color(hitObject.StartTime, hitObject.Color);

                            pb.Move(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + 2000, hitObject.Position, Pos);
                            
                        
                        }
                    }
                }  
            }
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
                
                        p3.Fade(hitObject.StartTime, hitObject.StartTime + 3000, 0.7, 0);
                        p3.Scale(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + 3000, 0, Random( 0.2, 0.5));
                        p3.Color(hitObject.StartTime, hitObject.Color);
                        p3.Additive(hitObject.StartTime, hitObject.StartTime + 3000);

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
    
        void particleBottomToUp(StoryboardLayer layer, int numOfParticles, int pStartTime, int pEndTime, Color4 particleColor){

            for(int i = 0; i < numOfParticles; i ++){


                int randomX = Random(-100, 740);
                int randomY = Random(0, 480);
                int distance = Random(10, 100);
                double angle = Random(-Math.PI, Math.PI);

                var p = layer.CreateSprite("sb/particle.png");

                int durationStart = Random(pStartTime, pEndTime - 5000 / 2);
                int durationEnd = durationStart + Random(5000 / 2, 10000 / 2);
                double Scale = 0;
                if(i % 10 == 0){
                    Scale = Random(0.5 / 3, 1 / 3);
                }else{
                    Scale = Random(0.15 / 3, 0.35 / 3);
                }

                var j = 1;
                var perDuration = 2000;
                
                p.MoveY(durationStart, durationEnd, 500, Random(200, 240));
                p.StartLoopGroup(durationStart, (durationEnd - durationStart) / perDuration);
                var dir = Random(10, 20) * j;
                p.MoveX(OsbEasing.InOutSine, 0, perDuration/2, randomX, randomX + dir);
                p.MoveX(OsbEasing.InOutSine, perDuration/2, perDuration, randomX + dir, randomX);
                p.EndGroup();
                p.Scale(durationStart, Scale);
                p.Fade(durationStart, durationStart + 1000, 0, 0.6);

                if(durationEnd >= pEndTime){
                    p.Fade(pEndTime - 1000, pEndTime, 0.8, 0);
                }else{
                    p.Fade(durationEnd - 1000, durationEnd, 0.8, 0);
                }
                
                p.Color(durationStart, particleColor);
                p.Additive(durationStart, durationEnd);
                j = j * -1;

            }
        }
    
    }
}
