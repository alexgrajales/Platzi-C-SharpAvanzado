using EmotionPlatziWeb.Models;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.Reflection;

namespace EmotionPlatziWeb.Util
{
    public class EmotionHelper
    {
        public EmotionServiceClient emoClient;
        public  EmotionHelper(string Key)
        {
            emoClient = new EmotionServiceClient(Key);
        }

        public async void DetectAndExtracFaces(Stream imageStream)
        {
            Emotion[] emotions = await emoClient.RecognizeAsync(imageStream);
            
            var emoPicture = new EmoPicture();
            emoPicture.EmoFaces = ExtractFaces(emotions, emoPicture);
        }

    private ObservableCollection<EmoFace> ExtractFaces(Emotion[] emotions, EmoPicture emopicture)
    {
        emopicture.EmoFaces = new ObservableCollection<EmoFace>();
        foreach (var emotion in emotions)
        {
                var emoface = new EmoFace()
                {
                    X = emotion.FaceRectangle.Left,
                    Y = emotion.FaceRectangle.Top,
                    Width = emotion.FaceRectangle.Width,
                    Height = emotion.FaceRectangle.Height,
                };                

                emoface.EmoEmotions = ProcessEmotions(emotion.Scores, emoface);
                emopicture.EmoFaces.Add(emoface);
        }
            return emopicture.EmoFaces;
    }

        private ObservableCollection<EmoEmotion> ProcessEmotions(Scores scores, EmoFace emoface)
        {
            var emotionList = new ObservableCollection<EmoEmotion>();
            var properties = scores.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //var filterproperties = properties.Where(p => p.PropertyType == typeof(float));
            var filterproperties = from p in properties
                                   where p.PropertyType == typeof(float)
                                   select p;

            var emotype = EmoEMotionEnum.UNdetermined;
            foreach (var prop in filterproperties)
            {
                if (!Enum.TryParse<EmoEMotionEnum>(prop.Name, out emotype))
                    emotype = EmoEMotionEnum.UNdetermined;
                var emoEmotion = new EmoEmotion();
                emoEmotion.Score = (float)prop.GetValue(scores);
                emoEmotion.EMotionType = emotype;
                emoEmotion.Face = emoface;
                emotionList.Add(emoEmotion);
            }
            return emotionList;
        }
    }
}