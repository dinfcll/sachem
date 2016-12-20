using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using sachem.Models;
using sachem.Models.DataAccess;

namespace sachemTests
{
    internal class TestRepository : IDataRepository
    {
        private readonly List<ChoixReponse> _listeChoixReponse = new List<ChoixReponse>();
        private readonly List<Courriel> _listeCourriel = new List<Courriel>();
        private readonly List<Cours> _listeCours = new List<Cours>();
        private readonly List<CoursInteret> _listeCoursInteret = new List<CoursInteret>();
        private readonly List<CoursSuivi> _listeCoursSuivi = new List<CoursSuivi>();
        private readonly List<Disponibilite> _listeDisponibilite = new List<Disponibilite>();
        private readonly List<EtuProgEtude> _listeEtuProgEtude = new List<EtuProgEtude>();
        private readonly List<Evaluation> _listeEvaluation = new List<Evaluation>();
        private readonly List<Formulaire> _listeFormulaire = new List<Formulaire>();
        private readonly List<Groupe> _listeGroupe = new List<Groupe>();
        private readonly List<GroupeEtudiant> _listeGroupeEtudiant = new List<GroupeEtudiant>();
        private readonly List<Inscription> _listeInscription = new List<Inscription>();
        private readonly List<Jumelage> _listeJumelage = new List<Jumelage>();
        private readonly List<Personne> _listePersonne = new List<Personne>();
        private readonly List<ProgrammeEtude> _listeProgrammeEtude = new List<ProgrammeEtude>();
        private readonly List<Question> _listeQuestion = new List<Question>();
        private readonly List<ReponseQuestion> _listeReponseQuestion = new List<ReponseQuestion>();
        private readonly List<Section> _listeSection = new List<Section>();
        private readonly List<Session> _listeSession = new List<Session>();
        private readonly List<Suivi> _listeSuivi = new List<Suivi>();
        private readonly List<p_College> _listeCollege = new List<p_College>();
        private readonly List<p_Contact> _listeContact = new List<p_Contact>();
        private readonly List<p_HoraireInscription> _listeHoraireInscription = new List<p_HoraireInscription>();
        private readonly List<p_Jour> _listeJour = new List<p_Jour>();
        private readonly List<p_Saison> _listeSaison = new List<p_Saison>();
        private readonly List<p_Sexe> _listeSexe = new List<p_Sexe>();
        private readonly List<p_StatutCours> _listeStatutCours = new List<p_StatutCours>();
        private readonly List<p_StatutInscription> _listeStatutInscription = new List<p_StatutInscription>();
        private readonly List<p_TypeCourriel> _listeTypeCourriel = new List<p_TypeCourriel>();
        private readonly List<p_TypeFormulaire> _listeTypeFormulaire = new List<p_TypeFormulaire>();
        private readonly List<p_TypeInscription> _listeTypeInscription = new List<p_TypeInscription>();
        private readonly List<p_TypeResultat> _listeTypeResultat = new List<p_TypeResultat>();
        private readonly List<p_TypeUsag> _listeTypeUsag = new List<p_TypeUsag>();

        public int SessionEnCours()
        {
            throw new NotImplementedException();
        }

        public string FindMdp(int id)
        {
            throw new NotImplementedException();
        }

        public void BeLazy(bool set)
        {
            throw new NotImplementedException();
        }

