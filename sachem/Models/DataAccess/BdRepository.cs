using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

        public void BeLazy(bool set)
        {
            _db.Configuration.LazyLoadingEnabled = set;
        }

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

        #region Any
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
        #endregion

        #region Where
        public IEnumerable<ChoixReponse> WhereChoixReponse(Expression<Func<ChoixReponse, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.ChoixReponse.Where(condition).AsNoTracking() : _db.ChoixReponse.Where(condition);
        }
        public IEnumerable<Courriel> WhereCourriel(Expression<Func<Courriel, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Courriel.Where(condition).AsNoTracking() : _db.Courriel.Where(condition);
        }
        public IEnumerable<Cours> WhereCours(Expression<Func<Cours, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Cours.Where(condition).AsNoTracking() : _db.Cours.Where(condition);
        }
        public IEnumerable<CoursInteret> WhereCoursInteret(Expression<Func<CoursInteret, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.CoursInteret.Where(condition).AsNoTracking() : _db.CoursInteret.Where(condition);
        }
        public IEnumerable<CoursSuivi> WhereCoursSuivi(Expression<Func<CoursSuivi, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.CoursSuivi.Where(condition).AsNoTracking() : _db.CoursSuivi.Where(condition);
        }
        public IEnumerable<Disponibilite> WhereDisponibilite(Expression<Func<Disponibilite, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Disponibilite.Where(condition).AsNoTracking() : _db.Disponibilite.Where(condition);
        }
        public IEnumerable<EtuProgEtude> WhereEtuProgEtude(Expression<Func<EtuProgEtude, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.EtuProgEtude.Where(condition).AsNoTracking() : _db.EtuProgEtude.Where(condition);
        }
        public IEnumerable<Evaluation> WhereEvaluation(Expression<Func<Evaluation, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Evaluation.Where(condition).AsNoTracking() : _db.Evaluation.Where(condition);
        }
        public IEnumerable<Formulaire> WhereFormulaire(Expression<Func<Formulaire, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Formulaire.Where(condition).AsNoTracking() : _db.Formulaire.Where(condition);
        }
        public IEnumerable<Groupe> WhereGroupe(Expression<Func<Groupe, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Groupe.Where(condition).AsNoTracking() : _db.Groupe.Where(condition);
        }
        public IEnumerable<GroupeEtudiant> WhereGroupeEtudiant(Expression<Func<GroupeEtudiant, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.GroupeEtudiant.Where(condition).AsNoTracking() : _db.GroupeEtudiant.Where(condition);
        }
        public IEnumerable<Inscription> WhereInscription(Expression<Func<Inscription, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Inscription.Where(condition).AsNoTracking() : _db.Inscription.Where(condition);
        }
        public IEnumerable<Jumelage> WhereJumelage(Expression<Func<Jumelage, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Jumelage.Where(condition).AsNoTracking() : _db.Jumelage.Where(condition);
        }
        public IEnumerable<Personne> WherePersonne(Expression<Func<Personne, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Personne.Where(condition).AsNoTracking() : _db.Personne.Where(condition);
        }
        public IEnumerable<ProgrammeEtude> WhereProgrammeEtude(Expression<Func<ProgrammeEtude, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.ProgrammeEtude.Where(condition).AsNoTracking() : _db.ProgrammeEtude.Where(condition);
        }
        public IEnumerable<Question> WhereQuestion(Expression<Func<Question, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Question.Where(condition).AsNoTracking() : _db.Question.Where(condition);
        }
        public IEnumerable<ReponseQuestion> WhereReponseQuestion(Expression<Func<ReponseQuestion, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.ReponseQuestion.Where(condition).AsNoTracking() : _db.ReponseQuestion.Where(condition);
        }
        public IEnumerable<Section> WhereSection(Expression<Func<Section, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Section.Where(condition).AsNoTracking() : _db.Section.Where(condition);
        }
        public IEnumerable<Session> WhereSession(Expression<Func<Session, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Session.Where(condition).AsNoTracking() : _db.Session.Where(condition);
        }
        public IEnumerable<Suivi> WhereSuivi(Expression<Func<Suivi, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.Suivi.Where(condition).AsNoTracking() : _db.Suivi.Where(condition);
        }
        //Where - sur table parametres
        public IEnumerable<p_College> WhereCollege(Expression<Func<p_College, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_College.Where(condition).AsNoTracking() : _db.p_College.Where(condition);
        }
        public IEnumerable<p_Contact> WhereContact(Expression<Func<p_Contact, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_Contact.Where(condition).AsNoTracking() : _db.p_Contact.Where(condition);
        }
        public IEnumerable<p_HoraireInscription> WhereHoraireInscription(Expression<Func<p_HoraireInscription, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_HoraireInscription.Where(condition).AsNoTracking() : _db.p_HoraireInscription.Where(condition);
        }
        public IEnumerable<p_Jour> WhereJour(Expression<Func<p_Jour, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_Jour.Where(condition).AsNoTracking() : _db.p_Jour.Where(condition);
        }
        public IEnumerable<p_Saison> WhereSaison(Expression<Func<p_Saison, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_Saison.Where(condition).AsNoTracking() : _db.p_Saison.Where(condition);
        }
        public IEnumerable<p_Sexe> WhereSexe(Expression<Func<p_Sexe, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_Sexe.Where(condition).AsNoTracking() : _db.p_Sexe.Where(condition);
        }
        public IEnumerable<p_StatutCours> WhereStatutCours(Expression<Func<p_StatutCours, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_StatutCours.Where(condition).AsNoTracking() : _db.p_StatutCours.Where(condition);
        }
        public IEnumerable<p_StatutInscription> WhereStatutInscription(Expression<Func<p_StatutInscription, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_StatutInscription.Where(condition).AsNoTracking() : _db.p_StatutInscription.Where(condition);
        }
        public IEnumerable<p_TypeCourriel> WhereTypeCourriel(Expression<Func<p_TypeCourriel, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_TypeCourriel.Where(condition).AsNoTracking() : _db.p_TypeCourriel.Where(condition);
        }
        public IEnumerable<p_TypeFormulaire> WhereTypeFormulaire(Expression<Func<p_TypeFormulaire, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_TypeFormulaire.Where(condition).AsNoTracking() : _db.p_TypeFormulaire.Where(condition);
        }
        public IEnumerable<p_TypeInscription> WhereTypeInscription(Expression<Func<p_TypeInscription, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_TypeInscription.Where(condition).AsNoTracking() : _db.p_TypeInscription.Where(condition);
        }
        public IEnumerable<p_TypeResultat> WhereTypeResultat(Expression<Func<p_TypeResultat, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_TypeResultat.Where(condition).AsNoTracking() : _db.p_TypeResultat.Where(condition);
        }
        public IEnumerable<p_TypeUsag> WhereTypeUsag(Expression<Func<p_TypeUsag, bool>> condition, bool asNoTracking = false)
        {
            return asNoTracking ? _db.p_TypeUsag.Where(condition).AsNoTracking() : _db.p_TypeUsag.Where(condition);
        }
        #endregion

        #region All
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
        #endregion

        #region Find
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
        #endregion

        #region Add
        public void AddChoixReponse(ChoixReponse itemToAdd, bool saveChanges = true)
        {
            _db.ChoixReponse.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddCourriel(Courriel itemToAdd, bool saveChanges = true)
        {
            _db.Courriel.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddCours(Cours itemToAdd, bool saveChanges = true)
        {
            _db.Cours.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddCoursInteret(CoursInteret itemToAdd, bool saveChanges = true)
        {
            _db.CoursInteret.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddCoursSuivi(CoursSuivi itemToAdd, bool saveChanges = true)
        {
            _db.CoursSuivi.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddDisponibilite(Disponibilite itemToAdd, bool saveChanges = true)
        {
            _db.Disponibilite.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddEtuProgEtude(EtuProgEtude itemToAdd, bool saveChanges = true)
        {
            _db.EtuProgEtude.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddEvaluation(Evaluation itemToAdd, bool saveChanges = true)
        {
            _db.Evaluation.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddFormulaire(Formulaire itemToAdd, bool saveChanges = true)
        {
            _db.Formulaire.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddGroupe(Groupe itemToAdd, bool saveChanges = true)
        {
            _db.Groupe.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddGroupeEtudiant(GroupeEtudiant itemToAdd, bool saveChanges = true)
        {
            _db.GroupeEtudiant.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddInscription(Inscription itemToAdd, bool saveChanges = true)
        {
            _db.Inscription.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddJumelage(Jumelage itemToAdd, bool saveChanges = true)
        {
            _db.Jumelage.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddPersonne(Personne itemToAdd, bool saveChanges = true)
        {
            _db.Personne.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddProgrammeEtude(ProgrammeEtude itemToAdd, bool saveChanges = true)
        {
            _db.ProgrammeEtude.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddQuestion(Question itemToAdd, bool saveChanges = true)
        {
            _db.Question.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddReponseQuestion(ReponseQuestion itemToAdd, bool saveChanges = true)
        {
            _db.ReponseQuestion.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddSection(Section itemToAdd, bool saveChanges = true)
        {
            _db.Section.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddSession(Session itemToAdd, bool saveChanges = true)
        {
            _db.Session.Add(itemToAdd);
            _db.SaveChanges();
        }
        public void AddSuivi(Suivi itemToAdd, bool saveChanges = true)
        {
            _db.Suivi.Add(itemToAdd);
            _db.SaveChanges();
        }
        #endregion

        #region AddRange
        public void AddRangeChoixReponse(IEnumerable<ChoixReponse> itemsToAdd, bool saveChanges = true)
        {
            _db.ChoixReponse.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeCourriel(IEnumerable<Courriel> itemsToAdd, bool saveChanges = true)
        {
            _db.Courriel.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeCours(IEnumerable<Cours> itemsToAdd, bool saveChanges = true)
        {
            _db.Cours.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeCoursInteret(IEnumerable<CoursInteret> itemsToAdd, bool saveChanges = true)
        {
            _db.CoursInteret.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeCoursSuivi(IEnumerable<CoursSuivi> itemsToAdd, bool saveChanges = true)
        {
            _db.CoursSuivi.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeDisponibilite(IEnumerable<Disponibilite> itemsToAdd, bool saveChanges = true)
        {
            _db.Disponibilite.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeEtuProgEtude(IEnumerable<EtuProgEtude> itemsToAdd, bool saveChanges = true)
        {
            _db.EtuProgEtude.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeEvaluation(IEnumerable<Evaluation> itemsToAdd, bool saveChanges = true)
        {
            _db.Evaluation.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeFormulaire(IEnumerable<Formulaire> itemsToAdd, bool saveChanges = true)
        {
            _db.Formulaire.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeGroupe(IEnumerable<Groupe> itemsToAdd, bool saveChanges = true)
        {
            _db.Groupe.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeGroupeEtudiant(IEnumerable<GroupeEtudiant> itemsToAdd, bool saveChanges = true)
        {
            _db.GroupeEtudiant.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeInscription(IEnumerable<Inscription> itemsToAdd, bool saveChanges = true)
        {
            _db.Inscription.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeJumelage(IEnumerable<Jumelage> itemsToAdd, bool saveChanges = true)
        {
            _db.Jumelage.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangePersonne(IEnumerable<Personne> itemsToAdd, bool saveChanges = true)
        {
            _db.Personne.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeProgrammeEtude(IEnumerable<ProgrammeEtude> itemsToAdd, bool saveChanges = true)
        {
            _db.ProgrammeEtude.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeQuestion(IEnumerable<Question> itemsToAdd, bool saveChanges = true)
        {
            _db.Question.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeReponseQuestion(IEnumerable<ReponseQuestion> itemsToAdd, bool saveChanges = true)
        {
            _db.ReponseQuestion.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeSection(IEnumerable<Section> itemsToAdd, bool saveChanges = true)
        {
            _db.Section.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeSession(IEnumerable<Session> itemsToAdd, bool saveChanges = true)
        {
            _db.Session.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        public void AddRangeSuivi(IEnumerable<Suivi> itemsToAdd, bool saveChanges = true)
        {
            _db.Suivi.AddRange(itemsToAdd);
            _db.SaveChanges();
        }
        #endregion

        #region Edit
        public void EditChoixReponse(ChoixReponse itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditCourriel(Courriel itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditCours(Cours itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditCoursInteret(CoursInteret itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditCoursSuivi(CoursSuivi itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditDisponibilite(Disponibilite itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditEtuProgEtude(EtuProgEtude itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditEvaluation(Evaluation itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditFormulaire(Formulaire itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditGroupe(Groupe itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditGroupeEtudiant(GroupeEtudiant itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditInscription(Inscription itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditJumelage(Jumelage itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditPersonne(Personne itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditProgrammeEtude(ProgrammeEtude itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditQuestion(Question itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditReponseQuestion(ReponseQuestion itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditSection(Section itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditSession(Session itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        public void EditSuivi(Suivi itemToEdit, bool saveChanges = true)
        {
            _db.Entry(itemToEdit).State = EntityState.Modified;
            if(saveChanges) _db.SaveChanges();
        }
        #endregion

        #region Remove
        public void RemoveChoixReponse(ChoixReponse itemToRemove, bool saveChanges = true)
        {
            _db.ChoixReponse.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveCourriel(Courriel itemToRemove, bool saveChanges = true)
        {
            _db.Courriel.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveCours(Cours itemToRemove, bool saveChanges = true)
        {
            _db.Cours.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveCoursInteret(CoursInteret itemToRemove, bool saveChanges = true)
        {
            _db.CoursInteret.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveCoursSuivi(CoursSuivi itemToRemove, bool saveChanges = true)
        {
            _db.CoursSuivi.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveDisponibilite(Disponibilite itemToRemove, bool saveChanges = true)
        {
            _db.Disponibilite.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveEtuProgEtude(EtuProgEtude itemToRemove, bool saveChanges = true)
        {
            _db.EtuProgEtude.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveEvaluation(Evaluation itemToRemove, bool saveChanges = true)
        {
            _db.Evaluation.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveFormulaire(Formulaire itemToRemove, bool saveChanges = true)
        {
            _db.Formulaire.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveGroupe(Groupe itemToRemove, bool saveChanges = true)
        {
            _db.Groupe.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveGroupeEtudiant(GroupeEtudiant itemToRemove, bool saveChanges = true)
        {
            _db.GroupeEtudiant.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveInscription(Inscription itemToRemove, bool saveChanges = true)
        {
            _db.Inscription.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveJumelage(Jumelage itemToRemove, bool saveChanges = true)
        {
            _db.Jumelage.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemovePersonne(Personne itemToRemove, bool saveChanges = true)
        {
            _db.Personne.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveProgrammeEtude(ProgrammeEtude itemToRemove, bool saveChanges = true)
        {
            _db.ProgrammeEtude.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveQuestion(Question itemToRemove, bool saveChanges = true)
        {
            _db.Question.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveReponseQuestion(ReponseQuestion itemToRemove, bool saveChanges = true)
        {
            _db.ReponseQuestion.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveSection(Section itemToRemove, bool saveChanges = true)
        {
            _db.Section.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveSession(Session itemToRemove, bool saveChanges = true)
        {
            _db.Session.Remove(itemToRemove);
            _db.SaveChanges();
        }
        public void RemoveSuivi(Suivi itemToRemove, bool saveChanges = true)
        {
            _db.Suivi.Remove(itemToRemove);
            _db.SaveChanges();
        }
        #endregion

        #region RemoveRange
        public void RemoveRangeChoixReponse(IEnumerable<ChoixReponse> itemsToRemove, bool saveChanges = true)
        {
            _db.ChoixReponse.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeCourriel(IEnumerable<Courriel> itemsToRemove, bool saveChanges = true)
        {
            _db.Courriel.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeCours(IEnumerable<Cours> itemsToRemove, bool saveChanges = true)
        {
            _db.Cours.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeCoursInteret(IEnumerable<CoursInteret> itemsToRemove, bool saveChanges = true)
        {
            _db.CoursInteret.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeCoursSuivi(IEnumerable<CoursSuivi> itemsToRemove, bool saveChanges = true)
        {
            _db.CoursSuivi.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeDisponibilite(IEnumerable<Disponibilite> itemsToRemove, bool saveChanges = true)
        {
            _db.Disponibilite.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeEtuProgEtude(IEnumerable<EtuProgEtude> itemsToRemove, bool saveChanges = true)
        {
            _db.EtuProgEtude.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeEvaluation(IEnumerable<Evaluation> itemsToRemove, bool saveChanges = true)
        {
            _db.Evaluation.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeFormulaire(IEnumerable<Formulaire> itemsToRemove, bool saveChanges = true)
        {
            _db.Formulaire.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeGroupe(IEnumerable<Groupe> itemsToRemove, bool saveChanges = true)
        {
            _db.Groupe.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeGroupeEtudiant(IEnumerable<GroupeEtudiant> itemsToRemove, bool saveChanges = true)
        {
            _db.GroupeEtudiant.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeInscription(IEnumerable<Inscription> itemsToRemove, bool saveChanges = true)
        {
            _db.Inscription.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeJumelage(IEnumerable<Jumelage> itemsToRemove, bool saveChanges = true)
        {
            _db.Jumelage.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangePersonne(IEnumerable<Personne> itemsToRemove, bool saveChanges = true)
        {
            _db.Personne.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeProgrammeEtude(IEnumerable<ProgrammeEtude> itemsToRemove, bool saveChanges = true)
        {
            _db.ProgrammeEtude.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeQuestion(IEnumerable<Question> itemsToRemove, bool saveChanges = true)
        {
            _db.Question.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeReponseQuestion(IEnumerable<ReponseQuestion> itemsToRemove, bool saveChanges = true)
        {
            _db.ReponseQuestion.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeSection(IEnumerable<Section> itemsToRemove, bool saveChanges = true)
        {
            _db.Section.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeSession(IEnumerable<Session> itemsToRemove, bool saveChanges = true)
        {
            _db.Session.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        public void RemoveRangeSuivi(IEnumerable<Suivi> itemsToRemove, bool saveChanges = true)
        {
            _db.Suivi.RemoveRange(itemsToRemove);
            _db.SaveChanges();
        }
        #endregion

        #region Liste
        public SelectList ListeTypeUsager(int idTypeUsager = 0)
        {
            return new SelectList(AllTypeUsag(), "id_TypeUsag", "TypeUsag", 
                idTypeUsager);
        }
        public SelectList ListeTypeUsagerDuPersonnel(int idTypeUsager = 0)
        {
            return new SelectList(WhereTypeUsag(x => x.id_TypeUsag == (int)TypeUsagers.Enseignant || x.id_TypeUsag == (int)TypeUsagers.Responsable), "id_TypeUsag", "TypeUsag",
                idTypeUsager);
        }
        public SelectList ListeSexe(int? sexe = 0)
        {
            if (sexe == null) sexe = 0;
            return new SelectList(AllSexe(), "id_Sexe", "Sexe", sexe);
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
        public SelectList ListeProgrammmeEtude(bool actif = true)
        {
            return new SelectList(WhereProgrammeEtude(x => x.Actif == actif), "id_ProgEtu", "CodeNomProgramme");
        }
        public SelectList ListeEtudiants(int id = 0)
        {
            return new SelectList(WherePersonne(x=>x.id_TypeUsag == (int)TypeUsagers.Etudiant)
                .OrderBy(x=>x.Nom)
                .ThenBy(x=>x.Prenom), "id_Pers", "Nom", id);
        }
        public SelectList ListeEnseignant(int id = 0)
        {
            return new SelectList(WherePersonne(x => x.id_TypeUsag == (int)TypeUsagers.Enseignant && x.Actif)
                .OrderBy(x => x.Nom)
                .ThenBy(x => x.Prenom), "id_Pers", "NomPrenom", id);
        }
        public SelectList ListeEnseignantEtResponsable(int id = 0)
        {
            return new SelectList(WherePersonne(x => x.id_TypeUsag == (int)TypeUsagers.Enseignant && x.Actif
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
        #endregion

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
