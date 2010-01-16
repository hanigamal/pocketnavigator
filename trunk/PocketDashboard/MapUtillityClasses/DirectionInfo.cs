using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Net;
using System.Xml.Serialization;
using System.Xml;
namespace MapUtilities
{
    [Serializable]
    public class DirectionInfo
    {
        public SerializableList<LatLng> Points;
        public SerializableList<DirectionStep> Steps;
        public SerializableList<StaticMapOverLay> Maps;
        //Image[] OnePolyLineDrawnImages;
        string key=null;
        MapsUtils util = null;
        bool isLostWay = true;
        bool isArrived = false;
#if SPEECH_ENABLED  
        System.Speech.Synthesis.SpeechSynthesizer speaker = new System.Speech.Synthesis.SpeechSynthesizer();
#endif
        public DirectionInfo()
        {
            util = new MapsUtils(key);
            Maps = new SerializableList<StaticMapOverLay>();
        }
        public DirectionInfo(string key, SerializableList<LatLng> aPoints, SerializableList<DirectionStep> aSteps)
        {
            Points = aPoints;
            Steps = aSteps;
            Maps = new SerializableList<StaticMapOverLay>();
            for (int i = 0; i < (Steps.Count-1); ++i)
            {
                StaticMapOverLay img = new StaticMapOverLay(key, Steps[i].step_Location, Steps[i + 1].step_Location, 400, 400);
                Maps.Add(img);
            }
            StaticMapOverLay img_level1 = new StaticMapOverLay(key, Steps[0].step_Location, Steps[Steps.Count-1].step_Location, 400, 400);
            Maps.Add(img_level1);

            StaticMapOverLay img_level0 = new StaticMapOverLay(key, Steps[0].step_Location, 400, 400);
            Maps.Add(img_level0);
            this.key = key;
            util = new MapsUtils(key);

#if DESKTOP_VERSION
            PreLoadAllMap();
#endif

        }
#if DESKTOP_VERSION
        public void PreLoadAllMap()
        {
            foreach (StaticMapOverLay img in Maps)
            {
                if (img.m_Image == null)
                {
                    img.LoadImageFromWeb();
                }
            }
        }
#endif
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loction"></param>
        /// <param name="zoom_instruction"></param>
        /// <returns></returns>
        public Image GetMap(LatLng loction, int zoom_instruction, ref PixelPoint MyLocation,ref Size ImageSize)
        {
            int i = 0;
            int zoom=0;
            int pic_index=0;
            foreach (StaticMapOverLay img in Maps)
            {
                if (img.IsLocationMapable(loction.Lat, loction.Lng))
                {
                    if (zoom < img.GetZoom())
                    {
                        pic_index = i;
                        zoom = img.GetZoom();
                    }
                                       
                }
                i++;
            }


#if DESKTOP_VERSION
                if (Maps[pic_index].GetImage() == null)
                {
                    Maps[pic_index].LoadImageFromWeb();
                }
#endif
            return Maps[pic_index].GetImageAndDrawMyLocationNPolyLineNTurn(loction.Lat, loction.Lng, Points, Steps, ref MyLocation,ref ImageSize);

            
        }
        private int GetNearestPolyIndex(LatLng loction)
        {
            int i = 0;
            int tracked_index = -1;
            double distance = double.MaxValue;
            
            for (i = 0; i < this.Points.Count - 1; i++)
            {
                int k = i + 1;
                double dis_ik = util.GetDistanceBetweenPoints(Points[i], Points[k]);
                //double dis_kl = util.GetDistanceBetweenPoints(Points[k], loction);

                while (dis_ik * 1000 < 1)
                {

                   
                    if (dis_ik * 100 < 20)
                    {
                        i++;
                        k++;
                    }
                    else
                    {
                        k++;
                    }
                    dis_ik = util.GetDistanceBetweenPoints(Points[i], Points[k]);


                }
                double dis_il = util.GetDistanceBetweenPoints(Points[i], loction);

                if (dis_il < dis_ik)
                {

                    double cur_dis = util.GetDistanceBetweenLineAndPointInUnknownUnit(Points[i], Points[k], loction);
                    if (cur_dis < 4000)
                    {
                       

                        //double brng1 = util.GetBearing(Points[i], loction);
                        //double brng2 = util.GetBearing(Points[k], loction);
                        //(Math.Abs(brng1 - brng2) > 95 && Math.Abs(brng1 - brng2) < 275) || 
                        if ((cur_dis < distance))
                        {
                            distance = cur_dis;
                            tracked_index = i;
                        }

                    }
                    
                }
            }
            return tracked_index;

        }
        public DirectionStep GetDirectionNextStep(int PolyLineIndex)
        {
            if (PolyLineIndex < 0 || PolyLineIndex >= this.Points.Count)
            {
                return null;
            }
            DirectionStep step = null;
            
            foreach (DirectionStep stp in this.Steps)
            {
                if (stp.PolyLineIndex > PolyLineIndex)
                {
                    return stp;
                }
                step = stp;
            }
            return null;
        }
        public double CalculateDistanceOfNextStep( LatLng CurrentLocation,int index,DirectionStep step)
        {
            if (index >= step.PolyLineIndex)
            {
                return 0;
            }
            double d = 0;
            for (int i = index+1; i < (step.PolyLineIndex); i++)
            {
                if (i + 1 < Points.Count)
                {
                    d += util.GetDistanceBetweenPoints(Points[i], Points[i + 1]);
                }
            }
            d += util.GetDistanceBetweenPoints(CurrentLocation, Points[index+1]);
            return d;
        }
        public string GetDirectionDiscriptionFromLocation(LatLng loc)
        {
            int index = GetNearestPolyIndex(loc);
            if (index < 0)
            {
                string Description= "You've lost your way!!!!";
                if (!isLostWay)
                {
#if SPEECH_ENABLED 
                    speaker.SpeakAsync(Description);
#endif
                    isLostWay = true;
                }
                return Description;
            }
            else
            {
                
                DirectionStep step=GetDirectionNextStep(index);
                isLostWay = false;
                if (step != null)
                {
                    double distance = CalculateDistanceOfNextStep(loc,index,step);
                    string str_dis;
                    if (distance < 1)
                    {
                        str_dis = " in " + String.Format("{0:0}", distance * 1000) + " Meters";
                    }
                    else
                    {
                        str_dis = " in " + String.Format("{0:0.0}", distance) + " KiloMeters";
                    }
                    string Description = step.step_description + str_dis;
                    if (!step.far_speech)
                    {
#if SPEECH_ENABLED                         
                        speaker.SpeakAsync(Description);
#endif
                        if(distance*1000<300)
                        {
                            step.near_speech=true;
                        }
                        step.far_speech=true;
                    }
                    if(distance*1000<300 && step.near_speech==false)
                    {
                            step.near_speech=true;
#if SPEECH_ENABLED 
                            speaker.SpeakAsync(Description);
#endif
                            
                    }
                    
                    return Description;
                }
                else
                {
                    isLostWay = true;
                    double distance = util.GetDistanceBetweenPoints(loc, Points[Points.Count-1]);
                    string str_dis;
                    if (distance < 1)
                    {
                        str_dis = "in " + String.Format("{0:0}", distance * 1000) + " Meters";
                    }
                    else
                    {
                        str_dis = "in " + String.Format("{0:0.0}", distance) + " KiloMeters";
                    }
                    string Description = "Arrive " + str_dis;
                    if (distance * 1000 < 500 )
                    {
                        if (!isArrived)
                        {
#if SPEECH_ENABLED                             
                            speaker.SpeakAsync(Description);
#endif
                        }
                    }
                    isArrived = true;
                    return Description;
                }
            }
        }
        public string GetLatLngStringOfStartPoint()
        {
            if(Steps!=null)
            {
                if(Steps.Count>=1)
                {
                    return this.Steps[0].step_Location.Lat + ", " + this.Steps[0].step_Location.Lng;
                }
            }
            return "";
        }
    }
}
