using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;

namespace sachem.Models
{
    [MetadataType(typeof(SessionMetadata))]
    public partial class Session
    {

    }
    public class SessionMetadata
    {
        [Display(Name = "Session")]
        public string NomSession;
    }
}