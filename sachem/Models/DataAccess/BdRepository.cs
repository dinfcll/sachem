using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using sachem.Methodes_Communes;

namespace sachem.Models.DataAccess
{
    public class BdRepository : IDataRepository
    {
        private readonly SACHEMEntities _db = new SACHEMEntities();
        private const int Brouillon = 2;
        private const int IdTypeUsagerResp = 3;
        private const int IdTypeUsagerEnseignant = 2;
        public int SessionEnCours()
        {
            return _db.Session.Max(s => s.id_Sess);
        }
        public string FindMdp(int id)
        {
            var personne = _db.Personne.FirstOrDefault(x => x.id_Pers == id);
            if (personne == null) return "";
            var mdp = personne.MP;
            _db.Entry(personne).State = EntityState.Unchanged;
            _db.Entry(personne).State = EntityState.Detached;
            return mdp;
        }

        //Debut Any - If found

        public bool AnyChoixReponse(Expression<Func<ChoixReponse, bool>> condition)
        {
            return _db.ChoixReponse.Any(condition);
        }
        public bool AnyCourriel(Expression<Func<Courriel, bool>> condition)
        {
            return _db.Courriel.Any(condition);
        }
        public bool AnyCours(Expression<Func<Cours, bool>> condition)
        {
            return _db.Cours.Any(condition);
        }
        public bool AnyCoursInteret(Expression<Func<CoursInteret, bool>> condition)
        {
            return _db.CoursInteret.Any(condition);
        }
        public bool AnyCoursSuivi(Expression<Func<CoursSuivi, bool>> condition)
        {
            return _db.CoursSuivi.Any(condition);
        }
        public bool AnyDisponibilite(Expression<Func<Disponibilite, bool>> condition)
        {
            return _db.Disponibilite.Any(condition);
        }
        public bool AnyEtuProgEtude(Expression<Func<EtuProgEtude, bool>> condition)
        {
            return _db.EtuProgEtude.Any(condition);
        }
        public bool AnyEvaluation(Expression<Func<Evaluation, bool>> condition)
        {
            return _db.Evaluation.Any(condition);
        }
        public bool AnyFormulaire(Expression<Func<Formulaire, bool>> condition)
        {
            return _db.Formulaire.Any(condition);
        }
        public bool AnyGroupe(Expression<Func<Groupe, bool>> condition)
        {
            return _db.Groupe.Any(condition);
        }
        public bool AnyGroupeEtudiant(Expression<Func<GroupeEtudiant, bool>> condition)
        {
            return _db.GroupeEtudiant.Any(condition);
        }
        public bool AnyInscription(Expression<Func<Inscription, bool>> condition)
        {
            return _db.Inscription.Any(condition);
        }
        public bool AnyJumelage(Expression<Func<Jumelage, bool>> condition)
        {
            return _db.Jumelage.Any(condition);
        }
        public bool AnyPersonne(Expression<Func<Personne, bool>> condition)
        {
            return _db.Personne.Any(condition);
        }
        public bool AnyProgrammeEtude(Expression<Func<ProgrammeEtude, bool>> condition)
        {
            return _db.ProgrammeEtude.Any(condition);
        }
        public bool AnyQuestion(Expression<Func<Question, bool>> condition)
        {
            return _db.Question.Any(condition);
        }
        public bool AnyReponseQuestion(Expression<Func<ReponseQuestion, bool>> condition)
        {
            return _db.ReponseQuestion.Any(condition);
        }
        public bool AnySection(Expression<Func<Section, bool>> condition)
        {
            return _db.Section.Any(condition);
        }
        public bool AnySession(Expression<Func<Session, bool>> condition)
        {
            return _db.Session.Any(condition);
        }
        public bool AnySuivi(Expression<Func<Suivi, bool>> condition)
        {
            return _db.Suivi.Any(condition);
        }
            //Any - sur table parametres
        public bool AnyCollege(Expression<Func<p_College, bool>> condition)
        {
            return _db.p_College.Any(condition);
        }
        public bool AnyContact(Expression<Func<p_Contact, bool>> condition)
        {
            return _db.p_Contact.Any(condition);
        }
        public bool AnyHoraireInscription(Expression<Func<p_HoraireInscription, bool>> condition)
        {
            return _db.p_HoraireInscription.Any(condition);
        }
        public bool AnyJour(Expression<Func<p_Jour, bool>> condition)
        {
            return _db.p_Jour.Any(condition);
        }
        public bool AnySaison(Expression<Func<p_Saison, bool>> condition)
        {
            return _db.p_Saison.Any(condition);
        }
        public bool AnySexe(Expression<Func<p_Sexe, bool>> condition)
        {
            return _db.p_Sexe.Any(condition);
        }
        public bool AnyStatutCours(Expression<Func<p_StatutCours, bool>> condition)
        {
            return _db.p_StatutCours.Any(condition);
        }
        public bool AnyStatutInscription(Expression<Func<p_StatutInscription, bool>> condition)
        {
            return _db.p_StatutInscription.Any(condition);
        }
        public bool AnyTypeCourriel(Expression<Func<p_TypeCourriel, bool>> condition)
        {
            return _db.p_TypeCourriel.Any(condition);
        }
        public bool AnyTypeFormulaire(Expression<Func<p_TypeFormulaire, bool>> condition)
        {
            return _db.p_TypeFormulaire.Any(condition);
        }
        public bool AnyTypeInscription(Expression<Func<p_TypeInscription, bool>> condition)
        {
            return _db.p_TypeInscription.Any(condition);
        }
        public bool AnyTypeResultat(Expression<Func<p_TypeResultat, bool>> condition)
        {
            return _db.p_TypeResultat.Any(condition);
        }
        public bool AnyTypeUsag(Expression<Func<p_TypeUsag, bool>> condition)
        {
            return _db.p_TypeUsag.Any(condition);
        }
        //Fin Any

