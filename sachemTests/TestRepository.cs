using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using sachem.Models;
using sachem.Models.DataAccess;

namespace sachemTests
{
    internal class TestRepository : IDataRepository
    {
        public int SessionEnCours()
        {
            throw new NotImplementedException();
        }
        public string FindMdp(int id)
        {
            throw new NotImplementedException();
        }

        //Debut Any
        public bool AnyChoixReponse(Expression<Func<ChoixReponse, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyCourriel(Expression<Func<Courriel, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyCours(Expression<Func<Cours, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyCoursInteret(Expression<Func<CoursInteret, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyCoursSuivi(Expression<Func<CoursSuivi, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyDisponibilite(Expression<Func<Disponibilite, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyEtuProgEtude(Expression<Func<EtuProgEtude, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyEvaluation(Expression<Func<Evaluation, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyFormulaire(Expression<Func<Formulaire, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyGroupe(Expression<Func<Groupe, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyGroupeEtudiant(Expression<Func<GroupeEtudiant, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyInscription(Expression<Func<Inscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyJumelage(Expression<Func<Jumelage, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyPersonne(Expression<Func<Personne, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyProgrammeEtude(Expression<Func<ProgrammeEtude, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyQuestion(Expression<Func<Question, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyReponseQuestion(Expression<Func<ReponseQuestion, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnySection(Expression<Func<Section, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnySession(Expression<Func<Session, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnySuivi(Expression<Func<Suivi, bool>> condition)
        {
            throw new NotImplementedException();
        }
        //Any - sur table parametres
        public bool AnyCollege(Expression<Func<p_College, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyContact(Expression<Func<p_Contact, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyHoraireInscription(Expression<Func<p_HoraireInscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyJour(Expression<Func<p_Jour, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnySaison(Expression<Func<p_Saison, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnySexe(Expression<Func<p_Sexe, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyStatutCours(Expression<Func<p_StatutCours, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyStatutInscription(Expression<Func<p_StatutInscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyTypeCourriel(Expression<Func<p_TypeCourriel, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyTypeFormulaire(Expression<Func<p_TypeFormulaire, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyTypeInscription(Expression<Func<p_TypeInscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyTypeResultat(Expression<Func<p_TypeResultat, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyTypeUsag(Expression<Func<p_TypeUsag, bool>> condition)
        {
            throw new NotImplementedException();
        }
        //Fin Any

        //Debut Get - Where
        public IEnumerable<ChoixReponse> GetChoixReponse(Expression<Func<ChoixReponse, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Courriel> GetCourriel(Expression<Func<Courriel, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Cours> GetCours(Expression<Func<Cours, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<CoursInteret> GetCoursInteret(Expression<Func<CoursInteret, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<CoursSuivi> GetCoursSuivi(Expression<Func<CoursSuivi, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Disponibilite> GetDisponibilite(Expression<Func<Disponibilite, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<EtuProgEtude> GetEtuProgEtude(Expression<Func<EtuProgEtude, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Evaluation> GetEvaluation(Expression<Func<Evaluation, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Formulaire> GetFormulaire(Expression<Func<Formulaire, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Groupe> GetGroupe(Expression<Func<Groupe, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<GroupeEtudiant> GetGroupeEtudiant(Expression<Func<GroupeEtudiant, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Inscription> GetInscription(Expression<Func<Inscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Jumelage> GetJumelage(Expression<Func<Jumelage, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Personne> GetPersonne(Expression<Func<Personne, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ProgrammeEtude> GetProgrammeEtude(Expression<Func<ProgrammeEtude, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Question> GetQuestion(Expression<Func<Question, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ReponseQuestion> GetReponseQuestion(Expression<Func<ReponseQuestion, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Section> GetSection(Expression<Func<Section, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Session> GetSession(Expression<Func<Session, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Suivi> GetSuivi(Expression<Func<Suivi, bool>> condition)
        {
            throw new NotImplementedException();
        }
            //Get - sur table parametres
        public IEnumerable<p_College> GetCollege(Expression<Func<p_College, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Contact> GetContact(Expression<Func<p_Contact, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_HoraireInscription> GetHoraireInscription(Expression<Func<p_HoraireInscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Jour> GetJour(Expression<Func<p_Jour, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Saison> GetSaison(Expression<Func<p_Saison, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Sexe> GetSexe(Expression<Func<p_Sexe, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_StatutCours> GetStatutCours(Expression<Func<p_StatutCours, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_StatutInscription> GetStatutInscription(Expression<Func<p_StatutInscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeCourriel> GetTypeCourriel(Expression<Func<p_TypeCourriel, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeFormulaire> GetTypeFormulaire(Expression<Func<p_TypeFormulaire, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeInscription> GetTypeInscription(Expression<Func<p_TypeInscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeResultat> GetTypeResultat(Expression<Func<p_TypeResultat, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeUsag> GetTypeUsag(Expression<Func<p_TypeUsag, bool>> condition)
        {
            throw new NotImplementedException();
        }
        //Fin Where

        //Debut All
        public IEnumerable<ChoixReponse> AllChoixReponse()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Courriel> AllCourriel()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Cours> AllCours()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<CoursInteret> AllCoursInteret()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<CoursSuivi> AllCoursSuivi()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Disponibilite> AllDisponibilite()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<EtuProgEtude> AllEtuProgEtude()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Evaluation> AllEvaluation()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Formulaire> AllFormulaire()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Groupe> AllGroupe()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<GroupeEtudiant> AllGroupeEtudiant()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Inscription> AllInscription()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Jumelage> AllJumelage()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Personne> AllPersonne()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ProgrammeEtude> AllProgrammeEtude()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Question> AllQuestion()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ReponseQuestion> AllReponseQuestion()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Section> AllSection()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Session> AllSession()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Suivi> AllSuivi()
        {
            throw new NotImplementedException();
        }
            //All - sur table parametres
        public IEnumerable<p_College> AllCollege()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Contact> AllContact()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_HoraireInscription> AllHoraireInscription()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Jour> AllJour()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Saison> AllSaison()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Sexe> AllSexe()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_StatutCours> AllStatutCours()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_StatutInscription> AllStatutInscription()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeCourriel> AllTypeCourriel()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeFormulaire> AllTypeFormulaire()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeInscription> AllTypeInscription()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeResultat> AllTypeResultat()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeUsag> AllTypeUsag()
        {
            throw new NotImplementedException();
        }
        //Fin All

        //Debut Find
        public ChoixReponse FindChoixReponse(int id)
        {
            throw new NotImplementedException();
        }
        public Courriel FindCourriel(int id)
        {
            throw new NotImplementedException();
        }
        public Cours FindCours(int id)
        {
            throw new NotImplementedException();
        }
        public CoursInteret FindCoursInteret(int id)
        {
            throw new NotImplementedException();
        }
        public CoursSuivi FindCoursSuivi(int id)
        {
            throw new NotImplementedException();
        }
        public Disponibilite FindDisponibilite(int id)
        {
            throw new NotImplementedException();
        }
        public EtuProgEtude FindEtuProgEtude(int id)
        {
            throw new NotImplementedException();
        }
        public Evaluation FindEvaluation(int id)
        {
            throw new NotImplementedException();
        }
        public Formulaire FindFormulaire(int id)
        {
            throw new NotImplementedException();
        }
        public Groupe FindGroupe(int id)
        {
            throw new NotImplementedException();
        }
        public GroupeEtudiant FindGroupeEtudiant(int id)
        {
            throw new NotImplementedException();
        }
        public Inscription FindInscription(int id)
        {
            throw new NotImplementedException();
        }
        public Jumelage FindJumelage(int id)
        {
            throw new NotImplementedException();
        }
        public Personne FindPersonne(int id)
        {
            throw new NotImplementedException();
        }
        public ProgrammeEtude FindProgrammeEtude(int id)
        {
            throw new NotImplementedException();
        }
        public Question FindQuestion(int id)
        {
            throw new NotImplementedException();
        }
        public ReponseQuestion FindReponseQuestion(int id)
        {
            throw new NotImplementedException();
        }
        public Section FindSection(int id)
        {
            throw new NotImplementedException();
        }
        public Session FindSession(int id)
        {
            throw new NotImplementedException();
        }
        public Suivi FindSuivi(int id)
        {
            throw new NotImplementedException();
        }
        //Fin Find

        //Debut Add
        public void AddChoixReponse(ChoixReponse itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddCourriel(Courriel itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddCours(Cours itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddCoursInteret(CoursInteret itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddCoursSuivi(CoursSuivi itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddDisponibilite(Disponibilite itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddEtuProgEtude(EtuProgEtude itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddEvaluation(Evaluation itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddFormulaire(Formulaire itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddGroupe(Groupe itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddGroupeEtudiant(GroupeEtudiant itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddInscription(Inscription itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddJumelage(Jumelage itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddPersonne(Personne itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddProgrammeEtude(ProgrammeEtude itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddQuestion(Question itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddReponseQuestion(ReponseQuestion itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddSection(Section itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddSession(Session itemToAdd)
        {
            throw new NotImplementedException();
        }
        public void AddSuivi(Suivi itemToAdd)
        {
            throw new NotImplementedException();
        }
        //Fin Add

        //Debut Edit
        public void EditChoixReponse(ChoixReponse itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditCourriel(Courriel itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditCours(Cours itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditCoursInteret(CoursInteret itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditCoursSuivi(CoursSuivi itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditDisponibilite(Disponibilite itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditEtuProgEtude(EtuProgEtude itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditEvaluation(Evaluation itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditFormulaire(Formulaire itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditGroupe(Groupe itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditGroupeEtudiant(GroupeEtudiant itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditInscription(Inscription itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditJumelage(Jumelage itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditPersonne(Personne itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditProgrammeEtude(ProgrammeEtude itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditQuestion(Question itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditReponseQuestion(ReponseQuestion itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditSection(Section itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditSession(Session itemToEdit)
        {
            throw new NotImplementedException();
        }
        public void EditSuivi(Suivi itemToEdit)
        {
            throw new NotImplementedException();
        }
        //Fin Edit

        //Debut Remove
        public void RemoveChoixReponse(ChoixReponse itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveCourriel(Courriel itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveCours(Cours itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveCoursInteret(CoursInteret itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveCoursSuivi(CoursSuivi itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveDisponibilite(Disponibilite itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveEtuProgEtude(EtuProgEtude itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveEvaluation(Evaluation itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveFormulaire(Formulaire itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveGroupe(Groupe itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveGroupeEtudiant(GroupeEtudiant itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveInscription(Inscription itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveJumelage(Jumelage itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemovePersonne(Personne itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveProgrammeEtude(ProgrammeEtude itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveQuestion(Question itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveReponseQuestion(ReponseQuestion itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveSection(Section itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveSession(Session itemToRemove)
        {
            throw new NotImplementedException();
        }
        public void RemoveSuivi(Suivi itemToRemove)
        {
            throw new NotImplementedException();
        }
        //Fin Remove

        //Debut Liste
        public SelectList ListeTypeUsager(int idTypeUsager)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeSexe(int? sexe = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeSession(int session = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeEnseignantEtREsponsable(int idSession, int idPers)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeCours(int cours = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeCollege(int college = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeStatutCours(int statut = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeProgrammmeCode(bool actif = true)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeEnseignant(int enseignant = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeSuperviseur(int superviseur = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeTypeInscription(int typeInscription = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeInscription(int inscription = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeStatutInscriptionSansBrouillon(int statut = 0)
        {
            throw new NotImplementedException();
        }
        public List<string> ListeJours()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Cours> ListeCoursSelonSession(int session)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Groupe> ListeGroupeSelonSessionEtCours(int cours, int session)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeStatutCours()
        {
            throw new NotImplementedException();
        }
        public SelectList ListeTypesCourriels(int typeCourriel = 0)
        {
            throw new NotImplementedException();
        }
        //Fin Liste

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}