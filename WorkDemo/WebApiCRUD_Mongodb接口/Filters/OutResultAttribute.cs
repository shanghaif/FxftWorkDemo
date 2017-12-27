using System.Net.Http;
using System.Web.Http.Filters;
using WebApiCRUD_Mongodb接口.Formatter;

namespace WebApiCRUD_Mongodb接口.Filters
{
    public class OutResultAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            // 若发生例外则不在这边处理
            if (context.Exception != null)
                return;

            base.OnActionExecuted(context);

            OutResult result = new OutResult();
            if (context.Response.IsSuccessStatusCode)
            {
                result.code = OutCode.成功;
            }
            else
            {
                result.code = OutCode.失败;
            }
            // 取得由 API 返回的状态代码
            //result.Status = actionExecutedContext.ActionContext.Response.StatusCode;
            // 取得由 API 返回的资料

            object data = null;
            bool isStandard = true;
            if (context.ActionContext.Response.Content != null)
            {
                data = context.ActionContext.Response.Content.ReadAsAsync<object>().Result;
                if (data != null && data is OutResult)
                {
                    var tmp = data as OutResult;
                    result = tmp;
                }
                else
                {
                    if (context.ActionContext.Response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        result.msg = "请求无效";
                    }
                    else
                    {
                        //result.data = data;
                        result = null;
                        isStandard = false;
                    }
                }
            }


            // 重新封装回传格式            
            context.Response = context.Request.CreateResponse(System.Net.HttpStatusCode.OK, isStandard ? result : data);
        }
    }
}