        //Debut Get - Where
        public IEnumerable<ChoixReponse> GetChoixReponse(Expression<Func<ChoixReponse, bool>> condition)
        {
            return _db.ChoixReponse.Where(condition);
        }
        public IEnumerable<Courriel> GetCourriel(Expression<Func<Courriel, bool>> condition)
        {
            return _db.Courriel.Where(condition);
        }
        public IEnumerable<Cours> GetCours(Expression<Func<Cours, bool>> condition)
        {
            return _db.Cours.Where(condition);
        }
        public IEnumerable<CoursInteret> GetCoursInteret(Expression<Func<CoursInteret, bool>> condition)
        {
            return _db.CoursInteret.Where(condition);
        }
        public IEnumerable<CoursSuivi> GetCoursSuivi(Expression<Func<CoursSuivi, bool>> condition)
        {
            return _db.CoursSuivi.Where(condition);
        }
        public IEnumerable<Disponibilite> GetDisponibilite(Expression<Func<Disponibilite, bool>> condition)
        {
            return _db.Disponibilite.Where(condition);
        }
        public IEnumerable<EtuProgEtude> GetEtuProgEtude(Expression<Func<EtuProgEtude, bool>> condition)
        {
            return _db.EtuProgEtude.Where(condition);
        }
        public IEnumerable<Evaluation> GetEvaluation(Expression<Func<Evaluation, bool>> condition)
        {
            return _db.Evaluation.Where(condition);
        }
        public IEnumerable<Formulaire> GetFormulaire(Expression<Func<Formulaire, bool>> condition)
        {
            return _db.Formulaire.Where(condition);
        }
        public IEnumerable<Groupe> GetGroupe(Expression<Func<Groupe, bool>> condition)
        {
            return _db.Groupe.Where(condition);
        }
        public IEnumerable<GroupeEtudiant> GetGroupeEtudiant(Expression<Func<GroupeEtudiant, bool>> condition)
        {
            return _db.GroupeEtudiant.Where(condition);
        }
        public IEnumerable<Inscription> GetInscription(Expression<Func<Inscription, bool>> condition)
        {
            return _db.Inscription.Where(condition);
        }
        public IEnumerable<Jumelage> GetJumelage(Expression<Func<Jumelage, bool>> condition)
        {
            return _db.Jumelage.Where(condition);
        }
        public IEnumerable<Personne> GetPersonne(Expression<Func<Personne, bool>> condition)
        {
            return _db.Personne.Where(condition);
        }
        public IEnumerable<ProgrammeEtude> GetProgrammeEtude(Expression<Func<ProgrammeEtude, bool>> condition)
        {
            return _db.ProgrammeEtude.Where(condition);
        }
        public IEnumerable<Question> GetQuestion(Expression<Func<Question, bool>> condition)
        {
            return _db.Question.Where(condition);
        }
        public IEnumerable<ReponseQuestion> GetReponseQuestion(Expression<Func<ReponseQuestion, bool>> condition)
        {
            return _db.ReponseQuestion.Where(condition);
        }
        public IEnumerable<Section> GetSection(Expression<Func<Section, bool>> condition)
        {
            return _db.Section.Where(condition);
        }
        public IEnumerable<Session> GetSession(Expression<Func<Session, bool>> condition)
        {
            return _db.Session.Where(condition);
        }
        public IEnumerable<Suivi> GetSuivi(Expression<Func<Suivi, bool>> condition)
        {
            return _db.Suivi.Where(condition);
        }
            //Get - sur table parametres
        public IEnumerable<p_College> GetCollege(Expression<Func<p_College, bool>> condition)
        {
            return _db.p_College.Where(condition);
        }
        public IEnumerable<p_Contact> GetContact(Expression<Func<p_Contact, bool>> condition)
        {
            return _db.p_Contact.Where(condition);
        }
        public IEnumerable<p_HoraireInscription> GetHoraireInscription(Expression<Func<p_HoraireInscription, bool>> condition)
        {
            return _db.p_HoraireInscription.Where(condition);
        }
        public IEnumerable<p_Jour> GetJour(Expression<Func<p_Jour, bool>> condition)
        {
            return _db.p_Jour.Where(condition);
        }
        public IEnumerable<p_Saison> GetSaison(Expression<Func<p_Saison, bool>> condition)
        {
            return _db.p_Saison.Where(condition);
        }
        public IEnumerable<p_Sexe> GetSexe(Expression<Func<p_Sexe, bool>> condition)
        {
            return _db.p_Sexe.Where(condition);
        }
        public IEnumerable<p_StatutCours> GetStatutCours(Expression<Func<p_StatutCours, bool>> condition)
        {
            return _db.p_StatutCours.Where(condition);
        }
        public IEnumerable<p_StatutInscription> GetStatutInscription(Expression<Func<p_StatutInscription, bool>> condition)
        {
            return _db.p_StatutInscription.Where(condition);
        }
        public IEnumerable<p_TypeCourriel> GetTypeCourriel(Expression<Func<p_TypeCourriel, bool>> condition)
        {
            return _db.p_TypeCourriel.Where(condition);
        }
        public IEnumerable<p_TypeFormulaire> GetTypeFormulaire(Expression<Func<p_TypeFormulaire, bool>> condition)
        {
            return _db.p_TypeFormulaire.Where(condition);
        }
        public IEnumerable<p_TypeInscription> GetTypeInscription(Expression<Func<p_TypeInscription, bool>> condition)
        {
            return _db.p_TypeInscription.Where(condition);
        }
        public IEnumerable<p_TypeResultat> GetTypeResultat(Expression<Func<p_TypeResultat, bool>> condition)
        {
            return _db.p_TypeResultat.Where(condition);
        }
        public IEnumerable<p_TypeUsag> GetTypeUsag(Expression<Func<p_TypeUsag, bool>> condition)
        {
            return _db.p_TypeUsag.Where(condition);
        }
        //Fin Where

