using System.Web.Mvc;

namespace sachem.Models
{
    public class Messages
    {
        public const string NomDuSite = "Sachem";
        public const string MotsDePasseDoiventEtreIdentiques=
            "Le mot de passe et la confirmation du mot de passe doivent être identiques.";

        #region MessageUnitaire

        public const string ChampRequis = "Requis.";
        public const string LongueurDeHuitCaracteres = "Longueur requise : 8 caractères.";
        public const string LongueurDeSeptCaracteres = "Longueur requise: 7 caractères.";
        public const string LongueurDeQuatreCaracteres = "Longueur requise: 4 caractères";
        public const string ResultatRequisSiReussi = "Le résultat est requis si le statut du cours est réussi.";
        public const string FormatAnnee = "Format : AAAA";
        public const string PlaceHolderDate = "AAAA/MM/JJ";
        public const string FormatEnDate = "Format: AAAA/MM/JJ";
        public const string FormatDeCourriel = "Format: nom@nomdomaine.com";
        public const string FormatTelephone = "Format : (999) 999-9999";
        public const string PlaceHolderTelephone = "(999) 999-9999";
        public const string PlaceHolderMotDePasseSecondeSaisie = "Confirmer le mot de passe";
        public const string FormatHeureMinuteSeconde = "Format : HH:MM:SS";
        public const string PlaceHolderHeure = "HH:MM:SS";
        public const string DatePlusHauteQueLAnneeDeFondationDuCegep = "La date indiquée doit être entre l'année 1967 et celle en cours.";
        public const string LongueurDeSixCarateres = "Longueur minimale de 6 caractères.";
        public const string NombreEntreZeroEtCent = "Veuillez entrer un nombre en 0 et 1000.";
        public const string ErreurLorsEnregistrement = "Erreur lors de l'enregistrement";
        public const string AucunCoursSuiviCegep = "Aucun cours suivi au Cégep";

        #endregion


        #region Inscription

        public const string InscriptionRechercheModifierInscriptionErreur = 
            "Erreur lors de la modification de l'inscription. N'oubliez pas qu'il est impossible de modifier l'inscription des anciennes sessions.";
        public const string InscriptionCoursReussiErreurLorsDuChargement = 
            "Un resultat et un statut d'un des cours ne concorde pas. Un cours réussi doit avoir une note supérieur " +
            "ou égal à 60 et un échec inférieur à 60.";
        public const string InscriptionCoursReussiIndication = 
            "Indiquez tous les cours de mathématiques(collégial) que vous avez réussis, abandonnés ou échoués au cégep. " +
            "Pour ceux réussis, indiquez vos résultats :";
        public const string InscriptionDirectivesTableauDisponibilite1 = "Indiquez vos heures de disponibilité en cliquant dans les cases appropriées.";
        public const string InscriptionDirectivesTableauDisponibilite2 = 
            "Chaque ✓ indique que vous êtes disponible pour une rencontre de 1h30 dans cette plage horaire";
        public const string InscriptionDirectivesTableauDisponibilite3 = "Il y aura au minimum une rencontre par semaine, soit 1h30/semaine";
        public const string InscriptionDirectivesTableauDisponibilite4 = "Inscrivez plusieurs choix pour faciliter le jumelage.";
        public const string InscriptionConfirmationDispoTitre = "Confirmation de vos disponibilités";
        public const string InscriptionTypeInscriptionChoisi = "Type d'inscription choisi:";
        public const string InscriptionTypeInscriptionChoisiAlert = "Veuillez choisir un type d'inscription";
        public const string InscriptionDisposChoisis = "Vos plages de disponibilités sélectionnées sont:";
        public const string InscriptionRemplirFormulaireDisposErreur = "Veuillez remplir le formulaire de disponibilités.";
        public const string InscriptionCoursQueJaide = "Les cours dans lesquels vous aimeriez donner de l'aide sont:";
        public const string InscriptionCoursSuivis = "Indiquez tous les cours de mathématiques (collégial) que vous avez suivis et réussis ainsi " +
                                                     "que le cégep où ces cours ont eu lieu :";
        public const string InscriptionCombienHeuresPretADonner = "Combien d'heures êtes vous pret à donner à chaque semaine (1h30/élève aidé)?";
        public const string InscriptionCoursChoisiUneSeuleFois = "Un cours ne peut être choisi qu'une seule fois.";
        public const string InscriptionRencontresConsecutives = "Êtes-vous prêt à faire deux rencontres consécutives (3h) ?";

        #endregion


        #region Importer données

        /// <summary>
        /// Le fichier est trop énorme: ({0}MiB). Taille maximale: {1}MiB
        /// </summary>
        /// <param name="filesize"></param>
        /// <param name="maxFilesize"></param>
        /// <returns></returns>
        public static string ImporterFichierTropEnorme(string filesize, string maxFilesize)
        { return $"Le fichier est trop énorme: ({filesize}MiB). Taille maximale: {maxFilesize}MiB."; }

