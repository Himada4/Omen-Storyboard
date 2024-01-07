using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections;
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

            introShapes(mono);
		    
            var blur = bgLayer.CreateSprite("sb/blur.jpg");
            blur.Scale(11454, 480.0f / bit.Height);
            blur.Fade(11454, 0.7);
            blur.Fade(55340, 0);
            WiggleScreen2(11454, 55340, 50, new Vector2(320, 240), 0, 2, blur);
            
        }

        void introShapes(StoryboardLayer layer){

            var triangle = new List<OsbSprite>(){layer.CreateSprite("sb/t.png",OsbOrigin.Centre, new Vector2(320, 242)), layer.CreateSprite("sb/t.png",OsbOrigin.Centre, new Vector2(320, 242))};
            var circle = new List<OsbSprite>(){layer.CreateSprite("sb/c.png"), layer.CreateSprite("sb/c.png")};
            var sqare = new List<OsbSprite>(){layer.CreateSprite("sb/p.png"), layer.CreateSprite("sb/p.png")};
            var cross = new List<OsbSprite>(){layer.CreateSprite("sb/p.png"), layer.CreateSprite("sb/p.png")};

            List<int> pluck = new List<int>(){
                483, 1168, 1340, 1511, 1682, 
                3225, 3911, 4083, 4254, 4425, 4768, 4940, 5111, 5454, 5625, 5797,
                6654, 6825, 6997, 7168, 8025, 8197, 8368, 8540, 11454
            };

            int c = 0;
            foreach(var t in triangle){
                t.Scale(OsbEasing.OutExpo, 483, 1168, 0, c == 0 ? 0.15 : 0.135);
                if(c != 0) t.Color(483, Color4.Black);
                c++;
            }
            c = 0;
            WiggleScreen(483, 11454, 150, new Vector2(320, 240), 0, 0.5f, triangle[0], triangle[1]);
            
            
            foreach(var t in circle){
                t.Scale(1168, c == 0 ? 0.05 : 0.045);
                if(c != 0) t.Color(1168, Color4.Black);
                c++;
            }
            c = 0;
            WiggleScreen(1168, 11454, 150, new Vector2(320, 240), 0, 0.5f, circle[0], circle[1]);
                    
                   
            foreach(var t in sqare){
                t.Scale(1340, c == 0 ? 15 : 14);
                if(c != 0) 
                    t.Color(1340, Color4.Black);
                //t.Rotate(1340, Math.PI/4);
                c++;
            }
            c = 0;
            WiggleScreen(1340, 11454, 150, new Vector2(320, 240), Math.PI/4, 0.5f, sqare[0], sqare[1]);
            
            
            foreach(var t in cross){
                t.ScaleVec(1511, 1, 18);
                //t.Rotate(1511, c == 0 ? -Math.PI/4 : Math.PI/4);
                t.ScaleVec(OsbEasing.InExpo, 8540, 11454, 1, 18, 1080, 480);
                
                c++;
            }
            WiggleScreen(1511, 11454, 150, new Vector2(320, 240), -Math.PI/4, 0.5f, cross[0]);
            WiggleScreen(1511, 11454, 150, new Vector2(320, 240), Math.PI/4, 0.5f, cross[1]);
            

            int count = 0;
            for(var i = 0; i < pluck.Count - 1; i ++){
                switch(count){
                    case 0:
                        foreach(var t in triangle){
                            t.Fade(pluck[i], 1);
                            t.Fade(pluck[i + 1], 0);
                        }
                    break;
                    case 1:
                        foreach(var t in circle){
                            t.Fade(pluck[i], 1);
                            t.Fade(pluck[i + 1], 0);
                        }
                    break;
                    case 2:
                        foreach(var t in sqare){
                            t.Fade(pluck[i], 1);
                            t.Fade(pluck[i + 1], 0);
                        }
                    break;
                    case 3:
                        foreach(var t in cross){
                            if(i == pluck.Count-2){
                                t.Fade(pluck[i], 1);
                                t.Fade(OsbEasing.OutExpo, pluck[i + 1],pluck[i + 1] + 600, 1, 0);
                            }else{
                                t.Fade(pluck[i], 1);
                                t.Fade(pluck[i + 1], 0);
                            }
                            
                        }
                    break;
                }
                if(count == 3) count = 0;
                else count++;
            }
        }

        public void WiggleScreen(double startTime, double endTime, int rate, Vector2 InitPos, double InitRot, float wiggleAmount, params OsbSprite[] sprites){

            //Rate average around 20 to 100 (bigger number, more shakes)

            var loopTime = (endTime-startTime)/rate;

            var previousCord = new Vector2(InitPos.X,InitPos.Y);

            for(int i = 0; i < rate - 1; i++){

                var xCord = Random(InitPos.X-wiggleAmount,InitPos.X+wiggleAmount);

                var yCord = Random(InitPos.Y-wiggleAmount,InitPos.Y+wiggleAmount);

                var tempCord = new Vector2(xCord,yCord);
                
                foreach(var sprite in sprites){
                    sprite.Move(OsbEasing.InOutSine,startTime+(loopTime*i),startTime+(loopTime*(i+1)),previousCord,tempCord);
                }
            
                //Log($"{startTime+(loopTime*i)} until {startTime+(loopTime*(i+1))}");

                previousCord = tempCord;
                
            }

            double previousRotation = InitRot;

            for(int i = 0; i < (rate - 1)/2; i++){

                double[] rotate = new double[]{
                    0.01 // , 0.02, 0.03 // Add these for more rotations
                };

                var rotInd = Random(0,rotate.Length);

                var tempRot = previousRotation - rotate[rotInd];

                foreach(var sprite in sprites){
                sprite.Rotate(OsbEasing.InOutSine,startTime+(2*loopTime*i),startTime+(2*loopTime*(i+1)),previousRotation,tempRot);
                }

                previousRotation = tempRot;

            }

            foreach(var sprite in sprites){

                sprite.Rotate(OsbEasing.InOutSine, startTime + loopTime * (rate - 1), startTime +  loopTime * rate, previousRotation, 0);
                sprite.Move(OsbEasing.InOutSine,  startTime + loopTime * (rate - 1),  startTime + loopTime * rate , previousCord, InitPos);

            }

        }

        public void WiggleScreen2(double startTime, double endTime, int rate, Vector2 InitPos, double InitRot, float wiggleAmount, params OsbSprite[] sprites){

            //Rate average around 20 to 100 (bigger number, more shakes)

            var loopTime = (endTime-startTime)/rate;

            var previousCord = new Vector2(InitPos.X,InitPos.Y);

            for(int i = 0; i < rate - 1; i++){

                var xCord = Random(InitPos.X-wiggleAmount,InitPos.X+wiggleAmount);

                var yCord = Random(InitPos.Y-wiggleAmount,InitPos.Y+wiggleAmount);

                var tempCord = new Vector2(xCord,yCord);
                
                foreach(var sprite in sprites){
                    sprite.Move(OsbEasing.InOutSine,startTime+(loopTime*i),startTime+(loopTime*(i+1)),previousCord,tempCord);
                }
            
                //Log($"{startTime+(loopTime*i)} until {startTime+(loopTime*(i+1))}");

                previousCord = tempCord;
                
            }

            double previousRotation = InitRot;

            for(int i = 0; i < (rate - 1)/2; i++){

                double[] rotate = new double[]{
                    0.01 // , 0.02, 0.03 // Add these for more rotations
                };

                var rotInd = Random(0,rotate.Length);

                var tempRot = rotate[rotInd];

                foreach(var sprite in sprites){
                sprite.Rotate(OsbEasing.InOutSine,startTime+(2*loopTime*i),startTime+(2*loopTime*(i+1)),previousRotation,tempRot);
                }

                previousRotation = tempRot;

            }

            foreach(var sprite in sprites){

                sprite.Rotate(OsbEasing.InOutSine, startTime + loopTime * (rate - 1), startTime +  loopTime * rate, previousRotation, 0);
                sprite.Move(OsbEasing.InOutSine,  startTime + loopTime * (rate - 1),  startTime + loopTime * rate , previousCord, InitPos);

            }

        }
    }
}
