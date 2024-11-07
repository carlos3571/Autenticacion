using Autenticacion.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs;

namespace Autenticacion.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (User.Email == "correo@gmail.com" && User.Password == "12345")
            {
                /// 
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email,User.Email),
                };

                // se asocia los claims creados a un nombre de cookie
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                // se agrega la identidad creada al claims principal de la aplicación
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                // se registra exitosamente la autenticación y se crea la cookie en el navegador
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
    

