using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace tryHttpPrint2
{
    class Program
    {
        private static HttpListener httpPostRequest = new HttpListener();
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            HttpListener httpPostRequest = new HttpListener();
            listener.Prefixes.Add("http://+:8888/"); //添加需要监听的url范围
            listener.Start(); //开始监听端口，接收客户端请求
            httpPostRequest.Prefixes.Add("http://10.0.0.217:30000/posttype/");
            httpPostRequest.Start();
            Thread ThrednHttpPostRequest = new Thread(new ThreadStart(httpPostRequestHandle));
            ThrednHttpPostRequest.Start();

            //阻塞主函数至接收到一个客户端请求为止
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;


            string responseString = string.Format("<HTML><BODY> Start Printing {0}</BODY></HTML>", DateTime.Now);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            //执行打印程式
            Print print = new Print();
            print.print3();

            //对客户端输出相应信息.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            //关闭输出流，释放相应资源
            output.Close();
            listener.Stop(); //关闭HttpListener
        }
        private static void httpPostRequestHandle()
        {
            while (true)
            {
                HttpListenerContext requestContext = httpPostRequest.GetContext();
                Thread threadsub = new Thread(new ParameterizedThreadStart((requestcontext) =>
                {
                    HttpListenerContext request = (HttpListenerContext)requestcontext;

                    //获取Post请求中的参数和值帮助类
                    HttpListenerPostParaHelper httppost = new HttpListenerPostParaHelper(request);
                    //获取Post过来的参数和数据
                    List<HttpListenerPostValue> lst = httppost.GetHttpListenerPostValue();

                    string userName = "";
                    string password = "";
                    string suffix = "";
                    string adType = "";

                    //使用方法
                    foreach (var key in lst)
                    {
                        if (key.type == 0)
                        {
                            string value = Encoding.UTF8.GetString(key.datas).Replace("\r\n", "");
                            if (key.name == "username")
                            {
                                userName = value;
                                Console.WriteLine(value);
                            }
                            if (key.name == "password")
                            {
                                password = value;
                                Console.WriteLine(value);
                            }
                            if (key.name == "suffix")
                            {
                                suffix = value;
                                Console.WriteLine(value);
                            }
                            if (key.name == "adtype")
                            {
                                adType = value;
                                Console.WriteLine(value);
                            }
                        }
                        if (key.type == 1)
                        {
                            string fileName = request.Request.QueryString["FileName"];
                            if (!string.IsNullOrEmpty(fileName))
                            {
                                string filePath = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToString("yyMMdd_HHmmss_ffff") + Path.GetExtension(fileName).ToLower();
                                if (key.name == "File")
                                {
                                    FileStream fs = new FileStream(filePath, FileMode.Create);
                                    fs.Write(key.datas, 0, key.datas.Length);
                                    fs.Close();
                                    fs.Dispose();
                                }
                            }
                        }
                    }

                    //Response
                    request.Response.StatusCode = 200;
                    request.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    request.Response.ContentType = "application/json";
                    requestContext.Response.ContentEncoding = Encoding.UTF8;
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = "true", msg = "提交成功" }));
                    request.Response.ContentLength64 = buffer.Length;
                    var output = request.Response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }));
                threadsub.Start(requestContext);
            }

        }
    }
}