        #region Any
        public bool AnyChoixReponse(Expression<Func<ChoixReponse, bool>> condition)
        {
            return _listeChoixReponse.AsQueryable().Any(condition);
        }
        public bool AnyCourriel(Expression<Func<Courriel, bool>> condition)
        {
            return _listeCourriel.AsQueryable().Any(condition);
        }
        public bool AnyCours(Expression<Func<Cours, bool>> condition)
        {
            return _listeCours.AsQueryable().Any(condition);
        }
        public bool AnyCoursInteret(Expression<Func<CoursInteret, bool>> condition)
        {
            return _listeCoursInteret.AsQueryable().Any(condition);
        }
        public bool AnyCoursSuivi(Expression<Func<CoursSuivi, bool>> condition)
        {
            return _listeCoursSuivi.AsQueryable().Any(condition);
        }
        public bool AnyDisponibilite(Expression<Func<Disponibilite, bool>> condition)
        {
            return _listeDisponibilite.AsQueryable().Any(condition);
        }
        public bool AnyEtuProgEtude(Expression<Func<EtuProgEtude, bool>> condition)
        {
            return _listeEtuProgEtude.AsQueryable().Any(condition);
        }
        public bool AnyEvaluation(Expression<Func<Evaluation, bool>> condition)
        {
            return _listeEvaluation.AsQueryable().Any(condition);
        }
        public bool AnyFormulaire(Expression<Func<Formulaire, bool>> condition)
        {
            return _listeFormulaire.AsQueryable().Any(condition);
        }
        public bool AnyGroupe(Expression<Func<Groupe, bool>> condition)
        {
            return _listeGroupe.AsQueryable().Any(condition);
        }
        public bool AnyGroupeEtudiant(Expression<Func<GroupeEtudiant, bool>> condition)
        {
            return _listeGroupeEtudiant.AsQueryable().Any(condition);
        }
        public bool AnyInscription(Expression<Func<Inscription, bool>> condition)
        {
            return _listeInscription.AsQueryable().Any(condition);
        }
        public bool AnyJumelage(Expression<Func<Jumelage, bool>> condition)
        {
            return _listeJumelage.AsQueryable().Any(condition);
        }
        public bool AnyPersonne(Expression<Func<Personne, bool>> condition)
        {
            return _listePersonne.AsQueryable().Any(condition);
        }
        public bool AnyProgrammeEtude(Expression<Func<ProgrammeEtude, bool>> condition)
        {
            return _listeProgrammeEtude.AsQueryable().Any(condition);
        }
        public bool AnyQuestion(Expression<Func<Question, bool>> condition)
        {
            return _listeQuestion.AsQueryable().Any(condition);
        }
        public bool AnyReponseQuestion(Expression<Func<ReponseQuestion, bool>> condition)
        {
            return _listeReponseQuestion.AsQueryable().Any(condition);
        }
        public bool AnySection(Expression<Func<Section, bool>> condition)
        {
            return _listeSection.AsQueryable().Any(condition);
        }
        public bool AnySession(Expression<Func<Session, bool>> condition)
        {
            return _listeSession.AsQueryable().Any(condition);
        }
        public bool AnySuivi(Expression<Func<Suivi, bool>> condition)
        {
            return _listeSuivi.AsQueryable().Any(condition);
        }
        //Any - sur table parametres
        public bool AnyCollege(Expression<Func<p_College, bool>> condition)
        {
            return _listeCollege.AsQueryable().Any(condition);
        }
        public bool AnyContact(Expression<Func<p_Contact, bool>> condition)
        {
            return _listeContact.AsQueryable().Any(condition);
        }
        public bool AnyHoraireInscription(Expression<Func<p_HoraireInscription, bool>> condition)
        {
            return _listeHoraireInscription.AsQueryable().Any(condition);
        }
        public bool AnyJour(Expression<Func<p_Jour, bool>> condition)
        {
            return _listeJour.AsQueryable().Any(condition);
        }
        public bool AnySaison(Expression<Func<p_Saison, bool>> condition)
        {
            return _listeSaison.AsQueryable().Any(condition);
        }
        public bool AnySexe(Expression<Func<p_Sexe, bool>> condition)
        {
            return _listeSexe.AsQueryable().Any(condition);
        }
        public bool AnyStatutCours(Expression<Func<p_StatutCours, bool>> condition)
        {
            return _listeStatutCours.AsQueryable().Any(condition);
        }
        public bool AnyStatutInscription(Expression<Func<p_StatutInscription, bool>> condition)
        {
            return _listeStatutInscription.AsQueryable().Any(condition);
        }
        public bool AnyTypeCourriel(Expression<Func<p_TypeCourriel, bool>> condition)
        {
            return _listeTypeCourriel.AsQueryable().Any(condition);
        }
        public bool AnyTypeFormulaire(Expression<Func<p_TypeFormulaire, bool>> condition)
        {
            return _listeTypeFormulaire.AsQueryable().Any(condition);
        }
        public bool AnyTypeInscription(Expression<Func<p_TypeInscription, bool>> condition)
        {
            return _listeTypeInscription.AsQueryable().Any(condition);
        }
        public bool AnyTypeResultat(Expression<Func<p_TypeResultat, bool>> condition)
        {
            return _listeTypeResultat.AsQueryable().Any(condition);
        }
        public bool AnyTypeUsag(Expression<Func<p_TypeUsag, bool>> condition)
        {
            return _listeTypeUsag.AsQueryable().Any(condition);
        }
        #endregion

