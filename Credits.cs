using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace StorybrewScripts
{
    public class Credits : StoryboardObjectGenerator
    {
        enum CreditType
        {
            Title,
            Name
        }

        FontGenerator Titles;

        FontGenerator Names;

        Vector2 titlePos = new Vector2(320, 240);
        Vector2 namePos = new Vector2(320, 240);

        double fade = 1;

        public override void Generate()
        {
		    Titles = LoadFont("sb/title", new FontDescription() {
                FontPath = "AssetLibrary/Title/GeneralSans-Regular.otf",
                FontSize = 100,
                Color = Color4.White,
                TrimTransparency = true,
                FontStyle = FontStyle.Regular,
                
            });
            
            Names = LoadFont("sb/names", new FontDescription() {
                FontPath = "AssetLibrary/Names/PilcrowRounded-Regular.otf",
                FontSize = 100,
                Color = Color4.White,
                TrimTransparency = true,
                FontStyle = FontStyle.Regular,
            });
            
            //For making diff dependent names
            
            CreditGenerator(
                CreditType.Name,
                483,
                3225,
                new List<string>(){"FREUDEMAN", "FREUDEMAN", "FREUDEMAN", "NYARVIS"},
                new List<string>(){"d-_-b", "Insane", "Normal", "nyarvis' Hard"},
                new Vector2(270, 178),
                0
            );

            CreditGenerator(
                CreditType.Name,
                483,
                3225,
                new List<string>(){"FREUDEMAN", "FREUDEMAN", "FREUDEMAN", "NYARVIS"},
                new List<string>(){"d-_-b", "Insane", "Normal", "nyarvis' Hard"},
                new Vector2(370, 302),
                Math.PI
            );
            

            //For making title
            CreditGenerator(
                CreditType.Title,
                new List<int>(){483, 3225, 5968, 8711}, 
                // ^ As you see below, there are only 3 Titles, but 4 timings, the 4th one is for the endTime for the 3rd Title. Make sure to add that!
                new List<string>(){"MAP", "HITSOUNDS", "STORYBOARD"},
                new Vector2(385, 189),
                Math.PI/2
            );

            CreditGenerator(
                CreditType.Title,
                new List<int>(){483, 3225, 5968, 8711}, 
                // ^ As you see below, there are only 3 Titles, but 4 timings, the 4th one is for the endTime for the 3rd Title. Make sure to add that!
                new List<string>(){"MAP", "HITSOUNDS", "STORYBOARD"},
                new Vector2(255, 291),
                -Math.PI/2
            );

            //For making rest of the names (Hitsounder's, and Storyboarder's)
            CreditGenerator(
                CreditType.Name,
                new List<int>(){3225, 5968, 8711}, 
                // ^ As you see below, there are only 2 Names, but 3 timings, the 3th one is for the endTime for the 2nd Name. Make sure to add that!
                new List<string>(){"FOSS","HIMADA"},
                new Vector2(270, 178),
                0
            );
            CreditGenerator(
                CreditType.Name,
                new List<int>(){3225, 5968, 8711}, 
                // ^ As you see below, there are only 2 Names, but 3 timings, the 3th one is for the endTime for the 2nd Name. Make sure to add that!
                new List<string>(){"FOSS","HIMADA"},
                new Vector2(370, 302),
                Math.PI
            );
            
        }

        //USE THIS WHEN GENERATING DYNAMIC CREDITS; DIFFERENT EFFECT PER DIFF(e.g Each diff have different mappers, etc);
        //Note that this is used for single case. (e.g Single use of this method will cover "Mapper's Name" Scene, but not for other scenes, so you have to
        //create those seperately, but probably just use the Static Credit Generation, since most of the time "HSer" Scene and "Storyboarder" Scenes
        // are the same throughout the set.)
        void CreditGenerator(CreditType CreditType, int startTime, int endTime, List<string> creditOptions, List<string> diffNames, Vector2 position, double rotation){
            
            if(CreditType == CreditType.Name){

                OsbSprite Name = null;

                for(int i = 0; i < diffNames.Count; i ++){

                    if(diffNames[i] == Beatmap.Name) 
                    Name = GetLayer("Credits").CreateSprite(Names.GetTexture(creditOptions[i]).Path, OsbOrigin.CentreLeft, position);
        
                }

                if(Name == null) return;

                Name.Fade(startTime - 200, startTime, 0, fade);
                Name.Fade(endTime - 200, endTime, fade, 0);
                Name.Scale(startTime - 200, 0.2);
                Name.Additive(startTime - 200, endTime);
                Name.Rotate(startTime - 200, rotation);

                int duration = endTime - startTime;
                Name.MoveY(OsbEasing.OutExpo, startTime - 200, startTime - 200 + (duration / 2), position.Y + 10, position.Y);
                Name.MoveY(OsbEasing.InExpo, startTime - 200 + (duration / 2), endTime, position.Y, position.Y - 10);
            }

            if(CreditType == CreditType.Title){ 
                
                OsbSprite Title = null;

                for(int i = 0; i < diffNames.Count; i ++){

                    if(diffNames[i] == Beatmap.Name) 
                    Title = GetLayer("Credits").CreateSprite(Titles.GetTexture(creditOptions[i]).Path, OsbOrigin.CentreLeft, titlePos);
        
                }

                Title.Fade(startTime - 200, startTime, 0, fade);
                Title.Fade(endTime - 200, endTime, fade, 0);
                Title.Scale(startTime - 200, 0.23);
                Title.Additive(startTime - 200, endTime);

                int duration = endTime - startTime;
                Title.MoveY(OsbEasing.OutExpo, startTime - 200, startTime - 200 + (duration / 2), titlePos.Y - 10, titlePos.Y);
                Title.MoveY(OsbEasing.InExpo, startTime - 200 + (duration / 2), endTime, titlePos.Y, titlePos.Y + 10);
            }
        }

        //USE THIS WHEN GENERATING STATIC CREDITS; SAME EFFECTS FOR ALL DIFFS(e.g Titles, Same Credit Throughout Set, etc.);
        void CreditGenerator(CreditType CreditType, List<int> creditStartTimes, List<string> creditContents, Vector2 position, double rotation){
            if(CreditType == CreditType.Name){

                for(int i  = 0; i < creditContents.Count; i ++){
                    var Name = GetLayer("Credits").CreateSprite(Names.GetTexture(creditContents[i]).Path, OsbOrigin.CentreLeft, position);
                    
                    Name.Fade(creditStartTimes[i] - 200, creditStartTimes[i], 0, fade);
                    Name.Fade(creditStartTimes[i + 1] - 200, creditStartTimes[i + 1], fade, 0);
                    Name.Scale(creditStartTimes[i] - 200, 0.2);
                    Name.Additive(creditStartTimes[i] - 200, creditStartTimes[i + 1]);
                    Name.Rotate(creditStartTimes[i] - 200, rotation);

                    int duration = creditStartTimes[i + 1] - creditStartTimes[i] - 200;
                    Name.MoveY(OsbEasing.OutExpo, creditStartTimes[i] - 200, creditStartTimes[i] - 200 + (duration / 2), position.Y + 10, position.Y);
                    Name.MoveY(OsbEasing.InExpo, creditStartTimes[i] - 200 + (duration / 2), creditStartTimes[i + 1], position.Y, position.Y - 10);

                }
            }

            if(CreditType == CreditType.Title){

                for(int i  = 0; i < creditContents.Count; i ++){
                    var Title = GetLayer("Credits").CreateSprite(Titles.GetTexture(creditContents[i]).Path, OsbOrigin.CentreLeft, position);
                    
                    Title.Fade(creditStartTimes[i] - 200, creditStartTimes[i], 0, fade);
                    Title.Fade(creditStartTimes[i + 1] - 200, creditStartTimes[i + 1], fade, 0);
                    Title.Scale(creditStartTimes[i] - 200, 0.23);
                    Title.Additive(creditStartTimes[i] - 200, creditStartTimes[i + 1]);
                    Title.Rotate(creditStartTimes[i] - 200, rotation);
                    
                    int duration = creditStartTimes[i + 1] - creditStartTimes[i] - 200;
                    Title.MoveY(OsbEasing.OutExpo, creditStartTimes[i] - 200, creditStartTimes[i] - 200 + (duration / 2), position.Y - 10, position.Y);
                    Title.MoveY(OsbEasing.InExpo, creditStartTimes[i] - 200 + (duration / 2), creditStartTimes[i + 1], position.Y, position.Y + 10);
                }
            } 
        }
    }
}