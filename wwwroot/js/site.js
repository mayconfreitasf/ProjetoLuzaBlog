//********************************************************//
//Bloco responsável pelo disparo de notificação via socket

var socket = new WebSocket("ws://localhost:5057/ws");
// Exibe a notificação
socket.onmessage = function (event) {
    var mensagem = event.data;
    alert("Nova Notificação: " + mensagem);
};

socket.onclose = function (event) {
    console.log("Conexão fechada!");
};

socket.onerror = function (error) {
    console.log("Erro no WebSocket:", error);
};
//******************************************************//

