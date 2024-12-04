using Microsoft.AspNetCore.Identity;
using ProjetoLuzaBlog.Dal;
using ProjetoLuzaBlog.Models;

namespace ProjetoLuzaBlog.WebSocket
{
    public class ServicoNotificacao
    {
        private readonly AppDbContext _context;
        private readonly IServicoWebSocket _webSocketServico;

        // Injeção de Dependência (DI)
        public ServicoNotificacao(AppDbContext context, IServicoWebSocket webSocketServico)
        {
            _context = context;
            _webSocketServico = webSocketServico;  // Dependência WebSocket injetada
        }

        public async Task EnviarNotificacao(Postagem postagem, string nome)
        {
            // Enviar notificação para todos os WebSockets conectados (responsabilidade única de notificação)
            var mensagem = $"{nome} publicou: '{postagem.DesConteudo}' ";
            await _webSocketServico.EnviarMsgParaTodos(mensagem);  // Delegando a responsabilidade de enviar a mensagem
        }
    }
}
