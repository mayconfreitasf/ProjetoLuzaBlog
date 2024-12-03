using System.Net.WebSockets;
using System.Text;

namespace ProjetoLuzaBlog.WebSocket
{
    public class ServicoWebSocket : IServicoWebSocket
    {
        private readonly List<System.Net.WebSockets.WebSocket> _conexoes = new List<System.Net.WebSockets.WebSocket>();

        public async Task HandleWebSocketConexao(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var socket = await context.WebSockets.AcceptWebSocketAsync();
                _conexoes.Add(socket);  // Conectar um novo WebSocket
                await ReceberMensagem(socket);  // Escutar mensagens desse WebSocket
            }
            else
            {
                context.Response.StatusCode = 400; // Bad Request
            }
        }

        private async Task ReceberMensagem(System.Net.WebSockets.WebSocket socket)
        {
            var buffer = new byte[1024 * 4];
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                // Continuar recebendo mensagens
                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);  
            }

            _conexoes.Remove(socket);  // Remover a conexão fechada
            await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        public async Task EnviarMsgParaTodos(string mensagem)
        {
            var byteArray = Encoding.UTF8.GetBytes(mensagem);

            foreach (var socket in _conexoes)
            {
                if (socket.State == WebSocketState.Open)
                {
                    // Envia mensagem
                    await socket.SendAsync(new ArraySegment<byte>(byteArray), WebSocketMessageType.Text, true, CancellationToken.None);  
                }
            }
        }
    }
}