        /// <summary>
        /// Seulement les fichiers de type {0} sont acceptés.
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string ImporterFichierCsvSeulement(string extension)
        { return $"Seulement les fichiers de type {extension} sont acceptés."; }

        /// <summary>
        /// Le serveur a répondu le code d'erreur: {0}.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static string ImporterCodeErreurServeur(string statusCode)
        { return $"Le serveur a répondu le code d'erreur: {statusCode}."; }

        /// <summary>
        /// Utiliser cette page pour téléverser le ficher de données ({0}) reçu de l'administration du CLL.
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string ImporterDirectivesDropZone(string extension)
        { return $"Utiliser cette page pour téléverser le ficher de données ({extension}) reçu de l'administration du CLL."; }

        /// <summary>
        /// Un fichier {0} de même nom est déjà présent sur le serveur.
        /// </summary>
        /// <param name="fichier"></param>
        /// <returns></returns>
        public static string ImporterFichierAvecLeMemeNomExisteDeja(string fichier)
        { return $"Un fichier {fichier} de même nom est déjà présent sur le serveur."; }

        /// <summary>
        /// Erreur lors du transfert de fichier {0}.
        /// </summary>
        /// <param name="fichier"></param>
        /// <returns></returns>
        public static string ImporterErreurTransfertFichier(string fichier)
        { return $"Erreur lors du transfert du fichier {fichier}."; }

        public const string ImporterAnnulerTeleversementConfirmation = "Etes-vous certain d'annuler le téléversement?";
        public const string ImporterTitre = "Importation de données";
        public const string ImporterMaxAtteint = "Vous ne pouvez pas téléverser plus de fichiers.";
        public const string ImporterTempsDeTraitementsEnMinutes = "Les fichers seront traités dans les prochaines dix minutes.";
        public const string ImporterDirectivesDropZoneFichier = "Glisser les fichiers ici ou cliquez ici pour téléverser.";
        public const string ImporterNavigateurNeSupportePasDropZone = "Votre navigateur ne supporte pas la fonction glisser-déposer de fichiers.";

        #endregion


        #region Contrat Engagement

        public const string ContratCaseDoitEtreCochee = "Cochez la case pour signer le contrat.";
        public const string ContratTitre = "Contrat d'engagement";
        public const string ContratJeMengageA = "Dans le but d'assurer l'efficacité du service de tutorat offert au Sachem, vous vous engagez à:";
        public const string ContratClause1 = "Respecter l'horaire prévu et à aviser votre partenaire dans le cas d'un retard prévisible " +
                                             "ou d'une absence. Le tuteur a la responsabilité de compléter un billet d'absence.";
        public const string ContratClause2 = "Reprendre dans les plus bref délais toute rencontre annulée " +
                                             "(au plus tard lors des 2 semaines qui suivront la rencontre).";
        public const string ContratClause3 = "Préparer vos rencontres de façon à utiliser adéquatement les périodes de tutorat. Chacun doit " +
                                             "fournir une certaine somme de travail entre les rencontres afin que celles-ci atteignent leur " +
                                             "objectif. À cette fin, l'élève aidé accepte que son tuteur vérifie au début de chaque rencontre " +
                                             "le travail effectué (exercices, résumés, etc.) depuis la rencontre précédente.";
        public const string ContratClause4 = "Accepter dans les plus bref délais toute rencontre annulée " +
                                             "(au plus tard lors des 2 semaines qui suivront la rencontre).";
        public const string ContratClause5 = "Aviser la sécretaire du Sachem si vous décidez d'abandonner votre cours ou le cégep, et ce, le plus tôt possible.";
        public const string ContratClause6 = "Aviser la sécretaire du Sachem si vous décidez d'interrompre vos rencontres de tutorat, et ce, le plus tôt possible.";
        public const string ContratClause7 = "Je m'engage à travailler sérieusement au Sachem, à être ponctuel et respectueux. De plus, je m'engage à " +
                                             "fournir des questions à mon tuteur 48 heures à l'avance afin que celui-ci puisse se préparer adéquatement.";
        public const string ContratClause8 = "Je m'engage à bien préparer mes rencontres, à être ponctuel et respectueux.";
        public const string ContratConfirmation = "Votre contrat d'engagement a déjà été signé.";

        #endregion


        #region Account

        /// <summary>
        /// Problème lors de l'envoi du courriel, le port {0} est bloqué.
        /// </summary>
        /// <param name="numeroPort"></param>
        /// <returns></returns>
        public static string AccountEnvoiCourrielImpossiblePortBloque(string numeroPort)
        { return $"Problème lors de l'envoi du courriel, le port {numeroPort} est bloqué."; }

        public static string AccountEnvoieMotDePasseParCourriel(string emailDestinataire)
        { return $"Si un compte est associé à {emailDestinataire}, vous recevrez un courriel avec un nouveau mot de passe que vous pourrez changer."; }

