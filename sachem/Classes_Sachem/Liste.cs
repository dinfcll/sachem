using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;
using sachem.Models.DataAccess;

public class Liste
{
    private static readonly IDataRepository dataRepository = new BdRepository();
    private static readonly SACHEMEntities db = new SACHEMEntities();
    const int BROUILLON = 2;

    public static List<SelectListItem> ListeSession(int session = 0)
    {
        var lSessions = dataRepository.GetSessions();
        var slSession = new List<SelectListItem>();
        slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", session));

        return slSession;
    }

    public static List<SelectListItem> ListePersonne(int idSession, int idPers)
    {
        var lPersonne = (from p in db.Personne
            join c in db.Groupe on p.id_Pers equals c.id_Enseignant
            where (p.id_TypeUsag == (int) TypeUsagers.Enseignant ||
                    p.id_TypeUsag == (int) TypeUsagers.Responsable) &&
                    p.Actif &&
                    c.id_Sess == (idSession == 0 ? c.id_Sess : idSession)
            orderby p.Nom, p.Prenom
            select p).Distinct();

        var slPersonne = new List<SelectListItem>();
        slPersonne.AddRange(new SelectList(lPersonne, "id_Pers", "NomPrenom", idPers));

        return slPersonne;
    }

    public static List<SelectListItem> ListeCours(int cours = 0)
    {
        var lCours = dataRepository.GetCours();
        var slCours = new List<SelectListItem>();
        slCours.AddRange(new SelectList(lCours, "id_Cours", "CodeNom", cours));
        return slCours;
    }

    public static List<SelectListItem> ListeCollege(int college = 0)
    {
        var lCollege = dataRepository.GetCollege();
        var slCollege = new List<SelectListItem>();
        slCollege.AddRange(new SelectList(lCollege, "id_College", "College", college));

        return slCollege;
    }

    public static List<SelectListItem> ListeStatutCours(int statut = 0)
    {
        var lStatut = dataRepository.GetStatut();
        var slStatut = new List<SelectListItem>();
        slStatut.AddRange(new SelectList(lStatut, "id_Statut", "Statut", statut));

        return slStatut;
    }

    public static List<SelectListItem> ListeEnseignant(int enseignant = 0)
    {
        var lEnseignant = dataRepository.AllEnseignantOrdered();
        var slEnseignant = new List<SelectListItem>();
        slEnseignant.AddRange(new SelectList(lEnseignant, "id_Pers", "Nom", enseignant));

        return slEnseignant;
    }

    public static List<SelectListItem> ListeSuperviseur(int superviseur = 0)
    {
        var lstEnseignant = from p in db.Personne
                            where p.id_TypeUsag == 2 && p.Actif == true
                            orderby p.Nom, p.Prenom
                            select p;
        var slEnseignant = new List<SelectListItem>();
        slEnseignant.AddRange(new SelectList(lstEnseignant, "id_Pers", "NomPrenom", superviseur));
        return slEnseignant;
    }

    public static List<SelectListItem> ListeTypeInscription(int typeInscription = 0)
    {
        var lTypeInscription = from typeinscription in db.p_TypeInscription select typeinscription;
        var slTypeInscription = new List<SelectListItem>();
        slTypeInscription.AddRange(new SelectList(lTypeInscription, "id_TypeInscription", "TypeInscription", typeInscription));

        return slTypeInscription;
    }

    public static List<SelectListItem> ListeStatutInscriptionSansBrouillon(int statut = 0)
    {
        var lStatut = from s in db.p_StatutInscription where s.id_Statut != BROUILLON select s;
        var slStatut = new List<SelectListItem>();
        slStatut.AddRange(new SelectList(lStatut, "id_Statut", "Statut", statut));

        return slStatut;
    }
}