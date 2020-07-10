﻿#region API 참조
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
#endregion

namespace Server
{
    public class JsonController
    {
        #region 변수
        private string path;
        #endregion

        #region 생성자
        public JsonController(string fileName, string dirPath = "")
        {
            var dir = Path.Combine(Environment.CurrentDirectory) + "\\";
            if (dir != "")
                dir = Path.Combine(Environment.CurrentDirectory) + "\\" + dirPath + "\\";
            path = dir + fileName + ".json";
            var di = new DirectoryInfo(dir);
            if (di.Exists == false)
                di.Create();
        }
        #endregion

        #region 쓰기
        public async Task Write(JObject obj)
        {
            File.WriteAllText(path, obj.ToString());
            
            using (StreamWriter file = File.CreateText(path))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                await obj.WriteToAsync(writer);
            }
        }
        #endregion

        #region 읽기
        public Dictionary<string, object> Read()
        {
            try
            {
                File.ReadAllText(path);

                using (StreamReader reader = File.OpenText(path))
                {
                    string read = reader.ReadToEnd();
                    Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(read);
                    return dict;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 추가
        public async void Add(Dictionary<string, object> addDict)
        {
            try
            {
                var dict = Read();
                var obj = new JObject();

                foreach (var kv in dict)
                    obj.Add(new JProperty(kv.Key, kv.Value));

                foreach (var kv in addDict)
                {
                    if (dict.ContainsKey(kv.Key))
                        obj.Remove(kv.Key);
                    obj.Add(new JProperty(kv.Key, kv.Value));
                }
                await Write(obj);
            }
            catch (Exception e)
            {
                new Exception("JsonController - Add\n" + e.Message);
            }
        }
        #endregion
    }
}