        //Debut All
        public IEnumerable<ChoixReponse> AllChoixReponse()
        {
            return _db.ChoixReponse;
        }
        public IEnumerable<Courriel> AllCourriel()
        {
            return _db.Courriel;
        }
        public IEnumerable<Cours> AllCours()
        {
            return _db.Cours;
        }
        public IEnumerable<CoursInteret> AllCoursInteret()
        {
            return _db.CoursInteret;
        }
        public IEnumerable<CoursSuivi> AllCoursSuivi()
        {
            return _db.CoursSuivi;
        }
        public IEnumerable<Disponibilite> AllDisponibilite()
        {
            return _db.Disponibilite;
        }
        public IEnumerable<EtuProgEtude> AllEtuProgEtude()
        {
            return _db.EtuProgEtude;
        }
        public IEnumerable<Evaluation> AllEvaluation()
        {
            return _db.Evaluation;
        }
        public IEnumerable<Formulaire> AllFormulaire()
        {
            return _db.Formulaire;
        }
        public IEnumerable<Groupe> AllGroupe()
        {
            return _db.Groupe;
        }
        public IEnumerable<GroupeEtudiant> AllGroupeEtudiant()
        {
            return _db.GroupeEtudiant;
        }
        public IEnumerable<Inscription> AllInscription()
        {
            return _db.Inscription;
        }
        public IEnumerable<Jumelage> AllJumelage()
        {
            return _db.Jumelage;
        }
        public IEnumerable<Personne> AllPersonne()
        {
            return _db.Personne;
        }
        public IEnumerable<ProgrammeEtude> AllProgrammeEtude()
        {
            return _db.ProgrammeEtude;
        }
        public IEnumerable<Question> AllQuestion()
        {
            return _db.Question;
        }
        public IEnumerable<ReponseQuestion> AllReponseQuestion()
        {
            return _db.ReponseQuestion;
        }
        public IEnumerable<Section> AllSection()
        {
            return _db.Section;
        }
        public IEnumerable<Session> AllSession()
        {
            return _db.Session;
        }
        public IEnumerable<Suivi> AllSuivi()
        {
            return _db.Suivi;
        }
            //All - sur table parametres
        public IEnumerable<p_College> AllCollege()
        {
            return _db.p_College;
        }
        public IEnumerable<p_Contact> AllContact()
        {
            return _db.p_Contact;
        }
        public IEnumerable<p_HoraireInscription> AllHoraireInscription()
        {
            return _db.p_HoraireInscription;
        }
        public IEnumerable<p_Jour> AllJour()
        {
            return _db.p_Jour;
        }
        public IEnumerable<p_Saison> AllSaison()
        {
            return _db.p_Saison;
        }
        public IEnumerable<p_Sexe> AllSexe()
        {
            return _db.p_Sexe;
        }
        public IEnumerable<p_StatutCours> AllStatutCours()
        {
            return _db.p_StatutCours;
        }
        public IEnumerable<p_StatutInscription> AllStatutInscription()
        {
            return _db.p_StatutInscription;
        }
        public IEnumerable<p_TypeCourriel> AllTypeCourriel()
        {
            return _db.p_TypeCourriel;
        }
        public IEnumerable<p_TypeFormulaire> AllTypeFormulaire()
        {
            return _db.p_TypeFormulaire;
        }
        public IEnumerable<p_TypeInscription> AllTypeInscription()
        {
            return _db.p_TypeInscription;
        }
        public IEnumerable<p_TypeResultat> AllTypeResultat()
        {
            return _db.p_TypeResultat;
        }
        public IEnumerable<p_TypeUsag> AllTypeUsag()
        {
            return _db.p_TypeUsag;
        }
        //Fin All

