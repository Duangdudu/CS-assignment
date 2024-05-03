using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCrawler
{
    class SimpleCrawler
    {
        private Hashtable urls = new Hashtable();
        private int count = 0;

        static void Main(string[] args)
        {
            SimpleCrawler myCrawler = new SimpleCrawler();
            string startUrl = "http://www.cnblogs.com/dstang2000/";
            if (args.Length >= 1) startUrl = args[0];
            myCrawler.urls.Add(startUrl, false);//加入初始页面

            // 计时开始
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // 普通版爬虫
            //myCrawler.Crawl();

            // 并行版爬虫
            myCrawler.ParallelCrawl();

            // 计时结束
            sw.Stop();
            Console.WriteLine("爬取完成，总共耗时：" + sw.ElapsedMilliseconds + "ms");
        }

        private void Crawl()
        {
            Console.WriteLine("开始爬行了.... ");
            while (true)
            {
                List<string> currentUrls = new List<string>();
                lock (urls)
                {
                    foreach (string url in urls.Keys)
                    {
                        if (!(bool)urls[url]) currentUrls.Add(url);
                    }
                }

                if (currentUrls.Count == 0 || count > 10) break;

                foreach (string url in currentUrls)
                {
                    Console.WriteLine("爬行" + url + "页面!");
                    string html = DownLoad(url);
                    lock (urls)
                    {
                        urls[url] = true;
                    }
                    count++;
                    Parse(html);
                    Console.WriteLine("爬行结束" + url + "页面!");
                }
            }
        }

        private void ParallelCrawl()
        {
            Console.WriteLine("开始并行爬行了.... ");
            while (true)
            {
                List<string> currentUrls = new List<string>();
                lock (urls)
                {
                    foreach (string url in urls.Keys)
                    {
                        if (!(bool)urls[url]) currentUrls.Add(url);
                    }
                }

                if (currentUrls.Count == 0 || count > 10) break;

                Parallel.ForEach(currentUrls, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, url => {
                    Console.WriteLine("爬行" + url + "页面!");
                    string html = DownLoad(url);
                    lock (urls)
                    {
                        urls[url] = true;
                    }
                    count++;
                    Parse(html);
                    Console.WriteLine("爬行结束" + url + "页面!");
                });
            }
        }

        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        private void Parse(string html)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');
                if (strRef.Length == 0) continue;
                lock (urls)
                {
                    if (urls[strRef] == null) urls[strRef] = false;
                }
            }
        }
    }
}