        public const string AccountPasswordErreurAncienMotDePasseInvalide = "L'ancien mot de passe est invalide.";
        public const string AccountForgotPasswordErreurAucunUsager="Aucun usager associé à cette adresse courriel.";
        public const string AccountConnexionErreur = "Votre tentative de connexion a échouée. Réessayez.";
        public const string AccountMotDePasseModifie = "Mot de passe modifié";
        public const string AccountExisteDeja = "Un compte existe déjà pour cet étudiant.";
        public const string AccountCree = "Le compte a été créé. Vous pouvez maintenant vous inscrire au Sachem.";
        public const string AccountCreerErreurEtudiantNonInscrit = 
            "Aucun étudiant ne correspond aux données saisies. Vous devez être inscrit à un cours offert par le département de mathématiques.";
        public const string AccountForgotPasswordEntrerCourriel = "Veuiller entrer le courriel rattaché à votre compte.";
        public const string AccountForgotPasswordEntrerCourrielExplication = 
            "Nous vous enverrons un courriel contenant les renseignements sur votre mot de passe.";
        public const string AccountForgotPasswordTitre = "Mot de passe oublié";
        public const string AccountLoginUtiliserCompteConnexion = "Utilisez un compte pour vous connecter.";
        public const string AccountLoginPasDeCompte = "Vous n'avez pas de compte? ";
        public const string AccountLoginConnexionEtudiantRestriction = "Votre nom d'utilisateur est votre numéro de DA (7 chiffres).";
        public const string AccountLoginConnexionEnseignantRestriction = "Votre nom d'utilisateur est votre nom, suivi de la première lettre de votre prénom.";
        public const string AccountPasswordUtiliserFormulaire = "Utilisez le formulaire ci-dessous pour changer votre mot de passe.";
        public const string AccountRegisterTitre = "Ouverture du compte";

        #endregion


        #region Consulter cours

        public const string ConsulterCoursDetailsTitre = "Détails du cours";
        public const string ConsulterCoursDetailsNombreEtu = "Nombre d'étudiants";
        public const string ConsulterCoursDetailsModifierGroupe = "Modifier groupe";
        public const string ConsulterCoursIndexTitre = "Consulter cours";

        #endregion


        #region Jumelage

        public static MvcHtmlString JumelageSupprimerQuestion()
        {
            return
                MvcHtmlString.Create(
                    $"Êtes-vous certain de mettre fin au jumelage entre: " +
                    $"<br/><b><span id='modalRetirerVu'></span></b>" +
                    $" et " +
                    $"<b><span id='modalRetirerJumeleA'></span></b>" +
                    $"?");
        }

        public static MvcHtmlString JumelageAjouterQuestion()
        {
            return
                MvcHtmlString.Create(
                    $"<p>" +
                    $"Êtes-vous certain de créer un jumelage entre: " +
                    $"<br /><b><span id='modalAjoutVu'></span></b> (<span id='modalAjoutTypeEleveVu'></span>)" +
                    $" et " +
                    $"<b><span id ='modalAjoutJumeleA'></span></b> (<span id='modalAjoutTypeEleveJumeleA'></span>)" +
                    $"?" +
                    $"</p><p>" +
                    $"Pour la plage horaire de:" +
                    $"<br /><b><span id='modalPlageHoraire'></span></b></p><p>" +
                    $"Avec comme superviseur enseignant:" +
                    $"</p>");
        }

        public const string JumelageSupprime = "Le jumelage a bien été retiré.";
        public const string JumelageAjoute = "Le jumelage a bien été crée.";
        public const string JumelageAjouterErreurSelectionnerEnseignant = "Veuillez sélectionner un enseignant.";
        public const string JumelageSupprimerJumelage = "Mettre fin à un jumelage";
        public const string JumelageAjouterJumelage = "Ajouter un jumelage";
        public const string JumelageAjouterQuestionConsecutif = "Désirez-vous étendre la durée de cette rencontre sur 3h " +
                                                                "pour créer un jumelage avec deux rencontres consécutives ?";

        #endregion


        #region Programmes Offerts

        public const string ProgrammesAjouterTitre = "Ajouter un programme d'étude";
        public const string ProgrammesSupprimerTitre = "Supprimer un programme d'étude";
        public const string ProgrammesModifierTitre = "Modifier un programme d'étude";
        public const string ProgrammeCodeOuNom = "Code ou nom de programme";
        public const string ProgrammeInactifErreur = "Impossible de mettre le programme inactif s'il est encore relié à des étudiants.";
        public static string ProgrammeSupprimerErreurEtudiantAssocie = "Un étudiant est associé à ce programme d'études. " +
                                                                       "Ce programme ne peut être supprimé.";

        /// <summary>
        /// Voulez-vous vraiment supprimer le programme d'études {0} ?
        /// </summary>
        /// <param name="nomProgrammeEtude"></param>
        /// <returns></returns>
        public static string ProgrammeSupprimerQuestion(string nomProgrammeEtude)
        { return $"Voulez-vous vraiment supprimer le programme d'études {nomProgrammeEtude} ?"; }
        
