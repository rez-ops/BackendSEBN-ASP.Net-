using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Services.IServices;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICongeService _congeService;
        private readonly IpermisionService _permisionService;
        private readonly ICompensationService _compensationService;
        private readonly IChangHoraService _changHoraService;
        private readonly IBadgeManqService _badgeManqService;
        private readonly IComandeService _comandeService;

        //**************
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public UserController(
            IUserService userService,
            ICongeService congeService,
            IpermisionService permisionService,
            ICompensationService compensationService,
            IChangHoraService changHoraService,
            IBadgeManqService badgeManqService,
            IConfiguration config,
            ApplicationDbContext context,
            IComandeService comandeService)
        {
            _userService = userService;
            _congeService = congeService;
            _permisionService = permisionService;
            _compensationService = compensationService;
            _changHoraService = changHoraService;
            _badgeManqService = badgeManqService;
            _config = config;
            _comandeService=comandeService;
            _context = context;
        }

        //****login
        [HttpPost("/user/login")]

        public async Task<ActionResult<User>> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User? user = await _userService.logIn(loginModel);
            if (user == null)
                return BadRequest(new { Message = "Vide." });

            return Ok(user);
        }

        //***********create conge 
       [HttpPost("/createConge/{matricule}")]
public async Task<IActionResult> CreateConge(int matricule, [FromBody] Conge conge)
{
    int nombreJourNewConge =0;
    if (conge == null)
        return BadRequest(new { Message = "Le congé n'est pas rempli." });


    User? user = await _userService.UserExistsAsync(matricule);
    if (user == null)
        return NotFound(new { Message = "Utilisateur non trouvé." });

    var currentdate = DateOnly.FromDateTime(DateTime.Now);
    var dateSortie = conge.DateSortie; 
    var dateEntre = conge.DateEntre;
    var minDate = currentdate.AddDays(-7);

if (!(minDate <= dateSortie && dateSortie <= dateEntre ))
{
    return BadRequest(new { Message = "La date du congé est invalide. La date de sortie et d'entrée doivent être dans les 7 jours précédant la date actuelle." });
}
    

    //le types de conge non pas tous en conte ????????????
    if(conge.IdTypeConge==1){
        var dateSortieDateTime = dateSortie.ToDateTime(new TimeOnly(0, 0)); 
        var dateEntreDateTime = dateEntre.ToDateTime(new TimeOnly(0, 0));


        nombreJourNewConge = (dateEntreDateTime - dateSortieDateTime).Days+1;


        int nombreJourRest = user.MaxLeaveDaysPerYear - user.TotalLeaveDaysTaken;
        if (nombreJourRest < nombreJourNewConge)
            return BadRequest(new { Message = $"Vous dépassez le nombre de jours restants. Les jours restants sont : {nombreJourRest}" });

    }
    user.TotalLeaveDaysTaken+=nombreJourNewConge;
    conge.UserId = user.Id;
    await _congeService.CreateCongeAsync(conge);

    return Ok(new { Message = "Congé créé avec succès." });
}


