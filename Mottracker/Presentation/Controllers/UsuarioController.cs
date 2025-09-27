using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Usuario;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApplicationService _applicationService;

        public UsuarioController(IUsuarioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os usuários", Description = "Retorna todos os registros de usuários cadastrados.")]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<UsuarioResponseDto>))]
        [SwaggerResponse(204, "Nenhum usuário encontrado")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosUsuarios();

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém usuário por ID", Description = "Retorna os dados de um usuário específico.")]
        [SwaggerResponse(200, "Usuário retornado com sucesso", typeof(UsuarioResponseDto))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterUsuarioPorId(id);

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("email/{email}")]
        [SwaggerOperation(Summary = "Obtém usuário por e-mail", Description = "Retorna os dados de um usuário com base no e-mail.")]
        [SwaggerResponse(200, "Usuário retornado com sucesso", typeof(UsuarioResponseDto))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetByEmail(string email)
        {
            try
            {
                var result = _applicationService.ObterUsuarioPorEmail(email);

                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo usuário", Description = "Salva um novo registro de usuário.")]
        [SwaggerResponse(201, "Usuário salvo com sucesso", typeof(UsuarioResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar o dado")]
        public IActionResult Post([FromBody] UsuarioRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosUsuario(entity);

                if (result != null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdUsuario }, result);

                return BadRequest("Não foi possível salvar os dados.");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza um usuário", Description = "Edita os dados de um usuário existente.")]
        [SwaggerResponse(200, "Usuário atualizado com sucesso", typeof(UsuarioResponseDto))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerResponse(400, "Erro ao atualizar o dado")]
        public IActionResult Put(int id, [FromBody] UsuarioRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosUsuario(id, entity);

                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta um usuário", Description = "Remove um usuário do sistema com base no ID.")]
        [SwaggerResponse(204, "Usuário deletado com sucesso")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerResponse(400, "Erro ao deletar o dado")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosUsuario(id);

            if (result != null)
                return NoContent();

            return NotFound();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Usuario;
using Mottracker.Docs.Samples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioUseCase _useCase;

        public UsuarioController(IUsuarioUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Lista usuários com paginação",
           Description = "Retorna uma lista paginada de usuários cadastrados no sistema. " +
                        "Este endpoint suporta paginação para otimizar a performance com grandes volumes de dados. " +
                        "Os usuários são retornados com informações completas e links HATEOAS para navegação."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de usuários retornada com sucesso", type: typeof(IEnumerable<UsuarioResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum usuário encontrado")]
        [SwaggerResponse(statusCode: 400, description: "Parâmetros de paginação inválidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [SwaggerResponseExample(statusCode: 200, typeof(UsuarioResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = await _useCase.ObterTodosUsuariosAsync(Deslocamento, RegistrosRetornado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Data?.Select(u => new
                {
                    u.IdUsuario,
                    u.NomeUsuario,
                    u.CPFUsuario,
                    u.SenhaUsuario,
                    u.CNHUsuario,
                    u.EmailUsuario,
                    u.TokenUsuario,
                    u.DataNascimentoUsuario,
                    u.CriadoEmUsuario,
                    u.ContratoUsuario,
                    u.Telefones,
                    u.UsuarioPermissoes,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Usuario", new { id = u.IdUsuario }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Usuario", new { id = u.IdUsuario }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Usuario", new { id = u.IdUsuario }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(Get), "Usuario", null, Request.Scheme),
                    create = Url.Action(nameof(Post), "Usuario", null, Request.Scheme),
                },
                pagina = new
                {
                    result.Value?.Deslocamento,
                    result.Value?.RegistrosRetornado,
                    result.Value?.TotalRegistros
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtém usuário por ID",
            Description = "Retorna os dados completos de um usuário específico baseado no ID fornecido. " +
                        "Inclui informações detalhadas do usuário e links HATEOAS para operações relacionadas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Usuário encontrado com sucesso", type: typeof(UsuarioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetById(
            [FromRoute, SwaggerParameter("ID único do usuário", Required = true)] int id)
        {
            var result = await _useCase.ObterUsuarioPorIdAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(GetById), "Usuario", new { id }),
                    get = Url.Action(nameof(Get), "Usuario", null),
                    put = Url.Action(nameof(Put), "Usuario", new { id }),
                    delete = Url.Action(nameof(Delete), "Usuario", new { id }),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("email/{email}")]
        [SwaggerOperation(
            Summary = "Obtém usuário por e-mail",
            Description = "Retorna os dados completos de um usuário específico baseado no e-mail fornecido. " +
                        "Útil para autenticação e busca por e-mail."
        )]
        [SwaggerResponse(statusCode: 200, description: "Usuário encontrado com sucesso", type: typeof(UsuarioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "E-mail é obrigatório e deve ser válido")]
        [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado para o e-mail fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByEmail(
            [FromRoute, SwaggerParameter("E-mail do usuário para busca", Required = true)] string email)
        {
            var result = await _useCase.ObterUsuarioPorEmailAsync(email);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(GetByEmail), "Usuario", new { email }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Usuario", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Usuario", new { id = result.Value?.IdUsuario }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Usuario", new { id = result.Value?.IdUsuario }, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria novo usuário",
            Description = "Cria um novo usuário no sistema com os dados fornecidos. " +
                        "Valida se todos os campos obrigatórios estão preenchidos e se não há duplicatas. " +
                        "Retorna os dados do usuário criado incluindo o ID gerado."
        )]
        [SwaggerRequestExample(typeof(UsuarioRequestDto), typeof(UsuarioRequestDtoSample))]
        [SwaggerResponse(statusCode: 201, description: "Usuário criado com sucesso", type: typeof(UsuarioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados inválidos - campos obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível criar o usuário - dados inválidos ou duplicados")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Post(
            [FromBody, SwaggerParameter("Dados do usuário a ser criado", Required = true)] UsuarioRequestDto entity)
        {
            var result = await _useCase.SalvarDadosUsuarioAsync(entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza usuário existente",
            Description = "Atualiza os dados de um usuário existente baseado no ID fornecido. " +
                        "Valida se o usuário existe e se os novos dados são válidos. " +
                        "Retorna os dados atualizados do usuário."
        )]
        [SwaggerRequestExample(typeof(UsuarioRequestDto), typeof(UsuarioRequestDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "Usuário atualizado com sucesso", type: typeof(UsuarioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido ou dados obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível atualizar o usuário - dados inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Put(
            [FromRoute, SwaggerParameter("ID único do usuário a ser atualizado", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados do usuário", Required = true)] UsuarioRequestDto entity)
        {
            var result = await _useCase.EditarDadosUsuarioAsync(id, entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove usuário",
            Description = "Remove permanentemente um usuário do sistema baseado no ID fornecido. " +
                        "Esta operação é irreversível e remove todos os dados associados ao usuário."
        )]
        [SwaggerResponse(statusCode: 204, description: "Usuário removido com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível remover o usuário")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Delete(
            [FromRoute, SwaggerParameter("ID único do usuário a ser removido", Required = true)] int id)
        {
            var result = await _useCase.DeletarDadosUsuarioAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Realiza login do usuário",
            Description = "Autentica um usuário no sistema usando e-mail e senha. " +
                        "Retorna os dados do usuário com token de autenticação se as credenciais forem válidas. " +
                        "Este endpoint não implementa segurança avançada, apenas valida as credenciais básicas."
        )]
        [SwaggerRequestExample(typeof(UsuarioLoginDto), typeof(UsuarioLoginDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "Login realizado com sucesso", type: typeof(UsuarioLoginResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados de login inválidos - e-mail ou senha ausentes")]
        [SwaggerResponse(statusCode: 401, description: "Credenciais inválidas - e-mail ou senha incorretos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Login(
            [FromBody, SwaggerParameter("Dados de login do usuário", Required = true)] UsuarioLoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.EmailUsuario) || string.IsNullOrWhiteSpace(loginDto.SenhaUsuario))
            {
                return BadRequest(new UsuarioLoginResponseDto
                {
                    LoginSucesso = false,
                    Mensagem = "E-mail e senha são obrigatórios"
                });
            }

            var result = await _useCase.ObterUsuarioPorEmailAsync(loginDto.EmailUsuario);

            if (!result.IsSuccess || result.Value == null)
            {
                return Unauthorized(new UsuarioLoginResponseDto
                {
                    LoginSucesso = false,
                    Mensagem = "E-mail ou senha incorretos"
                });
            }

            // Verificação simples da senha (sem hash por enquanto)
            if (result.Value.SenhaUsuario != loginDto.SenhaUsuario)
            {
                return Unauthorized(new UsuarioLoginResponseDto
                {
                    LoginSucesso = false,
                    Mensagem = "E-mail ou senha incorretos"
                });
            }

            // Gera um token simples (em produção, usar JWT)
            var token = Guid.NewGuid().ToString();
            
            // Atualiza o token do usuário (opcional)
            // await _useCase.AtualizarTokenUsuarioAsync(result.Value.IdUsuario, token);

            var loginResponse = new UsuarioLoginResponseDto
            {
                IdUsuario = result.Value.IdUsuario,
                NomeUsuario = result.Value.NomeUsuario,
                EmailUsuario = result.Value.EmailUsuario,
                TokenUsuario = token,
                DataNascimentoUsuario = result.Value.DataNascimentoUsuario,
                CriadoEmUsuario = result.Value.CriadoEmUsuario,
                LoginSucesso = true,
                Mensagem = "Login realizado com sucesso"
            };

            var hateaos = new
            {
                data = loginResponse,
                links = new
                {
                    self = Url.Action(nameof(Login), "Usuario", null, Request.Scheme),
                    profile = Url.Action(nameof(GetById), "Usuario", new { id = result.Value.IdUsuario }, Request.Scheme),
                    logout = Url.Action(nameof(Logout), "Usuario", null, Request.Scheme),
                }
            };

            return Ok(hateaos);
        }

        [HttpPost("logout")]
        [SwaggerOperation(
            Summary = "Realiza logout do usuário",
            Description = "Invalida o token de autenticação do usuário. " +
                        "Em uma implementação real, este endpoint invalidaria o token no servidor."
        )]
        [SwaggerResponse(statusCode: 200, description: "Logout realizado com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "Token inválido ou ausente")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public Task<IActionResult> Logout(
            [FromHeader, SwaggerParameter("Token de autenticação do usuário", Required = true)] string? authorization)
        {
            if (string.IsNullOrWhiteSpace(authorization))
            {
                return Task.FromResult<IActionResult>(BadRequest(new { mensagem = "Token de autorização é obrigatório" }));
            }

            // Em uma implementação real, invalidaria o token aqui
            // await _useCase.InvalidarTokenAsync(authorization);

            return Task.FromResult<IActionResult>(Ok(new { mensagem = "Logout realizado com sucesso" }));
        }
    }
}