        /// <summary>
        /// Impossible d'enregistrer ce programme d'études. Il existe déjà un programme ayant le code {0}.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ProgrammeAjouterErreurExisteDeja(string code)
        { return $"Impossible d'enregistrer ce programme d'études. Il existe déjà un programme ayant le code {code}."; }

        ///<summary>
        /// Le programme d'études {0} a été enregistré
        /// </summary>
        ///<param name="nomProgrammeEtude"></param>
        /// <returns></returns>
        public static string ProgrammeEnregistre(string nomProgrammeEtude)
        { return $"Le programme d'études {nomProgrammeEtude} a été enregistré."; }

        ///<summary>
        /// Le programme d'études {0} a été supprimé
        /// </summary>
        /// <param name="nomProgrammeEtude"></param>
        /// <returns></returns>
        public static string ProgrammeSupprime(string nomProgrammeEtude)
        { return $"Le programme d'études {nomProgrammeEtude} a été supprimé."; }

        #endregion


        #region Parametres - college - contact - courriel - horaire

        ///<summary>
        /// Les dates de début et de fin doivent faire partie de la session sélectionnée.
        /// </summary>
        /// <param name="dateDebut"></param>
        /// <param name="dateFin"></param>
        /// <returns></returns>
        public static string HoraireDatesRestriction(string dateDebut, string dateFin)
        { return $"Les dates de début et de fin doivent faire partie de la session. {dateDebut} et {dateFin}."; }

        /// <summary>
        /// Le collège {0} a été ajouté
        /// </summary>
        /// <param name="nomCollege"></param>
        /// <returns></returns>
        public static string CollegeAjoute(string nomCollege)
        { return $"Le collège {nomCollege} a été ajouté."; }

        /// <summary>
        /// Le collège {0} a bien été supprimé
        /// </summary>
        /// <param name="nomCollege"></param>
        /// <returns></returns>
        public static string CollegeSupprime(string nomCollege)
        { return $"Le collège {nomCollege} a bien été supprimé."; }

        public const string CollegeTitre = "Collèges d'enseignement";
        public const string CollegeAjouter = "Ajouter un collège d'enseignement";
        public const string CollegeModifier = "Modifier le nom du collège d'enseignement.";
        public const string CollegeNom = "Nom du collège d'enseignement:";
        public const string CollegeSupprimer = "Supprimer un collège d'enseignement.";
        public const string CollegeModifie = "Le nom du collège a bien été modifié.";
        public const string CollegeSupprimerConfirmation = "Voulez-vous vraiment supprimer ce collège d'enseignement ?";
        public const string CollegeDejaExistant = "Ce collège d'enseignement existe déjà.";

        public const string ContactTitre = "Modifier les informations de la page 'Nous contacter'";
        public const string ContactMisAJour="L’information de la section « Nous contacter » a été mise à jour.";

        public const string CourrielTitre = "Modifier courriel";
        public const string CourrielVarPrenomNom = "Affiche au destinataire son prénom et nom.";
        public const string CourrielVarNouveauPasse ="Affiche le nouveau mot de passe au destinataire, après une réinitialisation.";
        public const string CourrielVarsPourPersonnaliser = "Voici les mots clés pour personnaliser votre courriel";
        public const string CourrielCree="Le courriel a été ajouté.";
        public const string CourrielMisAJour="Le courriel a été mis à jour.";

        public const string HoraireTitre = "Modifier l'horaire d'ouverture et de fermeture d'inscription";
        public const string HoraireMisAJour="L’horaire d’inscription au SACHEM a été mis à jour.";
        public const string HoraireValidationDate = "La date de début doit être antérieure à la date de fin.";
        


        #endregion


        #region Cours

        /// <summary>
        /// Voulez-vous vraiment supprimer le cours {0}?
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>
        public static string CoursSupprimerQuestionSupprimerCours(string cours)
        { return $"Voulez-vous vraiment supprimer le cours {cours}?"; }

        /// <summary>
        /// Le cours {0} a été supprimé.
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>
        public static string CoursSupprime(string cours)
        { return $"Le cours {cours} a été supprimé."; }

        /// <summary>
        /// Il existe déjà un cours ayant le code {0}.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string CoursAjouterErreurCodeExisteDeja(string code)
        { return $"Il existe déjà un cours ayant le code {code}."; }

        /// <summary>
        /// Le cours {0} a été enregistré.
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>private string I_003(string cours)
        public static string CoursEnregistre(string cours)
        { return $"Le cours {cours} a été enregistré."; }

        public const string CoursAjouterTitre = "Ajouter un cours";
        public const string CoursModifierTitre = "Modifier un cours";
        public const string CoursSupprimerTitre = "Supprimer un cours";
        public const string CoursSupprimerErreurGroupeAssocie = "Un groupe est associé à ce cours. Le cours ne peut être supprimé.";

        #endregion


        #region Cours suivis

