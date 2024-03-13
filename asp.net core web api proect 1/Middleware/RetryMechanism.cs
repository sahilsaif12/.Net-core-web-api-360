using System.Diagnostics;

namespace asp.net_core_web_api_proect_1.Middleware
{
    public class RetryMechanism
    {
        private readonly int maxRetry=5;
        private readonly TimeSpan initialDelay=TimeSpan.FromSeconds(1);
        private readonly TimeSpan maxDelay= TimeSpan.FromSeconds(12);
        private readonly RequestDelegate _next;

        public RetryMechanism(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            int retryCount=0;
            bool success=false;
            TimeSpan delay=initialDelay;
            while(!success && retryCount<maxRetry)
            {
                try
                {
                    await _next(context);
                    success=true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Attempt no: {retryCount+1} ,\nstatus: Failed ,\ndelay from previous attempt:{delay.TotalSeconds} seconds , \n\n ");

                    TimeSpan timeSpan = (Math.Pow(2, retryCount)) * initialDelay;
                    delay = TimeSpan.FromSeconds(timeSpan.TotalSeconds);
                    retryCount++;
                    delay=delay>maxDelay?maxDelay:delay;

                    await Task.Delay(delay);

                }
            }

            if (!success)
            {
                Debug.WriteLine("Request failed after maximum number of attempts");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

        }
        
    }
}
