using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace sachem.Models
{
    public class Messages
    {
        //message formatter
        static private string Format(string text, params object[] args)
        { return string.Format(new StackFrame(1).GetMethod().Name + " - " + text, args); }


        #region messagesInformatifs

        /// <summary>
        /// Un groupe est associé à ce cours. Le cours ne peut être supprimé.
        /// </summary>
        /// <returns></returns>
        static public string I_001()
        { return Format("Un groupe est associé à ce cours. Le cours ne peut être supprimé."); }

        /// <summary>
        /// Il existe déjà un cours ayant le code {0}.
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        static public string I_002(string Code)
        { return Format("Il existe déjà un cours ayant le code {0}.", Code); }

        /// <summary>
        /// Le cours {0} a été enregistré.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>private string I_003(string cours)
        static public string I_003(string Cours)
        { return Format("Le cours {0} a été enregistré.", Cours); }

        /// <summary>
        /// Le cours {0} a été supprimé.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>private string I_009(string cours)
        static public string I_009(string Cours)
        { return Format("Le cours {0} a été supprimé.", Cours); }


        #endregion

        #region MessageContexte
        #endregion

        #region MessageUnitaire

        /// <summary>
        /// Requis
        /// </summary>
        /// <returns></returns>
        public const string U_001 = "Requis";

        /// <summary>
        /// Format : nom@nomdomaine
        /// </summary>
        /// <returns></returns>
        public const string U_003 = "Longueur requise : 8 caractères.";


        #endregion

        #region Question

        /// <summary>
        /// Voulez-vous vraiment supprimer le cours {0} ?
        /// </summary>
        /// <param name="CodeUsager"></param>
        /// <returns></returns>private string Q_001(string Cours)
        static public string Q_001(string Cours)
        { return String.Format("Voulez-vous vraiment supprimer le cours {0} ?", Cours); }


        #endregion

    }
}