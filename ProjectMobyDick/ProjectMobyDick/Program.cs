using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProjectMobyDick
{
    class Program
    {
        public static void Download()
        {
            WebClient webClient = new WebClient(); 
            webClient.DownloadFile("http://www.gutenberg.org/files/12345/12345.txt", @"C:\\Cenk\\cyagci.txt");
        }
        static void Main(string[] args)
        {
            KlasorKontrol(@"C:\\Cenk");
            FileKontrol(@"C:\\Cenk\\cyagci.txt");
            Download();
            StreamReader sr = new StreamReader(@"C:\\Cenk\\cyagci.txt");
            string data = sr.ReadToEnd();
            sr.Close();

            XmlTextWriter xmlText = new XmlTextWriter(@"C:\\Cenk\\mobydick.xml",System.Text.UTF8Encoding.UTF8);
            xmlText.WriteComment("MobyDick Harf Sayıcı by Cenk Yağcı");
            xmlText.WriteStartElement("words");
            string[] harf = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p","q","r", "s", "t", "u", "v", "x", "y", "z" };
            
            for (int i = 0; i < harf.Length; i++)
            {
                int sayac = 0;
                char h = Char.Parse(harf[i]);
                foreach (char d in data)
                {
                    if (d == h)
                        sayac++;
                }


                xmlText.WriteStartElement("word");
                xmlText.WriteAttributeString("count", sayac.ToString());
                xmlText.WriteAttributeString("text", h.ToString());
                xmlText.WriteEndElement();
                
                
            }
            
            xmlText.Close();
            Console.WriteLine("işlem başarılı");
            Console.Read();
            
        }

        private static void FileKontrol(string file)
        {
            bool kontrol = File.Exists(@"C:\\Cenk\\cyagci.txt");
            if (kontrol)
            {
                Console.WriteLine("Oluşturmak istenen dosya sistemde mevcut");
            }
            else
            {
               FileStream fstream = File.Create(@"C:\\Cenk\\cyagci.txt");
                fstream.Close();
                Console.WriteLine("Dosya oluşturuldu " + fstream.Name);
            }
        }

        private static void KlasorKontrol(string path)
        {
            bool kontrol = Directory.Exists(path);
            if (kontrol)
            {
                Console.WriteLine("Oluşturmak istenen klasör sistemde mevcut");
            }
            else
            {
                DirectoryInfo dinfo = Directory.CreateDirectory(path);
                if (dinfo != null)
                {
                    Console.WriteLine("Klasör oluşturuldu "+ dinfo.FullName);
                }
            }
        }


    }
}
