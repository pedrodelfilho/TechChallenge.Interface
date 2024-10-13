using TechChallenge.Entities.Responses;
using Microsoft.Extensions.Options;
using System.Text.Json;
using TechChallenge.Entities;
using TechChallenge.Entities.Requests;
using TechChallenge.Interface.Extensions;
using TechChallenge.Interface.Interfaces;

public class ContatoService : IContatoService
{
    private readonly HttpClient _httpClient;
    private readonly string _resourceContato;

    public ContatoService(HttpClient httpClient, IOptions<ApiSettings> settings)
    {
        _httpClient = httpClient;
        _resourceContato = settings.Value.ResourceContato;
    }

    public async Task<Contato> AtualizarContato(AtualizarContatoRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_resourceContato}atualizar/", request);
        response.EnsureSuccessStatusCode();

        return await DeserializarContato(response);
    }

    public async Task<Contato> CadastrarContato(RegistrarContatoRequest request)
    {
        var contatos = await ObterTodosContatos();

        var contato = contatos.Where(x => (FiltrarPorNome(request.Nome, x.Nome)) ||
                                           FiltrarPorTelefone(request.NrDDD + request.NrTelefone, x.DDD.NrDDD + x.NrTelefone) ||
                                           FiltrarPorEmail(request.Email, x.Email)).ToList();

        var contatoExist = DadosJaCadastrado(contato, request);

        if (!string.IsNullOrEmpty(contatoExist))
            throw new Exception("Contato não registrado! " + contatoExist);
        else
        {
            var response = await _httpClient.PostAsJsonAsync($"{_resourceContato}cadastrar", request);
            response.EnsureSuccessStatusCode();

            return await DeserializarContato(response);
        }        
    }
    

    public async Task<Contato> ObterContatoPorId(long id)
    {
        var response = await _httpClient.GetAsync($"{_resourceContato}obterporid/{id}");
        response.EnsureSuccessStatusCode();

        return await DeserializarContato(response);
    }

    public async Task<List<Contato>> ObterTodosContatos()
    {
        var response = await _httpClient.GetAsync($"{_resourceContato}obtertodos");
        response.EnsureSuccessStatusCode();

        return await DeserializarListContato(response);
    }

    public async Task RemoverContato(long id)
    {
        await _httpClient.DeleteAsync($"{_resourceContato}remover/{id}");
    }

    private static async Task<Contato> DeserializarContato(HttpResponseMessage response)
    {
        var jsonResponse = await response.Content.ReadAsStringAsync();

        var apiResponse = JsonSerializer.Deserialize<BaseResponse>(jsonResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        var contato = JsonSerializer.Deserialize<Contato>(apiResponse.Data.ToString(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return contato;
    }

    private static async Task<List<Contato>> DeserializarListContato(HttpResponseMessage response)
    {
        var jsonResponse = await response.Content.ReadAsStringAsync();

        var apiResponse = JsonSerializer.Deserialize<BaseResponse>(jsonResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        var contato = JsonSerializer.Deserialize<List<Contato>>(apiResponse.Data.ToString(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return contato;
    }

    private static bool FiltrarPorEmail(string email1, string email2)
    {
        return email1 == email2;
    }

    private static bool FiltrarPorTelefone(string tel1, string tel2)
    {
        return tel1 == tel2;
    }

    private static bool FiltrarPorNome(string nome1, string nome2)
    {
        return nome1 == nome2;
    }
    private static string DadosJaCadastrado(IEnumerable<Contato> contato, RegistrarContatoRequest request)
    {
        var message = string.Empty;
        if (contato.Where(x => x.DDD.NrDDD + x.NrTelefone == request.NrDDD + request.NrTelefone).Any())
            message += "Já possui cadastro existente com o mesmo número de telefone informado!\n";
        if (contato.Where(x => x.Nome == request.Nome).Any())
            message += "Já possui cadastro existente com o mesmo Nome informado!\n";
        if (contato.Where(x => x.Email == request.Email).Any())
            message += "Já possui cadastro existente com o mesmo E-mail informado!\n";

        return message;
    }
}