        #region Where
        public IEnumerable<ChoixReponse> WhereChoixReponse(Expression<Func<ChoixReponse, bool>> condition, bool asNoTracking = false)
        {
            return _listeChoixReponse.AsQueryable().Where(condition);
        }
        public IEnumerable<Courriel> WhereCourriel(Expression<Func<Courriel, bool>> condition, bool asNoTracking = false)
        {
            return _listeCourriel.AsQueryable().Where(condition);
        }
        public IEnumerable<Cours> WhereCours(Expression<Func<Cours, bool>> condition, bool asNoTracking = false)
        {
            return _listeCours.AsQueryable().Where(condition);
        }
        public IEnumerable<CoursInteret> WhereCoursInteret(Expression<Func<CoursInteret, bool>> condition, bool asNoTracking = false)
        {
            return _listeCoursInteret.AsQueryable().Where(condition);
        }
        public IEnumerable<CoursSuivi> WhereCoursSuivi(Expression<Func<CoursSuivi, bool>> condition, bool asNoTracking = false)
        {
            return _listeCoursSuivi.AsQueryable().Where(condition);
        }
        public IEnumerable<Disponibilite> WhereDisponibilite(Expression<Func<Disponibilite, bool>> condition, bool asNoTracking = false)
        {
            return _listeDisponibilite.AsQueryable().Where(condition);
        }
        public IEnumerable<EtuProgEtude> WhereEtuProgEtude(Expression<Func<EtuProgEtude, bool>> condition, bool asNoTracking = false)
        {
            return _listeEtuProgEtude.AsQueryable().Where(condition);
        }
        public IEnumerable<Evaluation> WhereEvaluation(Expression<Func<Evaluation, bool>> condition, bool asNoTracking = false)
        {
            return _listeEvaluation.AsQueryable().Where(condition);
        }
        public IEnumerable<Formulaire> WhereFormulaire(Expression<Func<Formulaire, bool>> condition, bool asNoTracking = false)
        {
            return _listeFormulaire.AsQueryable().Where(condition);
        }
        public IEnumerable<Groupe> WhereGroupe(Expression<Func<Groupe, bool>> condition, bool asNoTracking = false)
        {
            return _listeGroupe.AsQueryable().Where(condition);
        }
        public IEnumerable<GroupeEtudiant> WhereGroupeEtudiant(Expression<Func<GroupeEtudiant, bool>> condition, bool asNoTracking = false)
        {
            return _listeGroupeEtudiant.AsQueryable().Where(condition);
        }
        public IEnumerable<Inscription> WhereInscription(Expression<Func<Inscription, bool>> condition, bool asNoTracking = false)
        {
            return _listeInscription.AsQueryable().Where(condition);
        }
        public IEnumerable<Jumelage> WhereJumelage(Expression<Func<Jumelage, bool>> condition, bool asNoTracking = false)
        {
            return _listeJumelage.AsQueryable().Where(condition);
        }
        public IEnumerable<Personne> WherePersonne(Expression<Func<Personne, bool>> condition, bool asNoTracking = false)
        {
            return _listePersonne.AsQueryable().Where(condition);
        }
        public IEnumerable<ProgrammeEtude> WhereProgrammeEtude(Expression<Func<ProgrammeEtude, bool>> condition, bool asNoTracking = false)
        {
            return _listeProgrammeEtude.AsQueryable().Where(condition);
        }
        public IEnumerable<Question> WhereQuestion(Expression<Func<Question, bool>> condition, bool asNoTracking = false)
        {
            return _listeQuestion.AsQueryable().Where(condition);
        }
        public IEnumerable<ReponseQuestion> WhereReponseQuestion(Expression<Func<ReponseQuestion, bool>> condition, bool asNoTracking = false)
        {
            return _listeReponseQuestion.AsQueryable().Where(condition);
        }
        public IEnumerable<Section> WhereSection(Expression<Func<Section, bool>> condition, bool asNoTracking = false)
        {
            return _listeSection.AsQueryable().Where(condition);
        }
        public IEnumerable<Session> WhereSession(Expression<Func<Session, bool>> condition, bool asNoTracking = false)
        {
            return _listeSession.AsQueryable().Where(condition);
        }
        public IEnumerable<Suivi> WhereSuivi(Expression<Func<Suivi, bool>> condition, bool asNoTracking = false)
        {
            return _listeSuivi.AsQueryable().Where(condition);
        }
            //Where - sur table parametres
        public IEnumerable<p_College> WhereCollege(Expression<Func<p_College, bool>> condition, bool asNoTracking = false)
        {
            return _listeCollege.AsQueryable().Where(condition);
        }
        public IEnumerable<p_Contact> WhereContact(Expression<Func<p_Contact, bool>> condition, bool asNoTracking = false)
        {
            return _listeContact.AsQueryable().Where(condition);
        }
        public IEnumerable<p_HoraireInscription> WhereHoraireInscription(Expression<Func<p_HoraireInscription, bool>> condition, bool asNoTracking = false)
        {
            return _listeHoraireInscription.AsQueryable().Where(condition);
        }
        public IEnumerable<p_Jour> WhereJour(Expression<Func<p_Jour, bool>> condition, bool asNoTracking = false)
        {
            return _listeJour.AsQueryable().Where(condition);
        }
        public IEnumerable<p_Saison> WhereSaison(Expression<Func<p_Saison, bool>> condition, bool asNoTracking = false)
        {
            return _listeSaison.AsQueryable().Where(condition);
        }
        public IEnumerable<p_Sexe> WhereSexe(Expression<Func<p_Sexe, bool>> condition, bool asNoTracking = false)
        {
            return _listeSexe.AsQueryable().Where(condition);
        }
        public IEnumerable<p_StatutCours> WhereStatutCours(Expression<Func<p_StatutCours, bool>> condition, bool asNoTracking = false)
        {
            return _listeStatutCours.AsQueryable().Where(condition);
        }
        public IEnumerable<p_StatutInscription> WhereStatutInscription(Expression<Func<p_StatutInscription, bool>> condition, bool asNoTracking = false)
        {
            return _listeStatutInscription.AsQueryable().Where(condition);
        }
        public IEnumerable<p_TypeCourriel> WhereTypeCourriel(Expression<Func<p_TypeCourriel, bool>> condition, bool asNoTracking = false)
        {
            return _listeTypeCourriel.AsQueryable().Where(condition);
        }
        public IEnumerable<p_TypeFormulaire> WhereTypeFormulaire(Expression<Func<p_TypeFormulaire, bool>> condition, bool asNoTracking = false)
        {
            return _listeTypeFormulaire.AsQueryable().Where(condition);
        }
        public IEnumerable<p_TypeInscription> WhereTypeInscription(Expression<Func<p_TypeInscription, bool>> condition, bool asNoTracking = false)
        {
            return _listeTypeInscription.AsQueryable().Where(condition);
        }
        public IEnumerable<p_TypeResultat> WhereTypeResultat(Expression<Func<p_TypeResultat, bool>> condition, bool asNoTracking = false)
        {
            return _listeTypeResultat.AsQueryable().Where(condition);
        }
        public IEnumerable<p_TypeUsag> WhereTypeUsag(Expression<Func<p_TypeUsag, bool>> condition, bool asNoTracking = false)
        {
            return _listeTypeUsag.AsQueryable().Where(condition);
        }
        #endregion

