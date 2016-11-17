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
        private readonly SACHEMEntities db = new SACHEMEntities();//retirer le readonly de private
        public AccountController()
        {

        }

        #region fn_CookieStuff
        //Pour l'encryption du cookie (MachineCode)
        #pragma warning disable 0618

        [NonAction]
        private void CreerCookieConnexion(string NomUsager, string MotDePasse)
        {
            string mdpEncrypte = Crypto.Encrypt(MotDePasse, System.Configuration.ConfigurationManager.AppSettings.Get("CryptoKey")); //Encrypte le mdp pour le cookie
            HttpCookie Maintenir = new HttpCookie("SACHEMConnexion");
            Maintenir.Values.Add("NomUsager", NomUsager); //On ajoute le nom utilisateur
            //met le mdp encrypté dans le cookie
            Maintenir.Values.Add("MP", mdpEncrypte);

            //Le cookie a une durée de 1 journée
            Maintenir.Expires = DateTime.Now.AddDays(1d);

            //Attache le cookie à la réponse
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
            //On récupère la collection de cookie de l'application
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
            //Création du message envoyé par courriel
            MailMessage message = new MailMessage("sachemcllmail@gmail.com", Email, "Demande de réinitialisation de mot de passe", "Voici votre nouveau mot de passe: " + nouveaumdp + " .");
            client.Port = PORTCOURRIEL;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("sachemcllmail@gmail.com", System.Configuration.ConfigurationManager.AppSettings.Get("EmailSachemMDP")); //information de connexion au email d'envoi de message de SACHEM
            message.BodyEncoding = Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            try//pour savoir si l'envoi à fonctionner
            {
                client.Send(message);//Envoi
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
                //Décrypte le mot de passe encodé dans le cookie
                PersonneCookie.MP = Crypto.Decrypt(CookieMaintenirConnexion["MP"], "asdjh213498yashj2134987ash");
                //Remet le maintenir connexion à oui
                PersonneCookie.SouvenirConnexion = true;

                //Permet de garder l'endroit où on se trouvait avant de cliquer sur se connecter.
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

            //Validations des champs et de la connection
            if (mdpPlain == "")
                ModelState.AddModelError("MP", Messages.U_001); //Mot de passe requis
            else
                if (NomUsager == null)
                {
                    ModelState.AddModelError("NomUsager", Messages.U_001); //NomUsager/Matricule requis
                }
                else
                    if (Regex.IsMatch(NomUsager, @"^\d+$") && NomUsager.Length == 7) //Vérifie que le matricule est 7 de long et est numérique
                    {
                        if (!db.Personne.Any(x => x.Matricule.Substring(2) == NomUsager))
                        {
                            ModelState.AddModelError(string.Empty, Messages.I_017());  //Erreur de connection
                        }
                        else
                            PersonneBD = db.Personne.AsNoTracking().Where(x => x.Matricule.Substring(2) == NomUsager).FirstOrDefault();
                    }
                    else
                        if (!db.Personne.Any(x => x.NomUsager == NomUsager))
                        {
                            ModelState.AddModelError(string.Empty, Messages.I_017());  //Erreur de connection
                        }
                        else
                            PersonneBD = db.Personne.AsNoTracking().Where(x => x.NomUsager == NomUsager).FirstOrDefault();
            if (ModelState.IsValid)
            {
                //Encrypter le mdp et tester la connection
                MP = SachemIdentite.encrypterChaine(MP);

                //Vérifie si le mot de passe concorde 
                if (PersonneBD.MP != MP)
                    ModelState.AddModelError(string.Empty, Messages.I_017()); //Erreur de connection
                if (!ModelState.IsValid)
                {
                    PersonneBD.MP = "";
                    return View(PersonneBD); //Retourne le formulaire rempli avec l'erreur
                }

                //On va chercher le type d'inscription dans la BD pour le présent utilisateur (si c'est un étudiant, il faut donner le type soit tuteur ou élève)
                var typeinscr = (from i in db.Inscription
                                 where i.id_Pers == PersonneBD.id_Pers
                                 select i.id_TypeInscription).FirstOrDefault();

                //On va chercher le id inscription pour identifier l'etudiant (eleve, tuteur) pour son dossier etudiant
                var idinscr = (from i in db.Inscription
                               where i.id_Pers == PersonneBD.id_Pers
                               select i.id_Inscription).FirstOrDefault();

                //conserver le typeinscrit
                if (idinscr != 0)
                    SessionBag.Current.id_Inscription = idinscr;
                else
                    SessionBag.Current.id_Inscription = 0;

                //Si c'est un tuteur, on a type = 6
                if (typeinscr > 1)
                {
                    SessionBag.Current.id_TypeUsag = TypeUsagers.Tuteur;
                }
                else
                {
                    //sinon, c'est un élève aidé.
                    if (typeinscr == 1)
                    {
                        SessionBag.Current.id_TypeUsag = TypeUsagers.Eleve;
                    }
                    //Si c'est pas un étudiant, on va chercher directement dans la BD pour voir le ID du type.
                    else
                    {
                        SessionBag.Current.id_TypeUsag = PersonneBD.id_TypeUsag;
                    }
                }

                //Enseignant
                //On va chercher les id des enseignants dans les jumelages pour verifier si l'enseignant connecte est affilie a un ou des jumelags lors de l'acces a Dossier Etudiant et ...
                var idSuperviseur = (from i in db.Jumelage
                                     where i.id_Enseignant == PersonneBD.id_Pers
                                     select i.id_Enseignant).FirstOrDefault();
                if (idSuperviseur != 0)
                {
                    SessionBag.Current.idSuperviseur = idSuperviseur;
                }
                else
                    SessionBag.Current.idSuperviseur = 0;

                //Si tout va bien, on rempli la session avec les informations de l'utilisateur!
                AjoutInfoConnection(PersonneBD);
                SessionBag.Current.id_Pers = PersonneBD.id_Pers;
                if (SouvenirConnexion)
                    CreerCookieConnexion(NomUsager, mdpPlain);
                else
                    SupprimerCookieConnexion();
                //On retourne à l'accueil en attendant de voir la suite.
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
                        return RedirectToAction("Index", "Inscription");
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
            //Modifié pour accommoder nos tables maison. Extrait du PAM adapté pour le SACHEM
            //Si connecté, on ne peux pas s'inscrire
            if (SachemIdentite.ObtenirTypeUsager(Session) != TypeUsagers.Aucun)
                return RedirectToAction("Error", "Home", null);
            //Récupère les sexes
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
            //Get le sexe du formulaire
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");

            var validation = ConfirmeMdp(personne.MP, personne.ConfirmPassword);

            if (!validation)
                return View(personne);

            if (personne.Matricule7 == null)
                ModelState.AddModelError("Matricule7", Messages.U_001); //requis
            else if (personne.Matricule7.Length != 7 || !personne.Matricule.All(char.IsDigit)) //vérifie le matricule
                ModelState.AddModelError("Matricule7", Messages.U_004); //longueur
            else if (db.Personne.Any(x => x.Matricule == personne.Matricule && x.MP != null))
                ModelState.AddModelError(string.Empty, Messages.I_025()); //Un compte existe déjà pour cet étudiant.
            else if (!db.Personne.Any(x => x.Matricule == personne.Matricule))
                ModelState.AddModelError(string.Empty, Messages.I_027()); //Aucun étudiant ne correspond aux données saisies. 
            else
            {
                //Sort la personne de la BD pour la compléter
                //Exemple du PAM grande inspiration
                Personne EtudiantBD = db.Personne.AsNoTracking().Where(x => x.Matricule == personne.Matricule).FirstOrDefault();

                //Erreur si les infos ne concordent pas
                if (personne.DateNais != EtudiantBD.DateNais || personne.id_Sexe != EtudiantBD.id_Sexe)
                    ModelState.AddModelError(string.Empty, Messages.I_027());
                else
                {
                    //Mise à jour des infos
                    EtudiantBD.Courriel = personne.Courriel;
                    EtudiantBD.Telephone = SachemIdentite.FormatTelephone(personne.Telephone);
                    EtudiantBD.MP = personne.MP;
                    SachemIdentite.encrypterMPPersonne(ref EtudiantBD);

                    db.Entry(EtudiantBD).State = EntityState.Modified;

                    #region try-catch pour une ligne de code db.SaveChanges() qui marche pas, throw raise sur la date invalide AAAA/MM/DD
                    try
                    {
                        db.SaveChanges();//essai de sauvegarder
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
                                // lever exception
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
            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            return View(personne);
        }
        #endregion


        #region fn_MdpOublie

        // GET: /Account/Mot de passe oublié
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            //Un utilisateur connecté ne peut pas récupérer sont mot de passe.
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
            if (db.Personne.Any(y => y.Courriel == courriel && y.Actif == true))//vérifie si le courriel est associé à un compte utilisateur
            {
                //Création du mot de passe
                //Inspiré par la fonction trouvée sur le site web: http://madskristensen.net/post/generate-random-password-in-c
                string caracterePossible = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?"; //liste de caractères qui sont utilisés pour la création du mot de passe
                string nouveaumdp = "";
                Random r = new Random();

                //cette boucle crée un nouveau mot de passe qui aura une longueur de 10 caractères contenue dans la variable caractèrepossible
                for (int i = 0; i < 10; i++)
                    nouveaumdp = nouveaumdp + caracterePossible[r.Next(0, caracterePossible.Length)];

                if (EnvoyerCourriel(courriel, nouveaumdp))//Envoi le courriel et test s'il a été envoyé avant d'enregistré le nouveau mot de passe, la méthode retourne true ou false.
                {
                    Personne utilisateur = db.Personne.AsNoTracking().Where(x => x.Courriel == courriel).FirstOrDefault();
                    utilisateur.MP = nouveaumdp;//Change le mot de passe
                    SachemIdentite.encrypterMPPersonne(ref utilisateur);//l'Encrypte
                    db.Entry(utilisateur).State = EntityState.Modified;
                    db.SaveChanges();//L'enregistre
                    TempData["Success"] = Messages.I_019();
                    return RedirectToAction("Login", "Account");
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
            return View();
        }
        #endregion


        #region fn_ModifMDP
        //GET: /Account/Modifier Mot de passe
        [AllowAnonymous]
        public ActionResult ModifierPassword()
        {
            if (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Aucun) //Si on n'est pas connecté, on peut pas modifier de mot de passe! You silly!
                return RedirectToAction("Error", "Home", null);
            return View();
        }
        //
        // POST: /Account/Modifier Mot de Passe
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ModifierPassword(Personne personne, string Modifier)
        {
            int idpersonne = SessionBag.Current.id_Pers;//Chercher l'id et le mot de passe de l'utilisateur en cours dans l'objet sessionbag
            string ancienmdpbd = SessionBag.Current.MP;

            if (personne.AncienMotDePasse == null)
                ModelState.AddModelError("AncienMotDePasse", Messages.U_001); //requis

            if (!ConfirmeMdp(personne.MP, personne.ConfirmPassword))
                return View(personne);

            if (personne.AncienMotDePasse == null || personne.MP == null || personne.ConfirmPassword == null) //Validation pour les champs requis
                return View(personne);

            if (SachemIdentite.encrypterChaine(personne.AncienMotDePasse) != ancienmdpbd)//Vérifier si le champ ancien mot de passe est le bon mot de passe
            {
                ModelState.AddModelError("AncienMotDePasse", Messages.C_002);
                return View(personne);
            }
            else
            {
                Personne utilisateur = db.Personne.AsNoTracking().Where(x => x.id_Pers == idpersonne).FirstOrDefault();
                utilisateur.MP = personne.MP;//Change le mot de passe
                SachemIdentite.encrypterMPPersonne(ref utilisateur);//l'Encrypte
                SessionBag.Current.MP = utilisateur.MP;//Modifier le mot de passe dans le sessionbag
                SupprimerCookieConnexion(); //Supprime le cookie
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();//L'enregistre
                ViewBag.Success = Messages.I_018();
                return View(personne);
            }
            return View();
        }
        #endregion


        #region fn_Deconnexion

        //
        // GET: /Account/LogOff
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            //Supprime les données contenues dans la session et supprime le cookie puis retour à l'index.
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