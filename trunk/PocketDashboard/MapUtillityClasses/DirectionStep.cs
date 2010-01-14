using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
namespace MapUtilities
{
    [Serializable]
    public class DirectionStep
    {
        public string step_description="";
        public LatLng step_Location=new LatLng(0,0);
        public int PolyLineIndex = 0;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool far_speech=false;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool near_speech = false;

        public DirectionStep()
        {
            
        }
        
        public DirectionStep(LatLng location, string desc,int iPolyLineIndex)
        {
            step_description = desc;
            step_Location = location;
            PolyLineIndex = iPolyLineIndex;
        }
        public void WriteToStream(Stream s)
        {
            
        }
    }
}
