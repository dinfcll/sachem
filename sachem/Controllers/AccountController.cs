using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using sachem.Models;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace sachem.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private const int PORTCOURRIEL = 587;
        private readonly SACHEMEntities db = new SACHEMEntities();
        public AccountController()
        {

        }

        #region fn_CookieStuff
        #pragma warning disable 0618

        [NonAction]
        private void CreerCookieConnexion(string NomUsager, string MotDePasse)
        {
            string mdpEncrypte = Crypto.Encrypt(MotDePasse, System.Configuration.ConfigurationManager.AppSettings.Get("CryptoKey")); 
            HttpCookie Maintenir = new HttpCookie("SACHEMConnexion");
            Maintenir.Values.Add("NomUsager", NomUsager); 
            Maintenir.Values.Add("MP", mdpEncrypte);

            Maintenir.Expires = DateTime.Now.AddDays(1d);

            Response.Cookies.Add(Maintenir);
        }

        [NonAction]
        private void SupprimerCookieConnexion()
        {
            HttpCookie BiscuitPerime = new HttpCookie("SACHEMConnexion");
            BiscuitPerime.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(BiscuitPerime);
        }

        [NonAction]
        private bool CookieConnexionExiste()
        {
            HttpCookieCollection CollectionsCookiesSACHEM = Request.Cookies;
            HttpCookie CookieMaintenirConnexion = CollectionsCookiesSACHEM.Get("SACHEMConnexion");

            return CookieMaintenirConnexion != null;
        }

        #endregion


        #region fn_EnvoyerCourriel
        [NonAction]
        private bool EnvoyerCourriel(String Email, string nouveaumdp)
        {
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage("sachemcllmail@gmail.com", Email, "Demande de réinitialisation de mot de passe", "Voici votre nouveau mot de passe: " + nouveaumdp + " .");
            client.Port = PORTCOURRIEL;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("sachemcllmail@gmail.com", System.Configuration.ConfigurationManager.AppSettings.Get("EmailSachemMDP")); 
            message.BodyEncoding = Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            try
            {
                client.Send(message);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion


        #region fn_Login
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            if(SachemIdentite.ObtenirTypeUsager(Session) != TypeUsagers.Aucun)
            {
                return RedirectToAction("Index","DossierEtudiant",null);
            }

            if (CookieConnexionExiste())
            {
                Personne PersonneCookie = new Personne();
                HttpCookie CookieMaintenirConnexion = Request.Cookies.Get("SACHEMConnexion");
                PersonneCookie.NomUsager = CookieMaintenirConnexion["NomUsager"];
                PersonneCookie.MP = Crypto.Decrypt(CookieMaintenirConnexion["MP"], "asdjh213498yashj2134987ash");
                PersonneCookie.SouvenirConnexion = true;

                return View(PersonneCookie);
            }

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string NomUsager, string MP, bool SouvenirConnexion)
        {
            string mdpPlain = MP;
            Personne PersonneBD = new Personne();
            const int STATUT_ACCEPTE = 3;

            if (mdpPlain == "")
                ModelState.AddModelError("MP", Messages.U_001);
            else
                if (NomUsager == null)
            {
                ModelState.AddModelError("NomUsager", Messages.U_001); 
            }
            else
                    if (Regex.IsMatch(NomUsager, @"^\d+$") && NomUsager.Length == 7) 
            {
                if (!db.Personne.Any(x => x.Matricule.Substring(2) == NomUsager))
                {
                    ModelState.AddModelError(string.Empty, Messages.I_017());  
                }
                else
                    PersonneBD = db.Personne.AsNoTracking().Where(x => x.Matricule.Substring(2) == NomUsager).FirstOrDefault();
            }
            else
                        if (!db.Personne.Any(x => x.NomUsager == NomUsager))
            {
                ModelState.AddModelError(string.Empty, Messages.I_017()); 
            }
            else
                PersonneBD = db.Personne.AsNoTracking().Where(x => x.NomUsager == NomUsager).FirstOrDefault();
            if (ModelState.IsValid)
            {
                
                MP = SachemIdentite.encrypterChaine(MP);

                
                if (PersonneBD.MP != MP)
                    ModelState.AddModelError(string.Empty, Messages.I_017()); 
                if (!ModelState.IsValid)
                {
                    PersonneBD.MP = "";
                    return View(PersonneBD); 
                }

                
                var typeinscr = (from i in db.Inscription
                                 where i.id_Pers == PersonneBD.id_Pers
                                 select i.id_TypeInscription).FirstOrDefault();
                
                var idinscr = (from i in db.Inscription
                               where i.id_Pers == PersonneBD.id_Pers
                               select i.id_Inscription).FirstOrDefault();

                
                if (idinscr != 0)
                    SessionBag.Current.id_Inscription = idinscr;
                else
                    SessionBag.Current.id_Inscription = 0;

                
                if (typeinscr > 1)
                {
                    SessionBag.Current.id_TypeUsag = TypeUsagers.Tuteur;
                }
                else
                {
                    
                    if (typeinscr == 1)
                    {
                        SessionBag.Current.id_TypeUsag = TypeUsagers.Eleve;
                    }
                    
                    else
                    {
                        SessionBag.Current.id_TypeUsag = PersonneBD.id_TypeUsag;
                    }
                }

               
                var idSuperviseur = (from i in db.Jumelage
                                     where i.id_Enseignant == PersonneBD.id_Pers
                                     select i.id_Enseignant).FirstOrDefault();
                if (idSuperviseur != 0)
                {
                    SessionBag.Current.idSuperviseur = idSuperviseur;
                }
                else
                    SessionBag.Current.idSuperviseur = 0;

                
                AjoutInfoConnection(PersonneBD);
                SessionBag.Current.id_Pers = PersonneBD.id_Pers;
                if (SouvenirConnexion)
                    CreerCookieConnexion(NomUsager, mdpPlain);
                else
                    SupprimerCookieConnexion();
                
                if (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Eleve)
                {
                    return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.id_Inscription });
                }
                else
                {
                    if(SachemIdentite.TypeListeProf.Contains(SachemIdentite.ObtenirTypeUsager(Session)) || SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Tuteur)
                    {
                        if (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Super)
                        {
                            return RedirectToAction("Index", "Enseignant");
                        }
                        else
                        {
                            return RedirectToAction("Index", "DossierEtudiant");
                        }
                    }
                    else
                    {
                        
                        Inscription inscription = db.Inscription.FirstOrDefault(c => c.id_Pers == PersonneBD.id_Pers);
                        if (inscription != null && inscription.id_Inscription == STATUT_ACCEPTE && inscription.ContratEngagement == false)
                        {
                            return RedirectToAction("Index","ContratEngagement");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Inscription");
                        }  
                    }
                }
            }
            return View(PersonneBD);
        }
        #endregion


        #region fn_Register
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (SachemIdentite.ObtenirTypeUsager(Session) != TypeUsagers.Aucun)
                return RedirectToAction("Error", "Home", null);
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");
            ViewBag.Autorisation = false;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //Fortement Extrait du PAM, approuvé par J, Lainesse
        public ActionResult Register(Personne personne)
        {
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");

            var validation = ConfirmeMdp(personne.MP, personne.ConfirmPassword);

            if (!validation)
                return View(personne);

            if (personne.Matricule7 == null)
                ModelState.AddModelError("Matricule7", Messages.U_001); 
            else if (personne.Matricule7.Length != 7 || !personne.Matricule.All(char.IsDigit)) 
                ModelState.AddModelError("Matricule7", Messages.U_004); 
            else if (db.Personne.Any(x => x.Matricule == personne.Matricule && x.MP != null))
                ModelState.AddModelError(string.Empty, Messages.I_025()); 
            else if (!db.Personne.Any(x => x.Matricule == personne.Matricule))
                ModelState.AddModelError(string.Empty, Messages.I_027()); 
            else
            {
                
                Personne EtudiantBD = db.Personne.AsNoTracking().Where(x => x.Matricule == personne.Matricule).FirstOrDefault();

                if (personne.DateNais != EtudiantBD.DateNais || personne.id_Sexe != EtudiantBD.id_Sexe)
                    ModelState.AddModelError(string.Empty, Messages.I_027());
                else
                {
                    EtudiantBD.Courriel = personne.Courriel;
                    EtudiantBD.Telephone = SachemIdentite.FormatTelephone(personne.Telephone);
                    EtudiantBD.MP = personne.MP;
                    EtudiantBD.ConfirmPassword = personne.ConfirmPassword;
                    SachemIdentite.encrypterMPPersonne(ref EtudiantBD);

                    db.Entry(EtudiantBD).State = EntityState.Modified;

                    #region try-catch pour une ligne de code db.SaveChanges() qui marche pas, throw raise sur la date invalide AAAA/MM/DD
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
                    #endregion


                    AjoutInfoConnection(EtudiantBD);
                    SessionBag.Current.id_TypeUsag = TypeUsagers.Etudiant;
                    TempData["Success"] = Messages.I_026();
                    return RedirectToAction("Index", "Inscription");
                }
            }
            return View(personne);
        }
        #endregion


        #region fn_MdpOublie

        // GET: /Account/Mot de passe oublié
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            if (SachemIdentite.ObtenirTypeUsager(Session) != TypeUsagers.Aucun)
                return RedirectToAction("Error", "Home", null);

            return View();
        }

        //
        // POST: /Account/Mot de passe oublié
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string courriel)
        {
            if (db.Personne.Any(y => y.Courriel == courriel && y.Actif == true))
            {
                string caracterePossible = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?";
                string nouveaumdp = "";
                Random r = new Random();

                for (int i = 0; i < 10; i++)
                    nouveaumdp = nouveaumdp + caracterePossible[r.Next(0, caracterePossible.Length)];

                if (EnvoyerCourriel(courriel, nouveaumdp))
                {
                    Personne utilisateur = db.Personne.AsNoTracking().Where(x => x.Courriel == courriel).FirstOrDefault();
                    utilisateur.MP = nouveaumdp;
                    utilisateur.MP = SachemIdentite.encrypterChaine( utilisateur.MP );
                    db.Entry(utilisateur).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Success = Messages.I_019();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Problème lors de l'envoi du courriel, le port" + PORTCOURRIEL.ToString() + "est bloqué.");
                }
            }
            else
            {
                ModelState.AddModelError("Courriel", Messages.C_003);
            }
            return RedirectToAction("ForgotPassword", "Account", null);
        }
        #endregion


        #region fn_ModifMDP
        //GET: /Account/Modifier Mot de passe
        [AllowAnonymous]
        public ActionResult ModifierPassword()
        {
            if (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Aucun) 
                return RedirectToAction("Error", "Home", null);
            return View();
        }
        //
        // POST: /Account/Modifier Mot de Passe
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ModifierPassword(Personne personne, string Modifier, string Annuler)
        {
            int idpersonne = SessionBag.Current.id_Pers;
            string ancienmdpbd = SessionBag.Current.MP;

            if (personne.AncienMotDePasse == null)
                ModelState.AddModelError("AncienMotDePasse", Messages.U_001); 

            if (!ConfirmeMdp(personne.MP, personne.ConfirmPassword))
                return View(personne);

            if (personne.AncienMotDePasse == null || personne.MP == null || personne.ConfirmPassword == null)
                return View(personne);

            if (SachemIdentite.encrypterChaine(personne.AncienMotDePasse) != ancienmdpbd)
            {
                ModelState.AddModelError("AncienMotDePasse", Messages.C_002);
                return View(personne);
            }
            else
            {
                Personne utilisateur = db.Personne.AsNoTracking().Where(x => x.id_Pers == idpersonne).FirstOrDefault();
                utilisateur.MP = personne.MP;
                utilisateur.ConfirmPassword = personne.ConfirmPassword;
                SachemIdentite.encrypterMPPersonne(ref utilisateur);
                SessionBag.Current.MP = utilisateur.MP;
                SupprimerCookieConnexion();
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Success = Messages.I_018();
                return View(personne);
            }
        }
        #endregion


        #region fn_Deconnexion

        //
        // GET: /Account/LogOff
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account", null);
        }

        #endregion


        #region Fonctions secondaires
        private bool ConfirmeMdp(string s1, string s2)
        {
            if (s1 == null || s2 == null)
            {
                ModelState.AddModelError("MP", Messages.U_001);
                ModelState.AddModelError("ConfirmPassword", Messages.U_001);
                return false;
            }
            if (s1 != s2)
            {
                ModelState.AddModelError("ConfirmPassword", Messages.C_001);
                return false;
            }
            return true;
        }
        private void AjoutInfoConnection(Personne personne)
        {
            SessionBag.Current.NomUsager = personne.NomUsager;
            SessionBag.Current.Matricule7 = personne.Matricule7;
            SessionBag.Current.NomComplet = personne.PrenomNom;
            SessionBag.Current.MP = personne.MP;
        }
        #endregion
    }
}