        #region All
        public IEnumerable<ChoixReponse> AllChoixReponse()
        {
            return _listeChoixReponse.AsEnumerable();
        }
        public IEnumerable<Courriel> AllCourriel()
        {
            return _listeCourriel.AsEnumerable();
        }
        public IEnumerable<Cours> AllCours()
        {
            return _listeCours.AsEnumerable();
        }
        public IEnumerable<CoursInteret> AllCoursInteret()
        {
            return _listeCoursInteret.AsEnumerable();
        }
        public IEnumerable<CoursSuivi> AllCoursSuivi()
        {
            return _listeCoursSuivi.AsEnumerable();
        }
        public IEnumerable<Disponibilite> AllDisponibilite()
        {
            return _listeDisponibilite.AsEnumerable();
        }
        public IEnumerable<EtuProgEtude> AllEtuProgEtude()
        {
            return _listeEtuProgEtude.AsEnumerable();
        }
        public IEnumerable<Evaluation> AllEvaluation()
        {
            return _listeEvaluation.AsEnumerable();
        }
        public IEnumerable<Formulaire> AllFormulaire()
        {
            return _listeFormulaire.AsEnumerable();
        }
        public IEnumerable<Groupe> AllGroupe()
        {
            return _listeGroupe.AsEnumerable();
        }
        public IEnumerable<GroupeEtudiant> AllGroupeEtudiant()
        {
            return _listeGroupeEtudiant.AsEnumerable();
        }
        public IEnumerable<Inscription> AllInscription()
        {
            return _listeInscription.AsEnumerable();
        }
        public IEnumerable<Jumelage> AllJumelage()
        {
            return _listeJumelage.AsEnumerable();
        }
        public IEnumerable<Personne> AllPersonne()
        {
            return _listePersonne.AsEnumerable();
        }
        public IEnumerable<ProgrammeEtude> AllProgrammeEtude()
        {
            return _listeProgrammeEtude.AsEnumerable();
        }
        public IEnumerable<Question> AllQuestion()
        {
            return _listeQuestion.AsEnumerable();
        }
        public IEnumerable<ReponseQuestion> AllReponseQuestion()
        {
            return _listeReponseQuestion.AsEnumerable();
        }
        public IEnumerable<Section> AllSection()
        {
            return _listeSection.AsEnumerable();
        }
        public IEnumerable<Session> AllSession()
        {
            return _listeSession.AsEnumerable();
        }
        public IEnumerable<Suivi> AllSuivi()
        {
            return _listeSuivi.AsEnumerable();
        }
            //All - sur table parametres
        public IEnumerable<p_College> AllCollege()
        {
            return _listeCollege.AsEnumerable();
        }
        public IEnumerable<p_Contact> AllContact()
        {
            return _listeContact.AsEnumerable();
        }
        public IEnumerable<p_HoraireInscription> AllHoraireInscription()
        {
            return _listeHoraireInscription.AsEnumerable();
        }
        public IEnumerable<p_Jour> AllJour()
        {
            return _listeJour.AsEnumerable();
        }
        public IEnumerable<p_Saison> AllSaison()
        {
            return _listeSaison.AsEnumerable();
        }
        public IEnumerable<p_Sexe> AllSexe()
        {
            return _listeSexe.AsEnumerable();
        }
        public IEnumerable<p_StatutCours> AllStatutCours()
        {
            return _listeStatutCours.AsEnumerable();
        }
        public IEnumerable<p_StatutInscription> AllStatutInscription()
        {
            return _listeStatutInscription.AsEnumerable();
        }
        public IEnumerable<p_TypeCourriel> AllTypeCourriel()
        {
            return _listeTypeCourriel.AsEnumerable();
        }
        public IEnumerable<p_TypeFormulaire> AllTypeFormulaire()
        {
            return _listeTypeFormulaire.AsEnumerable();
        }
        public IEnumerable<p_TypeInscription> AllTypeInscription()
        {
            return _listeTypeInscription.AsEnumerable();
        }
        public IEnumerable<p_TypeResultat> AllTypeResultat()
        {
            return _listeTypeResultat.AsEnumerable();
        }
        public IEnumerable<p_TypeUsag> AllTypeUsag()
        {
            return _listeTypeUsag.AsEnumerable();
        }
        #endregion

