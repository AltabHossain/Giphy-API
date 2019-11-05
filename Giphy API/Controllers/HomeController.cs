using Giphy_API.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace Giphy_API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GiphyAPIKey"];
            string query = "pikachu";
            string offSet = "0";

            //Create the request to the API
            WebRequest request = WebRequest.Create("https://api.giphy.com/v1/gifs/search?api_key=" + apiKey + "&q=" + query + "&limit=25&offset=" + offSet + "&rating=G&lang=en");
            //Send the request off
            WebResponse response = request.GetResponse();
            //Get back the response stream
            Stream stream = response.GetResponseStream();
            //Make it accessible
            StreamReader reader = new StreamReader(stream);
            //Put into string which is json formatted
            string responseFromServer = reader.ReadToEnd();

            JObject parsedString = JObject.Parse(responseFromServer);

            Gif myGifs = parsedString.ToObject<Gif>();

            //Debug.WriteLine(myPokemon.moves[0].move.name);

            return View(myGifs);
        }
    }
}