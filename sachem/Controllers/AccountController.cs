using System;
using System.Data.Entity;
using System.Linq;

using System.Web.Mvc;

using System.Text.RegularExpressions;
using sachem.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace sachem.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();



        public AccountController()
        {

        }

        [NonAction]
        private bool EnvoyerCourriel(String Email, string nouveaumdp)
        {
            SmtpClient client = new SmtpClient();
            //Création du message envoyé par courriel
            MailMessage message = new MailMessage("sachemcllmail@gmail.com", Email, "Demande de réinitialisation de mot de passe", "Voici votre nouveau mot de passe: " + nouveaumdp + " .");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("sachemcllmail@gmail.com", "sachemadmin#123"); //information de connection au email d'envoi de message de SACHEM
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
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string NomUsager, string MP)
        {
            string mdpPlain = MP;
            Personne PersonneBD = new Personne();
            //Validations des champs et de la connection
            if (mdpPlain == "")
                ModelState.AddModelError("MP", Messages.U_001); //enter mdp
            else
                if (NomUsager == null)
                ModelState.AddModelError("NomUsager", Messages.U_001); //enter user
                else
                    if (Regex.IsMatch(NomUsager, @"^\d+$") && NomUsager.Length == 7) //Vérifie que le matricule est 7 de long
                        if (!db.Personne.Any(x => x.Matricule.Substring(2) == NomUsager))
                            ModelState.AddModelError(string.Empty, Messages.I_017());  //Erreur de connection
                        else
                            PersonneBD = db.Personne.AsNoTracking().Where(x => x.Matricule.Substring(2) == NomUsager).FirstOrDefault();
                    else
                        if (!db.Personne.Any(x => x.NomUsager == NomUsager))
                            ModelState.AddModelError(string.Empty, Messages.I_017());  //Erreur de connection
                        else
                            PersonneBD = db.Personne.AsNoTracking().Where(x => x.NomUsager == PersonneBD.NomUsager).FirstOrDefault();
            if (ModelState.IsValid)
            {
                //Encrypter le mdp et tester la connection
                MP = SachemIdentite.encrypterChaine(MP);
                                
                //Vérifie si le mot de passe concorde 
                if (PersonneBD.MP != MP)
                    ModelState.AddModelError(string.Empty, Messages.I_017()); //Erreur de connection
                if (!ModelState.IsValid)
                    return View(PersonneBD); //Retourne le formulaire rempli avec l'erreur

               /* var idInscr = db.Inscription.AsNoTracking().Where(x => x.id_Pers == PersonneBD.id_Pers).FirstOrDefault();
                var typeInscr = db.p_TypeInscription.AsNoTracking().Where(x => x.id_TypeInscription == idInscr.id_Inscription).FirstOrDefault();
                */
                //Si tout va bien, on rempli la session avec les informations de l'utilisateur!
                SessionBag.Current.NomUsager = PersonneBD.NomUsager;
                SessionBag.Current.Matricule7 = PersonneBD.Matricule7;
                SessionBag.Current.NomComplet = PersonneBD.PrenomNom;
                SessionBag.Current.MP = PersonneBD.MP;
                SessionBag.Current.id_TypeUsag = PersonneBD.id_TypeUsag;
                SessionBag.Current.id_Pers = PersonneBD.id_Pers;

                //On retourne à l'accueil en attendant de voir la suite.
                return RedirectToAction("Index", "Home");
            }
            return View(PersonneBD);
        }
        
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
        public ActionResult Register(Personne personne)
            //Fortement Extrait du PAM, approuvé par J, Lainesse
        {
            //Get le sexe du formulaire
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");

            if (personne.MP == null)
            {
                ModelState.AddModelError("MP", Messages.U_001); //requis
                ModelState.AddModelError("ConfirmPassword", Messages.U_001); //requis
            }
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
                    db.SaveChanges();

                    ViewBag.Success = Messages.I_026();
                 
                    return View();
                }
            }
            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            return View(personne);
        }


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
                for (int i = 0; i < 10; i++)
                    //cette boucle crée un nouveau mot de passe qui aura une longueur de 10 caractères contenue dans la variable caractèrepossible
                {
                    nouveaumdp = nouveaumdp + caracterePossible[r.Next(0, caracterePossible.Length)];
                }
                if (EnvoyerCourriel(courriel, nouveaumdp))//Envoi le courriel et test s'il a été envoyé avant d'enregistré le nouveau mot de passe, la méthode retourne true ou false.
                {
                    Personne utilisateur = db.Personne.AsNoTracking().Where(x => x.Courriel == courriel).FirstOrDefault();
                    utilisateur.MP = nouveaumdp;//Change le mot de passe
                    SachemIdentite.encrypterMPPersonne(ref utilisateur);//l'Encrypte
                    db.Entry(utilisateur).State = EntityState.Modified;
                    db.SaveChanges();//L'enregistre
                    ViewBag.Success = Messages.I_019();
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"Problème lors de l'envoi du courriel, le port 587 est bloqué.");
                }
            }
            else
            {
                ModelState.AddModelError("Courriel",Messages.C_003);
            }
            return View(courriel);
        }
        //GET: /Account/Modifier Mot de passe
        [AllowAnonymous]
        public ActionResult ModifierPassword()
        {
            return View();
        }
        //
        // POST: /Account/Modifier Mot de Passe
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ModifierPassword(Personne personne,string Modifier,string Annuler)
        {
            if(Annuler != null)//Verifier si c'est le bouton annuler qui a été cliqué
            {
                return RedirectToAction("Index", "Home");
            }
            if (Modifier != null)//Si modifier mdp a été cliqué
            {
                int idpersonne = SessionBag.Current.id_Pers;//Chercher l'id et le mot de passe de l'utilisateur en cours
                string ancienmdpbd = SessionBag.Current.MP;
                if (personne.AncienMotDePasse == null)
                {
                    ModelState.AddModelError("AncienMotDePasse", Messages.U_001); //requis
                }
                if (personne.MP == null)
                {
                    ModelState.AddModelError("MP", Messages.U_001); //requis
                }
                if (personne.ConfirmPassword == null)
                {
                    ModelState.AddModelError("ConfirmPassword", Messages.U_001); //requis
                }
                if (personne.AncienMotDePasse == null || personne.MP == null || personne.ConfirmPassword == null)//Validation pour les champs requis
                {
                    return View(personne);
                }
                if (SachemIdentite.encrypterChaine(personne.AncienMotDePasse) != ancienmdpbd)
                {
                    ModelState.AddModelError("AncienMotDePasse", Messages.C_002);
                    return View(personne);
                }
                else
                {
                    Personne utilisateur = db.Personne.AsNoTracking().Where(x => x.id_Pers == idpersonne).FirstOrDefault();
                    utilisateur.MP = personne.MP;//Change le mot de passe
                    SachemIdentite.encrypterMPPersonne(ref utilisateur);//l'Encrypte
                    db.Entry(utilisateur).State = EntityState.Modified;
                    db.SaveChanges();//L'enregistre
                    ViewBag.Success = Messages.I_018();
                    return View(personne);
                }
            }
            return View();
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            SessionBag.Current.NomUsager = null;
            SessionBag.Current.Matricule7 = null;
            SessionBag.Current.NomComplet = null;
            SessionBag.Current.MP = null;
            SessionBag.Current.id_TypeUsag = null;
            SessionBag.Current.id_Pers = null;
            return RedirectToAction("Index", "Home");
        }
    }
}