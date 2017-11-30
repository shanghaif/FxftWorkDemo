using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Josn的序列化和反序列化.Entity;
using Newtonsoft.Json;
using static System.Console;

namespace Josn的序列化和反序列化
{
    /// <summary>
    /// 打基础时用的，基本上没有什么用处了
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            var jsonStr = "{ \"resultInfo\":{ \"respCode\":\"0003\",\"respDesc\":\"IP not applied:121.34.131.146\"}}";

            //Json的序列化：JSon  -》 对象
            var jsonToObject = JsonConvert.DeserializeObject<ResposeResult>(jsonStr);
            //Json的反序列化：对象 -》 Json
            var objectToJson = JsonConvert.SerializeObject(jsonToObject);

            var jsonStr1 = "{ \"resultInfo\":{ \"respCode\":\"0003\",\"respDesc\":\"IP not applied:121.34.131.146\",\"respDesc1\":\"IP not applied:121.34.131.146\",\"respDesc2\":\"IP not applied:121.34.131.146\",\"respDesc3\":\"IP not applied:121.34.131.146\"}}";

            #region 多级JSon数据

            var jsonStr2 = "{\"respCode\":\"0003\",\"respDesc\":\"IP not applied:121.34.131.146\",\"data\":{\"respDesc1\":\"IP not applied:121.34.131.146\",\"respDesc2\":\"IP not applied:121.34.131.146\",\"respDesc3\":\"IP not applied:121.34.131.146\"}}";
            var jsonToObject2 = JsonConvert.DeserializeObject<AdditionalField>(jsonStr2);

            var item = new AdditionalField()
            {
                data = new DifferentField()
                {
                    respDesc1 = "one",
                    respDesc2 = "two",
                    respDesc3 = "three"
                },
                respCode = "5",
                respDesc = "four"
            };
            var json = JsonConvert.SerializeObject(item);

            #endregion

            #region VS自动生成JSon类  各种嵌套的JSon字符串


            var jsonData =
                "{\"apnList\":{\"list\":[\"cmmtm\"]},\"msisdn\":\"1064888683199\",\"openTime\":\"20161209201225\",\"packages\":{\"list\":[{\"pkgCode\":\"I00010100061\",\"pkgEfftTime\":\"20161209212930\",\"pkgExpireTime\":\"20370101000000\",\"pkgName\":\"GPRS中小流量新3元套餐\",\"pkgStatus\":\"生效\"}]},\"status\":\"正使用\",\"statusTime\":\"20170514154904\"}";

            var data1 = JsonConvert.DeserializeObject<Rootobject>(jsonData);
            var b = data1.status;

            #region 解析JSon字符串时类字段可少可多，只要位置正确就可以

            var data = JsonConvert.DeserializeObject<Data>(jsonData);
            var a = data.status;

            #endregion

            #endregion


            var testJson1 =
                   "{\"apnList\":[{\"apnName\":\"通用\",\"lastFlowTime\":\"\",\"pkgInfoList\":{\"list\":[{\"pkgCode\":\"I00010100061\",\"pkgName\":\"GPRS中小流量新3元套餐（原子产品）\",\"restFlow\":10.0,\"totalFlow\":10.0,\"usedFlow\":0.0}]},\"restFlow\":\"10.0\",\"totalFlow\":\"10.0\",\"usedFlow\":\"0.0\"}],\"msisdn\":\"1064888683199\"}";
            var Json1 = JsonConvert.DeserializeObject<MFlow>(testJson1);
            var mFlow = Json1.apnList[0].usedFlow;

            var testJson2 =
                "{\"apnInfo\":[{\"apnName\":\"通用\",\"monthlyList\":{\"list\":[{\"month\":\"201710\",\"usedFlow\":\"1.61\"}]}}]}";
            var Json2 = JsonConvert.DeserializeObject<MFlow2>(testJson2);
            var mFlow2 = Json2.apnInfo[0].monthlyList.list[0].usedFlow;

            ReadKey();

        }

    }
}




