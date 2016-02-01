using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anekatest1.Controllers
{
    public class imageData
    {
        public string Data
        {
            get; set;
        }
    }
    public class HomeController : Controller
    {
        public static string results = "Not Yet Ready.. Please Click Again";
        public static bool readyImage = false;
        public static byte[] imageData;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        [HttpPost]
        public void Upload(string fileData)
        {
            readyImage = false;

            string[] tokens= fileData.Split(',');
            imageData = new byte[tokens.Length];

            for(int i = 0; i < tokens.Length; i++)
            {
                imageData[i] = Byte.Parse(tokens[i]);

            }


            //string dataWithoutJpegMarker = fileData.Replace("data:image/png;base64,", String.Empty);
            /*
            fileRequest = new byte[fileData.Length * sizeof(char)];
            // System.Buffer.BlockCopy(fileData.ToCharArray(), 0, fileRequest, 0, fileRequest.Length);
            fileRequest = System.Text.Encoding.ASCII.GetBytes(fileData);
            //   fileRequest = Convert.FromBase64String(dataWithoutJpegMarker);
            */
            readyImage = true;
        }
        /*

        [HttpPost]
        public string getimage(FormCollection form)
        {
            readyImage = false;
            int sum = 0;
            string fileName = Request.Headers["imagelength"];
            /*  for(int i = 0; i < data.Length; i++)
              {
                  sum += data.ElementAt(i);
              }
              */
      /*      fileRequest = new byte[Request.ContentLength];
            Request.InputStream.Read(fileRequest, 0, Request.ContentLength);
            readyImage = true;
            results = "We got it";
            Console.WriteLine("we got a data of :" + sum);
            return ("black");
        }
    */

        [HttpGet]
        public ActionResult receive2(string p)
        {
            while (!readyImage) ;
            results = "It is " + isItDark(imageData);
            //    string back = "this is my result from controller";
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        string isItDark(byte[] data)
        {
            double fuzzy = 0.1;
            int r, g, b, max_rgb;
            int light = 0, dark = 0;

            for (int x = 0, len = data.Length; x < len-3; x += 4)
            {
                r = data[x];
                g = data[x + 1];
                b = data[x + 2];

                max_rgb = Math.Max(Math.Max(r, g), b);
                if (max_rgb < 128)
                    dark++;
                else
                    light++;
            }

            var dl_diff = ((light - dark) / (data.Length / 4));
            if (dl_diff + fuzzy < 0)
                return ("Dark"); /* Dark. */
            else
                return ("Light");/* Light. */
        }
    }
}