        //Debut Find
        public ChoixReponse FindChoixReponse(int id)
        {
            return _db.ChoixReponse.Find(id);
        }
        public Courriel FindCourriel(int id)
        {
            return _db.Courriel.Find(id);
        }
        public Cours FindCours(int id)
        {
            return _db.Cours.Find(id);
        }
        public CoursInteret FindCoursInteret(int id)
        {
            return _db.CoursInteret.Find(id);
        }
        public CoursSuivi FindCoursSuivi(int id)
        {
            return _db.CoursSuivi.Find(id);
        }
        public Disponibilite FindDisponibilite(int id)
        {
            return _db.Disponibilite.Find(id);
        }
        public EtuProgEtude FindEtuProgEtude(int id)
        {
            return _db.EtuProgEtude.Find(id);
        }
        public Evaluation FindEvaluation(int id)
        {
            return _db.Evaluation.Find(id);
        }
        public Formulaire FindFormulaire(int id)
        {
            return _db.Formulaire.Find(id);
        }
        public Groupe FindGroupe(int id)
        {
            return _db.Groupe.Find(id);
        }
        public GroupeEtudiant FindGroupeEtudiant(int id)
        {
            return _db.GroupeEtudiant.Find(id);
        }
        public Inscription FindInscription(int id)
        {
            return _db.Inscription.Find(id);
        }
        public Jumelage FindJumelage(int id)
        {
            return _db.Jumelage.Find(id);
        }
        public Personne FindPersonne(int id)
        {
            return _db.Personne.Find(id);
        }
        public ProgrammeEtude FindProgrammeEtude(int id)
        {
            return _db.ProgrammeEtude.Find(id);
        }
        public Question FindQuestion(int id)
        {
            return _db.Question.Find(id);
        }
        public ReponseQuestion FindReponseQuestion(int id)
        {
            return _db.ReponseQuestion.Find(id);
        }
        public Section FindSection(int id)
        {
            return _db.Section.Find(id);
        }
        public Session FindSession(int id)
        {
            return _db.Session.Find(id);
        }
        public Suivi FindSuivi(int id)
        {
            return _db.Suivi.Find(id);
        }
        //Fin Find