        #region Find
        public ChoixReponse FindChoixReponse(int id)
        {
            return _listeChoixReponse.Find(x => x.id_ChoixRep == id);
        }
        public Courriel FindCourriel(int id)
        {
            return _listeCourriel.Find(x => x.id_Courriel == id);
        }
        public Cours FindCours(int id)
        {
            return _listeCours.Find(x => x.id_Cours == id);
        }
        public CoursInteret FindCoursInteret(int id)
        {
            return _listeCoursInteret.Find(x => x.id_CoursInteret == id);
        }
        public CoursSuivi FindCoursSuivi(int id)
        {
            return _listeCoursSuivi.Find(x => x.id_CoursReussi == id);
        }
        public Disponibilite FindDisponibilite(int id)
        {
            return _listeDisponibilite.Find(x => x.id_Dispo == id);
        }
        public EtuProgEtude FindEtuProgEtude(int id)
        {
            return _listeEtuProgEtude.Find(x => x.id_EtuProgEtude == id);
        }
        public Evaluation FindEvaluation(int id)
        {
            return _listeEvaluation.Find(x => x.id_Evaluation == id);
        }
        public Formulaire FindFormulaire(int id)
        {
            return _listeFormulaire.Find(x => x.id_Formulaire == id);
        }
        public Groupe FindGroupe(int id)
        {
            return _listeGroupe.Find(x => x.id_Groupe == id);
        }
        public GroupeEtudiant FindGroupeEtudiant(int id)
        {
            return _listeGroupeEtudiant.Find(x => x.id_GroupeEtudiant == id);
        }
        public Inscription FindInscription(int id)
        {
            return _listeInscription.Find(x => x.id_Inscription == id);
        }
        public Jumelage FindJumelage(int id)
        {
            return _listeJumelage.Find(x => x.id_Jumelage == id);
        }
        public Personne FindPersonne(int id)
        {
            return _listePersonne.Find(x => x.id_Pers == id);
        }
        public ProgrammeEtude FindProgrammeEtude(int id)
        {
            return _listeProgrammeEtude.Find(x => x.id_ProgEtu == id);
        }
        public Question FindQuestion(int id)
        {
            return _listeQuestion.Find(x => x.id_Question == id);
        }
        public ReponseQuestion FindReponseQuestion(int id)
        {
            return _listeReponseQuestion.Find(x => x.id_RepQuest == id);
        }
        public Section FindSection(int id)
        {
            return _listeSection.Find(x => x.id_Section == id);
        }
        public Session FindSession(int id)
        {
            return _listeSession.Find(x => x.id_Sess == id);
        }
        public Suivi FindSuivi(int id)
        {
            return _listeSuivi.Find(x => x.id_Suivi == id);
        }
        #endregion

        #region Add
        public void AddChoixReponse(ChoixReponse itemToAdd, bool saveChanges = true)
        {
            _listeChoixReponse.Add(itemToAdd);
        }
        public void AddCourriel(Courriel itemToAdd, bool saveChanges = true)
        {
            _listeCourriel.Add(itemToAdd);
        }
        public void AddCours(Cours itemToAdd, bool saveChanges = true)
        {
            _listeCours.Add(itemToAdd);
        }
        public void AddCoursInteret(CoursInteret itemToAdd, bool saveChanges = true)
        {
            _listeCoursInteret.Add(itemToAdd);
        }
        public void AddCoursSuivi(CoursSuivi itemToAdd, bool saveChanges = true)
        {
            _listeCoursSuivi.Add(itemToAdd);
        }
        public void AddDisponibilite(Disponibilite itemToAdd, bool saveChanges = true)
        {
            _listeDisponibilite.Add(itemToAdd);
        }
        public void AddEtuProgEtude(EtuProgEtude itemToAdd, bool saveChanges = true)
        {
            _listeEtuProgEtude.Add(itemToAdd);
        }
        public void AddEvaluation(Evaluation itemToAdd, bool saveChanges = true)
        {
            _listeEvaluation.Add(itemToAdd);
        }
        public void AddFormulaire(Formulaire itemToAdd, bool saveChanges = true)
        {
            _listeFormulaire.Add(itemToAdd);
        }
        public void AddGroupe(Groupe itemToAdd, bool saveChanges = true)
        {
            _listeGroupe.Add(itemToAdd);
        }
        public void AddGroupeEtudiant(GroupeEtudiant itemToAdd, bool saveChanges = true)
        {
            _listeGroupeEtudiant.Add(itemToAdd);
        }
        public void AddInscription(Inscription itemToAdd, bool saveChanges = true)
        {
            _listeInscription.Add(itemToAdd);
        }
        public void AddJumelage(Jumelage itemToAdd, bool saveChanges = true)
        {
            _listeJumelage.Add(itemToAdd);
        }
        public void AddPersonne(Personne itemToAdd, bool saveChanges = true)
        {
            _listePersonne.Add(itemToAdd);
        }
        public void AddProgrammeEtude(ProgrammeEtude itemToAdd, bool saveChanges = true)
        {
            _listeProgrammeEtude.Add(itemToAdd);
        }
        public void AddQuestion(Question itemToAdd, bool saveChanges = true)
        {
            _listeQuestion.Add(itemToAdd);
        }
        public void AddReponseQuestion(ReponseQuestion itemToAdd, bool saveChanges = true)
        {
            _listeReponseQuestion.Add(itemToAdd);
        }
        public void AddSection(Section itemToAdd, bool saveChanges = true)
        {
            _listeSection.Add(itemToAdd);
        }
        public void AddSession(Session itemToAdd, bool saveChanges = true)
        {
            _listeSession.Add(itemToAdd);
        }
        public void AddSuivi(Suivi itemToAdd, bool saveChanges = true)
        {
            _listeSuivi.Add(itemToAdd);
        }
        #endregion

