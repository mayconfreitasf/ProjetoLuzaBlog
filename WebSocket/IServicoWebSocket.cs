namespace ProjetoLuzaBlog.WebSocket
{
    public interface IServicoWebSocket
    {
        Task HandleWebSocketConexao(HttpContext context);  // Gerenciar a conexão
        Task EnviarMsgParaTodos(string message);  // Enviar mensagem para todos usuarios
    }
}