        /// <summary>
        /// Un des deux champs {0}, {1} doit être complété.
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        public static string CoursSuiviCompleterLesChamps(string param1, string param2)
        { return $"Un des deux champs {param1}, {param2} doit être complété."; }

        public const string CoursSuiviAjouterErreurExisteDeja=
            "Impossible d’ajouter ce cours, puisqu'il est présent dans votre liste de cours suivis antérieurement.";
        public const string CoursSuiviAjouterTitre = "Ajouter un cours suivi antérieur";
        public const string CoursSuiviModifierTitre = "Modifier un cours suivi antérieur";
        public const string CoursSuiviSupprimerTitre = "Supprimer un cours suivi antérieur";


        #endregion


        #region Dossier etudiant

        public const string DossierEtudiantRechercheTitre = "Recherche des dossiers d'étudiant";
        public const string DossierEtudiantCoursAnterieur = "Cours de mathématiques suivis antérieurement";
        public const string DossierEtudiantAjouterCoursAnterieur = "Ajouter un cours suivi antérieur";
        public const string DossierEtudiantDemandeInscription = "Demande d'inscription";
        public const string DossierEtudiantIdentificationEtu = "Identification de l'étudiant";
        public const string DossierEtudiantBonAchatAchete = "Le bon d'achat a été acheté.";
        public const string DossierEtudiantBonAchatPasAchete = "Le bon d'achat n'a pas été acheté.";
        public const string DossierEtudiantTelephoneModifieSucces = "Le numéro de téléphone a été modifié.";
        public const string DossierEtudiantTelephoneModifieErreur = "Le numéro de téléphone saisi est invalide.";
        public const string DossierEtudiantCourrielModifieSucces = "L'adresse courriel a été modifiée.";
        public const string DossierEtudiantCourrielModifieErreur = "L'adresse courriel saisie est invalide.";

        #endregion


        #region Enseignant

        /// <summary>
        /// Voulez-vous vraiment supprimer l'enseignant {0} ?
        /// </summary>
        /// <param name="enseignant"></param>
        /// <returns></returns>
        public static string EnseignantSupprimerQuestionSupprimerEnseignant(string enseignant)
        { return $"Voulez-vous vraiment supprimer l'enseignant {enseignant} ?"; }

        public const string EnseignantSupprimerErreurJumelagePresent="L'enseignant ne peut être supprimé car il est encore présent dans un jumelage.";

        public const string EnseignantSupprimerErreurLuiMeme="Un responsable ne peut pas se supprimer lui-même.";

        public const string EnseignantSupprimerErreurLierCours = "L'enseignant ne peut être supprimé car il est relié à un cours.";

        /// <summary>
        /// Impossible d’enregistrer cet enseignant. Il existe déjà un enseignant ayant le nom d'usager {0}.
        /// </summary>
        /// <param name="nomUsager"></param>
        /// <returns></returns>
        public static string EnseignantAjouterErreurExisteDeja(string nomUsager)
        { return $"Impossible d’enregistrer cet enseignant. Il existe déjà un enseignant ayant le nom d'usager {nomUsager}."; }

        /// <summary>
        /// L'usager {0} a été modifié.
        /// </summary>
        /// 
        /// <param name="nomUsager"></param>
        /// <returns></returns>
        public static string EnseignantModifierUsagerModfie(string nomUsager)
        { return $"L'usager {nomUsager} a été modifié."; }

        /// <summary>
        /// L'enseignant {0} a été créé. Souhaitez-vous y associer un groupe?
        /// </summary>
        /// <param name="nomUsager"></param>
        /// <param name="idEnseignant"></param>
        /// <returns></returns>
        public static MvcHtmlString EnseignantAjouterUnGroupeAEnseignant(string nomUsager, int idEnseignant)
        { return MvcHtmlString.Create($"L'enseignant {nomUsager} a été créé. Souhaitez-vous <a href=\"/Groupes/Create?idEns={idEnseignant}\">y associer un groupe?</a>"); }

        /// <summary>
        /// L'enseignant {0} a été supprimé.
        /// </summary>
        /// <param name="enseignant"></param>
        /// <returns></returns>
        public static string EnseignantSupprime(string enseignant)
        { return $"L'enseignant {enseignant} a été supprimé."; }

        public const string EnseignantAjouterTitre = "Ajouter un enseignant";

        public const string EnseignantModifierTitre = "Modifier un enseignant";

        public const string EnseignantSupprimerTitre = "Supprimer un enseignant";

        #endregion


        #region Etudiant


        /// <summary>
        /// Impossible d'enregistrer cet étudiant. Il existe déja im tudiant ayant le matricule {0}.
        /// </summary>
        /// <param name="matricule"></param>
        /// <returns></returns>
        public static string EtudiantAjouterErreurMatriculeExisteDeja(string matricule)
        { return $"Impossible d'enregistrer cet étudiant. Il existe déja un étudiant ayant le matricule {matricule}."; }