        #region AddRange
        public void AddRangeChoixReponse(IEnumerable<ChoixReponse> itemsToAdd, bool saveChanges = true)
        {
            _listeChoixReponse.AddRange(itemsToAdd);
        }
        public void AddRangeCourriel(IEnumerable<Courriel> itemsToAdd, bool saveChanges = true)
        {
            _listeCourriel.AddRange(itemsToAdd);
        }
        public void AddRangeCours(IEnumerable<Cours> itemsToAdd, bool saveChanges = true)
        {
            _listeCours.AddRange(itemsToAdd);
        }
        public void AddRangeCoursInteret(IEnumerable<CoursInteret> itemsToAdd, bool saveChanges = true)
        {
            _listeCoursInteret.AddRange(itemsToAdd);
        }
        public void AddRangeCoursSuivi(IEnumerable<CoursSuivi> itemsToAdd, bool saveChanges = true)
        {
            _listeCoursSuivi.AddRange(itemsToAdd);
        }
        public void AddRangeDisponibilite(IEnumerable<Disponibilite> itemsToAdd, bool saveChanges = true)
        {
            _listeDisponibilite.AddRange(itemsToAdd);
        }
        public void AddRangeEtuProgEtude(IEnumerable<EtuProgEtude> itemsToAdd, bool saveChanges = true)
        {
            _listeEtuProgEtude.AddRange(itemsToAdd);
        }
        public void AddRangeEvaluation(IEnumerable<Evaluation> itemsToAdd, bool saveChanges = true)
        {
            _listeEvaluation.AddRange(itemsToAdd);
        }
        public void AddRangeFormulaire(IEnumerable<Formulaire> itemsToAdd, bool saveChanges = true)
        {
            _listeFormulaire.AddRange(itemsToAdd);
        }
        public void AddRangeGroupe(IEnumerable<Groupe> itemsToAdd, bool saveChanges = true)
        {
            _listeGroupe.AddRange(itemsToAdd);
        }
        public void AddRangeGroupeEtudiant(IEnumerable<GroupeEtudiant> itemsToAdd, bool saveChanges = true)
        {
            _listeGroupeEtudiant.AddRange(itemsToAdd);
        }
        public void AddRangeInscription(IEnumerable<Inscription> itemsToAdd, bool saveChanges = true)
        {
            _listeInscription.AddRange(itemsToAdd);
        }
        public void AddRangeJumelage(IEnumerable<Jumelage> itemsToAdd, bool saveChanges = true)
        {
            _listeJumelage.AddRange(itemsToAdd);
        }
        public void AddRangePersonne(IEnumerable<Personne> itemsToAdd, bool saveChanges = true)
        {
            _listePersonne.AddRange(itemsToAdd);
        }
        public void AddRangeProgrammeEtude(IEnumerable<ProgrammeEtude> itemsToAdd, bool saveChanges = true)
        {
            _listeProgrammeEtude.AddRange(itemsToAdd);
        }
        public void AddRangeQuestion(IEnumerable<Question> itemsToAdd, bool saveChanges = true)
        {
            _listeQuestion.AddRange(itemsToAdd);
        }
        public void AddRangeReponseQuestion(IEnumerable<ReponseQuestion> itemsToAdd, bool saveChanges = true)
        {
            _listeReponseQuestion.AddRange(itemsToAdd);
        }
        public void AddRangeSection(IEnumerable<Section> itemsToAdd, bool saveChanges = true)
        {
            _listeSection.AddRange(itemsToAdd);
        }
        public void AddRangeSession(IEnumerable<Session> itemsToAdd, bool saveChanges = true)
        {
            _listeSession.AddRange(itemsToAdd);
        }
        public void AddRangeSuivi(IEnumerable<Suivi> itemsToAdd, bool saveChanges = true)
        {
            _listeSuivi.AddRange(itemsToAdd);
        }
        #endregion

