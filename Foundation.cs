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
            GetLayer("REMOVEBG").CreateSprite(Beatmap.BackgroundPath).Fade(0,0);
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

            p.Fade(164540 + 483, 1);
            p.Fade(164540 + 11454, 0);
            p.Scale(OsbEasing.OutExpo, 164540 + 483, 164540 + 1168, 0, 50);
            p.Rotate(OsbEasing.OutExpo, 164540 + 483, 164540 + 1168, Math.PI/2, 0);

            pb.Fade(164540 + 483, 1);
            pb.Fade(164540 + 11454, 0);
            pb.Scale(OsbEasing.OutExpo, 164540 + 483, 164540 + 1168, 0, 48);
            pb.Rotate(OsbEasing.OutExpo, 164540 + 483, 164540 + 1168, Math.PI/2, 0);
            pb.Color(164540 + 483, Color4.Black);

            var list = new List<OsbSprite>(){p, pb};

            foreach(var s in list){
                s.Scale(OsbEasing.InExpo, 8711, 11454, s.ScaleAt(8711).X, s.ScaleAt(8711).X + 1000);
                s.Scale(OsbEasing.InExpo, 164540 + 8711, 164540 + 11454, s.ScaleAt(164540 + 8711).X, 0);
            }

            

            var bgLayer = GetLayer("BG");
            var bg = bgLayer.CreateSprite(Beatmap.BackgroundPath);
            var bit = GetMapsetBitmap(Beatmap.BackgroundPath);
            bg.Scale(55340, 480.0f / bit.Height);
            bg.Fade(55340, 1);
            bg.Fade(77282, 0);
            bg.Fade(121168, 1);
            bg.Fade(143111, 0);
            
            bg.Fade(145854, 148597, 1, 0);
            
            bg.StartLoopGroup(148597, 6);
            bg.Fade(0, 151340 - 148597, 0.2, 0);
            bg.EndGroup();
            
             
            //143454 143968 144483 144825 144997 145168 145340 145511 145682
            bg.Fade(143454, 143968, 0.2, 0);
            bg.Fade(143968, 144483, 0.2, 0);
            bg.Fade(144483, 144825, 0.2, 0);
            bg.Fade(144825, 144997, 0.1, 0);

            bg.Fade(144997, 145168, 0.05, 0);
            bg.Fade(145168, 145340, 0.1, 0);
            bg.Fade(145340, 145511, 0.1, 0);
            bg.Fade(145511, 145682, 0.1, 0);
            
            bg.Fade(145682, 145854, 0.2, 0);

            bg.Additive(55340, 77282);
            bg.Additive(121168, 143111);
            var blackBg = GetLayer("black addtive").CreateSprite("sb/additive.png");
            blackBg.Fade(143111, 143111, 1, 0);
            blackBg.Scale(121168, 480.0f / bit.Height);
            blackBg.Fade(145854, 1);
            blackBg.Fade(165054, 0);
            WiggleScreen2(55340, 77282, 25, new Vector2(320 , 240), 0, 2, bg);
            WiggleScreen2(121168, 143111, 25, new Vector2(320 , 240), 0, 2, bg, blackBg);
            

            introShapes(mono);
		    
            var blur = bgLayer.CreateSprite("sb/blur.jpg");
            blur.Scale(11454, 480.0f / bit.Height);
            blur.Fade(11454, 0.7);
            blur.Fade(55340, 0);
            WiggleScreen2(11454, 55340, 50, new Vector2(320, 240), 0, 2, blur);
            blur.Fade(99225, 0.7);
            WiggleScreen2(99225, 121168, 25, new Vector2(320, 240), 0, 2, blur);
            blur.Fade(121168, 0);

            blur.Fade(143111, 0.7);
            blur.Fade(165054, 0);
            blur.Additive(143111, 165054);

            outroShapes(mono);
            synthWave(mono);
            chorus1(GetLayer("chrous"), 55340, 77282, false);
            chorus1(GetLayer("chrous"), 121168, 143111, true);


            var vig = GetLayer("vig").CreateSprite("sb/vig.png");
            var vigbit = GetMapsetBitmap("sb/vig.png");
            vig.Fade(0, 0.8);
            vig.Fade(AudioDuration, 0);
            vig.Scale(0, 480.0f / vigbit.Height);
            
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

        void outroShapes(StoryboardLayer layer){

            var triangle = new List<OsbSprite>(){layer.CreateSprite("sb/t.png",OsbOrigin.Centre, new Vector2(320, 242)), layer.CreateSprite("sb/t.png",OsbOrigin.Centre, new Vector2(320, 242))};
            var circle = new List<OsbSprite>(){layer.CreateSprite("sb/c.png"), layer.CreateSprite("sb/c.png")};
            var sqare = new List<OsbSprite>(){layer.CreateSprite("sb/p.png"), layer.CreateSprite("sb/p.png")};
            var cross = new List<OsbSprite>(){layer.CreateSprite("sb/p.png"), layer.CreateSprite("sb/p.png")};

            List<int> pluck = new List<int>(){
                483, 1168, 1340, 1511, 1682, 
                3225, 3911, 4083, 4254, 4425, 4768, 4940, 5111, 5454, 5625, 5797,
                6654, 6825, 6997, 7168, 8025, 8197, 8368, 8540, 11454
            };

            for(int i = 0; i < pluck.Count; i++) pluck[i] += 164540;

            int c = 0;
            foreach(var t in triangle){
                t.Scale(OsbEasing.OutExpo, 164540 + 483, 164540 + 1168, 0, c == 0 ? 0.15 : 0.135);
                if(c != 0) t.Color(164540 + 483, Color4.Black);
                c++;
            }
            c = 0;
            WiggleScreen(164540 + 483, 164540 + 11454, 150, new Vector2(320, 240), 0, 0.5f, triangle[0], triangle[1]);
            
            
            foreach(var t in circle){
                t.Scale(164540 + 1168, c == 0 ? 0.05 : 0.045);
                if(c != 0) t.Color(164540 + 1168, Color4.Black);
                c++;
            }
            c = 0;
            WiggleScreen(164540 + 1168, 164540 + 11454, 150, new Vector2(320, 240), 0, 0.5f, circle[0], circle[1]);
                    
                   
            foreach(var t in sqare){
                t.Scale(164540 + 1340, c == 0 ? 15 : 14);
                if(c != 0) 
                    t.Color(164540 + 1340, Color4.Black);
                //t.Rotate(1340, Math.PI/4);
                c++;
            }
            c = 0;
            WiggleScreen(164540 + 1340, 164540 + 11454, 150, new Vector2(320, 240), Math.PI/4, 0.5f, sqare[0], sqare[1]);
            
            
            foreach(var t in cross){
                t.ScaleVec(164540 + 1511, 1, 18);
                //t.Rotate(1511, c == 0 ? -Math.PI/4 : Math.PI/4);
                t.ScaleVec(OsbEasing.InExpo, 164540 + 8540, 164540 + 11454, 1, 18, 1, 0);
                
                c++;
            }
            WiggleScreen(164540 + 1511, 164540 + 11454, 150, new Vector2(320, 240), -Math.PI/4, 0.5f, cross[0]);
            WiggleScreen(164540 + 1511, 164540 + 11454, 150, new Vector2(320, 240), Math.PI/4, 0.5f, cross[1]);
            

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

        void synthWave(StoryboardLayer layer){
            var startTime = 77282;
            var endTime = 99225;
            var numOfLoops = 5;
            var perLoop = (endTime - startTime) / numOfLoops;
            var numOfBars = 10;
            var totalBars = numOfLoops * numOfBars;
            var barDelay = perLoop / numOfBars;
            
            
            

           
            

            

            for(int i = 0; i < totalBars; i ++){

                var line = layer.CreateSprite("sb/line.png");
                line.MoveY(OsbEasing.InQuart, startTime + barDelay * i, startTime + barDelay * i + perLoop, 240 + 20, 480);
                line.Rotate(startTime + barDelay * i, Math.PI/2);

                if(i < 41){
                    line.Fade(startTime + barDelay * i, startTime + barDelay * i + perLoop * 0.7, 0, 1);
                }else{
                    line.Fade(startTime + barDelay * i, endTime, 0, 1);
                    line.Fade(endTime, endTime, 1, 0);
                }

            }
            
            /*
            for(int i = 0; i < numOfBars; i ++){
                var line = layer.CreateSprite("sb/line.png");
                //line.Fade(FadeInTime, FadeInTime, 0, 1);
                //line.Fade(endTime, 0);
                line.Rotate(startTime + barDelay * i, Math.PI/2);

                line.StartLoopGroup(startTime + barDelay * i, numOfLoops);
                line.MoveY(OsbEasing.InQuart, 0, perLoop, 240 - 20, 0);
                line.Fade(0, perLoop * 0.7, 0, 1);
                line.EndGroup();
            }
            */

            /*
            for(int i = 0; i < numOfBars; i ++){
                var line = layer.CreateSprite("sb/line.png");
                //line.Fade(FadeInTime, FadeInTime, 0, 1);
                line.Fade(endTime, 0);
                line.Rotate(startTime + barDelay * i, Math.PI/2);

                line.StartLoopGroup(startTime + barDelay * i, numOfLoops);
                line.MoveY(OsbEasing.InQuart, 0, perLoop, 240 + 20, 480);
                line.Fade(0, perLoop * 0.7, 0, 1);
                line.EndGroup();
            }
            */

            var black = layer.CreateSprite("sb/p.png");
            black.ScaleVec(77282, 1080, 480);
            black.Fade(77282, 1);
            black.Fade(82768, 82768 + 500, 0.6, 0);
            black.Color(77282, Color4.Black);
            black.Color(82768, Color4.White);
            black.Fade(99225, 99225 + 500, 0.6, 0);
            black.Color(55340, Color4.White);
            black.Fade(55340, 55340 + 500, 0.6, 0);
            black.Fade(121168, 121168 + 500, 0.6, 0);




            // Define the number of vertical lines and their properties
            int numVerticalLines = 16;
            double angleStep = Math.PI / numVerticalLines;
            double perLine = 640 / numVerticalLines;

            var h = layer.CreateSprite("sb/p.png", OsbOrigin.CentreLeft, new Vector2(-106, 260));
            h.ScaleVec(OsbEasing.OutExpo, 77282, 79340, 0, 0.5, 640, 0.5);
            h.Fade(OsbEasing.InExpo, 81397, 82768, 1, 0);

            // Loop through each vertical line
            for (int i = 0; i < numVerticalLines; i++)
            {
                // Create a sprite for the line
                var angle = -angleStep * i + Math.PI/2;
                if(angle > 1.5) continue;
                var lineSprite = layer.CreateSprite("sb/p.png", OsbOrigin.TopCentre, new Vector2((float)(perLine * i), 240 + 20));
                lineSprite.ScaleVec(OsbEasing.InExpo, 77282, 82768, new Vector2(0.5f,0), new Vector2(0.5f, 480));
                lineSprite.Rotate(82768, angle);
                lineSprite.Fade(99225, 99225, 1, 0);
                
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

        void chorus1(StoryboardLayer layer, int startTime, int endTime, bool pulse){


            
            int totalDuration = endTime - startTime;
            int numOfLoops = 3;
            float LoopDuration = totalDuration / numOfLoops;
            

            double numOfShapes = 24;
            double perShape = 1000 / numOfShapes;
            var posX = new List<double>();
            for(int i = 0; i < numOfShapes; i ++){
                posX.Add(-120 + perShape * i);
            }
            var unitDuration = LoopDuration / (posX.Count - 1);
            var delay = 0;
            var beat = 122540 - 121168; 

            for(int i = 0; i < numOfShapes; i ++){
                if(i == 0) continue;
                
                OsbSprite p = null;
                OsbSprite pb = null;

                switch(i % 4){
                    case 0:
                        p = layer.CreateSprite("sb/t.png");
                        p.Scale(startTime, 0.15);
                        p.Rotate(startTime, endTime, 0, Math.PI);

                        pb = layer.CreateSprite("sb/t.png");
                        pb.Color(startTime, Color4.Black);
                        if(!pulse)pb.Scale(startTime, 0.135);
                        else{
                            pb.StartLoopGroup(startTime, 16);
                            pb.Scale(OsbEasing.OutExpo, 0, beat, 0.12, 0.135);
                            pb.EndGroup();
                        }
                        pb.Rotate(startTime, endTime, 0, Math.PI);

                    break;
                    case 1:
                        p = layer.CreateSprite("sb/c.png");
                        p.Scale(startTime, 0.05);
                        p.Rotate(startTime, endTime, 0, Math.PI);

                        pb = layer.CreateSprite("sb/c.png");
                        pb.Color(startTime, Color4.Black);
                        if(!pulse)pb.Scale(startTime, 0.045);
                        else{
                            pb.StartLoopGroup(startTime, 16);
                            pb.Scale(OsbEasing.OutExpo, 0, beat, 0.02, 0.045);
                            pb.EndGroup();
                        }
                        pb.Rotate(startTime, endTime, 0, Math.PI);
                        
                    break;
                    case 2:
                        p = layer.CreateSprite("sb/p.png");
                        p.Scale(startTime, 15);
                        p.Rotate(startTime, endTime, 0, Math.PI);

                        pb = layer.CreateSprite("sb/p.png");
                        pb.Color(startTime, Color4.Black);
                        if(!pulse)pb.Scale(startTime, 14);
                        else{
                            pb.StartLoopGroup(startTime, 16);
                            pb.Scale(OsbEasing.OutExpo, 0, beat, 11, 14);
                            pb.EndGroup();
                        }
                        pb.Rotate(startTime, endTime, 0, Math.PI);
                    break;
                    case 3:
                        p = layer.CreateSprite("sb/p.png");
                        if(!pulse)p.ScaleVec(startTime, 1, 18);
                        else{
                            p.StartLoopGroup(startTime, 16);
                            p.ScaleVec(OsbEasing.OutExpo, 0, beat, 1, 14, 1, 18);
                            p.EndGroup();
                        }
                        p.Rotate(startTime, endTime, Math.PI/4, Math.PI/4 + Math.PI);

                        pb = layer.CreateSprite("sb/p.png");
                        if(!pulse)pb.ScaleVec(startTime, 1, 18);
                        else{
                            pb.StartLoopGroup(startTime, 16);
                            pb.ScaleVec(OsbEasing.OutExpo, 0, beat, 1, 14, 1, 18);
                            pb.EndGroup();
                        }
                        pb.Rotate(startTime, endTime, -Math.PI/4, -Math.PI/4 + Math.PI);
                    break;
                }

                p.Fade(startTime, startTime, 0, 1);
                p.Fade(endTime, 0);
                //if(i == numOfShapes - 1) p.Color(startTime, Color4.Red);
                

                var first = i * unitDuration;
                var second = LoopDuration - first;
            

                p.StartLoopGroup(startTime, numOfLoops);
                p.MoveX(0, first, posX[i], posX[0]);
                p.MoveX(first, first + second, posX[posX.Count - 1], posX[i]);
                p.EndGroup();

                p.StartLoopGroup(startTime + delay - LoopDuration, numOfLoops * 6);
                p.MoveY(OsbEasing.InOutSine, 0, LoopDuration / 8, 100, 150);
                p.MoveY(OsbEasing.InOutSine, LoopDuration / 8, LoopDuration / 4, 150, 100);
                p.EndGroup();


                pb.Fade(startTime, startTime, 0, 1);
                pb.Fade(endTime, 0);
                //if(i == numOfShapes - 1) p.Color(startTime, Color4.Red);
                

                pb.StartLoopGroup(startTime, numOfLoops);
                pb.MoveX(0, first, posX[i], posX[0]);
                pb.MoveX(first, first + second, posX[posX.Count - 1], posX[i]);
                pb.EndGroup();

                pb.StartLoopGroup(startTime + delay - LoopDuration, numOfLoops * 6);
                pb.MoveY(OsbEasing.InOutSine, 0, LoopDuration / 8, 100, 150);
                pb.MoveY(OsbEasing.InOutSine, LoopDuration / 8, LoopDuration / 4, 150, 100);
                pb.EndGroup();

                delay += 150;
                
            }

            
            

        }
    }
}