        /// <summary>
        /// L'étudiant {0} a été enregistré.
        /// </summary>
        /// <param name="etudiant"></param>
        /// <returns></returns>
        public static string EtudiantEnregistre(string etudiant)
        { return $"L'étudiant {etudiant} a été enregistré."; }

        /// <summary>
        /// Impossible de retirer le programme d'étude {0} des programmes suivis par l'étudiant.
        /// </summary>
        /// <param name="programme"></param>
        /// <returns></returns>
        public static string EtudiantProgrammeSupprimerErreur(string programme)
        { return $"Impossible de retirer le programme d'étude {programme} des programmes suivis par l'étudiant."; }
        
        /// <summary>
        /// Le programme d'étude {0} a été retiré de la liste des programmes suivis par l'étudiant.
        /// </summary>
        /// <param name="programme"></param>
        /// <returns></returns>
        public static string EtudiantProgrammeSupprimerListeEtudiant(string programme)
        { return $"Le programme d'étude {programme} a été retiré de la liste des programmes suivis par l'étudiant."; }

        /// <summary>
        /// Voulez-vous vraiment supprimer l'étudiant {0} ?
        /// </summary>
        /// <param name="etudiant"></param>
        /// <returns></returns>
        public static string EtudiantSupprimerQuestionSupprimerEtudiant(string etudiant)
        { return $"Voulez-vous vraiment supprimer l'étudiant {etudiant} ?"; }

        /// <summary>
        /// L'étudiant {0} a été supprimé.
        /// </summary>
        /// <param name="etudiant"></param>
        /// <returns></returns>
        public static string EtudiantSupprime(string etudiant)
        { return $"L'étudiant {etudiant} a été supprimé."; }

        /// <summary>
        /// L'étudiant {0} a été modifié.
        /// </summary>
        /// <param name="nomEtudiant"></param>
        /// <returns></returns>
        public static string EtudiantModifie(string nomEtudiant)
        { return $"L'étudiant {nomEtudiant} a été modifié."; }

        public const string EtudiantSupprimerErreurLierGroupe = "L'étudiant ne peut être supprimé, car il est associé à un groupe.";

        public const string EtudiantSupprimerErreurJumele="L'étudiant ne peut être supprimé s'il est jumelé.";

        public const string EtudiantAjouterTitre = "Ajouter un étudiant";

        public const string EtudiantModifierTitre = "Modifier un étudiant";

        public const string EtudiantSupprimerTitre = "Supprimer un étudiant";

        public const string EtudiantProgrammeEtudeSupprimerTitre = "Supprimer un programme d'étude";

        public const string EtudiantProgrammeEtudeQuestionSupprimerProgrammeSansCode = "Voulez-vous vraiment supprimer le programme d'études:";

        /// <summary>
        /// Voulez-vous vraiment supprimer le programme d'études {0}
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string EtudiantProgrammeEtudeQuestionSupprimerProgramme(string code)
        { return $"Voulez-vous vraiment supprimer le programme d'études {code}?"; }

        #endregion


        #region Groupes
        /// <summary>
        /// Voulez-vous vraiment supprimer le groupe {0} ?
        /// </summary>
        /// <param name="groupe"></param>
        /// <returns></returns>
        public static string GroupesSupprimerQuestion(string groupe)
        { return $"Voulez-vous vraiment supprimer le groupe {groupe} ?"; }

        /// <summary>
        /// L'étudiant {0} est déjà inscrit à un cours. Que désirez-vous faire? 
        /// </summary>
        /// <param name="prenomNom"></param>
        /// <returns></returns>
        public static string GroupesAjouterEtudiantsQuestion(string prenomNom)
        { return $"L'étudiant {prenomNom} est déjà inscrit à un cours. Que désirez-vous faire?"; }

        /// <summary>
        /// Impossible d'ajouter l'étudiant {0} au groupe puisqu'il en fait déjà partie.
        /// </summary>
        /// <param name="matricule"></param>
        /// <returns></returns>
        public static string GroupesAjouterEtudiantsErreur(string matricule)
        { return $"Impossible d'ajouter l'étudiant {matricule} au groupe, il est déjà présent dans ce groupe."; }

        /// <summary>
        /// Voulez-vous vraiment retirer l'étudiant {0} du groupe {1}?
        /// </summary>
        /// <param name="matricule"></param>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string GroupesRetirerEtudiantsQuestion(string matricule, int noGroupe)
        { return $"Voulez-vous vraiment retirer l'étudiant {matricule} du groupe {noGroupe}?"; }

        /// <summary>
        /// L'étudiant {0} a été retiré du groupe {1}.
        /// </summary>
        /// <param name="matricule"></param>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string GroupesRetirerEtudiantsReussi(string matricule, int noGroupe)
        { return $"L'étudiant {matricule} a été retiré du groupe {noGroupe}."; }

