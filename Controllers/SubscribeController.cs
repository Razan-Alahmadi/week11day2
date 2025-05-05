using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactiveLikeApiDemo.Models;
using ReactiveLikeApiDemo.Services;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ReactiveLikeApiDemo.Controllers
{
    [ApiController]
    [Route("api/subscribe")]
    public class SubscribeController : ControllerBase
    {
        [HttpGet]
        public async Task Subscribe(CancellationToken token)
        {
            // Set the response content type to Server-Sent Events (SSE)
            Response.ContentType = "text/event-stream";

            // Keep the connection alive using TaskCompletionSource
            var tcs = new TaskCompletionSource();

            // Subscribe to the broadcast service to receive updates
            var subscription = BroadcastService.Subscribe(async post =>
            {
                try
                {
                    // Serialize the post object to JSON
                    var json = JsonSerializer.Serialize(post);

                    // Send the update in SSE format
                    await Response.WriteAsync($"data: {json}\n\n");

                    // Ensure the data is sent to the client
                    await Response.Body.FlushAsync();
                }
                catch
                {
                    // If an error occurs, complete the task
                    tcs.TrySetResult();
                }
            });

            // Handle client disconnection
            token.Register(() =>
            {
                subscription.Dispose(); // Dispose the subscription
                tcs.TrySetResult();     // Complete the task to close the connection
            });

            // Keep the HTTP request open as long as the client is connected
            await tcs.Task;
        }
    }
}
