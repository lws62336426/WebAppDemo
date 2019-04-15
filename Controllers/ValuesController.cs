using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values/5
        //[HttpGet("{id}")]
        //[Log("Get")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //[HttpGet("get2")]
        //[Log("Get2")]
        //public ActionResult<string> Get2(int id)
        //{
        //    return "value2";
        //}

        [HttpGet]
        public ActionResult<string> Index()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId1:" + Thread.CurrentThread.ManagedThreadId);
            var result = Test4().Result;
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId4:" + Thread.CurrentThread.ManagedThreadId);
            return result;
        }

        //线程ID一致
        public static string Test()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId2:" + Thread.CurrentThread.ManagedThreadId);
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://www.baidu.com").Result;
                Console.WriteLine("Thread.CurrentThread.ManagedThreadId3:" + Thread.CurrentThread.ManagedThreadId);
                return response.Content.ReadAsStringAsync().Result;
            }
        }


        //出现3个线程.开始请求线程.Task.Run一个线程.异步方法await一个线程
        public static async Task<string> Test2()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId2:" + Thread.CurrentThread.ManagedThreadId);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.baidu.com");
                Console.WriteLine("Thread.CurrentThread.ManagedThreadId3:" + Thread.CurrentThread.ManagedThreadId);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> Test3()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId2:" + Thread.CurrentThread.ManagedThreadId);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.baidu.com");
                Console.WriteLine("Thread.CurrentThread.ManagedThreadId3:" + Thread.CurrentThread.ManagedThreadId);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> Test4()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId2:" + Thread.CurrentThread.ManagedThreadId);
            return await Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Thread.CurrentThread.ManagedThreadId3:" + Thread.CurrentThread.ManagedThreadId);
                return "xishuai";
            });
        }
    }
}