        /// <summary>
        /// L'étudiant {0} a été ajouté au groupe {1}.
        /// </summary>
        /// <param name="matricule"></param>
        /// <param name="idGroupe"></param>
        /// <returns></returns>
        public static string GroupesAjouterEtudiantReussi(string matricule, int idGroupe)
        { return $"L'étudiant {matricule} a été ajouté au groupe {idGroupe}."; }

        /// <summary>
        /// L'étudiant {0} ne peut pas être déplacé au groupe {1} du cours {2},car il y est déjà!
        /// </summary>
        /// <param name="matricule"></param>
        /// <param name="idGroupe"></param>
        /// <param name="nomCours"></param>
        /// <returns></returns>
        public static string GroupesDeplacerEtudiantErreur(string matricule, int idGroupe, string nomCours)
        { return $"L'étudiant {matricule} ne peut pas être déplacé au groupe {idGroupe} du cours {nomCours}, car il y est déjà."; }

        /// <summary>
        /// L'étudiant {Matricule} a été déplacé au groupe {IdGroupe} du cours {NomCours}.
        /// </summary>
        /// <param name="matricule"></param> 
        /// <param name="idGroupe"></param>
        /// <param name="nomCours"></param>
        /// <returns></returns>
        public static string GroupesDeplacerEtudiantReussi(string matricule, int idGroupe, string nomCours)
        { return $"L'étudiant {matricule} a été déplacé au groupe {idGroupe} du cours {nomCours}."; }

        /// <summary>
        /// Impossible d'enregistrer ce groupe. Il existe déjà un groupe ayant le numéro {0}.
        /// </summary>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string GroupesAjouterErreurMemeNumero(int noGroupe)
        { return $"Impossible d'enregistrer ce groupe. Il existe déjà un groupe ayant le numéro {noGroupe}."; }

        /// <summary>
        /// Voulez-vous vraiment supprimer le groupe {0}?
        /// </summary>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string GroupesSupprimerQuestion(int noGroupe)
        { return $"Voulez-vous vraiment supprimer le groupe {noGroupe}?"; }

        /// <summary>
        /// Le groupe {0} a été supprimé.
        /// </summary>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string GroupesSupprimerReussi(int noGroupe)
        { return $"Le groupe {noGroupe} a été supprimé."; }


        /// <summary>
        /// Des étudiants sont rattachés au groupe {0} Souhaitez-vous le supprimer définitivement?
        /// </summary>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string GroupesSupprimerQuestionEtudiantsRattaches(int noGroupe)
        { return $"Des étudiants sont rattachés au groupe {noGroupe}. Souhaitez-vous le supprimer définitivement?"; }

        /// <summary>
        /// Le groupe {0} a été créé. Souhaitez-vous y associer des étudiants?
        /// </summary>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string GroupesAjouterQuestionAssocierEtudiants(int noGroupe)
        { return $"Le groupe {noGroupe} a été créé. Souhaitez-vous y associer des étudiants?"; }

        public const string GroupesAjouterTitre = "Ajouter un groupe";

        public const string GroupesAjouterEtudiantTitre = "Ajouter un étudiants au groupe";

        public const string GroupesDeplacerEtudiantTitre = "Déplacer un étudiant de groupe";

        public const string GroupesSupprimerEtudiantTitre = "Supprimer un étudiant du groupe";

        public const string GroupesEtudiantDeplacer = "Déplacer l'étudiant";

        public const string GroupesEtudiantsAjouter = "Ajouter l'étudiant";

        public const string GroupesModifierTitre = "Modifier un groupe";

        public const string GroupesSupprimerTitre = "Supprimer un groupe";

        #endregion


        #region Pages Erreur

        public const string ErreurOptionSupplementaire = "Pour toute question, réferrez-vous à un enseignant de mathématiques.";

        public const string ErreurCausesPossibles = "Voici les causes probables de ce problème :";

        public const string ErreurFermeTitre = "L'inscription au SACHEM est présentement fermée";

        public const string ErreurDejaTitre = "Votre inscription est déjà complétée";

        public const string ErreurDejaCause = "Vous pouvez la modifier à partir de votre profil.";

        public const string Erreur404Cause1 = "Vous n'êtes pas connecté au site. Sélectionnez \"Se connecter\" pour vous connecter et réessayez.";

        public const string Erreur404Cause2 = "Vous tentez d'accéder à une page qui est réservée à un autre type d'usager que le vôtre.";

        public const string Erreur404Cause3 = "Votre session sur le site a expiré à cause d'une trop grande période d'inactivité. Reconnectez-vous et réessayez.";
        #endregion


        #region Nous contacter

        public const string ContacterTitre = "Nous Contacter";

        public const string ContacterPersonneRessource = "Personne à contacter";

        public const string ContacterFacebook = "La page Facebook du SACHEM";

        public const string ContacterSiteWeb = "Site web du service d'aide";

        #endregion


        #region Recherche = Aucun

        public const string AucunCoursSansRecherche = "Aucun cours.";

        public const string AucunDemandeInscriptionSansRecherche = "Aucun demande d'inscription.";