        //Debut Add
        public void AddChoixReponse(ChoixReponse itemToAdd)
        {
            _db.ChoixReponse.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddCourriel(Courriel itemToAdd)
        {
            _db.Courriel.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddCours(Cours itemToAdd)
        {
            _db.Cours.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddCoursInteret(CoursInteret itemToAdd)
        {
            _db.CoursInteret.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddCoursSuivi(CoursSuivi itemToAdd)
        {
            _db.CoursSuivi.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddDisponibilite(Disponibilite itemToAdd)
        {
            _db.Disponibilite.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddEtuProgEtude(EtuProgEtude itemToAdd)
        {
            _db.EtuProgEtude.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddEvaluation(Evaluation itemToAdd)
        {
            _db.Evaluation.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddFormulaire(Formulaire itemToAdd)
        {
            _db.Formulaire.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddGroupe(Groupe itemToAdd)
        {
            _db.Groupe.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddGroupeEtudiant(GroupeEtudiant itemToAdd)
        {
            _db.GroupeEtudiant.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddInscription(Inscription itemToAdd)
        {
            _db.Inscription.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddJumelage(Jumelage itemToAdd)
        {
            _db.Jumelage.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddPersonne(Personne itemToAdd)
        {
            _db.Personne.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddProgrammeEtude(ProgrammeEtude itemToAdd)
        {
            _db.ProgrammeEtude.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddQuestion(Question itemToAdd)
        {
            _db.Question.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddReponseQuestion(ReponseQuestion itemToAdd)
        {
            _db.ReponseQuestion.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddSection(Section itemToAdd)
        {
            _db.Section.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddSession(Session itemToAdd)
        {
            _db.Session.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddSuivi(Suivi itemToAdd)
        {
            _db.Suivi.Add(itemToAdd);
            _db.SaveChanges();
        }
        //Fin Add

        //Debut Edit
        public void EditChoixReponse(ChoixReponse itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditCourriel(Courriel itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditCours(Cours itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditCoursInteret(CoursInteret itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditCoursSuivi(CoursSuivi itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditDisponibilite(Disponibilite itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditEtuProgEtude(EtuProgEtude itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditEvaluation(Evaluation itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditFormulaire(Formulaire itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditGroupe(Groupe itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditGroupeEtudiant(GroupeEtudiant itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditInscription(Inscription itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditJumelage(Jumelage itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditPersonne(Personne itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditProgrammeEtude(ProgrammeEtude itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditQuestion(Question itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditReponseQuestion(ReponseQuestion itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditSection(Section itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditSession(Session itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void EditSuivi(Suivi itemToEdit)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            _db.SaveChanges();
        }
        //Fin Edit

        //Debut Remove
        public void RemoveChoixReponse(ChoixReponse itemToRemove)
        {
            _db.ChoixReponse.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveCourriel(Courriel itemToRemove)
        {
            _db.Courriel.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveCours(Cours itemToRemove)
        {
            _db.Cours.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveCoursInteret(CoursInteret itemToRemove)
        {
            _db.CoursInteret.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveCoursSuivi(CoursSuivi itemToRemove)
        {
            _db.CoursSuivi.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveDisponibilite(Disponibilite itemToRemove)
        {
            _db.Disponibilite.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveEtuProgEtude(EtuProgEtude itemToRemove)
        {
            _db.EtuProgEtude.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveEvaluation(Evaluation itemToRemove)
        {
            _db.Evaluation.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveFormulaire(Formulaire itemToRemove)
        {
            _db.Formulaire.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveGroupe(Groupe itemToRemove)
        {
            _db.Groupe.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveGroupeEtudiant(GroupeEtudiant itemToRemove)
        {
            _db.GroupeEtudiant.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveInscription(Inscription itemToRemove)
        {
            _db.Inscription.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveJumelage(Jumelage itemToRemove)
        {
            _db.Jumelage.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemovePersonne(Personne itemToRemove)
        {
            _db.Personne.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveProgrammeEtude(ProgrammeEtude itemToRemove)
        {
            _db.ProgrammeEtude.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveQuestion(Question itemToRemove)
        {
            _db.Question.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveReponseQuestion(ReponseQuestion itemToRemove)
        {
            _db.ReponseQuestion.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveSection(Section itemToRemove)
        {
            _db.Section.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveSession(Session itemToRemove)
        {
            _db.Session.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveSuivi(Suivi itemToRemove)
        {
            _db.Suivi.Remove(itemToRemove);
            _db.SaveChanges();
        }
        //Fin Remove

        //Debut Liste
        public SelectList ListeTypeUsager(int idTypeUsager = 0)
        {
            var req = _db.p_TypeUsag.Where(x => x.id_TypeUsag == IdTypeUsagerEnseignant || 
            x.id_TypeUsag == IdTypeUsagerResp);
            return new SelectList(req, "id_TypeUsag", "TypeUsag", 
                req.Select(x=>x.id_TypeUsag== idTypeUsager));
        }
        public SelectList ListeSexe(int? sexe = 0)
        {
            if (sexe == null) sexe = 0;
            return new SelectList(_db.p_Sexe, "id_Sexe", "Sexe", sexe);
        }
        public SelectList ListeSession(int session = 0)
        {
            return new SelectList(AllSession(), "id_Sess", "NomSession", session);
        }
        public SelectList ListeCours(int cours = 0)
        {
            return new SelectList(AllCours(), "id_Cours", "CodeNom", cours);
        }
        public SelectList ListeCollege(int college = 0)
        {
            return new SelectList(AllCollege(), "id_College", "College", college);
        }
        public SelectList ListeStatutCours(int statut = 0)
        {
            return new SelectList(AllStatutCours(), "id_Statut", "Statut", statut);
        }
        public SelectList ListeProgrammmeCode(bool actif = true)
        {
            return new SelectList(GetProgrammeEtude(x => x.Actif), "id_ProgEtu", "CodeNomProgramme");
        }
        public SelectList ListeEtudiants(int id = 0)
        {
            return new SelectList(GetPersonne(x=>x.id_TypeUsag == (int)TypeUsagers.Etudiant)
                .OrderBy(x=>x.Nom)
                .ThenBy(x=>x.Prenom), "id_Pers", "Nom", id);
        }
        public SelectList ListeEnseignant(int id = 0)
        {
            return new SelectList(GetPersonne(x => x.id_TypeUsag == (int)TypeUsagers.Enseignant && x.Actif)
                .OrderBy(x => x.Nom)
                .ThenBy(x => x.Prenom), "id_Pers", "NomPrenom", id);
        }
        public SelectList ListeEnseignantEtResponsable(int id = 0)
        {
            return new SelectList(GetPersonne(x => x.id_TypeUsag == (int)TypeUsagers.Enseignant && x.Actif
            || x.id_TypeUsag == (int)TypeUsagers.Responsable)
                .OrderBy(x => x.Nom)
                .ThenBy(x => x.Prenom), "id_Pers", "NomPrenom", id);
        }
        public SelectList ListeTypeInscription(int typeInscription = 0)
        {
            return new SelectList(AllTypeInscription(), "id_TypeInscription", "TypeInscription", typeInscription);
        }
        public SelectList ListeInscription(int inscription = 0)
        {
            return new SelectList(AllInscription(), "id_Inscription", "Inscription", inscription);
        }
        public SelectList ListeStatutInscriptionSansBrouillon(int statut = 0)
        {
            return new SelectList(_db.p_StatutInscription.Where(x => x.id_Statut != Brouillon), "id_Statut", "Statut", statut);
        }
        public SelectList ListeStatutCours()
        {
            return new SelectList(_db.p_StatutCours
                .OrderBy(x=>x.id_Statut), "id_Statut", "Statut");
        }
        public SelectList ListeTypesCourriels(int typeCourriel = 0)
        {
            return new SelectList(_db.p_TypeCourriel.AsNoTracking()
                .OrderBy(i => i.id_TypeCourriel), "id_TypeCourriel", "TypeCourriel", typeCourriel);
        }
        public List<string> ListeJours()
        {
            var jours = new List<string>();
            for (var i = (int)Semaine.Lundi; i < (int)Semaine.Samedi; i++)
            {
                jours.Add(((Semaine)i).ToString());
            }
            return jours.ToList();
        }
        //Fin Liste

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
