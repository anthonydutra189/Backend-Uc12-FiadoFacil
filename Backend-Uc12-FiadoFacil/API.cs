using System.Net;
using System.Text;
using System.Text.Json;

namespace Backend_Uc12_FiadoFacil
{
    internal class API
    {
        HttpListener listener;

        public API()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5001/");
        }

        public async Task Iniciar()
        {
            listener.Start();

            Console.WriteLine("API iniciada em http://localhost:5001/");

            while (listener.IsListening)
            {
                var contexto = await listener.GetContextAsync();

                await ProcessarPedido(contexto);
            }
        }

        public async Task ProcessarPedido(HttpListenerContext contexto)
        {

            contexto.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            if (contexto.Request.HttpMethod == "GET")
            {
                if (contexto.Request.Url?.AbsolutePath == "/User")
                {
                    User user = new User();

                    List<User> lista = await User.BuscarTodosAsync();

                    string json = JsonSerializer.Serialize(lista);

                    byte[] bytes = Encoding.UTF8.GetBytes(json);

                    contexto.Response.ContentType = "application/json";
                    contexto.Response.ContentEncoding = Encoding.UTF8;
                    contexto.Response.ContentLength64 = bytes.Length;

                    await contexto.Response.OutputStream.WriteAsync(bytes);

                    contexto.Response.StatusCode = 200;
                    contexto.Response.Close();

                    return;

                }

                contexto.Response.StatusCode = 404;
                contexto.Response.Close();

            }
        }
    }
}