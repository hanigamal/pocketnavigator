# staticmaptest Introduction #

staticmaptest is desktop part of this solution, which loads Maps data in browser control and dumps into a xml, so that mobile version can use it.

note that these are just raw example and you should borrow only idea from this code to use in your application.

## How To Export an XML Using Desktop Version? ##
  1. Run desktop version and enter the directions
  1. Click button step1 (hold for few seconds, page will load)
  1. Click button step2 (hold for few seconds, page will load)
  1. Click Export button to export an XML file.
  1. Once the direction-info is extracted, you can export this in an xml file by clicking "export direction info button"

Note: you need to keep this xml file in the same location on mobile where your executable is. executable reads all XML from its own directory.


## How Code Works? ##
Map and direction info will be pulled by a peer Desktop application which will export all these information in an XML file. This XML file will have these 2 Items
  1. Static Map Images covering the track (at zoomed & top level)
  1. Direction steps and description.
Static map images can be downloaded from Google using there [static map API](http://code.google.com/apis/maps/documentation/staticmaps/). Direction steps and descriptions can only be downloaded via a AJAX API. Since I had only AJAX way of getting this data, I created a [webpage](http://code.google.com/p/pocketnavigator/source/browse/trunk/staticmaptest/DirectionsTest.html) which can do all necessary DOM operation using JavaScript.  Once the DOM is populated i can easily grab the data from C# API (Web browser control + GetElementByID APIs).


Class [StaticMapOverLay](http://code.google.com/p/pocketnavigator/source/browse/trunk/PocketDashboard/MapUtillityClasses/StaticMapOverLay.cs) is representing static image , its serializable class, and can be serialized from XML.
On Desktop side it populates its data from web automatically if you provide Lat & Lag(of center) and size. See different constructor for details.
On Mobile side its instantiated from XML de-serialization.

```


    public class StaticMapOverLay
    {
        public double m_latitude;
        public double m_longitude;
        public int m_zoom;
        public double m_width;
        public double m_height;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Image m_Image;
        MapsUtils util;

        public StaticMapOverLay()
        {
            util = new MapsUtils();
        }
        public StaticMapOverLay(string map_key, double latitude, double longitude, int zoom, int width, int height)
        {
            this.m_latitude = latitude;
            this.m_longitude = longitude;
            this.m_zoom = zoom;
            this.m_width = width;
            this.m_height = height;
            this.util = new MapsUtils(map_key);
        }
        public StaticMapOverLay(string map_key, LatLng loc1,LatLng loc2, int width, int height)
        {
            this.util = new MapsUtils(map_key);
            this.m_latitude = (loc1.Lat+loc2.Lat)/2;
            this.m_longitude = (loc1.Lng + loc2.Lng) / 2; ;
            this.m_zoom = util.GetSuiatbleZoomForPoints(loc1,loc2,width,height);
            this.m_width = width;
            this.m_height = height;
        }
        public StaticMapOverLay(string map_key, LatLng center, int width, int height)
        {
            this.util = new MapsUtils(map_key);
            this.m_latitude = center.Lat;
            this.m_longitude = center.Lng;
            this.m_zoom = 2;
            this.m_width = width;
            this.m_height = height;

        }
#if DESKTOP_VERSION
        public void LoadImageFromFile(string filename)
        {
            Image m = Image.FromFile(filename);
            this.m_Image = m;
        }
        public void LoadImageFromWeb()
        {
            WebRequest wr = WebRequest.Create(util.GetStaticMapUrl("" + this.m_latitude, "" + this.m_longitude, this.m_zoom, (int)this.m_width, (int)this.m_height));
            WebResponse resp = wr.GetResponse();
            m_Image = Image.FromStream(resp.GetResponseStream());
        }
#endif
        public Image GetImageAndDrawMyLocation(double latitude, double longitude)
        {
            PixelPoint mypoint = util.GetCordinateOnStaticImage(latitude, longitude, this.m_latitude, this.m_longitude, this.m_zoom, this.m_width, this.m_height);
            Bitmap bmp;
            if (this.m_Image != null)
            {
                bmp = new Bitmap(this.m_Image);
            }
            else
            {
                bmp = new Bitmap((int)this.m_width, (int)this.m_height);
            }
           Graphics g = Graphics.FromImage(bmp);
            DrawMarker(g, new LatLng(latitude, latitude), Color.Blue);
            return bmp;
        }
         
        public Image GetImageAndDrawMyLocationNPolyLineNTurn(double latitude, double longitude, List<LatLng> PolyLine,List<DirectionStep> steps,ref PixelPoint CurLoc,ref Size ImageSize)
        {
            /*See code for detail*/    
        }
        public void DrawMyLocation_PolyLine_Turns(Graphics g, LatLng CurLocation, List<LatLng> PolyLine, List<DirectionStep> steps, ref PixelPoint CurLoc, ref Size ImageSize)
        {
            /*See code for detail*/            
        }
        /// <summary>
        /// finds whether the given lat lng can be mapped on this static image or not.
        /// </summary>
        /// <returns></returns>
        public bool IsLocationMapable(double latitude, double longitude)
        {
            PixelPoint mypoint = util.GetCordinateOnStaticImage(latitude, longitude, this.m_latitude, this.m_longitude, this.m_zoom, this.m_width, this.m_height);
            if (mypoint.x < this.m_width && mypoint.x >= 0 && mypoint.y >= 0 && mypoint.y < this.m_height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }




```
Class [DirectionInfo](http://code.google.com/p/pocketnavigator/source/browse/trunk/PocketDashboard/MapUtillityClasses/DirectionInfo.cs) is Toplevel class, contains few static images & steps of direction.

```


    [Serializable]
    public class DirectionInfo
    {
        public SerializableList<LatLng> Points;
        public SerializableList<DirectionStep> Steps;
        public SerializableList<StaticMapOverLay> Maps;
        string key=null;
        MapsUtils util = null;
        bool isLostWay = true;
        bool isArrived = false;
#if SPEECH_ENABLED  
        System.Speech.Synthesis.SpeechSynthesizer speaker = new System.Speech.Synthesis.SpeechSynthesizer();
#endif
        //use this constructor on mobile, for offline version. to serialize from XML
		public DirectionInfo()
        {
            util = new MapsUtils(key);
            Maps = new SerializableList<StaticMapOverLay>();
        }
		// once you provide direction step, it will figure out how many images will be required to cover this route. 
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
		/// To load all images at the desktop side.
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
          /// Call this function to get image for direction information.
		  ///zoom instruction to indicate zoomed image OR zoomed in image.
        public Image GetMap(LatLng loction, int zoom_instruction, ref PixelPoint MyLocation,ref Size ImageSize)
        {
           //See code for detail
            
        }
		
        /// Call this function to draw image on your picture box (image etc) for direction information
         public void DrawMap(Graphics g,LatLng loction, int zoom_instruction, ref PixelPoint MyLocation, ref Size ImageSize)
        {
            //See code for detail
        }
       
        public string GetDirectionDiscriptionFromLocation(LatLng loc)
        {
            //See code for detail
        }
        
    }


```

You can call function
```
GetDirectionDiscriptionFromLocation(LatLng loc)
```
to get the direction description.

You can call this function
```
GetCordinateOnStaticImage(double latitude, double longitude, double centerLat, double centerLang, int zoom,double width,double height)
```
to get the co-ordinate on Image which maps to a Latitude and Longitude



---


**Frequently Asked Questions**<br>
<b>Question</b>:How desktop APP and Mobile APP communicate with each other? use USB cable ? and how the mobile app read data from the computer app via the USB? <br>
<b>Answer</b>: No they don't communicate. XML file exported by desktop application will be read by mobile app (completely disconnected).