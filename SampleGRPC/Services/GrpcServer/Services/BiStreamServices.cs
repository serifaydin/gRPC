using Grpc.Core;
using Microsoft.Extensions.Logging;
using MyGRPC;
using System;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class BiStreamServices : BiStreamService.BiStreamServiceBase
    {
        private readonly ILogger<BiStreamServices> _logger;

        public BiStreamServices(ILogger<BiStreamServices> logger)
        {
            _logger = logger;
        }

        public override async Task StartBiStreaming(IAsyncStreamReader<BiStreamModel> requestStream, IServerStreamWriter<BiStreamModel> responseStream, ServerCallContext context)
        {
            if (requestStream != null)
            {
                if (!await requestStream.MoveNext())
                    return;
            }

            try
            {
                if (!string.IsNullOrEmpty(requestStream.Current.Message))
                {
                    if (string.Equals(requestStream.Current.Message, "qw!", System.StringComparison.OrdinalIgnoreCase))
                        return;
                }

                var message = requestStream.Current.Message;
                Console.WriteLine($"Message from client: {requestStream.Current.Username} Message : {requestStream.Current.Message}");
                await responseStream.WriteAsync(new BiStreamModel
                {
                    Username = requestStream.Current.Username,
                    Message = $"Reply stream from the server @:{DateTime.UtcNow:f} to {requestStream.Current.Username}"
                });
            }
            catch (System.Exception e)
            {
                _logger.LogInformation(e.Message);
            }
        }
    }
}