        #region Edit
        public void EditChoixReponse(ChoixReponse itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeChoixReponse)
            {
                if (v.id_ChoixRep == itemToEdit.id_ChoixRep)
                {
                    index = count;
                }
                count++;
            }
            _listeChoixReponse.RemoveAt(index);
            _listeChoixReponse.Insert(index, itemToEdit);
        }
        public void EditCourriel(Courriel itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeCourriel)
            {
                if (v.id_TypeCourriel == itemToEdit.id_TypeCourriel)
                {
                    index = count;
                }
                count++;
            }
            _listeCourriel.RemoveAt(index);
            _listeCourriel.Insert(index, itemToEdit);
        }
        public void EditCours(Cours itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeCours)
            {
                if (v.id_Cours == itemToEdit.id_Cours)
                {
                    index = count;
                }
                count++;
            }
            _listeCours.RemoveAt(index);
            _listeCours.Insert(index, itemToEdit);
        }
        public void EditCoursInteret(CoursInteret itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeCoursInteret)
            {
                if (v.id_CoursInteret == itemToEdit.id_CoursInteret)
                {
                    index = count;
                }
                count++;
            }
            _listeCoursInteret.RemoveAt(index);
            _listeCoursInteret.Insert(index, itemToEdit);
        }
        public void EditCoursSuivi(CoursSuivi itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeCoursSuivi)
            {
                if (v.id_CoursReussi == itemToEdit.id_CoursReussi)
                {
                    index = count;
                }
                count++;
            }
            _listeCoursSuivi.RemoveAt(index);
            _listeCoursSuivi.Insert(index, itemToEdit);
        }
        public void EditDisponibilite(Disponibilite itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeDisponibilite)
            {
                if (v.id_Dispo == itemToEdit.id_Dispo)
                {
                    index = count;
                }
                count++;
            }
            _listeDisponibilite.RemoveAt(index);
            _listeDisponibilite.Insert(index, itemToEdit);
        }
        public void EditEtuProgEtude(EtuProgEtude itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeEtuProgEtude)
            {
                if (v.id_EtuProgEtude == itemToEdit.id_EtuProgEtude)
                {
                    index = count;
                }
                count++;
            }
            _listeEtuProgEtude.RemoveAt(index);
            _listeEtuProgEtude.Insert(index, itemToEdit);
        }
        public void EditEvaluation(Evaluation itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeEvaluation)
            {
                if (v.id_Evaluation == itemToEdit.id_Evaluation)
                {
                    index = count;
                }
                count++;
            }
            _listeEvaluation.RemoveAt(index);
            _listeEvaluation.Insert(index, itemToEdit);
        }
        public void EditFormulaire(Formulaire itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeFormulaire)
            {
                if (v.id_Formulaire == itemToEdit.id_Formulaire)
                {
                    index = count;
                }
                count++;
            }
            _listeFormulaire.RemoveAt(index);
            _listeFormulaire.Insert(index, itemToEdit);
        }
        public void EditGroupe(Groupe itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeGroupe)
            {
                if (v.id_Groupe == itemToEdit.id_Groupe)
                {
                    index = count;
                }
                count++;
            }
            _listeGroupe.RemoveAt(index);
            _listeGroupe.Insert(index, itemToEdit);
        }
        public void EditGroupeEtudiant(GroupeEtudiant itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeGroupeEtudiant)
            {
                if (v.id_GroupeEtudiant == itemToEdit.id_GroupeEtudiant)
                {
                    index = count;
                }
                count++;
            }
            _listeGroupeEtudiant.RemoveAt(index);
            _listeGroupeEtudiant.Insert(index, itemToEdit);
        }
        public void EditInscription(Inscription itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeInscription)
            {
                if (v.id_Inscription == itemToEdit.id_Inscription)
                {
                    index = count;
                }
                count++;
            }
            _listeInscription.RemoveAt(index);
            _listeInscription.Insert(index, itemToEdit);
        }
        public void EditJumelage(Jumelage itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeJumelage)
            {
                if (v.id_Jumelage == itemToEdit.id_Jumelage)
                {
                    index = count;
                }
                count++;
            }
            _listeJumelage.RemoveAt(index);
            _listeJumelage.Insert(index, itemToEdit);
        }
        public void EditPersonne(Personne itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listePersonne)
            {
                if (v.id_Pers == itemToEdit.id_Pers)
                {
                    index = count;
                }
                count++;
            }
            _listePersonne.RemoveAt(index);
            _listePersonne.Insert(index, itemToEdit);
        }
        public void EditProgrammeEtude(ProgrammeEtude itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeProgrammeEtude)
            {
                if (v.id_ProgEtu == itemToEdit.id_ProgEtu)
                {
                    index = count;
                }
                count++;
            }
            _listeProgrammeEtude.RemoveAt(index);
            _listeProgrammeEtude.Insert(index, itemToEdit);
        }
        public void EditQuestion(Question itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeQuestion)
            {
                if (v.id_Question == itemToEdit.id_Question)
                {
                    index = count;
                }
                count++;
            }
            _listeQuestion.RemoveAt(index);
            _listeQuestion.Insert(index, itemToEdit);
        }
        public void EditReponseQuestion(ReponseQuestion itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeReponseQuestion)
            {
                if (v.id_RepQuest == itemToEdit.id_RepQuest)
                {
                    index = count;
                }
                count++;
            }
            _listeReponseQuestion.RemoveAt(index);
            _listeReponseQuestion.Insert(index, itemToEdit);
        }
        public void EditSection(Section itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeSection)
            {
                if (v.id_Section == itemToEdit.id_Section)
                {
                    index = count;
                }
                count++;
            }
            _listeSection.RemoveAt(index);
            _listeSection.Insert(index, itemToEdit);
        }
        public void EditSession(Session itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeSession)
            {
                if (v.id_Sess == itemToEdit.id_Sess)
                {
                    index = count;
                }
                count++;
            }
            _listeSession.RemoveAt(index);
            _listeSession.Insert(index, itemToEdit);
        }
        public void EditSuivi(Suivi itemToEdit, bool saveChanges = true)
        {
            var index = -1;
            var count = 0;
            foreach (var v in _listeSuivi)
            {
                if (v.id_Suivi == itemToEdit.id_Suivi)
                {
                    index = count;
                }
                count++;
            }
            _listeSuivi.RemoveAt(index);
            _listeSuivi.Insert(index, itemToEdit);
        }
        #endregion

        #region Remove
        public void RemoveChoixReponse(ChoixReponse itemToRemove, bool saveChanges = true)
        {
            _listeChoixReponse.Remove(itemToRemove);
        }
        public void RemoveCourriel(Courriel itemToRemove, bool saveChanges = true)
        {
            _listeCourriel.Remove(itemToRemove);
        }
        public void RemoveCours(Cours itemToRemove, bool saveChanges = true)
        {
            _listeCours.Remove(itemToRemove);
        }
        public void RemoveCoursInteret(CoursInteret itemToRemove, bool saveChanges = true)
        {
            _listeCoursInteret.Remove(itemToRemove);
        }
        public void RemoveCoursSuivi(CoursSuivi itemToRemove, bool saveChanges = true)
        {
            _listeCoursSuivi.Remove(itemToRemove);
        }
        public void RemoveDisponibilite(Disponibilite itemToRemove, bool saveChanges = true)
        {
            _listeDisponibilite.Remove(itemToRemove);
        }
        public void RemoveEtuProgEtude(EtuProgEtude itemToRemove, bool saveChanges = true)
        {
            _listeEtuProgEtude.Remove(itemToRemove);
        }
        public void RemoveEvaluation(Evaluation itemToRemove, bool saveChanges = true)
        {
            _listeEvaluation.Remove(itemToRemove);
        }
        public void RemoveFormulaire(Formulaire itemToRemove, bool saveChanges = true)
        {
            _listeFormulaire.Remove(itemToRemove);
        }
        public void RemoveGroupe(Groupe itemToRemove, bool saveChanges = true)
        {
            _listeGroupe.Remove(itemToRemove);
        }
        public void RemoveGroupeEtudiant(GroupeEtudiant itemToRemove, bool saveChanges = true)
        {
            _listeGroupeEtudiant.Remove(itemToRemove);
        }
        public void RemoveInscription(Inscription itemToRemove, bool saveChanges = true)
        {
            _listeInscription.Remove(itemToRemove);
        }
        public void RemoveJumelage(Jumelage itemToRemove, bool saveChanges = true)
        {
            _listeJumelage.Remove(itemToRemove);
        }
        public void RemovePersonne(Personne itemToRemove, bool saveChanges = true)
        {
            _listePersonne.Remove(itemToRemove);
        }
        public void RemoveProgrammeEtude(ProgrammeEtude itemToRemove, bool saveChanges = true)
        {
            _listeProgrammeEtude.Remove(itemToRemove);
        }
        public void RemoveQuestion(Question itemToRemove, bool saveChanges = true)
        {
            _listeQuestion.Remove(itemToRemove);
        }
        public void RemoveReponseQuestion(ReponseQuestion itemToRemove, bool saveChanges = true)
        {
            _listeReponseQuestion.Remove(itemToRemove);
        }
        public void RemoveSection(Section itemToRemove, bool saveChanges = true)
        {
            _listeSection.Remove(itemToRemove);
        }
        public void RemoveSession(Session itemToRemove, bool saveChanges = true)
        {
            _listeSession.Remove(itemToRemove);
        }
        public void RemoveSuivi(Suivi itemToRemove, bool saveChanges = true)
        {
            _listeSuivi.Remove(itemToRemove);
        }
        #endregion

        #region RemoveRange
        public void RemoveRangeChoixReponse(IEnumerable<ChoixReponse> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeCourriel(IEnumerable<Courriel> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeCours(IEnumerable<Cours> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeCoursInteret(IEnumerable<CoursInteret> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeCoursSuivi(IEnumerable<CoursSuivi> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeDisponibilite(IEnumerable<Disponibilite> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeEtuProgEtude(IEnumerable<EtuProgEtude> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeEvaluation(IEnumerable<Evaluation> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeFormulaire(IEnumerable<Formulaire> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeGroupe(IEnumerable<Groupe> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeGroupeEtudiant(IEnumerable<GroupeEtudiant> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeInscription(IEnumerable<Inscription> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeJumelage(IEnumerable<Jumelage> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangePersonne(IEnumerable<Personne> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeProgrammeEtude(IEnumerable<ProgrammeEtude> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeQuestion(IEnumerable<Question> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeReponseQuestion(IEnumerable<ReponseQuestion> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeSection(IEnumerable<Section> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeSession(IEnumerable<Session> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeSuivi(IEnumerable<Suivi> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Liste
        public SelectList ListeTypeUsager(int idTypeUsager = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeTypeUsagerDuPersonnel(int idTypeUsager = 0)
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
        public SelectList ListeProgrammmeEtude(bool actif = true)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeEtudiants(int id = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeEnseignant(int id = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeEnseignantEtResponsable(int id = 0)
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
        public SelectList ListeStatutCours()
        {
            throw new NotImplementedException();
        }
        public SelectList ListeTypesCourriels(int typeCourriel = 0)
        {
            throw new NotImplementedException();
        }
        public List<string> ListeJours()
        {
            throw new NotImplementedException();
        }
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}