        public const string AucunGroupe = "Aucun groupe ne répond à ces critères.";

        public const string AucunCollege = "Aucun collège ne répond à ces critères.";

        public const string AucunCours = "Aucun cours ne répond à ces critères.";

        public const string AucunDossierEtudiant = "Aucun dossier d'étudiant ne répond à ces critères.";

        public const string AucunEnseignant = "Aucun enseignant ne répond à ces critères.";

        public const string AucunEtudiant = "Aucun étudiant ne répond à ces critères.";

        public const string AucunJumelage = "Aucun jumelage ne répond à ces critères.";

        public const string AucunProgramme = "Aucun programme d'études ne répond à ces critères.";

        public const string AucunInscription = "Aucune inscription ne répond à ces critères.";
        #endregion


        #region Mots

        public const string Ok = "Ok";
        public const string Oui = "Oui";
        public const string Non = "Non";
        public const string Cours = "Cours";
        public const string Groupe = "Groupe";
        public const string Groupes = "Groupes";
        public const string Contact = "Contact";
        public const string College = "Collège";
        public const string Courriel = "Courriel de confirmation";
        public const string Horaire = "Horaire d'inscription";
        public const string CoursEnseignes = "Cours enseignés";
        public const string Enseignant = "Enseignant";
        public const string Enseignants = "Enseignants";
        public const string Etudiants = "Étudiants";
        public const string Inscription = "Inscription";
        public const string Jumelage = "Jumelage";
        public const string Jumelages = "Jumelages";
        public const string Recherche = "Recherche";
        public const string Superviseur = "Superviseur";
        public const string ProgrammeEtude = "Programme d'étude";
        public const string Session = "Session";
        public const string Statut = "Statut";
        public const string Resultat = "Résultat";
        public const string PlageHoraire = "Plage horaire";
        public const string MotDePasse = "Mot de passe";
        public const string Confirmation = "Confirmation";
        public const string Deconnexion = "Déconnexion";
        public const string SeConnecter = "Se connecter";
        public const string Annuler = "Annuler";
        public const string Autre = "Autre";
        public const string Ajouter = "Ajouter";
        public const string Heures = "Heures";
        public const string Jumele = "Jumelé";
        public const string JumeleA = "Jumelé à";
        public const string Achete = "Acheté";
        public const string PasAchete = "Pas acheté";
        public const string DossierEtudiant = "Dossier de l'étudiant";
        public const string DossierEtudiantMon = "Mon Dossier";
        public const string JumelagePossible = "Jumelages possibles";
        public const string Deplacer = "Déplacer";
        public const string Disponibilites = "Disponibilités";
        public const string TuteurEleveAide = "Tuteur élève aidé";
        public const string TuteurCours = "Tuteur de cours";
        public const string TuteurBenevole = "Tuteur bénévole";
        public const string TuteurRemunere = "Tuteur rémunéré";
        public const string Valider = "Valider";
        public const string Connexion = "Connexion";
        public const string Consulter = "Consulter";
        public const string Sinscrire = "S’inscrire";
        public const string Rechercher = "Rechercher";
        public const string Decision = "Décision";
        public const string Enregistrer = "Enregistrer";
        public const string Supprimer = "Supprimer";
        public const string NousContacter = "Nous Contacter";
        public const string TypeInscription = "Type d'inscription";
        public const string Actif = "Actif";
        public const string Toutes = "Toutes";
        public const string Tous = "Tous";
        public const string Modifier = "Modifier";
        public const string NoDa = "No DA";
        public const string Prenom = "Prénom";
        public const string Nom = "Nom";
        public const string Administration = "Administration";
        public const string PremierChoix = "Premier choix";
        public const string DeuxiemeChoix = "Deuxième choix";
        public const string TroisiemeChoix = "Troisième choix";

        #endregion


        #region boutons et liens longs (voir zone mots pour ceux courts)

        public const string EnregistrerPoursuivre = "Enregistrer et poursuivre";
        public const string EnregistrerBrouillon = "Enregistrer un brouillon";
        public const string ModifierMotPasse = "Modifier le mot de passe";
        public const string RetourMonDossierEtudiant = "Retourner à mon dossier";
        public const string VeuillezChoisir = "-- Veuillez choisir --";
        public const string RetourRecherche = "Retourner à la recherche";
        public const string InfosCompte = "Informations de compte";
        public const string InscrivezVous = "Inscrivez-vous";
        public const string RetourEnArriere = "Retourner en arrière";
        public const string RetourDetailsGroupe = "Retourner au détails du groupe";
        public const string ModifierInfosPerso = "Modifier informations";
        public const string ConsulterGroupesEnseignant = "Consulter les groupes de l'enseignant";
        public const string IdentificationEtudiant = "Identification de l'étudiant";
        public const string InformationsSpecifiques = "Informations spécifiques";
        public const string DetailsInscription = "Détails de l'inscription";
        public const string PageIndisponible = "Page indisponible";

        #endregion
    }
}