//*************create badge manquant
        [HttpPost("/createBadgeManquant/{matricule}")]
        public async Task<IActionResult> CreateBadgeManquant(int matricule, [FromBody] BadgeManquant badgeManquant)
        {
            if (badgeManquant == null)
                return BadRequest(new { Message = "Le badge manquant n'est pas rempli." });

            User? user = await _userService.UserExistsAsync(matricule);
            if (user == null)
                return NotFound(new { Message = "Utilisateur non trouvé." });

            badgeManquant.UserId = user.Id;
            await _badgeManqService.createBadgeManqAsync(badgeManquant);

            return Ok(new { Message = "Badge manquant créé avec succès." });
        }


        //*************create permission
        [HttpPost("/createPermission/{matricule}")]
        public async Task<IActionResult> CreatePermission(int matricule, [FromBody] Permission permission)
        {
            if (permission == null)
                return BadRequest(new { Message = "La permission n'est pas remplie." });

            User? user = await _userService.UserExistsAsync(matricule);
            if (user == null)
                return NotFound(new { Message = "Utilisateur non trouvé." });

            permission.UserId = user.Id;
            await _permisionService.CreatePermisionAsync(permission);

            return Ok(new { Message = "Permission créée avec succès." });
        }

        //*************create compensation
        [HttpPost("/createCompensation/{matricule}")]
        public async Task<IActionResult> CreateCompensation(int matricule, [FromBody] Componsation componsation)
        {
            if (componsation == null)
                return BadRequest(new { Message = "La compensation n'est pas remplie." });

            User? user = await _userService.UserExistsAsync(matricule);
            if (user == null)
                return NotFound(new { Message = "Utilisateur non trouvé." });

            componsation.UserId = user.Id;
            await _compensationService.CreateCompensationAsync(componsation);

            return Ok(new { Message = "Compensation créée avec succès." });
        }

        //*************create changement horaire
        [HttpPost("/createChangementHoraire/{matricule}")]
        public async Task<IActionResult> CreateChangementHoraire(int matricule, [FromBody] ChangementHoraire changementHoraire)
        {
            if (changementHoraire == null)
                return BadRequest(new { Message = "Le changement d'horaire n'est pas rempli." });

            User? user = await _userService.UserExistsAsync(matricule);
            if (user == null)
                return NotFound(new { Message = "Utilisateur non trouvé." });

            changementHoraire.UserId = user.Id;
            await _changHoraService.CreateChangHoraAsync(changementHoraire);

            return Ok(new { Message = "Changement d'horaire créé avec succès." });
        }
        //*************historique de creation

        [HttpGet("badges/{userId}")]
        public async Task<ActionResult<List<BadgeManquant?>>> GetBadgeCreateByUser(int userId)
        {
            var badges = await _badgeManqService.getBadgeCreateByUser(userId);
            
            if (badges == null || badges.Count == 0)
            {
                return NotFound("No badges found for the specified user.");
            }

            return Ok(badges);
        }


        [HttpGet("changements/{userId}")]
        public async Task<ActionResult<List<ChangementHoraire?>>> GetChangHorCreateByUser(int userId)
        {
            var changements = await _changHoraService.getChangHorCreateByUser(userId);

            if (changements == null || changements.Count == 0)
            {
                return NotFound("No time changes found for the specified user.");
            }

            return Ok(changements);
        }


        [HttpGet("compensations/{userId}")]
        public async Task<ActionResult<List<Componsation?>>> GetCompoCreateByUser(int userId)
        {
            var compensations = await _compensationService.getCompoCreateByUser(userId);

            if (compensations == null || compensations.Count == 0)
            {
                return NotFound("No compensations found for the specified user.");
            }

            return Ok(compensations);
        }

        [HttpGet("conges/{userId}")]
        public async Task<ActionResult<List<Conge?>>> GetCongeCreateByUser(int userId)
        {
            var conges = await _congeService.getCongeCreateByUser(userId);

            if (conges == null || conges.Count == 0)
            {
                return NotFound("No leave records found for the specified user.");
            }

            return Ok(conges);
        }

        [HttpGet("permissions/{userId}")]
        public async Task<ActionResult<List<Permission?>>> GetPermissCreateByUser(int userId)
        {
            var permissions = await _permisionService.getPermissCreateByUser(userId);

            if (permissions == null || permissions.Count == 0)
            {
                return NotFound("No permissions found for the specified user.");
            }

            return Ok(permissions);
        }

        //********************************  users managment *****************************
        // get all useres
        [HttpGet("allUsers")]
        public async Task<ActionResult<List<User?>>> GetAllUsers()
        {
            var users = await _userService.getAllUsers();
            
            if (users == null || users.Count == 0)
            {
                return NotFound(new { Message = "No users foundaaaaaaaaaa." });
            }

            return Ok(users);
        }
        [HttpGet("TypeConges")]
        public async Task<ActionResult<List<TypeConge?>>> getAllTypeConge(){
            var TypeConges=await _congeService.getAlTypeConge();
            if(TypeConges==null ||TypeConges.Count==0 ){
                return NotFound(new { Message = "No type available." });
            }
            return Ok(TypeConges);
        }


        [HttpPut("/UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] User user)
        {
            if (userId != user.Id)
                return BadRequest(new { Message = "User ID mismatch." });

            var existingUser = await _userService.GetUserByIdAsync(userId);
            if (existingUser == null)
                return NotFound(new { Message = "User not found." });

            await _userService.UpdateUser(user);
            return Ok(new { Message = "User updated successfully." });
        }

        [HttpDelete("/delete/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var existingUser = await _userService.GetUserByIdAsync(userId);
            if (existingUser == null)
                return NotFound(new { Message = "User not found." });

            await _userService.DeleteUser(userId);
            return Ok(new { Message = "User deleted successfully." });
        }

        // ********ajouter nouveau user 
        [HttpPost("createUser/{userId}")]
        public async Task<IActionResult> CreateUser(int userId,[FromBody] User user)
        {
            var existingUser = await _userService.GetUserByIdAsync(userId);
            if(existingUser.Role!="Admin")
                return Unauthorized(new { Message = "user not hotorized." } );
            if (user == null)
                return BadRequest(new { Message = "L'utilisateur n'est pas fourni." });

            try
            {
                await _userService.CreateUser(user);
                return Ok(new
                {
                    Message = "Utilisateur créé avec succès.",
                    User = user
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Une erreur s'est produite lors de la création de l'utilisateur.", Error = ex.Message });
            }
        }

        //*********************************************************************




        //********create comande 
        [HttpPost("createComande/{userId}")]
public async Task<IActionResult> CreateComande(int userId, [FromBody] Comande comande)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    try
    {
        // Assign the userId to the Comande
        comande.UserId = userId;
        await _comandeService.CreateComande(comande);
        return Ok(new
        {
            Message = "Comande created successfully",
            Comande = comande
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { Message = "An error occurred while creating the comande", Error = ex.Message });
    }
}

[HttpGet("GetAllComandesByUserId/{userId}")]
public async Task<IActionResult> GetAllComandesByUserId(int userId)
{
    var comandes = await _comandeService.GetAllComandesByUserId(userId);
    if (comandes == null || comandes.Count == 0)
    {
        return NotFound(new { Message = "No comandes found for this user." });
    }
    return Ok(comandes);
}

[HttpGet("GetComandeById/{id}")]
public async Task<IActionResult> GetComandeById(int id)
{
    var comande = await _comandeService.GetComandeCreebyId(id);
    if (comande == null)
    {
        return NotFound(new { Message = "Comande not found." });
    }
    return Ok(comande);
}




        //*********************************
        [HttpGet("search/departments")]
    public async Task<IActionResult> SearchDepartments(string query)
{
    if (string.IsNullOrEmpty(query))
        return BadRequest("Query cannot be empty");

    var departments = await _context.departments
        .Where(d => d.Nom.ToLower().Contains(query.ToLower()))
        .Select(d => new { d.Id, d.Nom, d.CostCenter })
        .ToListAsync();

    return Ok(departments);
}
//********************
[HttpGet("search/articles")]
    public async Task<IActionResult> SearchArticles(string query)
{
    if (string.IsNullOrEmpty(query))
        return BadRequest("Query cannot be empty");

    var departments = await _context.articles
        .Where(d => d.Machine_zone.ToLower().Contains(query.ToLower()))
        .Select(d => new { d.Id, d.Machine_zone, d.Reference })
        .ToListAsync();

    return Ok(departments);
}

        
    }
}
