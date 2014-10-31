using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Xml;
namespace KBBI_Crawler_2
{
    class Program
    {
        public const int MAX_KATA = 5000;
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("D:\\sitemap.xml");
            List<string> semuaAlamat = new List<string>();
            List<ArtiKata> semuaArti = new List<ArtiKata>();
            System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\kompilasi-kata-arti-5.txt");

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("url/loc");

            for (int i = 0; i < MAX_KATA; i++)
            {
                semuaAlamat.Add(nodes.Item(i).InnerText);
                Console.Write("Alamat ke-");
                Console.WriteLine(i);
            }
            HtmlWeb docWeb = new HtmlWeb();
            HtmlDocument docHTML = new HtmlDocument();
            int iterasi = 0;
            foreach (string _str in semuaAlamat)
            {
                try
                {
                    docHTML = docWeb.Load(_str);
                    String kata1 = _str.Substring(19);
                    String kata2 = docHTML.DocumentNode.SelectSingleNode("//div[@id='d1']").InnerText;
                    ArtiKata dummy = new ArtiKata(kata1, kata2);
                    semuaArti.Add(dummy);
                }
                catch(Exception e)
                {
                    // do nothing
                    String kata1 = _str.Substring(19);
                    ArtiKata dummy = new ArtiKata(kata1, "arti tidak ditemukan");
                    semuaArti.Add(dummy);
                    Console.WriteLine("Gagal");
                }
                iterasi++;
                Console.WriteLine(iterasi);
            }

            iterasi = 0;
            foreach (ArtiKata _arti in semuaArti)
            {
                file.WriteLine("INSERT INTO `arti-kata`.`kbbi` (`kata`, `arti`) VALUES ('" + _arti.kata + "', '" + _arti.artinya + "');");
                Console.WriteLine("INSERT INTO `arti-kata`.`kbbi` (`kata`, `arti`) VALUES ('" + _arti.kata + "', '" + _arti.artinya + "');");
                iterasi++;
            }
            file.Close();
            Console.WriteLine(iterasi);
            Console.WriteLine("Success!");


            /*
            HtmlWeb doc1 = new HtmlWeb();
            HtmlDocument doc = doc1.Load("http://kbbi.web.id/zus");

            ViewBag.Message = doc.DocumentNode.SelectSingleNode("//div[@id='d1']").InnerText;
             */
            Console.ReadLine();
        }
    }

    public class ArtiKata
    {
        public String kata;
        public String artinya;

        public ArtiKata()
        {

        }
        public ArtiKata(String kata1, String kata2)
        {
            kata = kata1;
            artinya = kata2;
        }
    }
}
